using comReader.View;
using comReaderLib.Core;
using System;
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
            Icon = Properties.Resources.taskBar;
            trayIcon.Icon = Properties.Resources.taskBar;
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
            this.ShowInTaskbar = true;

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
            AddNewItem();
        }

        private void AddNewItem()
        {
            try
            {
                var newList = formView.GetData();
                int index = 1;

                foreach (var item in newList)
                {
                    if (index > listEvent.Items.Count)
                    {
                        listEvent.Items.Add(item);
                    }
                    index++;
                }
            }
            catch (Exception) { }
        }

        private void bnStart_Click(object sender, EventArgs e)
        {
            controller.Resume();
            bnStart.Enabled = false;
            bnStop.Enabled = true;
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //controller.StopProgram();
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.StopProgram();
        }
    }
}
