using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace Logic
{
    /// <summary>
    /// Класс, содержащий методы и классы для работы с данными
    /// </summary>
    public class DataBaseManager
    {
        public static void LoadNhibernateCfg()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(Product).Assembly);

            new SchemaExport(cfg).Execute(true, true, false);
        }
    }

}

