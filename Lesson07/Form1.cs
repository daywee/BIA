using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Lesson07
{
    public partial class Form1 : Form
    {
        private Population _population;
        private readonly Timer _evolveTimer;
        private int _evolveTimerTicks;

        public Form1()
        {
            InitializeComponent();
            //InitFunctionsComboBox();
            _evolveTimer = new Timer { Interval = 1 };
            //InitEventListeners();

            _population = GetPopulation();

            renderContainer.Paint += (o, e) => PaintGraph(e.Graphics);

            _evolveTimerTicks = 0;
            _evolveTimer.Tick += (o, e) =>
            {
                if (_evolveTimerTicks++ > 10000)
                {
                    _evolveTimer.Stop();
                }
                _population.Evolve();
                renderContainer.Refresh();
                generationLabel.Text = _evolveTimerTicks.ToString();
                distanceLabel.Text = _population.BestSequence.Cost.ToString();
            };

            _evolveTimer.Start();
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

            _population.BaseCitiesSequence.Cities.ForEach(DrawCity);

            for (int i = 0; i < _population.BestSequence.Cities.Count - 1; i++)
            {
                var c1 = _population.BestSequence.Cities[i];
                var c2 = _population.BestSequence.Cities[i + 1];
                DrawRoute(c1, c2);
            }
            DrawRoute(_population.BestSequence.Cities.First(), _population.BestSequence.Cities.Last());
        }


        //private void RenderPopulation()
        //{
        //    var points = new float[_population.CurrentPopulation.Count, _population.Dimensions + 1];
        //    for (var i = 0; i < _population.CurrentPopulation.Count; i++)
        //    {
        //        var individual = _population.CurrentPopulation[i];
        //        points[i, 0] = (float)individual.Position[0];
        //        points[i, 1] = (float)individual.Position[1];
        //        points[i, 2] = (float)individual.Cost + 1000; // render point higher then function
        //    }

        //    renderContainer.Refresh();
        //}


        //private void InitFunctionsComboBox()
        //{
        //    functionsComboBox.SelectedIndex = 0;

        //    new[] { "DE/rand/1", "DE/current-to-best/1", "Particle Swarm", "Hill Climbing", "Simulated Annealing", "SOMA" }
        //        .ForEach(algorithm => algorithmsComboBox.Items.Add(algorithm));
        //    algorithmsComboBox.SelectedIndex = 0;
        //}

        //private void InitEventListeners()
        //{
        //    void HandleEvolve(object sender, EventArgs args)
        //    {
        //        _population.Evolve();
        //        RenderPopulation();
        //        generationLabel.Text = _population.Generation.ToString();
        //    }

        //    void HandleAutoEvolutionStopped()
        //    {
        //        _evolveTimer.Stop();
        //        _evolveTimerTicks = 0;
        //        evolveFiftyTimesButton.Text = "Evolve 50x";
        //    }

        //    _evolveTimer.Tick += (o, e) =>
        //    {
        //        _evolveTimerTicks++;
        //        if (_evolveTimerTicks >= 50)
        //            HandleAutoEvolutionStopped();

        //        HandleEvolve(o, e);
        //    };

        //    functionsComboBox.SelectedIndexChanged += (o, e) =>
        //    {
        //        _population = GetPopulation();
        //        RenderFunction();
        //    };

        //    algorithmsComboBox.SelectedIndexChanged += (o, e) =>
        //    {
        //        _population = GetPopulation();
        //        RenderFunction();
        //    };

        //    newPopulationButton.Click += (o, e) =>
        //    {
        //        _population.CreateNewPopulation();
        //        RenderPopulation();
        //        generationLabel.Text = _population.Generation.ToString();
        //    };

        //    evolveButton.Click += HandleEvolve;

        //    evolveFiftyTimesButton.Click += (o, e) =>
        //    {
        //        if (_evolveTimer.Enabled)
        //        {
        //            HandleAutoEvolutionStopped();
        //        }
        //        else
        //        {
        //            _evolveTimer.Start();
        //            evolveFiftyTimesButton.Text = "Stop";
        //        }
        //    };
        //}

        private Population GetPopulation()
        {
            return new Population(CitiesSequence.GetDefaultSequence(), new GeneticAlgorithm());
        }
    }
}
