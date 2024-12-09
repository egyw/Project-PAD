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
        private static bool isShiftActive = false;
        public static void addKeyboard(Form form, Panel panel, TextBox textBox, Panel container)
        {
            keyboardPanel = panel;
            activeTextBox = textBox;

            int buttonWidth = Screen.PrimaryScreen.WorkingArea.Width / 15;
            int buttonHeight = 90;
            int x = 0;
            int y = 0;

            int[] buttonsPerRow = new int[] { 14, 14, 13, 12, 1 };

            string[] buttonLabels = new string[] {
                "`", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=", "Backspace",
                "Tab", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "[", "]", "\\",
                "Caps", "A", "S", "D", "F", "G", "H", "J", "K", "L", ";", "'", "Enter",
                "Shift", "Z", "X", "C", "V", "B", "N", "M", ",", ".", "/", "Clear",
                "Space"
            };

            int index = 0;
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(index);
                for (int j = 0; j < buttonsPerRow[i]; j++)
                {
                    Button button = new Button();
                    button.Text = buttonLabels[index];
                    button.Tag = buttonLabels[index];

                    if (button.Text == "Shift" || button.Text == "Caps" || button.Text == "Tab" || button.Text == "Backspace" || button.Text == "Enter")
                    {
                        button.Size = new Size(buttonWidth * 2, buttonHeight); 
                    }
                    else if (button.Text == "Clear")
                    {
                        button.Size = new Size(buttonWidth * 3, buttonHeight);
                    }
                    else if (button.Text == "Space")
                    {
                        button.Size = new Size(buttonWidth * 15, buttonHeight);
                    }
                    else
                    {
                        button.Size = new Size(buttonWidth, buttonHeight);
                    }

                    button.Location = new Point(x, y);
                    button.Click += new EventHandler(keyboardButton_Click);
                    keyboardPanel.Controls.Add(button);
                    x += button.Width;
                    index++;
                }
                x = 0;
                y += 90;
            }
           
            container.Controls.Add(keyboardPanel);
            int xPos = (container.Width - keyboardPanel.Width) / 2;
            int yPos = (container.Height - keyboardPanel.Height) / 2;

            keyboardPanel.Location = new Point(xPos,  yPos);
        }

        private static void keyboardButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;

            if (buttonText == "Shift")
            {
                isShiftActive = !isShiftActive;
                UpdateKeyboard(); 
            }
            else if (buttonText == "Backspace")
            {
                if (activeTextBox.Text.Length > 0)
                    activeTextBox.Text = activeTextBox.Text.Substring(0, activeTextBox.Text.Length - 1);
            }
            else if (buttonText == "Clear")
            {
                activeTextBox.Clear();
            }
            else if (buttonText == "Space")
            {
                activeTextBox.Text += " ";
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

        private static void UpdateKeyboard()
        {
            foreach (Button button in keyboardPanel.Controls)
            {
                string originalText = (string)button.Tag; 

                if (isShiftActive)
                {
                    if ("1234567890".Contains(originalText))
                    {
                        button.Text = GetShiftSymbol(originalText);
                    }
                    else if (originalText.Length == 1 && Char.IsLetter(originalText[0])) 
                    {
                        button.Text = originalText.ToUpper();
                    }
                    else
                    {
                        button.Text = originalText; 
                    }
                }
                else
                {
                    if ("1234567890".Contains(originalText)) 
                    {
                        button.Text = originalText;
                    }
                    else if (originalText.Length == 1 && Char.IsLetter(originalText[0])) 
                    {
                        button.Text = originalText.ToLower();
                    }
                    else
                    {
                        button.Text = originalText; 
                    }
                }
            }
        }

        private static string GetShiftSymbol(string number)
        {
            switch (number)
            {
                case "1": return "!";
                case "2": return "@";
                case "3": return "#";
                case "4": return "$";
                case "5": return "%";
                case "6": return "^";
                case "7": return "&";
                case "8": return "*";
                case "9": return "(";
                case "0": return ")";
                case "-": return "_";
                case "=": return "+";
                default: return number;
            }
        }
    }
}
