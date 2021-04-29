using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Logic.Model
{
    /// <summary>
    /// Класс, предоставляющий методы работы с фаловыми БД
    /// </summary>
    class DataBaseFromFile : IDataBaseModel
    {
        public string mode { get; set; }
        public string path { get; set; }

        /// <summary>
        /// Метод добавления новой записи, создаёт новый файл с записью
        /// </summary>
        /// <param name="objectToAdd">Обьект для добавления</param>
        /// <returns>Обнвленная коллекция</returns>
        public ICollection<DataBaseObject> Add(DataBaseObject objectToAdd)
        {
            switch (mode)
            {
                case ("json"):
                    string newFileNameJson = path + "\\newRecordDB-" + objectToAdd.GetHashCode().ToString() + ".json";
                    BinaryWriter binaryWriterJson = new BinaryWriter(File.Create(newFileNameJson));
                    DllFileInfo writableObjJson = (DllFileInfo)objectToAdd;
                    binaryWriterJson.Write(JsonConvert.SerializeObject(writableObjJson));
                    binaryWriterJson.Close();
                    break;

                case ("bin"):
                    string newFileName = path + "\\newRecordDB-" + objectToAdd.GetHashCode().ToString() + ".bin";
                    BinaryWriter binaryWriter = new BinaryWriter(File.Create(newFileName));
                    AccessInfo writableObj = (AccessInfo)objectToAdd;
                    binaryWriter.Write(writableObj.SystemId.ToString());
                    binaryWriter.Write(writableObj.Login);
                    binaryWriter.Write(writableObj.Hashcode);
                    binaryWriter.Write(writableObj.Password);
                    binaryWriter.Write(writableObj.Email);
                    binaryWriter.Close();
                    break;
            }
            return LoadAll();
        }
        
        /// <summary>
        /// Удаление записи из файловой БД
        /// </summary>
        /// <param name="objectToDelete">Сущность для удаления</param>
        /// <returns>Обновлённая коллекция</returns>
        public ICollection<DataBaseObject> Delete(DataBaseObject objectToDelete)
        {
            string[] files;
            switch (mode)
            {
                case ("json"):
                    files = Directory.GetFiles(path, "*.json");
                    BinaryReader binaryReaderJson;
                    DllFileInfo objectToDeleteJson = (DllFileInfo) objectToDelete;
                    bool rewriteJson = false;
                    ICollection<DataBaseObject> rewriteCollection = new LinkedList<DataBaseObject>();
                    for (int i = 0; i < files.Length; i++)
                    {
                        binaryReaderJson = new BinaryReader(File.OpenRead(files[i]));
                        rewriteCollection = new LinkedList<DataBaseObject>();
                        string jsonString = "";
                        while (binaryReaderJson.PeekChar() > -1)
                        {
                            jsonString = jsonString + binaryReaderJson.ReadString();
                        }
                        LinkedList<DllFileInfo> currObjects = new LinkedList<DllFileInfo>();
                        if (jsonString.Length > 2)
                        {
                            currObjects = JsonConvert.DeserializeObject<LinkedList<DllFileInfo>>(jsonString);
                            if (currObjects.Select(x => x.SystemId.Equals(objectToDeleteJson.SystemId)).Count() > 0)//danger - lambda
                            {
                                currObjects.Remove(currObjects.First(x => x.SystemId.Equals(objectToDeleteJson.SystemId)));
                                rewriteJson = true;
                            }
                        }
                        binaryReaderJson.Close();
                        if (rewriteJson)
                        {
                            BinaryWriter binaryWriter = new BinaryWriter(File.Create(files[i]));
                            binaryWriter.Write(JsonConvert.SerializeObject(currObjects));
                            binaryWriter.Close();
                            break;
                        }
                    }
                    break;
                case ("bin"):
                    files = Directory.GetFiles(path, "*.bin");
                    BinaryReader binaryReader;
                    AccessInfo objectToDeleteBin = (AccessInfo) objectToDelete;
                    bool rewrite = false;
                    ICollection<DataBaseObject> rewriteCollectionBin = new LinkedList<DataBaseObject>();
                    for (int i = 0; i < files.Length; i++)
                    {
                        binaryReader = new BinaryReader(File.OpenRead(files[i]));
                        rewriteCollectionBin = new LinkedList<DataBaseObject>();
                        while (binaryReader.PeekChar() > -1)
                        {
                            AccessInfo currentInfo = new AccessInfo(
                                Guid.Parse(binaryReader.ReadString()),//Guid
                                binaryReader.ReadString(),//login
                                binaryReader.ReadString(),//hash
                                binaryReader.ReadString(),//pass
                                binaryReader.ReadString());//email
                            if (objectToDeleteBin.SystemId.Equals(currentInfo.SystemId)){rewrite = true;}
                            else { rewriteCollectionBin.Add(currentInfo); }
                        }
                        binaryReader.Close();
                        if (rewrite)
                        {
                            BinaryWriter binaryWriter = new BinaryWriter(File.Create(files[i]));
                            for (int j = 0; j < rewriteCollectionBin.Count(); j++)
                            {
                                AccessInfo accessInfo = (AccessInfo)rewriteCollectionBin.ElementAt(j);
                                binaryWriter.Write(accessInfo.SystemId.ToString());
                                binaryWriter.Write(accessInfo.Login);
                                binaryWriter.Write(accessInfo.Hashcode);
                                binaryWriter.Write(accessInfo.Password);
                                binaryWriter.Write(accessInfo.Email);
                            }
                            binaryWriter.Close();
                            break;
                        }
                    }
                    break;
            }
            return LoadAll();
        }

        /// <summary>
        /// Инициализирует файловую БД
        /// </summary>
        /// <returns>Коллекция записей</returns>
        public ICollection<DataBaseObject> Load()
        {
            return LoadAll();
        }

        /// <summary>
        /// Сохраняет все записи из файловой БД в новый файл
        /// </summary>
        /// <param name="newPath">Путь, по которому будет сохранена БД</param>
        public void Save(string newPath)
        {
            ICollection<DataBaseObject> dataBaseObjects = LoadAll();
            switch (mode)
            {
                case ("json"):
                    string newFileNameJson = newPath + "\\programDB-" + dataBaseObjects.GetHashCode().ToString() + ".json";
                    BinaryWriter binaryWriterJson = new BinaryWriter(File.Create(newFileNameJson));
                    binaryWriterJson.Write(JsonConvert.SerializeObject(dataBaseObjects));
                    binaryWriterJson.Close();
                    break;
                case ("bin"):
                    string newFileName = newPath + "\\programDB-" + dataBaseObjects.GetHashCode().ToString() + ".bin";
                    BinaryWriter binaryWriter = new BinaryWriter(File.Create(newFileName));
                    for (int i = 0; i < dataBaseObjects.Count(); i++)
                    {
                        AccessInfo accessInfo = (AccessInfo) dataBaseObjects.ElementAt(i);
                        binaryWriter.Write(accessInfo.SystemId.ToString());
                        binaryWriter.Write(accessInfo.Login);
                        binaryWriter.Write(accessInfo.Hashcode);
                        binaryWriter.Write(accessInfo.Password);
                        binaryWriter.Write(accessInfo.Email);
                    }
                    binaryWriter.Close();
                    break;
            }
        }

        /// <summary>
        /// Обновление записи в БД
        /// </summary>
        /// <param name="objectToUpdate">Обновлённая версия объекта</param>
        /// <returns>Обновлённую коллекцию</returns>
        public ICollection<DataBaseObject> Update(DataBaseObject objectToUpdate)
        {
            string[] files;
            switch (mode)
            {
                case ("json"):
                    files = Directory.GetFiles(path, "*.json");
                    BinaryReader binaryReaderJson;
                    DllFileInfo objectToUpdateJson = (DllFileInfo)objectToUpdate;
                    bool rewriteJson = false;
                    for (int i = 0; i < files.Length; i++)
                    {
                        binaryReaderJson = new BinaryReader(File.OpenRead(files[i]));
                        string jsonString = "";
                        while (binaryReaderJson.PeekChar() > -1)
                        {
                            jsonString = jsonString + binaryReaderJson.ReadString();
                        }
                        LinkedList<DllFileInfo> currObjects = new LinkedList<DllFileInfo>();
                        if (jsonString.Length > 2)
                        {
                            currObjects = JsonConvert.DeserializeObject<LinkedList<DllFileInfo>>(jsonString);
                            if (currObjects.Select(x => x.SystemId.Equals(objectToUpdateJson.SystemId)).Count() > 0)//danger - lambda
                            {
                                DllFileInfo currDllInfo = currObjects.First(x => x.SystemId.Equals(objectToUpdateJson.SystemId)) ;
                                currDllInfo.FileName = objectToUpdateJson.FileName;
                                currDllInfo.FileVersion = objectToUpdateJson.FileVersion;
                                currDllInfo.DateOfLastEdit = objectToUpdateJson.DateOfLastEdit;
                                rewriteJson = true;
                            }
                        }
                        binaryReaderJson.Close();
                        if (rewriteJson)
                        {
                            BinaryWriter binaryWriter = new BinaryWriter(File.Create(files[i]));
                            binaryWriter.Write(JsonConvert.SerializeObject(currObjects));
                            binaryWriter.Close();
                            break;
                        }
                    }
                    break;
                case ("bin"):
                    AccessInfo objectToUpdateBin = (AccessInfo)objectToUpdate;
                    files = Directory.GetFiles(path, "*.bin");
                    BinaryReader binaryReader;
                    bool updateFile = false;
                    ICollection<DataBaseObject> rewriteCollection = new LinkedList<DataBaseObject>();
                    for (int i = 0; i < files.Length; i++)
                    {
                        binaryReader = new BinaryReader(File.OpenRead(files[i]));
                        rewriteCollection = new LinkedList<DataBaseObject>();
                        while (binaryReader.PeekChar() > -1)
                        {
                            AccessInfo currentInfo = new AccessInfo(
                                Guid.Parse(binaryReader.ReadString()),//Guid
                                binaryReader.ReadString(),//login
                                binaryReader.ReadString(),//hash
                                binaryReader.ReadString(),//pass
                                binaryReader.ReadString());//email
                            if (objectToUpdateBin.SystemId.Equals(currentInfo.SystemId))
                            {
                                rewriteCollection.Add(objectToUpdateBin);
                                updateFile = true;
                            }
                            else
                            {
                                rewriteCollection.Add(currentInfo);
                            }
                        }
                        binaryReader.Close();
                        if (updateFile)
                        {
                            BinaryWriter binaryWriter = new BinaryWriter(File.Create(files[i]));
                            for (int j = 0; j < rewriteCollection.Count(); j++)
                            {
                                AccessInfo accessInfo = (AccessInfo)rewriteCollection.ElementAt(j);
                                binaryWriter.Write(accessInfo.SystemId.ToString());
                                binaryWriter.Write(accessInfo.Login);
                                binaryWriter.Write(accessInfo.Hashcode);
                                binaryWriter.Write(accessInfo.Password);
                                binaryWriter.Write(accessInfo.Email);
                            }
                            binaryWriter.Close();
                            break;
                        }
                    }
                    break;
            }
            return LoadAll();
        }

        /// <summary>
        /// Реинициализиурет всё, что находится по пути, указанном в path
        /// </summary>
        /// <returns>Коллекцию записей из данной директории</returns>
        private ICollection<DataBaseObject> LoadAll()
        {
            string[] files;
            ICollection<DataBaseObject> dataBaseObjects = new LinkedList<DataBaseObject>();
            switch (mode)
            {
                case ("json"):
                    files = Directory.GetFiles(path, "*.json");
                    BinaryReader binaryReaderForJson;
                    for (int i = 0; i < files.Length; i++)
                    {
                        binaryReaderForJson = new BinaryReader(File.OpenRead(files[i]));
                        string jsonString = "";
                        while (binaryReaderForJson.PeekChar() > -1)
                        {
                            jsonString = jsonString + binaryReaderForJson.ReadString();
                        }
                        if (jsonString.Length > 2)
                        {
                            LinkedList<DllFileInfo> newObjects = JsonConvert.DeserializeObject<LinkedList<DllFileInfo>>(jsonString);
                            dataBaseObjects = dataBaseObjects.Union(newObjects).ToList();
                        }
                        binaryReaderForJson.Close();
                    }
                    break;
                case ("bin"):
                    files = Directory.GetFiles(path, "*.bin");
                    BinaryReader binaryReader;
                    for (int i = 0; i < files.Length; i++)
                    {
                        binaryReader = new BinaryReader(File.OpenRead(files[i]));
                        while (binaryReader.PeekChar() > -1)
                        {
                            dataBaseObjects.Add(new AccessInfo(
                                Guid.Parse(binaryReader.ReadString()),//Guid
                                binaryReader.ReadString(),//login
                                binaryReader.ReadString(),//hash
                                binaryReader.ReadString(),//pass
                                binaryReader.ReadString())//email
                            );
                        }
                        binaryReader.Close();
                    }
                    break;
            }
            return dataBaseObjects;
        }

    }
}
