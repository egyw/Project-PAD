
namespace POS
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnOrders = new System.Windows.Forms.Button();
            this.panelTopMiddle = new System.Windows.Forms.Panel();
            this.pictureRefresh = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            this.panelTopMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnOrders);
            this.panelTop.Controls.Add(this.panelTopMiddle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1448, 47);
            this.panelTop.TabIndex = 0;
            // 
            // btnOrders
            // 
            this.btnOrders.Location = new System.Drawing.Point(1341, 4);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(104, 40);
            this.btnOrders.TabIndex = 1;
            this.btnOrders.Text = "ORDERS";
            this.btnOrders.UseVisualStyleBackColor = true;
            // 
            // panelTopMiddle
            // 
            this.panelTopMiddle.Controls.Add(this.pictureRefresh);
            this.panelTopMiddle.Controls.Add(this.textBox1);
            this.panelTopMiddle.Controls.Add(this.pictureBox1);
            this.panelTopMiddle.Location = new System.Drawing.Point(616, 0);
            this.panelTopMiddle.Name = "panelTopMiddle";
            this.panelTopMiddle.Size = new System.Drawing.Size(350, 47);
            this.panelTopMiddle.TabIndex = 0;
            // 
            // pictureRefresh
            // 
            this.pictureRefresh.Image = ((System.Drawing.Image)(resources.GetObject("pictureRefresh.Image")));
            this.pictureRefresh.Location = new System.Drawing.Point(322, 13);
            this.pictureRefresh.Name = "pictureRefresh";
            this.pictureRefresh.Size = new System.Drawing.Size(25, 25);
            this.pictureRefresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureRefresh.TabIndex = 1;
            this.pictureRefresh.TabStop = false;
            this.pictureRefresh.Click += new System.EventHandler(this.pictureRefresh_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(39, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(265, 34);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panelLeft
            // 
            this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelLeft.Controls.Add(this.button2);
            this.panelLeft.Controls.Add(this.button1);
            this.panelLeft.Controls.Add(this.label1);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 47);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(413, 640);
            this.panelLeft.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(25, 154);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(358, 40);
            this.button2.TabIndex = 2;
            this.button2.Text = "Take Away";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 85);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(358, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "DINE IN";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Order Type";
            // 
            // panelRight
            // 
            this.panelRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(413, 47);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(1035, 640);
            this.panelRight.TabIndex = 2;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1448, 687);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "FormMain";
            this.Text = "POS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTopMiddle.ResumeLayout(false);
            this.panelTopMiddle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.Panel panelTopMiddle;
        private System.Windows.Forms.PictureBox pictureRefresh;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}