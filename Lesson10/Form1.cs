using Lesson10.OptimizationAlgorithms;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Lesson10
{
    public partial class Form1 : Form
    {
        private Population _population;
        private readonly Timer _evolveTimer;
        private readonly OneDimensionFunctionBase _optimizationFunction1 = new FirstTestFunction();
        private readonly OneDimensionFunctionBase _optimizationFunction2 = new SecondTestFunction();
        private List<Individual> _points = new List<Individual>();

        public Form1()
        {
            InitializeComponent();
            renderContainer.Paint += (o, e) => PaintGraph(e.Graphics);

            _population = new Population(_optimizationFunction1, _optimizationFunction2, new Nsga2Algorithm(), 2);

            void HandleClick()
            {
                _population.Evolve();
                _points.AddRange(_population.CurrentPopulation);
                _points = _points.Distinct().ToList();
                RenderPopulation();
            }

            for (int i = 0; i < 100; i++)
            {
                HandleClick();
            }

            RenderPopulation();
        }

        private void PaintGraph(Graphics g)
        {
            float ToRealCoordsX(double x) => (float)x * 80 + 350;
            float ToRealCoordsY(double y) => (float)-y * 80 + 50;

            void DrawX()
            {
                double xStart = -4;
                double xEnd = 0;
                double y = 0.2;
                var pen = Pens.Black;
                var font = new Font(FontFamily.GenericMonospace, 10);
                g.DrawLine(pen, ToRealCoordsX(xStart), ToRealCoordsY(y), ToRealCoordsX(xEnd), ToRealCoordsY(y));


                for (double i = xStart; i <= xEnd; i += 0.5)
                {
                    g.DrawLine(pen, ToRealCoordsX(i), ToRealCoordsY(y), ToRealCoordsX(i), ToRealCoordsY(y + 0.2));
                    g.DrawString(i.ToString("0.0"), font, Brushes.Black, ToRealCoordsX(i - 0.3), ToRealCoordsY(y + 0.4));
                }
            }

            void DrawY()
            {
                double x = 0.2;
                double yStart = -3.5;
                double yEnd = 0;
                var pen = Pens.Black;
                var font = new Font(FontFamily.GenericMonospace, 10);
                g.DrawLine(pen, ToRealCoordsX(x), ToRealCoordsY(yStart), ToRealCoordsX(x), ToRealCoordsY(yEnd));

                for (double i = yStart; i <= yEnd; i += 0.5)
                {
                    g.DrawLine(pen, ToRealCoordsX(x), ToRealCoordsY(i), ToRealCoordsX(x + 0.2), ToRealCoordsY(i));
                    g.DrawString(i.ToString("0.0"), font, Brushes.Black, ToRealCoordsX(x + 0.4), ToRealCoordsY(i + 0.1));
                }
            }

            void DrawPoint(float x, float y)
            {
                g.FillEllipse(Brushes.BlueViolet, x - 5, y - 5, 10, 10);
            }


            foreach (var individual in _points)
            {
                DrawPoint(ToRealCoordsX(individual.Cost1), ToRealCoordsY(individual.Cost2));
            }

            DrawX();
            DrawY();
        }

        private void RenderPopulation()
        {
            renderContainer.Refresh();
        }
    }
}
