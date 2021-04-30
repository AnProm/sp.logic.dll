using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
namespace Logic.Model
{
    /// <summary>
    /// Базовый интерфейс модели работы с базами данных
    /// </summary>
    public interface IDataBaseModel
    {
        string mode { get; set; }

        string path { get; set; }
        /// <summary>
        /// Метод удаления из базы
        /// </summary>
        /// <param name="objectToDelete">Объект для удаления</param>
        /// <returns>Коллекцию базы данных без объекта</returns>
        ICollection<DataBaseObject> Delete(DataBaseObject objectToDelete);
        /// <summary>
        /// Метод добавления в базу данных
        /// </summary>
        /// <param name="objectToAdd">Объект для добавления</param>
        /// <returns>Коллекцию базы данных с объектом</returns>
        ICollection<DataBaseObject> Add(DataBaseObject objectToAdd);
        /// <summary>
        /// Метод обновления базы данных
        /// </summary>
        /// <param name="objectToUpdate">Обновленных объект с совпадающим GUID</param>
        /// <returns>Коллекцию с  обновленным объектом</returns>
        ICollection<DataBaseObject> Update(DataBaseObject objectToUpdate);
        /// <summary>
        /// Метод загрузки базы данных
        /// </summary>
        /// <returns>Коллекцию базы данных по указанному пути</returns>
        ICollection<DataBaseObject> Load();

        /// <summary>
        /// Метод получения ТЕКУЩЕЙ коллекции
        /// </summary>
        /// <returns>Активная коллекция</returns>
        ICollection<DataBaseObject> GetAll();
        /// <summary>
        /// Метод сохранения колекции по пути
        /// </summary>
        /// <param name="pathToSave">Сохранить коллекцию по пути</param>
        void Save(string pathToSave);

    }
}