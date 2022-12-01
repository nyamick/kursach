using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach
{
    class Particle
    {
        public static Random rand = new Random();

        public int Radius;
        public float X;
        public float Y;
        public float Direction;
        public float Speed;

        public Particle()
        {
            Direction = rand.Next(360);
            Speed = 1 + rand.Next(10);
            Radius = 2 + rand.Next(10);
        }


    }
}
