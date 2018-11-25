using Shared.ExtensionMethods;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Lesson08
{
    public partial class Form1 : Form
    {
        private Population _population;
        private readonly Timer _evolveTimer;

        public Form1()
        {
            InitializeComponent();
            InitAlgorithmsComboBox();
            InitTspProblemsComboBox();
            _evolveTimer = new Timer { Interval = 1 };
            InitEventListeners();

            _population = GetPopulation();
        }

        private void PaintGraph(Graphics g)
        {
            void DrawCity(City city)
            {
                g.FillRectangle(Brushes.Black, (float)(city.Position[0] - 2.5),
                    (float)(city.Position[1] - 2.5), 5, 5);
            }

            void DrawRoute(City first, City second)
            {
                g.DrawLine(Pens.Black, (float)first.Position[0], (float)first.Position[1], (float)second.Position[0], (float)second.Position[1]);
            }

            _population.BestSequence.Cities.ForEach(DrawCity);

            for (int i = 0; i < _population.BestSequence.Cities.Count - 1; i++)
            {
                var c1 = _population.BestSequence.Cities[i];
                var c2 = _population.BestSequence.Cities[i + 1];
                DrawRoute(c1, c2);
            }
            DrawRoute(_population.BestSequence.Cities.First(), _population.BestSequence.Cities.Last());
        }

        private void InitTspProblemsComboBox()
        {
            new[] { "TSP problem 1" }
                .ForEach(algorithm => tspProblemsComboBox.Items.Add(algorithm));
            tspProblemsComboBox.SelectedIndex = 0;
        }

        private void InitAlgorithmsComboBox()
        {
            new[] { "ACO", "Genetic algorithm" }
                .ForEach(algorithm => algorithmsComboBox.Items.Add(algorithm));
            algorithmsComboBox.SelectedIndex = 0;
        }

        private void RenderPopulation()
        {
            renderContainer.Refresh();
        }

        private void InitEventListeners()
        {
            void HandleEvolve(object sender, EventArgs args)
            {
                _population.Evolve();
                RenderPopulation();
                generationLabel.Text = _population.Generation.ToString();
                distanceLabel.Text = _population.BestSequence.Cost.ToString();
                tourLabel.Text = string.Join(", ", _population.BestSequence.Cities.Select(e => e.Name));
            }

            void HandleAutoEvolutionStopped()
            {
                _evolveTimer.Stop();
                evolveButton.Text = "Evolve";
            }

            void HandleAutoEvolutionStarted()
            {
                _evolveTimer.Start();
                evolveButton.Text = "Stop";
            }

            void HandleSettingsChanged(object sender, EventArgs e)
            {
                HandleAutoEvolutionStopped();
                _population = GetPopulation();
                RenderPopulation();
            }

            _evolveTimer.Tick += HandleEvolve;

            tspProblemsComboBox.SelectedIndexChanged += HandleSettingsChanged;

            algorithmsComboBox.SelectedIndexChanged += HandleSettingsChanged;

            newPopulationButton.Click += (o, e) =>
            {
                _population.CreateNewPopulation();
                RenderPopulation();
                generationLabel.Text = _population.Generation.ToString();
            };

            evolveButton.Click += (o, e) =>
            {
                //HandleEvolve(o,e);
                if (_evolveTimer.Enabled)
                    HandleAutoEvolutionStopped();
                else
                    HandleAutoEvolutionStarted();
            };

            renderContainer.Paint += (o, e) => PaintGraph(e.Graphics);
        }

        private Population GetPopulation()
        {
            string algorithmName = (string)algorithmsComboBox.SelectedItem;
            string tspProblemName = (string)tspProblemsComboBox.SelectedItem;

            IAlgorithm algorithm;
            CitiesSequence citiesSequence;
            switch (tspProblemName)
            {
                case "TSP problem 1":
                    citiesSequence = CitiesSequence.GetDefaultSequence();
                    break;
                default:
                    throw new InvalidOperationException($"TSP problem '{tspProblemName}' is not supported.");
            }
            switch (algorithmName)
            {
                case "Genetic algorithm":
                    algorithm = new GeneticAlgorithm();
                    break;
                case "ACO":
                    algorithm = new AntColonyOptimizationAlgorithm(citiesSequence.Cities);
                    break;
                default:
                    throw new InvalidOperationException($"Algorithm '{algorithmName}' is not supported.");
            }
            

            return new Population(citiesSequence, algorithm);
        }
    }
}
