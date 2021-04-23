using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model
{
    public interface IAsmModel
    {
        public string[] DoOperation(string a, string b, bool mode);
    }
}
