using System;
using System.Collections.Generic;
using comReaderLib.Domain;
using System.Linq;

namespace comReaderLib.Dao
{
    public class DAOContextImpl : IDAOContext
    {
        private ContextReader db;
        private static DAOContextImpl dao = null;

        public static IDAOContext GetInstance()
        {
            if (dao == null)
            {
                dao = new DAOContextImpl();
            }
            return dao;
        }

        public void AddCheckPoint(List<CheckPointEntry> checkPoints)
        {
            db = new ContextReader();
            try
            {
                foreach (var point in checkPoints)
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

        public void AddCard(Card card)
        {
            db = new ContextReader();
            try
            {
                db.Cards.Add(card);
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

        public bool CheckPerson(string cardNumber)
        {
            bool Result = true;
            db = new ContextReader();
            try
            {
                var people = from p in db.Persons where p.CardNumber == cardNumber select p;
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

        public bool CheckCard(string cardNumber)
        {
            bool Result = true;
            db = new ContextReader();
            try
            {
                var people = from p in db.Cards where p.CardNumber == cardNumber select p;
                if (people.Count() <= 0)
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

    }
}
