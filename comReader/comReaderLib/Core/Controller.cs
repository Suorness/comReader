using comReaderLib.Domain;
using comReaderLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
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
        }
        public static Controller GetInstanse(IView view)
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
            using (var db = new ContextReader())
            {
                Person person = new Person();
                CheckPointEntry checkPointEntries = new CheckPointEntry();
                checkPointEntries.CardNumber = "1final test";
                checkPointEntries.DeviceNumber = "1test";
                checkPointEntries.CheckDate = DateTime.Now;
                db.CheckPointEntries.Add(checkPointEntries);
                person.FirstName = "1now test";
                person.LastName = "1ok";
                person.CardNumber = "   1string";
                person.CardNumber = "1test";
                db.Persons.Add(person);
                db.SaveChanges();
            }

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
