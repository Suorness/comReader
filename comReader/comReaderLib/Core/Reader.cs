using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace comReaderLib.Core
{
    public class Reader
    {
        public Reader(string portNumber)
        {
            try
            {
                serialPort = new SerialPort();
                port = portNumber;
                serialPort.PortName = portNumber;
                serialPort.BaudRate = 9600;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.Open();

                deviceNumber = GetDeviceNumber();
                if (deviceNumber != null)
                {
                    IsActive = true;
                }
                else
                {
                    serialPort.Close();
                }
            }
            catch (Exception)
            {
                IsActive = false;
            }
        }
        /// <summary>
        /// Отправляет команду заполнения буфера устройства данными
        /// </summary>
        public void SendRequest()
        {
            try
            {
                //"#05244 r" + "\r\n";
                serialPort.WriteLine("#" + deviceNumber + " r" + "\r\n");
            }
            catch (Exception)
            {
                IsActive = false;
            }
        }
        /// <summary>
        /// Получение данных из буфера устройства
        /// </summary>
        /// <returns>
        /// Данные из буфера
        /// </returns>
        public string GetResponsу()
        {
            string str = null;
            str = serialPort.ReadExisting();
            if ((str == null) || (str == String.Empty))
            {
                IsActive = false;
            }
            return str;


        }
        /// <summary>
        /// Получение номера устройства
        /// </summary>
        /// <returns></returns>
        private string GetDeviceNumber()
        {
            string result = null;
            try
            {
                serialPort.WriteLine("i");
                Thread.Sleep(500);
                string responsy = GetResponsу();
                string pattern = @"S/N:(\d+)";
                Regex regex = new Regex(pattern);
                MatchCollection matchGroup = regex.Matches(responsy);
                if (matchGroup.Count == 1)
                {
                    result = matchGroup[0].Value;
                    result = result.Replace("S/N:", String.Empty);
                }
            }
            catch (Exception)
            {
                result = String.Empty;
            }
            return result;
        }
        /// <summary>
        /// Получение ответов от устройства
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        ///  Порт устройства
        /// </summary>
        public string Port { get; }
        /// <summary>
        /// Номер устройства
        /// </summary>
        public string DeviceNumber { get => deviceNumber; }


        private SerialPort serialPort;
        private string port;
        private string deviceNumber = null;

    }
}
