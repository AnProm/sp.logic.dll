using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;

namespace Logic
{
    public class DataBaseRepository
    {
        /// <summary>
        /// Операция добавления элемента в базу данных
        /// </summary>
        /// <param name="objectToAdd"> Объект наследуемый от DataBaseObject (Либо AccessInfo, либо DllFileInfo) </param>
        public void Add(DataBaseObject objectToAdd)
        {
            using(ISession session = HybernateHelper.OpenSession())
            {
                using(ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(objectToAdd);
                    transaction.Commit();
                }
            }
        }
        /// <summary>
        /// Обновляет данные существующей записи в базе данных
        /// </summary>
        /// <param name="objectToAdd"> Объект наследуемый от DataBaseObject (Либо AccessInfo, либо DllFileInfo) </param>
        public void Update(DataBaseObject objectToAdd)
        {

            using (ISession session = HybernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(objectToAdd);
                    transaction.Commit();
                }
            }
        }
        /// <summary>
        /// Удаляет заданный объект в базе данных
        /// </summary>
        /// <param name="objectToAdd"> Объект наследуемый от DataBaseObject (Либо AccessInfo, либо DllFileInfo)</param>
        public void Delete(DataBaseObject objectToAdd)
        {

            using (ISession session = HybernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(objectToAdd);
                    transaction.Commit();
                }
            }
        }
        /// <summary>
        /// Позволяет получить объект по его уникальному  системному ключу
        /// </summary>
        /// <param name="systemId">Уникальный системный ключ записи</param>
        /// <returns> Объект наследуемый от DataBaseObject (Либо AccessInfo, либо DllFileInfo) с совпадающим Guid</returns>
        public DataBaseObject GetFirstBySystemId(Guid systemId)
        {
            using (ISession session = HybernateHelper.OpenSession())
            {
                var queryResult = session.QueryOver<DataBaseObject>().Where(x => x.SystemId == systemId).SingleOrDefault();
                Console.ReadLine();
                return queryResult ?? null;
            }
        }
    }
}
