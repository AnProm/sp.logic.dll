using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using NHibernate;
using NHibernate.Cfg;

namespace Logic.Model
{
    public class DataBaseMainModel : IDataBaseModel
    {
        public string mode { get; set; }

        public string path { get; set; }

        /// <summary>
        /// Операция добавления элемента в базу данных
        /// </summary>
        /// <param name="objectToAdd"> Объект наследуемый от DataBaseObject (Либо AccessInfo, либо DllFileInfo) </param>
        public ICollection<DataBaseObject> Add(DataBaseObject objectToAdd)
        {
            using (ISession session = HybernateHelper.OpenSession(path))
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(objectToAdd);
                    transaction.Commit();
                }
            }
            return GetAll();
        }
        /// <summary>
        /// Обновляет данные существующей записи в базе данных
        /// </summary>
        /// <param name="objectToAdd"> Объект наследуемый от DataBaseObject (Либо AccessInfo, либо DllFileInfo) </param>
        public ICollection<DataBaseObject> Update(DataBaseObject objectToAdd)
        {

            using (ISession session = HybernateHelper.OpenSession(path))
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(objectToAdd);
                    transaction.Commit();
                }
            }
            return GetAll();
        }
        /// <summary>
        /// Удаляет заданный объект в базе данных
        /// </summary>
        /// <param name="objectToAdd"> Объект наследуемый от DataBaseObject (Либо AccessInfo, либо DllFileInfo)</param>
        public ICollection<DataBaseObject> Delete(DataBaseObject objectToRemove)
        {

            using (ISession session = HybernateHelper.OpenSession(path))
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(objectToRemove);

                    transaction.Commit();

                }
            }
            return GetAll();
        }
        /// <summary>
        /// Позволяет получить объект по его уникальному  системному ключу
        /// </summary>
        /// <param name="systemId">Уникальный системный ключ записи</param>
        /// <returns> Объект наследуемый от DataBaseObject (Либо AccessInfo, либо DllFileInfo) с совпадающим Guid</returns>
        public ICollection<DataBaseObject> GetAll()
        {
            ObservableCollection<DataBaseObject> result = new ObservableCollection<DataBaseObject>();
            using (ISession session = HybernateHelper.OpenSession(path))
            {
                var queryResult = session.Query<DataBaseObject>().ToList();
                foreach (DataBaseObject obj in queryResult)
                    result.Add(obj);
            }
            return result;
        }

        public ICollection<DataBaseObject> Load()
        {
            return GetAll();
        }

        public void Save(string pathToSave)
        {
            using (ISession session = HybernateHelper.OpenSession(path))
            {
                session.Disconnect();
                session.Close();

                session.Dispose();
                HybernateHelper.CloseSession(path);


            }

        }
    }


}
