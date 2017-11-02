using comReaderLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace comReader.View
{
    class FormView : IView
    {
        public void ShowText(string str)
        {
            listData.Items.Add(str + "\r\n");                      
        }

        public FormView (ListBox list)
        {
            listData = list;
        }
        ListBox listData;
    }
}
