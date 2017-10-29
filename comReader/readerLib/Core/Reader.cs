using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace readerLib.Core
{
    public class Reader
    {
        public Reader(string portNumber)
        {
            serialPort = new SerialPort();
            port = portNumber;
            serialPort.PortName = portNumber;
            serialPort.BaudRate = 9600;
            serialPort.Parity = Parity.None;
            serialPort.StopBits = StopBits.One;
            serialPort.Open();

            deviceNumber = DeviceNumber();
            if (deviceNumber != null)
            {
                IsActive = true;
            }
            else
            {
                serialPort.Close();
            }
        }
        public void sendRequest()
        {
            //"#05244 r" + "\r\n";
            serialPort.WriteLine("#" + deviceNumber + " r" + "\r\n");
        }
        public string getResponsу()
        {
            string str = null;
            str = serialPort.ReadExisting();
            if ((str == null) || (str == String.Empty))
            {
                IsActive = false;
            }
            return str;
        }
        private string DeviceNumber()
        {
            string result = null;

            serialPort.WriteLine("i");
            Thread.Sleep(500);
            string responsy = getResponsу();
            string pattern = @"S/N:(\d+)";
            Regex regex = new Regex(pattern);
            MatchCollection matchGroup = regex.Matches(responsy);
            if (matchGroup.Count == 1)
            {
                result = matchGroup[0].Value;
                result = result.Replace("S/N:", String.Empty);
            }
            return result;
        }
        public bool IsActive { get; set; }
        public string Port { get; }

        private SerialPort serialPort;
        private string port;
        private string deviceNumber = null;
    }
}
