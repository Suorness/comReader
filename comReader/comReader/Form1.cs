using comReader.View;
using readerLib.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace comReader
{
    public partial class Form1 : Form
    {
        Controller controller = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            controller = Controller.getInstanse(new FormView());
            controller.TestDB();
            
        }

        private void treu_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            treu.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {        
        }

        private void Form1_Resize_1(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                treu.Visible = true;
            }
        }
    }
}
