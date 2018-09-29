using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;
using Shared.ExtensionMethods;
using Shared.TestFunctions;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lesson02
{
    public partial class Form1 : Form
    {
        private readonly ILPlotCube _plotCube;
        private readonly Dictionary<string, FunctionBase> _functions = new Dictionary<string, FunctionBase>();
        private ILSurface _currentSurface;
        private ILPoints _points;
        private ILPoints _bestPoint;
        private readonly Population _population;

        public Form1()
        {
            InitializeComponent();
            InitFunctionsComboBox();
            InitEventListeners();
            _population = new Population(_functions[(string)functionsComboBox.SelectedItem], maxPopulationCount: 50, dimensions: 2);

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
                points[i, 2] = 1000;
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
            renderContainer.Refresh();
        }

        private void RenderBestIndividual()
        {
            var best = _population.BestIndividual;
            var bestPoint = new[] { (float)best[0], (float)best[1], 1001 };

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
            new FunctionBase[] { new AckleyFunction(), new RosenbrockFunction(), new SchwefelFunction(), new SphereFunction() }
                .ForEach(func =>
                {
                    _functions.Add(func.Name, func);
                    functionsComboBox.Items.Add(func.Name);
                });
            functionsComboBox.SelectedIndex = 0;
            functionsComboBox.SelectedIndexChanged += (o, e) => RenderFunction();
        }

        private void InitEventListeners()
        {
            functionsComboBox.SelectedIndexChanged += (o, e) => _population.OptimizationFunction = _functions[(string)functionsComboBox.SelectedItem];

            newPopulationButton.Click += (o, e) =>
            {
                _population.CreateNewPopulation();
                RenderPopulation();
                generationLabel.Text = _population.Generation.ToString();
            };

            evolveButton.Click += (o, e) =>
            {
                _population.Evolve();
                RenderPopulation();
                generationLabel.Text = _population.Generation.ToString();
            };
        }
    }
}
