using comReaderLib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComReaderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleView = new ConsoleView();           
            Controller controller = Controller.GetInstanse(consoleView);

            while (true)
            {
                
            }
        }
    }
}
