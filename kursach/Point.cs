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
    public abstract class Point 
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
    public abstract class IImpactPoint
    {
        public float X;
        public float Y;
        public abstract void ImpactParticle(Particle particle);
        public virtual void Render(Graphics g)
        {
            g.FillEllipse(
                    new SolidBrush(Color.Red),
                    X - 5,
                    Y - 5,
                    10,
                    10
                );
        }
    }
    public class GravityPoint : Point
    {
        public int Power = 100;
        public override void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;
            float r2 = (float)Math.Max(100, gX * gX + gY * gY);

            particle.SpeedX += gX * Power / r2;
            particle.SpeedY += gY * Power / r2;
        }
    }
    public class AntiGravityPoint : IImpactPoint
    {
        public int Power = 100;
        public override void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;
            float r2 = (float)Math.Max(100, gX * gX + gY * gY);

            particle.SpeedX -= gX * Power / r2;
            particle.SpeedY -= gY * Power / r2;
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
      
        public override void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;

            double r = Math.Sqrt(gX * gX + gY * gY); // считаем расстояние от центра точки до центра частицы
            if (r + particle.Radius < 100 / 2) // если частица оказалось внутри входого портала
            {
                if (particle is ParticleColorful)
                {
                    var p = (particle as ParticleColorful);

                    var m = new Matrix();
                    

                    var points = new[] { new PointF(gX, gY), new PointF(p.SpeedX, p.SpeedY) };
                    m.TransformPoints(points);
                   
                    //то перемещаем её в другой портал
                    p.X = exitPoint.X - points[0].X;
                    p.Y = exitPoint.Y - points[0].Y;
                    p.SpeedX = points[1].X;
                    p.SpeedY = points[1].Y;
                }

            }
        }

        public override void Render(Graphics g) //отрисовываем входной портал 
        {
            base.Render(g);

            g.DrawString(
            $"Вход",
            new Font("Verdana", 10),
            new SolidBrush(Color.Pink ),
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

        public override void Render(Graphics g) //отрисовка выходного портала 
        {
            base.Render(g);

            g.DrawString(
            $"Выход",
            new Font("Verdana", 10),
            new SolidBrush(Color.Pink),
            X,
            Y
            );
        }

    }
    public class CountPoint : Point
    {
        public float Radius = 100; 
        public int Count = 0;
        public override void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;
            double r = Math.Sqrt(gX * gX + gY * gY);  // считаем расстояние от центра точки до центра частицы
            var p = (particle as ParticleColorful);
            if (r + particle.Radius < Radius / 2)  //если частица попала в счетчик
            {
                p.Radius = 0;  //обнуляем радиус частицы
                p.Life = 0;  //убиваем частицу 
                Count++; //увеличиваем счетичк на 1
                
            }
        }
        public override void Render(Graphics g) //отрисовывем счетчик
        {
            g.DrawEllipse( 
                 new Pen(Color.HotPink, 2),
                 X - Radius / 2,
                 Y - Radius / 2,
                 Radius,
                 Radius);
            var stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center; 
            g.DrawString(
                 $"{Count}",
                 new Font("Segoe UI", 14),
                 new SolidBrush(Color.HotPink),
                 X, Y, stringFormat);

        }
    }

}