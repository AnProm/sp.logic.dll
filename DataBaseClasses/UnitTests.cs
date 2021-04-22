using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using NUnit.Compatibility;
namespace Logic
{
    public static class UnitTests
    {
       /// <summary>
       /// Тест для проверки корректности файла настройки NHibernate
       /// </summary>
            [TestFixture]
            public static class GenerateSchema_Fixture
            {
                [Test]
                public static void Can_generate_schema()
                {
                DataBaseManager.LoadNhibernateCfg();
                }
            }
                
        }
}
