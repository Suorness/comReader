using comReader.View;
using comReaderLib.Core;
using comReaderLib.Interfaces;
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
    public partial class mainForm : Form
    {
        Controller controller = null;

        public mainForm()
        {
            InitializeComponent();
            controller = Controller.GetInstanse(new FormView(listEvent));
            controller.Start();
        }

        public ListBox GetListBox()
        {
            return listEvent;
        }

        private void mainForm_Load(object sender, EventArgs e)
        {

        }

        private void trayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            trayIcon.Visible = false;

        }

        private void mainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                trayIcon.Visible = true;
            }
        }

        private void bnStop_Click(object sender, EventArgs e)
        {
            
        }
        //class FormView : IView
        //{
        //    public void ShowText(string str)
        //    {
        //        listData.Items.Add(str + "\r\n");
        //    }

        //    public FormView(ref ListBox list)
        //    {
        //        listData = list;
        //    }
        //    ListBox listData;
        //}
    }
}
