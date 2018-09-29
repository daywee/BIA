using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;
using Shared.ExtensionMethods;
using Shared.TestFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Lesson02
{
    public partial class Form1 : Form
    {
        private const int RenderedPointsCount = 100;
        private const int RenderedPointDimensions = 3;

        private readonly ILPlotCube _plotCube;
        private readonly Dictionary<string, FunctionBase> _functions = new Dictionary<string, FunctionBase>();
        private ILSurface _currentSurface;
        private ILPoints _points;

        public Form1()
        {
            InitializeComponent();
            InitFunctionsComboBox();

            _plotCube = new ILPlotCube();
            var scene = new ILScene { _plotCube };
            var panel = new ILPanel { Scene = scene };

            renderContainer.Controls.Add(panel);
            RenderFunction();
        }

        void RenderFunction()
        {
            var function = _functions[(string)functionsComboBox.SelectedItem];
            var surface = new ILSurface((x, y) => (float)function.Calculate(x, y), xmin: -5f, xmax: 5f, ymin: -5f, ymax: 5f, xlen: 100, ylen: 100);

            if (_currentSurface != null)
            {
                _plotCube.Remove(_currentSurface);
                _currentSurface.Dispose();
            }
            _currentSurface = surface;
            _plotCube.Add(surface);

            RenderPoints();
            renderContainer.Refresh();
        }

        private void RenderPoints()
        {
            var random = new Random();

            var array = new float[RenderedPointsCount, RenderedPointDimensions];
            foreach (var i in Enumerable.Range(0, RenderedPointsCount))
            {
                var (x1, x2) = random.NextUniformDistribution2D();
                array[i, 0] = (float)x1;
                array[i, 1] = (float)x2;
                array[i, 2] = 1000;
            }

            if (_points != null)
            {
                _plotCube.Remove(_points);
                _points.Dispose();
            }

            _points = new ILPoints();
            _points.Positions.Update(array);

            _plotCube.Add(_points);
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
    }
}
