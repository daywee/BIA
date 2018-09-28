using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;
using System.Windows.Forms;
using TestFunctions;

namespace Lesson02
{
    public partial class Form1 : Form
    {
        private readonly ILPlotCube _plotCube;

        public Form1()
        {
            InitializeComponent();

            _plotCube = new ILPlotCube();
            var scene = new ILScene { _plotCube };
            var panel = new ILPanel { Scene = scene };

            renderContainer.Controls.Add(panel);
            RenderFunction();
        }

        private void RenderFunction()
        {
            var f = new AckleyFunction();
            var surface = new ILSurface((x, y) => (float)f.Calculate(x, y));

            _plotCube.Add(surface);
            renderContainer.Refresh();
        }
    }
}
