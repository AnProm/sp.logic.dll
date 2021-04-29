using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
namespace Logic
{
    public class HybernateHelper
    { 
    
        public const string FIRST_PART = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =";
        public const string SECOND_PART = ";Integrated Security=True";
        private static ISessionFactory _sessionFactory;
        static Configuration cfg;
        /// <summary>
        /// Создают фабрику для сессий используя файл настройки библиотеки NHibernate
        /// </summary>

        private static ISessionFactory SessionFactory(string filePath)
            {
                if (_sessionFactory == null)
                {
                    cfg = new Configuration();
                    string secondPart = ";Integrated Security=True";
                    string Result = FIRST_PART + filePath + SECOND_PART;
                    cfg.SetProperty("connection.connection_string",Result);
                   
                    cfg.Configure();
                    cfg.AddAssembly(typeof(DataBaseObject).Assembly);
                    _sessionFactory = cfg.BuildSessionFactory();
                
                }
                return _sessionFactory;
            
        }
        /// <summary>
        /// Открывает сессию, однопоточный объект на время работы программы, работающий как фабрика для объектов транзакций
        /// </summary>
        /// <returns></returns>
        public static ISession OpenSession(string filepath)
        {
            return SessionFactory(filepath).OpenSession();
        }
        
        public static void CloseSession(string filepath)
        {
            
            _sessionFactory.Close();
            _sessionFactory.Dispose();
            cfg = null;
            _sessionFactory = null;
        }
    }
}
