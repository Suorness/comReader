using comReaderLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comReaderLib.Dao
{
    public interface IDAOContext
    {
        /// <summary>
        /// Добавляет новые данные от валидатора в базу 
        /// </summary>
        /// <param name="checkPoints"></param>
        /// 
        void AddCheckPoint(List<CheckPointEntry> checkPoints);
        /// <summary>
        /// Добавляет device в список устройств
        /// </summary>
        /// <param name="device"></param>
        void AddDevice(Device device);
        /// <summary>
        /// Проверяет наличие устройства в списке устройств
        /// </summary>
        /// <param name="deviceNumber"> Номер устройства</param>
        /// <returns>true - наличие; false - отсутсвие</returns>
        bool CheckDevice(string deviceNumber);

        void AddCard(Card card);
        bool CheckCard(string cardNumber);
    }
}
