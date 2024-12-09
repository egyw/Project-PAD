using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace POS
{
    class Keyboard
    {
        private static Panel keyboardPanel;
        private static TextBox activeTextBox;

        public static void addKeyboard(Form form, Panel panel, TextBox textBox)
        {
            keyboardPanel = panel;
            activeTextBox = textBox;

            int buttonWidth = 40;
            int buttonHeight = 40;
            int offsetX = 10;
            int offsetY = 10;

            string[] buttonLabels = new string[] {
                "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")",
                "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", 
                "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", 
                "A", "S", "D", "F", "G", "H", "J", "K", "L", "Z", 
                "X", "C", "V", "B", "N", "M", "Back", "Clear" ,"Enter", ""
            };

            for (int i = 0; i < buttonLabels.Length; i++)
            {
                Button button = new Button();
                button.Text = buttonLabels[i];
                button.Size = new Size(buttonWidth, buttonHeight);
                button.Location = new Point(offsetX + (i % 10) * (buttonWidth + 5), offsetY + (i / 10) * (buttonHeight + 5));
                button.Click += new EventHandler(keyboardButton_Click);
                keyboardPanel.Controls.Add(button);
            }

            form.Controls.Add(keyboardPanel);
        }

        private static void keyboardButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;

            if (buttonText == "Back")
            {
                if (activeTextBox.Text.Length > 0)
                    activeTextBox.Text = activeTextBox.Text.Substring(0, activeTextBox.Text.Length - 1);
            }
            else if (buttonText == "Clear")
            {
                activeTextBox.Clear();
            }
            else
            {
                activeTextBox.Text += buttonText;
            }
        }

        public static void clearKeyboard()
        {
            if (keyboardPanel != null)
            {
                keyboardPanel.Controls.Clear();
            }
        }
    }
}
