using comReaderLib.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            data.Add(str + "\r\n");
            
        }
        public List<string> GetData()
        {
            //var newData = data.Cop
            return data;
        }
        public FormView ()
        {
        }
        List<string> data = new List<string>();
        //private int count = 0;
        //BindingList<MyData> data = new BindingList<MyData>();
        //public class MyData
        //{
        //    public string Name { get; set; }
        //    public int Id { get; set; }
        //}
    }
}
