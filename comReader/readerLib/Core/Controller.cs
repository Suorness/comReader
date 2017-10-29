using readerLib.Dao;
using readerLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Data.Entity;

namespace readerLib.Core
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
        }
        public static Controller getInstanse(IView view)
        {
            if (controller == null)
            {
                controller = new Controller(view);
                
            }
            return controller;
        }
        public void Start()
        {
            listReader = new List<Reader>();
            List<String> list = getListPort();
            view.ShowText("Начало работы");
            foreach (var com in list)
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
            timerUpdate.Enabled = true;
            //TODO
            while (true) { }
        }
        public void TestDB()
        {
            ReaderModelContainer dbContext;
            dbContext = new ReaderModelContainer();
            ControlData controlData = new ControlData();
            controlData.CardNumber = "test card#";
            controlData.DeviceNumber = "test devid";
            //controlData.
            dbContext.Entry<ControlData>(controlData).State = EntityState.Added;
            //dbContext.ControlDataSet.Add(controlData);
            dbContext.SaveChanges();

        }
        private void OnTimedUpdata(Object source, ElapsedEventArgs e)
        {
            view.ShowText("Обновление данных и устройств");
            checkListReader();
            getData();
            
        }

        private void checkListReader()
        {

            listReader.RemoveAll(item => !item.IsActive);
            List<String> list = getListPort();
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
        private void getData()
        {
            foreach (var reader in listReader)
            {
                reader.sendRequest();
            }
            //TODO
            Thread.Sleep(500);
            //
            foreach (var reader in listReader)
            {
                view.ShowText(reader.getResponsу());
            }
        }
        
        private List<String> getListPort()
        {
            List<string> list = new List<String>();
            list.AddRange(SerialPort.GetPortNames());
            return list;
        }

        private System.Timers.Timer timerUpdate;
        private List<Reader> listReader;
        private static Controller controller = null;
        private bool working = true;
        private IView view;
    }
}
