using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static kursach.Particle;

namespace kursach
{
    public abstract class Point // добавить наследование от одного класса
    {
        public float X = 0;
        public float Y = 0;
        public Color Color;
        public float X1 = 100;
        public float Y1 = 100;

        public abstract void ImpactParticle(Particle particle);
        public virtual void Render(Graphics g)
        {
            g.DrawEllipse(
                new Pen(Color),
                X - X1 / 2,
                Y - Y1 / 2,
                X1,
                Y1
            );
        }
    }

    public class PaintPoint : Point
    {
        public override void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;

            double r = Math.Sqrt(gX * gX + gY * gY);
            if (r + particle.Radius < X1 / 2)
            {
                if (particle is ParticleColorful)
                {
                    var p = (particle as ParticleColorful);
                    p.FromColor = Color;
                    p.ToColor = Color;
                }

            }

        }

        public override void Render(Graphics g)
        {
            base.Render(g);
        }
    }

    public class EnterPoint : Point
    {
        public ExitPoint exitPoint;
        public int Angle = 0;
        public override void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;

            double r = Math.Sqrt(gX * gX + gY * gY);
            if (r + particle.Radius < 100 / 2)
            {
                if (particle is ParticleColorful)
                {
                    var p = (particle as ParticleColorful);

                    var m = new Matrix();
                    m.Rotate(Angle);

                    var points = new[] { new PointF(gX, gY), new PointF(p.SpeedX, p.SpeedY) };
                    m.TransformPoints(points);

                    p.X = exitPoint.X - points[0].X;
                    p.Y = exitPoint.Y - points[0].Y;
                    p.SpeedX = points[1].X;
                    p.SpeedY = points[1].Y;
                }

            }
        }

        public override void Render(Graphics g)
        {
            base.Render(g);

            g.DrawString(
            $"Вход",
            new Font("Verdana", 10),
            new SolidBrush(Color.Black),
            X,
            Y
            );
        }
    }

    public class ExitPoint : Point
    {


        public override void ImpactParticle(Particle particle)
        {

        }

        public override void Render(Graphics g)
        {
            base.Render(g);

            g.DrawString(
            $"Выход",
            new Font("Verdana", 10),
            new SolidBrush(Color.Black),
            X,
            Y
            );
        }
    }
}