using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static kursach.Emitter;
using static kursach.Particle;

namespace kursach
{
    public partial class Form1 : Form
    {
        /*List<Particle> particles = new List<Particle>();*/
        List<Emitter> emitters = new List<Emitter>();
        Emitter emitter;
        EnterPoint ep;
        ExitPoint exp;
        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            this.emitter = new Emitter // создаю эмиттер и привязываю его к полю emitter
            {
                Direction = 0,
                Spreading = 10,
                SpeedMin = 10,
                SpeedMax = 10,
                ColorFrom = Color.Gold,
                ColorTo = Color.FromArgb(0, Color.Red),
                ParticlesPerTick = 10,
                X = picDisplay.Width / 10,
                Y = picDisplay.Height / 10,
            };

            emitters.Add(this.emitter);
            exp = new ExitPoint
            {
                
                Color = Color.Blue,
                X = (float)(picDisplay.Width * 0.5),
                Y = picDisplay.Height / 2,
                X1 = 100,
                Y1 = 100
            };

            ep = new EnterPoint
            {
                exitPoint = exp,
                Color = Color.Purple,
                X = (float)(picDisplay.Width * 0.28),
                Y = picDisplay.Height / 2,
                X1 = 100,
                Y1 = 100
            };

            emitter.impactPoints.Add(ep);
            emitter.impactPoints.Add(exp);

            /* emitter.impactPoints.Add(new GravityPoint
             {
                 X = (float)(picDisplay.Width * 0.25),
                 Y = picDisplay.Height / 2
             });

             emitter.impactPoints.Add(new AntiGravityPoint
             {
                 X = picDisplay.Width / 2,
                 Y = picDisplay.Height / 2
             });

             emitter.impactPoints.Add(new GravityPoint
             {
                 X = (float)(picDisplay.Width * 0.75),
                 Y = picDisplay.Height / 2
             });*/

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState();

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.Render(g);
            }

            picDisplay.Invalidate();
        }

        private int MousePositionX = 0;
        private int MousePositionY = 0;
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (radioButton1.Checked == true)
            {
                exp.X = e.X;
                exp.Y = e.Y;
            }
            else 
            {
                ep.X = e.X;
                ep.Y = e.Y;
            }

        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            emitter.Direction = tbDirection.Value;
            lblDirection.Text = $"{tbDirection.Value}°";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            emitter.GravitationY = tbGravitation.Value / 10;
        }
    }
}
