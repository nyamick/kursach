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
        List<Emitter> emitters = new List<Emitter>();
        Emitter emitter;
  
        EnterPoint ep;
        ExitPoint exp;
        CountPoint countPoint;

        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            this.emitter = new Emitter 
            {
                Direction = 0,
                Spreading = 5,
                SpeedMin = 1,
                SpeedMax = 5,
                ColorFrom = Color.Pink,
                ColorTo = Color.FromArgb(0, Color.Red),
                ParticlesPerTick = 100,
                X = picDisplay.Width / 2,
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

            emitter.impactPoints.Add(new GravityPoint
            {
                X = picDisplay.Width / 2 + 100,
                Y = picDisplay.Height / 2,
            });
            emitter.impactPoints.Add(new GravityPoint
            {
                X = picDisplay.Width / 2 - 100,
                Y = picDisplay.Height / 2,
            });

            emitter.impactPoints.Add(ep);
            emitter.impactPoints.Add(exp);

            countPoint = new CountPoint { X = picDisplay.Width / 8, Y = picDisplay.Height / 2, };
            emitter.impactPoints.Add(countPoint);



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
            
            
                /*exp.X = e.X;
                exp.Y = e.Y;
            
*/
        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            emitter.Direction = tbDirection.Value;
            lblDirection.Text = $"{tbDirection.Value}°";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
           
            foreach (var p in emitter.impactPoints)
            {
                if (p is GravityPoint) 
                {
                  
                    (p as GravityPoint).Power = tbGravitation.Value;
                }
            }
            lbGrav.Text = $"{tbGravitation.Value}";
        }

        

        private void picDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            if (rbExp.Checked == true)
            {
                exp.X = e.X;
                exp.Y = e.Y;
            }
            else if (radioButton2.Checked == true)
            {
                ep.X = e.X;
                ep.Y = e.Y;
            }
            else
            {
                countPoint.X = e.X;
                countPoint.Y = e.Y;
            }
        }

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            emitter.SpeedMax = trackBar1.Value;
            emitter.SpeedMin = trackBar1.Value / 5;
        }

        

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            emitter.ParticlesPerTick = trackBar2.Value/2;
            lblCount.Text = $"{trackBar2.Value}";
        }

        private void tbLife_Scroll(object sender, EventArgs e)
        {
            emitter.LifeMin = tbLife.Value / 3 ;
            emitter.LifeMax = tbLife.Value * 10;
            lbLife.Text = $"{tbLife.Value}";


        }
    }
}
