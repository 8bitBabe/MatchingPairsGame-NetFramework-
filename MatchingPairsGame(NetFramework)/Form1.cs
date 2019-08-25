using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingPairsGame_NetFramework_
{
    public partial class Form1 : Form
    {
        Label firstClicked = null;
        Label secondClicked = null;

        Random random = new Random();
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };
        public Form1()
        {
            InitializeComponent();
            AssignIconToSquares();
        }

        private void AssignIconToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    //chooses random number that will assign an icon from list
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];

                    //makes the icons "invisible" by making the icon the same color as the bg
                    iconLabel.ForeColor = iconLabel.BackColor;

                    //removes icon from the form so it won't repeat icons more than twice
                    icons.RemoveAt(randomNumber);
                }
            }
            

        }

        private void labelClick(object sender, EventArgs e)
        {
            //what input? the one from sender, so to speak
            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                //checks if the timer had already started, avoids choosing a third icon
                if (timer1.Enabled == true)
                    return;

                //checks if you already clicked the label
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (firstClicked == null)
                {
                    //if not, it "reveals" the icon by turning the color black
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                CheckWinner();

                //if the icons match, tells the game to stop keeping track of the matching icons
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                //start the timer to briefly show the icons bc the icons don't match
                timer1.Start();
            }
        }

        private void timerTick(object sender, EventArgs e)
        {

            //resets the icons to invisible
            timer1.Stop();
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            firstClicked = null;
            secondClicked = null;
        }

        private void CheckWinner()
        {
            /*iterates through the labels checking the colors. If all colors
             equal black, then the player won triggering the message box*/
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            MessageBox.Show("You matched all the icons!", "Congrats!");
            Close();
        }
    }
}
