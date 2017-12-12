using comReaderLib.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comReaderLib.Core
{
    public class Factory
    {
        public static Factory GetInstance()
        {
            if (factory == null)
            {
                factory = new Factory();
            }
            return factory;
        }
        public IDAOContext GetDao()
        {
            return dao;
        }

        private Factory() { }
        private static Factory factory = null;
        private IDAOContext dao = new MysqlDao();
    }
}
