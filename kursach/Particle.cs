using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach
{
     public class Particle
    {
        public static Random rand = new Random();

        public int Radius;
        public float X;
        public float Y;
        public float SpeedX;
        public float SpeedY;
        public float Life;

        public Particle()
        {
            var direction = (double)rand.Next(360);
            var speed = 1 + rand.Next(10);

            // рассчитываем вектор скорости
            SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);
            Radius = 2 + rand.Next(10);
            Life = 20 + rand.Next(100);
        }
        public virtual void Draw(Graphics g)
        {

            float k = Math.Min(1f, Life / 100);
            int alpha = (int)(k * 255);
            var color = Color.FromArgb(alpha, Color.Black);
            var b = new SolidBrush(color); ;
            g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            b.Dispose();
        }
        public class ParticleColorful : Particle
        {
            public Color FromColor;
            public Color ToColor;
            public static Color MixColor(Color color1, Color color2, float k)
            {
                return Color.FromArgb(
                    (int)(color2.A * k + color1.A * (1 - k)),
                    (int)(color2.R * k + color1.R * (1 - k)),
                    (int)(color2.G * k + color1.G * (1 - k)),
                    (int)(color2.B * k + color1.B * (1 - k))
                );
            }
            public override void Draw(Graphics g)
            {
                float k = Math.Min(1f, Life / 100);
                var color = MixColor(ToColor, FromColor, k);
                var b = new SolidBrush(color);
                g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);
                b.Dispose();
            }

        }
    }
}
