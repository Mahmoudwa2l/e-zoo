namespace WinFormsApp1
{
    partial class Form2
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
            menu = new Panel();
            exitbutton = new PictureBox();
            startbutton = new PictureBox();
            menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)exitbutton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)startbutton).BeginInit();
            SuspendLayout();
            // 
            // menu
            // 
            menu.BackgroundImage = Properties.Resources.menu;
            menu.BackgroundImageLayout = ImageLayout.Stretch;
            menu.Controls.Add(exitbutton);
            menu.Controls.Add(startbutton);
            menu.Location = new Point(111, 87);
            menu.Name = "menu";
            menu.Size = new Size(442, 492);
            menu.TabIndex = 0;
            // 
            // exitbutton
            // 
            exitbutton.Image = Properties.Resources.exit_normal;
            exitbutton.Location = new Point(109, 290);
            exitbutton.Name = "exitbutton";
            exitbutton.Size = new Size(236, 91);
            exitbutton.SizeMode = PictureBoxSizeMode.StretchImage;
            exitbutton.TabIndex = 1;
            exitbutton.TabStop = false;
            exitbutton.MouseLeave += exitbutton_MouseLeave;
            exitbutton.MouseHover += exitbutton_MouseHover;
            // 
            // startbutton
            // 
            startbutton.Image = Properties.Resources.start_normal;
            startbutton.Location = new Point(109, 181);
            startbutton.Name = "startbutton";
            startbutton.Size = new Size(236, 91);
            startbutton.SizeMode = PictureBoxSizeMode.StretchImage;
            startbutton.TabIndex = 0;
            startbutton.TabStop = false;
            startbutton.MouseLeave += startbutton_MouseLeave;
            startbutton.MouseHover += startbutton_MouseHover;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 0, 0);
            ClientSize = new Size(684, 711);
            Controls.Add(menu);
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form2";
            Load += Form2_Load;
            menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)exitbutton).EndInit();
            ((System.ComponentModel.ISupportInitialize)startbutton).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel menu;
        private PictureBox startbutton;
        private PictureBox exitbutton;
    }
}