using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class AnimationLoadingPayment : Form
    {
        int second = 0;
        float opacity = 0f; // Opacity untuk animasi fade
        bool fadeIn = true; // Menentukan apakah sedang fade in atau fade out
        bool pilgambar = false;

        public AnimationLoadingPayment()
        {
            InitializeComponent();

        }

        private void AnimationLoadingPayment_Load(object sender, EventArgs e)
        {
            pilgambar = FormCustomAllPayment.cekTransaction;
            timer1.Interval = 500; 
            timer1.Start();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            second++;
            Console.WriteLine(second);

            if (second >= 8 && fadeIn)
            {
                opacity += 0.05f;
                if (opacity >= 1f)
                {
                    opacity = 1f;
                    fadeIn = false; 
                }
                if (pilgambar)
                {
                    pictureBox2.Visible = true;
                    pictureBox2.Invalidate();
                }
                else
                {
                    pictureBox3.Visible = true;
                    pictureBox3.Invalidate();
                }
               
            }


            if (second >= 12)
            {
                opacity = 0f;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                timer1.Stop();
                this.Close(); 
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Hanya menggambar ulang pictureBox2 dengan opacity
            if (pictureBox2.Visible && pictureBox2.Image != null)
            {
                DrawImageWithOpacity(e.Graphics, pictureBox2.Image, pictureBox2.Bounds, opacity);
            }
            if (pictureBox3.Visible && pictureBox3.Image != null)
            {
                DrawImageWithOpacity(e.Graphics, pictureBox3.Image, pictureBox3.Bounds, opacity);
            }
        }

        private void DrawImageWithOpacity(Graphics g, Image img, Rectangle destRect, float opacity)
        {
            ColorMatrix colorMatrix = new ColorMatrix
            {
                Matrix33 = opacity // Set transparansi alpha
            };

            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            g.DrawImage(img, destRect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, attributes);
        }

    }
}
