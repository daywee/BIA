using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;
using Lesson10.OptimizationAlgorithms;
using Shared.ExtensionMethods;
using Shared.TestFunctions;

namespace Lesson10
{
    public partial class Form1 : Form
    {
        private readonly ILPlotCube _plotCube;
        private ILSurface _currentSurface;
        private ILPoints _points;
        private ILPoints _bestPoint;
        private Population _population;
        private readonly Timer _evolveTimer;
        private int _evolveTimerTicks;

        public Form1()
        {
            InitializeComponent();
            InitFunctionsComboBox();
            _evolveTimer = new Timer { Interval = 100 };
            InitEventListeners();

            _population = GetPopulation((string)algorithmsComboBox.SelectedItem, (string)functionsComboBox.SelectedItem);

            _plotCube = new ILPlotCube();
            var scene = new ILScene { _plotCube };
            var panel = new ILPanel { Scene = scene };

            renderContainer.Controls.Add(panel);
            RenderFunction();
        }

        void RenderFunction()
        {
            var function = _population.OptimizationFunction1;
            var surface = new ILSurface((x, y) => (float)function.Calculate(x, y),
                xmin: (float)function.MinX, xmax: (float)function.MaxX,
                ymin: (float)function.MinX, ymax: (float)function.MaxX,
                xlen: 100, ylen: 100);

            if (_currentSurface != null)
            {
                _plotCube.Remove(_currentSurface);
                _currentSurface.Dispose();
            }
            _currentSurface = surface;
            _plotCube.Add(surface);

            renderContainer.Refresh();
        }

        private void RenderPopulation()
        {
            var points = new float[_population.CurrentPopulation.Count, _population.Dimension + 1];
            for (var i = 0; i < _population.CurrentPopulation.Count; i++)
            {
                var individual = _population.CurrentPopulation[i];
                points[i, 0] = (float)individual.Position[0];
                points[i, 1] = (float)individual.Position[1];
                points[i, 2] = (float)individual.Cost1 + 1000; // render point higher then function
            }

            if (_points != null)
            {
                _plotCube.Remove(_points);
                _points.Dispose();
            }

            _points = new ILPoints();
            _points.Color = Color.White;
            _points.Positions.Update(points);
            _plotCube.Add(_points);

            RenderBestIndividual();
            renderContainer.Refresh();
        }

        private void RenderBestIndividual()
        {
            var best = _population.BestIndividual;
            var bestPoint = new[] { (float)best.Position[0], (float)best.Position[1], (float)best.Cost1 + 1500 }; // render point higher then function and other points

            if (_bestPoint != null)
            {
                _plotCube.Remove(_bestPoint);
                _bestPoint.Dispose();
            }

            _bestPoint = new ILPoints { Color = Color.BlueViolet };
            _bestPoint.Positions.Update(bestPoint);

            _plotCube.Add(_bestPoint);
        }

        private void InitFunctionsComboBox()
        {
            new FunctionBase[] { new AckleyFunction(), new RosenbrockFunction(), new SchwefelFunction(), new SphereFunction(), new BoothFunction(), new DeJongFunction(), new RastriginFunction() }
                .ForEach(func => functionsComboBox.Items.Add(func.Name));
            functionsComboBox.SelectedIndex = 0;

            new[] { "ES (μ,λ)", "ES (μ+λ)", "DE/rand/1", "DE/current-to-best/1", "Particle Swarm", "Hill Climbing", "Simulated Annealing", "SOMA" }
                .ForEach(algorithm => algorithmsComboBox.Items.Add(algorithm));
            algorithmsComboBox.SelectedIndex = 0;
        }

        private void InitEventListeners()
        {
            void HandleEvolve(object sender, EventArgs args)
            {
                _population.Evolve();
                RenderPopulation();
                generationLabel.Text = _population.Generation.ToString();
                var mean = _population.CalculateMean();
                var best = _population.BestIndividual;
                meanLabel.Text = $"Mean x: {mean.Position[0]} y: {mean.Position[1]}, cost: {mean.Cost1}";
                bestIndividualLabel.Text = $"Best x: {best.Position[0]} y: {best.Position[1]}, cost: {best.Cost1}";
            }

            void HandleAutoEvolutionStopped()
            {
                _evolveTimer.Stop();
                _evolveTimerTicks = 0;
                evolveFiftyTimesButton.Text = "Evolve 50x";
            }

            _evolveTimer.Tick += (o, e) =>
            {
                _evolveTimerTicks++;
                if (_evolveTimerTicks >= 50)
                    HandleAutoEvolutionStopped();

                HandleEvolve(o, e);
            };

            functionsComboBox.SelectedIndexChanged += (o, e) =>
            {
                _population = GetPopulation((string)algorithmsComboBox.SelectedItem, (string)functionsComboBox.SelectedItem);
                RenderFunction();
            };

            algorithmsComboBox.SelectedIndexChanged += (o, e) =>
            {
                _population = GetPopulation((string)algorithmsComboBox.SelectedItem, (string)functionsComboBox.SelectedItem);
                RenderFunction();
            };

            newPopulationButton.Click += (o, e) =>
            {
                _population.CreateNewPopulation();
                RenderPopulation();
                generationLabel.Text = _population.Generation.ToString();
            };

            evolveButton.Click += HandleEvolve;

            evolveFiftyTimesButton.Click += (o, e) =>
            {
                if (_evolveTimer.Enabled)
                {
                    HandleAutoEvolutionStopped();
                }
                else
                {
                    _evolveTimer.Start();
                    evolveFiftyTimesButton.Text = "Stop";
                }
            };
        }

        private Population GetPopulation(string algorithmName, string optimizationFunctionName)
        {
            return new Population(new AckleyFunction(), new BoothFunction(), new Nsga2Algorithm(), 2); 
        }
    }
}
