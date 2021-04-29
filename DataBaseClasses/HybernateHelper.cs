﻿using System;
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
        private static ISessionFactory _sessionFactory;
        
        /// <summary>
        /// Создают фабрику для сессий используя файл настройки библиотеки NHibernate
        /// </summary>
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var cfg = new Configuration();
                    cfg.Configure();
                    cfg.AddAssembly(typeof(DataBaseObject).Assembly);
                    _sessionFactory = cfg.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }
        /// <summary>
        /// Открывает сессию, однопоточный объект на время работы программы, работающий как фабрика для объектов транзакций
        /// </summary>
        /// <returns></returns>
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
        
        public static void CloseSession()
        {
            SessionFactory.Close();
        }
    }
}