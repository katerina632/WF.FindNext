using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5__Slider.ProgressBar
{
    public partial class Form1 : Form
    {
        int[] numbers;
        int countClicks = 1;
        Timer timer = new Timer();
        int timeLeft = 0;
        public Form1()
        {
            InitializeComponent();
            foreach (Button button in panel1.Controls.OfType<Button>())
            {

                button.BackColor = Color.LightGray;
            }

            timer.Interval = 1000;
            timer.Tick += ShowTimer;
            numbers = new int[16] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,16 };

        }

        void ShowTimer(object vObject, EventArgs e)
        {
            if (timeLeft > 0)
            {
                --timeLeft;
                timerToolStripStatusLabel1.Text = timeLeft.ToString() + " sec";
                if (countClicks == 17)
                {
                    timer.Stop();
                    nextToolStripStatusLabel2.Text = $"You win!";
                    MessageBox.Show($"Your time - {trackBar1.Value- timeLeft} sec", "You win!");
                    trackBar1.Enabled = true;
                    foreach (Button button in panel1.Controls.OfType<Button>())
                    {
                        button.BackColor = Color.LightGray;
                        button.Enabled = false;
                    }

                }
            }
            else
            {
                timer.Stop();

                MessageBox.Show("Time is up!", "Timer");
            }
        }
        private void Shuffle()
        {
            Random rand = new Random();
            for (int i = numbers.Length - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);
                var temp = numbers[j];
                numbers[j] = numbers[i];
                numbers[i] = temp;
            }
        }

        private void ButtonClick(object sender, EventArgs e)
        {               
            var button = (Button)sender;

            if (button.Text == countClicks.ToString())
            {
                button.BackColor = Color.Green;
                if (toolStripProgressBar1.Value + 7 > 100)
                {
                    toolStripProgressBar1.Value = 100;
                    countClicks++;
                }
                else
                {
                    toolStripProgressBar1.Value += 7;
                    nextToolStripStatusLabel2.Text = $"Next number: {countClicks + 1}";
                    countClicks++;
                }
            }
            else
                button.BackColor = Color.Red;
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            Shuffle();
            int count = 0;
            countClicks = 1;

            trackBar1.Enabled = false;

            toolStripProgressBar1.Value =0;
            nextToolStripStatusLabel2.Text = $"Next number: 1";
            foreach (Button button in panel1.Controls.OfType<Button>())
            {
                button.BackColor = Color.LightGray;
                button.Enabled = true;
                button.Click -= ButtonClick;
            }

            timeLeft = Decimal.ToInt32(trackBar1.Value) ;
            timer.Start();
           
            foreach (Button button in panel1.Controls.OfType<Button>())
            {
                button.Text = numbers[count++].ToString();
                button.Click += ButtonClick;
            }

            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timerToolStripStatusLabel1.Text = trackBar1.Value.ToString() + " sec";
        }
    }
}
