using SnakeLadderGame.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SnakeLadderGame
{
    public partial class Form1 : Form
    {
        int p1Position,p2Position;
        int p1Direction,p2Direction;
        bool p1Turn;
        int pressed;
        int redDraw, blueDraw;
        public Form1()
        {
            p1Position = 1;
            p2Position = 1;

            p2Direction = 1;
            p1Direction = 1;
          
            pressed = 0;
            redDraw= 0;
            blueDraw= 0;
            InitializeComponent();
            textBox1.Text = "Red Draw";
        }

     
       
        private void button1_Click(object sender, EventArgs e)
        {   
            // draw to see who starts
            Random rand = new Random();
            if (pressed == 0)
            {

                redDraw = rand.Next(1, 7);
                panel3.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("d" + redDraw);
                textBox1.Text = "Blue Draw";
                pressed++;
            }
            else if (pressed == 1)
            {
                blueDraw = rand.Next(1, 7);
                while (blueDraw == redDraw)
                {
                    blueDraw = rand.Next(1, 7);

                }
                string wonText;
                panel3.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("d" + blueDraw);
                if (redDraw > blueDraw)
                {
                    button1.Enabled = false;
                    wonText = "Red Starts";
                    p1Turn = true;
                    Application.DoEvents();
                    Thread.Sleep(600);
                    panel3.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("redStart");

                }
                else
                {
                    button1.Enabled = false;
                    wonText = "Blue Starts";
                    p1Turn = false;
                    Application.DoEvents();
                    Thread.Sleep(600);
                    panel3.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("blueStart");

                }
                Application.DoEvents();
                Thread.Sleep(600);
                panel3.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("roll");
                textBox1.Text = wonText;

                button1.Enabled = true;
               
                pressed++;
            }
            // logic for the game
            else
            {
                int diceValue = rand.Next(1, 7);
                panel3.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("d" + diceValue);
                button1.Enabled = false;
                bool won;
                // check for the turn
                if (p1Turn)
                {

                    won = move(diceValue, ref p1Position, ref p1Direction, panel4);
                    if (won)
                    {
                        textBox1.Text = "Red Won!";
                        panel3.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("redWon");
                        button1.Enabled = false;
                    }
                    else
                        textBox1.Text = "Blue Turn";
                    p1Turn = false;
                }
                else
                {
                    won = move(diceValue, ref p2Position, ref p2Direction, panel5);
                    if (won)
                    {
                        textBox1.Text = "Blue Won";
                        panel3.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("blueWon");
                        button1.Enabled = false;

                    }
                    else
                        textBox1.Text = "Red Turn";
                    p1Turn = true;
                }
                button1.Enabled = true;
            }

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private bool move(int dice,ref int position,ref int direction,Panel panel)
        {
            int nextPositin = dice + position;
            int x = panel.Location.X;
            int y = panel.Location.Y;
            if (nextPositin > 100)
            {
                textBox1.Text = "Cannot Move";
                Application.DoEvents();
                Thread.Sleep(500);
            }
            else
            {
                for (int i = 0; i < dice; i++)
                {

                    if (position == 10 || position == 30 || position == 50 || position == 70 || position == 90)
                    {
                        y -= 60;
                        direction *= -1;


                    }
                    else if (position == 20 || position == 40 || position == 60 || position == 80)
                    {
                        y -= 60;
                        direction *= -1;
                    }

                    else
                    {
                        x += (84 * direction);
                    }

                    position++;
                    panel.Location = new Point(x, y);
                    Application.DoEvents();
                    Thread.Sleep(150);

                }

            }
            if (position == 100)
            {
                return true;
            }
            checkForStairs(ref position, ref direction, panel);
            checkForSnakes(ref position, ref direction, panel);

            return false;


        }

        private void checkForStairs(ref int position, ref int direction,Panel panel)
        {     
            int x=panel.Location.X;
            int y = panel.Location.Y;
            if (position == 8)
            {
                x += 84;
                y -= (2 * 60);
                panel.Location = new Point(x, y);

                position = 29;
                direction = 1;

            }
            else if (position == 22)
            {
                x -= 84;
                y -= (4 * 60);
                panel.Location = new Point(x, y);

                position = 61;
                direction = 1;
            }
            else if (position == 65)
            {
                x -= 84;
                y -= (3 * 60);
                panel.Location = new Point(x, y);

                position = 97;
                direction = -1;

            }
            else if (position == 54)
            {
                x += 84;
                y -= (60);
                panel.Location = new Point(x, y);
                position = 68;
                direction = 1;

            }
            else if (position == 72)
            {
                x -= 84;
                y -= (2 * 60);
                panel.Location = new Point(x, y);

                position = 93;
                direction = -1;

            }
        }
        private void checkForSnakes(ref int position, ref int direction, Panel panel)
        {
            int x = panel.Location.X;
            int y = panel.Location.Y;
            if (position == 23)
            {
                x += 84;
                y += 60;
                panel.Location = new Point(x, y);
                position = 17;
                direction = -1;
            }
            else if(position == 99)
            {
                x += (2*84);
                y += (7*60);
                panel.Location = new Point(x, y);
                position = 24;
                direction = 1;
            }
            else if (position == 45)
            {
               
                y += (4 * 60);
                panel.Location = new Point(x, y);
                position = 5;
                direction = 1;
            }
            else if (position == 52)
            {
                x -= 84;
                y += (2 * 60);
                panel.Location = new Point(x, y);
                position = 33;
                direction = -1;
            }
            else if (position == 90)
            {

                y += (4 * 60);
                panel.Location = new Point(x, y);
                position = 50;
                direction = 1;
            }
        }
    }
}
