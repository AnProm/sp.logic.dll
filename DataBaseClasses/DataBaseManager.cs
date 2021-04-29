
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
        public static void LoadNhibernateCfg(string filePath)
        {
            string firstPart = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =";
            string secondPart = ";Integrated Security=True";
            string Result = firstPart + filePath + secondPart;
            var cfg = new Configuration();
            cfg.SetProperty("connection.connection_string", Result);
            cfg.Configure();
            cfg.AddAssembly(typeof(DataBaseObject).Assembly);

            new SchemaExport(cfg).Execute(true, true, false);
        }
    }

}