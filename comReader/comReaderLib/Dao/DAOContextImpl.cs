using System;
using System.Collections.Generic;
using comReaderLib.Domain;
using System.Linq;
namespace comReaderLib.Dao
{
    public class DAOContextImpl : DAOContext
    {
        
        public static DAOContext GetInstance()
        {
            if (dao == null)
            {
                dao = new DAOContextImpl();
            }
            return dao;
        }
        public void AddCheckPoint(List<CheckPointEntry> checkPoint)
        {
            db = new ContextReader();
            try
            {
                foreach (var point in checkPoint)
                {
                    db.CheckPointEntries.Add(point);
                }
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                db?.Dispose();
            } 
        }
        public void AddPerson(Person person)
        {
            db = new ContextReader();
            try
            {
                db.Persons.Add(person);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db?.Dispose();
            }
        }
        public void AddDevice(Device device)
        {
            db = new ContextReader();
            try
            {
                db.Devices.Add(device);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db?.Dispose();
            }
        }
        
        public bool CheckDevice(string deviceNumber)
        {
            bool Result = true;
            db = new ContextReader();
            try
            {
                var devices = from p in db.Devices where p.DeviceNumber == deviceNumber select p;
                if (devices.Count() <= 0)
                {
                    Result = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db?.Dispose();
            }
            return Result;
        }

        public bool CheckPerson(string CardNumber)
        {
            bool Result = true;
            db = new ContextReader();
            try
            {
                var people = from p in db.Persons where p.CardNumber == CardNumber select p;
                if (people.Count()<=0)
                {
                    Result = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db?.Dispose();
            }
            return Result;
        }

        private DAOContextImpl()
        {
        }
        private ContextReader db;
        private static DAOContextImpl dao = null;

    }
}
