using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model
{
    interface IDataBase
    {
        string mode { get; set; }
        void Delete(string path, DataBaseObject objectToDelete);
        void Add(string path, DataBaseObject objectToAdd);
        void Update(string path, DataBaseObject objectToUpdate);
        ICollection<DataBaseObject> Load(string path);
        void Save(string path);
    }
}
