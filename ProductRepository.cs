using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;

namespace Logic
{
    public class ProductRepository
    {
        public void Add(Product product)
        {
            using(ISession session = HybernateHelper.OpenSession())
            {
                using(ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(product);
                    transaction.Commit();
                }
            }
        }
        public void Update(Product product)
        {

            using (ISession session = HybernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(product);
                    transaction.Commit();
                }
            }
        }
        public void Delete(Product product)
        {

            using (ISession session = HybernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(product);
                    transaction.Commit();
                }
            }
        }
        public Product GetFirstByName(string name)
        {
            using (ISession session = HybernateHelper.OpenSession())
            {
                var queryResult = session.QueryOver<Product>().Where(x => x.Name == name).SingleOrDefault();
                Console.ReadLine();
                return queryResult ?? null;
            }
        }
    }
}
