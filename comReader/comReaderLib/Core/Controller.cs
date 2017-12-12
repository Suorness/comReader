using comReaderLib.Dao;
using comReaderLib.Domain;
using comReaderLib.Interfaces;
using NLog;
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

        private static Logger logger = LogManager.GetCurrentClassLogger();

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
            ShowMessage("Начало работы");
            foreach (var com in list)
            {
                ShowMessage("Инициализация " + com);
                var reader = new Reader(com);
                if (reader.IsActive)
                {
                    listReader.Add(reader);
                    if (!dao.CheckDevice(reader.DeviceNumber))
                    {

                        var device = new Device
                        {
                            DeviceNumber = reader.DeviceNumber,
                            Description = "Описание отсутвует"
                        };
                        dao.AddDevice(device);
                    }
                    ShowMessage("Инициализация " + com + " прошла успешно");
                }
                else
                {
                    ShowMessage("Инициализация " + com + " провалилась");
                }

            }
            timerUpdate.Enabled = true;
        }
        /// <summary>
        /// Останавливает обновление данных
        /// </summary>
        public void Stop()
        {
            timerUpdate.Stop();
            timerUpdate.Enabled = false;
        }

        public void StopProgram()
        {
            foreach (var reader in listReader)
            {
                reader.Stop();
            }
        }
        /// <summary>
        /// Возобновляет обновление данных
        /// </summary>
        public void Resume()
        {
            timerUpdate.Enabled = true;
            timerUpdate.Start();
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
                if ( (list!= null) && (list.Count()> 0) )
                { 
                    
                    foreach (var point in list)
                    {
                        if (!dao.CheckCard(point.CardNumber))
                        {

                            var card = new Card
                            {
                                CardNumber = point.CardNumber
                            };

                            dao.AddCard(card);
                        }
                        ;
                    }
                    dao.AddCheckPoint(list);
                    
                }
            }
            catch (Exception ex)
            {
                string message = "Произошла ошибка при работе с базой данных: " + ex.Message;
                view.ShowText(message);
                logger.Error(message);
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
                    ShowMessage("Инициализация " + com);
                    var reader = new Reader(com);
                    if (reader.IsActive)
                    {
                        listReader.Add(reader);
                        ShowMessage("Инициализация " + com + " прошла успешно");
                    }
                    else
                    {
                        ShowMessage("Инициализация " + com + " провалилась");
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
                    var entryPoint = new CheckPointEntry
                    {
                        CardNumber = responsy,
                        DeviceNumber = reader.DeviceNumber,
                        CheckDate = DateTime.Now
                    };
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
            bool result = true;
            string pattern = @"No";
            Regex regex = new Regex(pattern);
            MatchCollection matchGroup = regex.Matches(responsy);
            if (matchGroup.Count == 1)
            {
                result = false;
            }
            else if (responsy == String.Empty)
            {
                result = false;
            }
            return result;
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

        private void ShowMessage(string message)
        {
            view.ShowText(message);
            logger.Info(message);
        }

        private Factory factory;
        private IDAOContext dao;
        private System.Timers.Timer timerUpdate;
        private List<Reader> listReader;
        private static Controller controller = null;
        private IView view;
    }
}
