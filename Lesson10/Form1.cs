using Lesson10.OptimizationAlgorithms;
using System.Drawing;
using System.Windows.Forms;

namespace Lesson10
{
    public partial class Form1 : Form
    {
        private Population _population;
        private readonly Timer _evolveTimer;
        private readonly OneDimensionFunctionBase _optimizationFunction1 = new FirstTestFunction();
        private readonly OneDimensionFunctionBase _optimizationFunction2 = new SecondTestFunction();

        public Form1()
        {
            InitializeComponent();
            renderContainer.Paint += (o, e) => PaintGraph(e.Graphics);

            RenderPopulation();
        }

        private void PaintGraph(Graphics g)
        {
            const float increment = 1f;

            float ToRealCoordsX(float x) => x * 50 + 700;
            float ToRealCoordsY(double x) => (float) -x;

            void DrawFunction(OneDimensionFunctionBase f, Pen pen)
            {
                for (float x = f.MinX; x < f.MaxX; x += increment)
                    g.DrawLine(pen, ToRealCoordsX(x), ToRealCoordsY(f.Calculate(x)), ToRealCoordsX(x + increment), ToRealCoordsY(f.Calculate(x + increment)));
            }

            DrawFunction(_optimizationFunction1, Pens.Red);
            DrawFunction(_optimizationFunction2, Pens.Blue);
        }

        private void RenderPopulation()
        {
            renderContainer.Refresh();
        }

        private Population GetPopulation(string algorithmName, string optimizationFunctionName)
        {
            return new Population(_optimizationFunction1, _optimizationFunction2, new Nsga2Algorithm(), 2);
        }
    }
}
