using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;
using Shared.ExtensionMethods;
using Shared.TestFunctions;

namespace Lesson04
{
    public partial class Form1 : Form
    {
        private readonly ILPlotCube _plotCube;
        private readonly Dictionary<string, FunctionBase> _functions = new Dictionary<string, FunctionBase>();
        private ILSurface _currentSurface;
        private ILPoints _points;
        private ILPoints _additionalPoints;
        private ILPoints _bestPoint;
        private Population _population;
        private readonly Timer _evolveTimer;
        private int _evolveTimerTicks;

        public Form1()
        {
            InitializeComponent();
            InitFunctionsComboBox();
            _evolveTimer = new Timer { Interval = 50 };
            InitEventListeners();
            _population = new Population(_functions[(string)functionsComboBox.SelectedItem],
                GetAlgorithm((string)algorithmsComboBox.SelectedItem), dimensions: 2);

            _plotCube = new ILPlotCube();
            var scene = new ILScene { _plotCube };
            var panel = new ILPanel { Scene = scene };

            renderContainer.Controls.Add(panel);
            RenderFunction();
        }

        void RenderFunction()
        {
            var function = _population.OptimizationFunction;
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
            var points = new float[_population.CurrentPopulation.Count, _population.Dimensions + 1];
            for (var i = 0; i < _population.CurrentPopulation.Count; i++)
            {
                var individual = _population.CurrentPopulation[i];
                points[i, 0] = (float)individual[0];
                points[i, 1] = (float)individual[1];
                points[i, 2] = (float)individual.Cost + 1000; // render point higher then function
            }

            if (_points != null)
            {
                _plotCube.Remove(_points);
                _points.Dispose();
            }

            _points = new ILPoints();
            _points.Positions.Update(points);
            _plotCube.Add(_points);

            RenderBestIndividual();
            RenderAdditionalPoints();
            renderContainer.Refresh();
        }

        private void RenderAdditionalPoints()
        {
            var points = new float[_population.AdditionalIndividualsToRender.Count, _population.Dimensions + 1];
            for (var i = 0; i < _population.AdditionalIndividualsToRender.Count; i++)
            {
                var individual = _population.AdditionalIndividualsToRender[i];
                points[i, 0] = (float)individual[0];
                points[i, 1] = (float)individual[1];
                points[i, 2] = (float)individual.Cost + 1000; // render point higher then function
            }

            if (_additionalPoints != null)
            {
                _plotCube.Remove(_additionalPoints);
                _additionalPoints.Dispose();
            }

            _additionalPoints = new ILPoints();
            _additionalPoints.Color = Color.Yellow;
            _additionalPoints.Positions.Update(points);
            _plotCube.Add(_additionalPoints);
        }

        private void RenderBestIndividual()
        {
            var best = _population.BestIndividual;
            var bestPoint = new[] { (float)best[0], (float)best[1], (float)best.Cost + 1500 }; // render point higher then function and other points

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
            new FunctionBase[] { new AckleyFunction(), new RosenbrockFunction(), new SchwefelFunction(), new SphereFunction(), new BoothFunction() }
                .ForEach(func =>
                {
                    _functions.Add(func.Name, func);
                    functionsComboBox.Items.Add(func.Name);
                });
            functionsComboBox.SelectedIndex = 0;

            new[] { "Hill Climbing", "Simulated Annealing", "SOMA" }
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
                meanLabel.Text = $"x: {mean[0]} y: {mean[1]}, cost: {mean.Cost}";
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
                _population = new Population(_functions[(string)functionsComboBox.SelectedItem],
                    GetAlgorithm((string)algorithmsComboBox.SelectedItem), dimensions: 2);
                RenderFunction();
            };

            algorithmsComboBox.SelectedIndexChanged += (o, e) =>
            {
                _population = new Population(_functions[(string)functionsComboBox.SelectedItem],
                    GetAlgorithm((string)algorithmsComboBox.SelectedItem), dimensions: 2);
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

            standardDeviationTrackBar.ValueChanged += (o, e) =>
            {
                var x = standardDeviationTrackBar.Value / 10f;
                _population.StandardDeviation = x;
                standardDeviationValueLabel.Text = x.ToString(CultureInfo.InvariantCulture);
            };

            meanTrackBar.ValueChanged += (o, e) =>
            {
                var x = meanTrackBar.Value / 10f;
                _population.Mean = x;
                meanValueLabel.Text = x.ToString(CultureInfo.InvariantCulture);
            };
        }

        private IAlgorithm GetAlgorithm(string name)
        {
            switch (name)
            {
                case "Hill Climbing":
                    return new HillClimbingAlgorithm();
                case "Simulated Annealing":
                    return new SimulatedAnnealingAlgorithm();
                case "SOMA":
                    return new SomaAlgorithm();
                default:
                    throw new InvalidOperationException($"Algorithm '{name}' is not supported.");
            }
        }
    }
}
