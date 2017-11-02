using comReaderLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comReaderLib.Dao
{
    public interface DAOContext
    {
        /// <summary>
        /// Добавляет новые данные от валидатора в базу 
        /// </summary>
        /// <param name="checkPoint"></param>
        /// 
        void AddCheckPoint(List<CheckPointEntry> checkPoint);
        /// <summary>
        /// Проверяет наличие в базе записи с данной карточкой 
        /// </summary>
        /// <param name="person"></param>
        /// <returns>
        /// True - присутвует 
        /// False - отсутствует 
        /// </returns>
        bool CheckPerson(string CardNumber);
        /// <summary>
        /// Добавляет person в список доступных людей
        /// </summary>
        /// <param name="person"></param>
        void AddPerson(Person person);
    }
}
