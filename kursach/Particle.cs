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

        public int Radius; // радиус частицы
        public float X; //X координата положения частицы в пространстве
        public float Y; //координата положения частицы в пространстве
        public float SpeedX;  // скорость перемещения по оси X
        public float SpeedY;  // скорость перемещения по оси Y
        public float Life;  // запас здоровья частицы

        public Particle()     // метод генерации частицы
        {
            var direction = (double)rand.Next(360);
            var speed = 1 + rand.Next(10);

            SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);
            Radius = 2 + rand.Next(10);
            Life = 20 + rand.Next(100);
        }
        public virtual void Draw(Graphics g)
        {

            float k = Math.Min(1f, Life / 100); // рассчитываем коэффициент прозрачности по шкале от 0 до 1.0
            int alpha = (int)(k * 255);  // рассчитываем значение альфа канала в шкале от 0 до 255
                                         // по аналогии с RGB, он используется для задания прозрачности

            var color = Color.FromArgb(alpha, Color.White);
            var b = new SolidBrush(color); ; // создаем цвет из уже существующего, но привязываем к нему еще и значение альфа канала
            g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            b.Dispose();
        }
        public class ParticleColorful : Particle
        {
            public Color FromColor;
            public Color ToColor;
            // для смеси цветов
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
                // так как k уменшается от 1 до 0, то порядок цветов обратный
                var color = MixColor(ToColor, FromColor, k);
                var b = new SolidBrush(color);
                g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);
                b.Dispose();
            }

        }
    }
}
