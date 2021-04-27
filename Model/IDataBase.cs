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
        string path { get; set; }
        void Delete(DataBaseObject objectToDelete);
        void Add(DataBaseObject objectToAdd);
        void Update(DataBaseObject objectToUpdate);
        ICollection<DataBaseObject> Load();
        void Save(string path);
    }
}
