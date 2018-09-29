using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;
using Shared.ExtensionMethods;
using Shared.TestFunctions;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lesson02
{
    public partial class Form1 : Form
    {
        private readonly ILPlotCube _plotCube;
        private readonly Dictionary<string, FunctionBase> _functions = new Dictionary<string, FunctionBase>();
        private ILSurface _currentSurface;

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
            var surface = new ILSurface((x, y) => (float)function.Calculate(x, y), xmin: -5f, xmax: 5f, ymin: -5f, ymax: 5f, xlen:100, ylen :100);

            if (_currentSurface != null)
            {
                _plotCube.Remove(_currentSurface);
                _currentSurface.Dispose();
            }
            _currentSurface = surface;
            _plotCube.Add(surface);

            renderContainer.Refresh();
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
