using comReaderLib.Domain;
using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace comReaderLib.Dao
{
    public class MysqlDao : IDAOContext
    {
        private const string SqlAddCard = "INSERT INTO card (card_number, person_id) VALUES ('{0}',{1})";

        private const string SqlAddCheckPoint = "INSERT INTO check_entity (date_stamp, device_id_fk, card_id) VALUES ";

        private const string SqlGetDeviceId = "SELECT id FROM device WHERE device.device_number = '{0}'";

        private const string SqlGetCardId = "SELECT id FROM card WHERE card.card_number = '{0}'";

        private const string SqlAddDevice = "INSERT INTO device (device_number, description) VALUES ('{0}', '{1}')";

        private const string SqlCheckCard = "SELECT COUNT(*) FROM card WHERE card.card_number = '{0}'";

        private const string SqlCheckDevice = "SELECT COUNT(*) FROM device WHERE device.device_number = '{0}'";

        private const string SqlCheckPerson = "SELECT COUNT(*) FROM person JOIN card ON card.person_id = person.id WHERE card.card_number = '{0}'";

        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["MySQL connection"].ConnectionString; ; }
        }

        public void AddCard(Card card)
        {
            string personId;
            if (card.Person == null)
            {
                personId = "NULL";
            }
            else
            {
                personId = card.Person.Id.ToString();
            }

            string query = string.Format(SqlAddCard, card.CardNumber, personId);

            using (var conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                logger.Info("Добавлена карта {0}", card.CardNumber);
            }
        }

        public void AddCheckPoint(List<CheckPointEntry> checkPoints)
        {
            string query = SqlAddCheckPoint;
            string entry = "('{0}', {1}, {2})";
            foreach (var checkPoint in checkPoints)
            {
                logger.Info("Добавлена контрольная точка {0}", checkPoint.CardNumber);
                query += string.Format(entry, checkPoint.CheckDate.ToString("yyyy-MM-dd H:mm:ss"), GetDeviceIdByNumber(checkPoint.DeviceNumber), GetCardIdByNumber(checkPoint.CardNumber));
                if (checkPoint != checkPoints.Last())
                {
                    query += ", ";
                }
            }

            using (var conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddDevice(Device device)
        {
            string query = string.Format(SqlAddDevice, device.DeviceNumber, device.Description);
            using (var conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                logger.Info("Добавлено устройство {0}", device.DeviceNumber);
            }
        }

        public bool CheckCard(string cardNumber)
        {
            bool result = false;
            string query = string.Format(SqlCheckCard, cardNumber);
            using (var conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool CheckDevice(string deviceNumber)
        {
            bool result = false;
            string query = string.Format(SqlCheckDevice, deviceNumber);
            using (var conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                {
                    result = true;
                }
            }

            return result;
        }


        private int GetDeviceIdByNumber(string deviceNumber)
        {
            int id;
            string query = string.Format(SqlGetDeviceId, deviceNumber);
            using (var conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return id;
        }

        private int GetCardIdByNumber(string cardNumber)
        {
            int id;
            string query = string.Format(SqlGetCardId, cardNumber);
            using (var conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return id;
        }

        private static MySqlConnection GetConnection()
        {
            MySqlConnection databaseConnection = new MySqlConnection(ConnectionString);
            try
            {
                databaseConnection.Open();
            }
            catch (Exception)
            {
                ////TODO: change exception
                throw new Exception("Couldn't connect to the database.");
            }

            return databaseConnection;
        }
    }
}
