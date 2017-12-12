using comReaderLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComReaderConsole
{
    public class ConsoleView : IView
    {
        public void ShowText(string str)
        {
            Console.WriteLine(str);
        }
    }
}
