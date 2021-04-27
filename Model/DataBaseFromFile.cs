using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Logic.Model
{
    class DataBaseFromFile : IDataBase
    {
        public string mode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string path { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Add(DataBaseObject objectToAdd)
        {
            switch (mode)
            {
                case ("json"):

                    break;

                case ("bin"):
                    string newFileName = path + "\\newRecordDB-" + objectToAdd.GetHashCode().ToString() + ".bin";
                    BinaryWriter binaryWriter = new BinaryWriter(File.Create(newFileName));
                    AccessInfo writableObj = (AccessInfo)objectToAdd;
                    binaryWriter.Write(writableObj.Login);
                    binaryWriter.Write(writableObj.Hashcode);
                    binaryWriter.Write(writableObj.Password);
                    binaryWriter.Write(writableObj.Email);
                    break;
            }
        }

        public void Delete(DataBaseObject objectToDelete)
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
                                binaryReader.ReadString(),//login
                                binaryReader.ReadString(),//hash
                                binaryReader.ReadString(),//pass
                                binaryReader.ReadString());//email
                            if (objectToDelete.Equals(currentInfo)){rewrite = true;}
                            else { rewriteCollection.Add(currentInfo); }
                        }
                        if (rewrite)
                        {
                            BinaryWriter binaryWriter = new BinaryWriter(File.Create(files[i]));
                            for (int j = 0; j < rewriteCollection.Count(); j++)
                            {
                                AccessInfo accessInfo = (AccessInfo)rewriteCollection.ElementAt(j);
                                binaryWriter.Write(accessInfo.Login);
                                binaryWriter.Write(accessInfo.Hashcode);
                                binaryWriter.Write(accessInfo.Password);
                                binaryWriter.Write(accessInfo.Email);
                            }
                            break;
                        }
                    }
                    break;

            }
            throw new NotImplementedException();
        }

        public ICollection<DataBaseObject> Load()
        {
            string[] files;
            ICollection<DataBaseObject> dataBaseObjects = new LinkedList<DataBaseObject>();
            switch (mode)
            {
                case ("json"):
                    files = Directory.GetFiles(path, "*.json");
                    


                    break;
                case ("bin"):
                    files = Directory.GetFiles(path, "*.bin");
                    BinaryReader binaryReader;
                    for (int i =0; i<files.Length; i++)
                    {
                        binaryReader = new BinaryReader(File.OpenRead(files[i]));
                        while(binaryReader.PeekChar() > -1)
                        {
                            dataBaseObjects.Add(new AccessInfo(
                                binaryReader.ReadString(),//login
                                binaryReader.ReadString(),//hash
                                binaryReader.ReadString(),//pass
                                binaryReader.ReadString())//email
                            );
                        }
                    }
                    break;
            }
            return dataBaseObjects;
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
                                binaryReader.ReadString(),//login
                                binaryReader.ReadString(),//hash
                                binaryReader.ReadString(),//pass
                                binaryReader.ReadString())//email
                            );
                        }
                    }
                    string newFileName = newPath + "\\prograDB-" + dataBaseObjects.GetHashCode().ToString() + ".bin";
                    BinaryWriter binaryWriter = new BinaryWriter(File.Create(newFileName));
                    for (int i = 0; i < dataBaseObjects.Count(); i++)
                    {
                        AccessInfo accessInfo = (AccessInfo) dataBaseObjects.ElementAt(i);
                        binaryWriter.Write(accessInfo.Login);
                        binaryWriter.Write(accessInfo.Hashcode);
                        binaryWriter.Write(accessInfo.Password);
                        binaryWriter.Write(accessInfo.Email);
                    }
                    break;
            }
        }

        public void Update(DataBaseObject objectToUpdate)
        {
            string[] files;
            switch (mode)
            {
                case ("json"):

                    break;

                case ("bin"):
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
                                binaryReader.ReadString(),//login
                                binaryReader.ReadString(),//hash
                                binaryReader.ReadString(),//pass
                                binaryReader.ReadString());//email
                            
                            if (objectToUpdate.Equals(currentInfo))
                            { updateFile = true; }
                            rewriteCollection.Add(currentInfo);
                        }
                        if (updateFile)
                        {
                            BinaryWriter binaryWriter = new BinaryWriter(File.Create(files[i]));
                            for (int j = 0; j < rewriteCollection.Count(); j++)
                            {
                                AccessInfo accessInfo = (AccessInfo)rewriteCollection.ElementAt(j);
                                binaryWriter.Write(accessInfo.Login);
                                binaryWriter.Write(accessInfo.Hashcode);
                                binaryWriter.Write(accessInfo.Password);
                                binaryWriter.Write(accessInfo.Email);
                            }
                            break;
                        }
                    }
                    break;

            }
        }
    }
}
