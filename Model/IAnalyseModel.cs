using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model
{
    public interface IAnalyseModel
    {
        public string getResult(string inpString, bool mode);
    }
}
