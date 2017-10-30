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
            using (var db = new ContextReader3())
            {
                Person person = new Person();
                DataControl dataControls = new DataControl();
                dataControls.NumberCard = "1final test";
                dataControls.NumberDevice = "1test";
                dataControls.Time = DateTime.Now;
                db.listOfData.Add(dataControls);
                person.FirstName = "1now test";
                person.LastName = "1ok";
                person.NumberCard = "   1string";
                person.NumberCard = "1test";
                db.listOfPerson.Add(person);
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
