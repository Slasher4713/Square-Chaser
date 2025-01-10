using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Media;

namespace Square_Chaser
{
    public partial class Form1 : Form
    {
        Random randGen = new Random();

        Stopwatch p1Stopwatch = new Stopwatch();
        Stopwatch p2Stopwatch = new Stopwatch();

        //Global Varibles 
        Rectangle player1 = new Rectangle(50, 50, 10, 10);
        Rectangle player2 = new Rectangle(100, 50, 10, 10);
        Rectangle powerUp = new Rectangle(50, 50, 5, 5);
        Rectangle pointSquare = new Rectangle(50, 30, 5, 5);
        Rectangle border = new Rectangle(20, 20, 390, 390);
        Rectangle acid = new Rectangle(150, 0, 5, 20);
        Rectangle acid2 = new Rectangle(150, 0, 5, 20);

        int player1Score = 0;
        int player2Score = 0;

        int player1Speed = 4;
        int player2Speed = 4;
        int acidSpeed = 15;
        int acid2Speed = 10;

        bool wPressed = false;
        bool aPressed = false;
        bool sPressed = false;
        bool dPressed = false;

        bool upPressed = false;
        bool downPressed = false;
        bool leftPressed = false;
        bool rightPressed = false;

        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush violetBrush = new SolidBrush(Color.Violet);
        Pen bluePen = new Pen(Color.Blue, 10);

        //Sound
        SoundPlayer point = new SoundPlayer(Properties.Resources.point);
        SoundPlayer acidHit = new SoundPlayer(Properties.Resources.acidHit);
        SoundPlayer Vroom = new SoundPlayer(Properties.Resources.Vroom);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            powerUp.Y = randGen.Next(30, 380);
            powerUp.X = randGen.Next(30, 380);
            pointSquare.Y = randGen.Next(30, 380);
            pointSquare.X = randGen.Next(30, 380);
            acid.X = randGen.Next(30, 380);
            acid2.X = randGen.Next(30, 380);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            acid.Y += acidSpeed;
            acid2.Y += acid2Speed;

            //Move player1
            if (wPressed == true && player1.Y > 30)
            {
                player1.Y -= player1Speed;
            }

            if (aPressed == true && player1.X > 30)
            {
                player1.X -= player1Speed;
            }

            if (sPressed == true && player1.Y < border.Y + border.Height - player1.Height - 10)
            {
                player1.Y += player1Speed;
            }

            if (dPressed == true && player1.X < border.Y + border.Width - player1.Width - 10)
            {
                player1.X += player1Speed;
            }

            // Move player2
            if (upPressed == true && player2.Y > 30)
            {
                player2.Y -= player2Speed;
            }

            if (leftPressed == true && player2.X > 30)
            {
                player2.X -= player2Speed;
            }

            if (downPressed == true && player2.Y < border.Y + border.Height - player2.Height - 10)
            {
                player2.Y += player2Speed;
            }

            if (rightPressed == true && player2.X < border.Y + border.Width - player2.Width - 10)
            {
                player2.X += player2Speed;
            }

            if (player1.IntersectsWith(powerUp) && p1Stopwatch.ElapsedMilliseconds == 0)
            {
                player1Speed += 3;
                powerUp.Y = randGen.Next(30, 380);
                powerUp.X = randGen.Next(30, 380);
                p1Stopwatch.Start();
                Vroom.Play();
            }
            
            if (player2.IntersectsWith(powerUp) && p2Stopwatch.ElapsedMilliseconds == 0)
            {
                player2Speed += 3;
                powerUp.Y = randGen.Next(30, 380);
                powerUp.X = randGen.Next(30, 380);
                p2Stopwatch.Start();
                Vroom.Play();
            }

            if (player1.IntersectsWith(pointSquare))
            {
                player1Score++;
                pointSquare.Y = randGen.Next(30, 380);
                pointSquare.X = randGen.Next(30, 380);
                point.Play();
            }

            if (player2.IntersectsWith(pointSquare))
            {
                player2Score++;
                pointSquare.Y = randGen.Next(30, 380);
                pointSquare.X = randGen.Next(30, 380);
                point.Play();
            }

            if (p1Stopwatch.ElapsedMilliseconds > 5000)
            {
                player1Speed -= 3;
                p1Stopwatch.Reset();
                p1Stopwatch.Stop();
            }

            if (p2Stopwatch.ElapsedMilliseconds > 5000)
            {
                player2Speed -= 3;
                p2Stopwatch.Reset();
                p2Stopwatch.Stop();
            }
            
            if (player1Score == 5)
            {
                this.BackColor = Color.Red;
                timer.Stop();
            }

            if (player2Score == 5)
            {
                this.BackColor = Color.Green;
                timer.Stop();
            }

            //Acid Movement

            if (acid.Y > 420)
            {
                acid.X = randGen.Next(30, 380);
                acid.Y = 0;
            }

            if (player1.IntersectsWith(acid))
            {
                player1Score--;
                acid.X = randGen.Next(30, 380);
                acid.Y = 0;
                acidHit.Play();
            }
            if (player2.IntersectsWith(acid))
            {
                player2Score--;
                acid.X = randGen.Next(30, 380);
                acid.Y = 0;
                acidHit.Play();
            }

            if (acid2.Y > 420)
            {
                acid2.X = randGen.Next(30, 380);
                acid2.Y = 0;
            }

            if (player1.IntersectsWith(acid2))
            {
                player1Score--;
                acid2.X = randGen.Next(30, 380);
                acid2.Y = 0;
                acidHit.Play();
            }
            if (player2.IntersectsWith(acid2))
            {
                player2Score--;
                acid2.X = randGen.Next(30, 380);
                acid2.Y = 0;
                acidHit.Play();
            }

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            player1ScoreLabel.Text = $"{player1Score}";
            player2ScoreLabel.Text = $"{player2Score}";
            e.Graphics.DrawRectangle(bluePen, border);
            e.Graphics.FillRectangle(redBrush, player1);
            e.Graphics.FillRectangle(greenBrush, player2);
            e.Graphics.FillEllipse(yellowBrush, powerUp);
            e.Graphics.FillEllipse(whiteBrush, pointSquare);
            e.Graphics.FillRectangle(violetBrush, acid);
            e.Graphics.FillRectangle(violetBrush, acid2);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
                case Keys.A:
                    aPressed = false;
                    break;
                case Keys.D:
                    dPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
                case Keys.A:
                    aPressed = true;
                    break;
                case Keys.D:
                    dPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;

            }
        }
    }
}
