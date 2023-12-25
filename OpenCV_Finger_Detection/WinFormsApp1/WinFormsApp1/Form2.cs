using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Properties;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {



        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void startbutton_MouseHover(object sender, EventArgs e)
        {
            startbutton.Image = Resources.start_hover;
        }
        private void startbutton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 Form1 = new Form1();
            Form1.Show();

        }

        private void startbutton_MouseLeave(object sender, EventArgs e)
        {
            startbutton.Image = Resources.start_normal;
        }

        private void exitbutton_MouseHover(object sender, EventArgs e)
        {
            exitbutton.Image = Resources.exit_hover;
        }
        private void exitbutton_MouseLeave(object sender, EventArgs e)
        {
            exitbutton.Image = Resources.exit_normal;
        }

        private void exitbutton_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
