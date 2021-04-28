﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Logic.Model
{
    class DataBaseFromFile : IDataBaseModel
    {
        public string mode { get; set; }
        public string path { get; set; }

        public ICollection<DataBaseObject> Add(DataBaseObject objectToAdd)
        {
            switch (mode)
            {
                case ("json"):

                    break;

                case ("bin"):
                    string newFileName = path + "\\newRecordDB-" + objectToAdd.GetHashCode().ToString() + ".bin";
                    BinaryWriter binaryWriter = new BinaryWriter(File.Create(newFileName));
                    AccessInfo writableObj = (AccessInfo)objectToAdd;
                    binaryWriter.Write(writableObj.guid().ToString());
                    binaryWriter.Write(writableObj.Login);
                    binaryWriter.Write(writableObj.Hashcode);
                    binaryWriter.Write(writableObj.Password);
                    binaryWriter.Write(writableObj.Email);
                    binaryWriter.Close();
                    break;
            }
            return LoadAll();
        }

        public ICollection<DataBaseObject> Delete(DataBaseObject objectToDelete)
        {
            string[] files;
            switch (mode)
            {
                case ("json"):

                    break;

                case ("bin"):
                    files = Directory.GetFiles(path, "*.bin");
                    BinaryReader binaryReader;
                    bool rewrite = false;
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
                            if (objectToDelete.Equals(currentInfo)){rewrite = true;}
                            else { rewriteCollection.Add(currentInfo); }
                        }
                        binaryReader.Close();
                        if (rewrite)
                        {
                            BinaryWriter binaryWriter = new BinaryWriter(File.Create(files[i]));
                            for (int j = 0; j < rewriteCollection.Count(); j++)
                            {
                                AccessInfo accessInfo = (AccessInfo)rewriteCollection.ElementAt(j);
                                binaryWriter.Write(accessInfo.guid().ToString());
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

        public ICollection<DataBaseObject> Load()
        {
            return LoadAll();
        }

        public void Save(string newPath)
        {
            string[] files;
            ICollection<DataBaseObject> dataBaseObjects = new LinkedList<DataBaseObject>();
            switch (mode)
            {
                case ("json"):

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
                    string newFileName = newPath + "\\prograDB-" + dataBaseObjects.GetHashCode().ToString() + ".bin";
                    BinaryWriter binaryWriter = new BinaryWriter(File.Create(newFileName));
                    for (int i = 0; i < dataBaseObjects.Count(); i++)
                    {
                        AccessInfo accessInfo = (AccessInfo) dataBaseObjects.ElementAt(i);
                        binaryWriter.Write(accessInfo.guid().ToString());
                        binaryWriter.Write(accessInfo.Login);
                        binaryWriter.Write(accessInfo.Hashcode);
                        binaryWriter.Write(accessInfo.Password);
                        binaryWriter.Write(accessInfo.Email);
                    }
                    binaryWriter.Close();
                    break;
            }
        }

        public ICollection<DataBaseObject> Update(DataBaseObject objectToUpdate)
        {
            string[] files;
            switch (mode)
            {
                case ("json"):

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
                            
                            if (objectToUpdateBin.guid().Equals(currentInfo.guid()))
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
                                binaryWriter.Write(accessInfo.guid().ToString());
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
