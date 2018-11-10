using System.Collections.Generic;

namespace Lesson05
{
    public class ParticleSwarmIndividual : Individual
    {
        public Vector PersonalBestPosition { get; set; }
        public Vector Velocity { get; set; }

        public ParticleSwarmIndividual()
        {
        }

        public ParticleSwarmIndividual(int dimensions)
            : base(dimensions)
        {
        }

        public ParticleSwarmIndividual(IEnumerable<double> coordinates)
            : base(coordinates)
        {
        }
    }
}
