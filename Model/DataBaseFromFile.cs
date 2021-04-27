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

        public void Add(string path, DataBaseObject objectToAdd)
        {
            switch (mode)
            {
                case ("json"):

                    break;

                case ("bin"):

                    break;

            }
            throw new NotImplementedException();
        }

        public void Delete(string path, DataBaseObject objectToDelete)
        {
            switch (mode)
            {
                case ("json"):

                    break;

                case ("bin"):

                    break;

            }
            throw new NotImplementedException();
        }

        public ICollection<DataBaseObject> Load(string path)
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

        public void Save(string path)
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
                    string newFileName = path + "\\prograDB-" + dataBaseObjects.GetHashCode().ToString() + ".bin";
                    File.Create(newFileName);
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

        public void Update(string path, DataBaseObject objectToUpdate)
        {
            
            switch (mode)
            {
                case ("json"):

                    break;

                case ("bin"):
                    
                    break;

            }
            throw new NotImplementedException();
        }
    }
}
