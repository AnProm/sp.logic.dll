using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
namespace Logic.Model
{
    public interface IDataBaseModel
    {
        string mode { get; set; }

        string path { get; set; }
        ICollection<DataBaseObject> Delete(DataBaseObject objectToDelete);
        ICollection<DataBaseObject> Add(DataBaseObject objectToAdd);
        ICollection<DataBaseObject> Update(DataBaseObject objectToUpdate);
        ICollection<DataBaseObject> Load();
        void Save(string pathToSave);

    }
}