using comReaderLib.Dao;
using comReaderLib.Domain;
using comReaderLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace comReaderLib.Core
{
    public class Controller
    {
        private Controller(IView view)
        {
            listReader = new List<Reader>();
            timerUpdate = new System.Timers.Timer();
            timerUpdate.Interval = 5000;
            timerUpdate.Elapsed += OnTimedUpdata;
            this.view = view;
            factory = Factory.GetInstance();
            dao = factory.GetDao();
        }

        public static Controller GetInstanse(IView view)
        {
            if (controller == null)
            {
                controller = new Controller(view);

            }
            return controller;
        }
        /// <summary>
        /// Запуск контроллера
        /// </summary>
        public void Start()
        {
            listReader = new List<Reader>();
            List<String> list = GetListPort();
            view.ShowText("Начало работы");
            foreach (var com in list)
            {
                view.ShowText("Инициализация " + com);
                var reader = new Reader(com);
                if (reader.IsActive)
                {
                    listReader.Add(reader);
                    if (!dao.CheckDevice(reader.DeviceNumber))
                    {

                        var device = new Device();
                        device.DeviceNumber = reader.DeviceNumber;
                        device.Description = "Описание отсутвует";
                        dao.AddDevice(device);
                    }
                    view.ShowText("Инициализация " + com + " прошла успешно");
                }
                else
                {
                    view.ShowText("Инициализация " + com + " провалилась");
                }

            }
            timerUpdate.Enabled = true;
        }
        /// <summary>
        /// Останавливает обновление данных
        /// </summary>
        public void Stop()
        {
            timerUpdate.Enabled = false;
        }
        /// <summary>
        /// Возобновляет обновление данных
        /// </summary>
        public void Resume()
        {
            timerUpdate.Enabled = true;
        }
        /// <summary>
        /// Обновление данных с устройст по таймеру
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimedUpdata(Object source, ElapsedEventArgs e)
        {
            //timerUpdate.Enabled = false;
            view.ShowText("Обновление данных и устройств");

            CheckListReader();
            var list = GetData();
            try
            {
                dao.AddCheckPoint(list);
                foreach (var point in list)
                {
                    if (!dao.CheckPerson(point.CardNumber))
                    {
                        var person = new Person();
                        person.CardNumber = point.CardNumber;
                        person.DateOfBirth = DateTime.Now;
                        dao.AddPerson(person);
                    }
                }
            }
            catch (Exception ex)
            {
                string message = "Произошла ошибка при работе с базой данных" + ex.Message;
                view.ShowText(message);
            }      
        }
        /// <summary>
        /// Обнавляет текущий список Reader 
        ///  *удаляет отключенные устройства
        ///  *добавляет подключеннные устройства
        /// </summary>
        private void CheckListReader()
        {
            listReader.RemoveAll(item => !item.IsActive);
            List<String> list = GetListPort();
            var listCom = from reader in listReader select reader.Port;
            var resultMatch = listCom.Where(a => !list.Contains(a));

            if (resultMatch.GetEnumerator().Current != null)
            {
                foreach (var com in resultMatch)
                {
                    view.ShowText("Инициализация " + com);
                    var reader = new Reader(com);
                    if (reader.IsActive)
                    {
                        listReader.Add(reader);

                        view.ShowText("Инициализация " + com + " прошла успешно");
                    }
                    else
                    {
                        view.ShowText("Инициализация " + com + " провалилась");
                    }
                }
            }
        }
        /// <summary>
        /// возвращает список элементов для добавления в базу
        /// </summary>
        private List<CheckPointEntry> GetData()
        {
            var listPoint = new List<CheckPointEntry>();
            foreach (var reader in listReader)
            {
                reader.SendRequest();
            }
            Thread.Sleep(500);
            foreach (var reader in listReader)
            {
                string responsy = reader.GetResponsу();
                if (CheckData(responsy))
                {
                    var entryPoint = new CheckPointEntry();
                    entryPoint.CardNumber = responsy;
                    entryPoint.DeviceNumber = reader.DeviceNumber;
                    entryPoint.CheckDate = DateTime.Now;
                    listPoint.Add(entryPoint);
                }
                view.ShowText(responsy);
            }
            return listPoint;
        }
        /// <summary>
        /// Проверяет ответ от Reader на наличие данные
        /// </summary>
        /// <param name="responsy"></param> 
        /// Ответ от Reader
        /// <returns></returns>
        /// True - данные есть
        /// False - данных нет
        private bool CheckData(string responsy)
        {
            bool Result = true;
            //TODO: Проверить ответ 
            string pattern = @"No";
            Regex regex = new Regex(pattern);
            MatchCollection matchGroup = regex.Matches(responsy);
            if (matchGroup.Count == 1)
            {
                Result = false;
            }
            return Result;
        }
        /// <summary>
        /// Возвращает список доступных портов на ПК
        /// </summary>
        /// <returns>
        /// Возвращает список портов 
        /// </returns>
        private List<String> GetListPort()
        {
            List<string> list = new List<String>();
            list.AddRange(SerialPort.GetPortNames());
            return list;
        }

        private Factory factory;
        private DAOContext dao;
        private System.Timers.Timer timerUpdate;
        private List<Reader> listReader;
        private static Controller controller = null;
        private IView view;
    }
}
