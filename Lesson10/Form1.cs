using System.Collections.Generic;
using Lesson10.OptimizationAlgorithms;
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

            evolveButton.Click += (o, e) => HandleClick();

            for (int i = 0; i < 1000; i++)
            {
                HandleClick();
            }

            RenderPopulation();
        }

        private void PaintGraph(Graphics g)
        {
            const float increment = 1f;

            float ToRealCoordsX(double x) => (float)x * 50 + 700;
            float ToRealCoordsY(double y) => (float)-y * 50 + 100;

            void DrawFunction(OneDimensionFunctionBase f, Pen pen)
            {
                for (float x = f.MinX; x < f.MaxX; x += increment)
                    g.DrawLine(pen, ToRealCoordsX(x), ToRealCoordsY(f.Calculate(x)), ToRealCoordsX(x + increment), ToRealCoordsY(f.Calculate(x + increment)));
            }

            void DrawPoint(float x, float y)
            {
                g.FillEllipse(Brushes.BlueViolet, x - 5, y - 5, 10, 10);
            }

            //DrawFunction(_optimizationFunction1, Pens.Red);
            //DrawFunction(_optimizationFunction2, Pens.Blue);

            foreach (var individual in _points)
            {
                DrawPoint(ToRealCoordsX(individual.Cost1), ToRealCoordsY(individual.Cost2));
            }
        }

        private void RenderPopulation()
        {
            renderContainer.Refresh();
        }
    }
}
