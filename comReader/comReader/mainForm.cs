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
        FormView formView = new FormView();
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            controller = Controller.GetInstanse(formView);
            controller.Start();
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
            controller.Stop();
            bnStop.Enabled = false;
            bnStart.Enabled = true;
        }

        private void timerUpdateLog_Tick(object sender, EventArgs e)
        {
            //int actualIndex = listEvent.SelectedIndex;
            //listEvent.DataSource = null;
            //listEvent.DataSource = formView.GetData();
            //listEvent.DisplayMember = "Name";
            //listEvent.ValueMember = "Id";
            //listEvent.SelectedIndex = actualIndex;
            //listEvent.Invalidate();
            AddNewItem();
        }

        private void AddNewItem()
        {
            var newList = formView.GetData();
            int index = 1;
            foreach(var item in newList)
            {
                if (index>listEvent.Items.Count)
                {
                    listEvent.Items.Add(item);
                }
                index++;
            }
        }

        private void bnStart_Click(object sender, EventArgs e)
        {
            controller.Resume();
            bnStart.Enabled = false;
            bnStop.Enabled = true;
        }
    }
}
