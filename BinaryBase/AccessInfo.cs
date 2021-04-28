using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{    /// <summary>
     /// Класс для базы данных содержащих информацию по записи о доступе. Логин, Хэш, Пароль и Email.
     /// </summary>
    public class AccessInfo : DataBaseObject
    {
       
        public virtual string Login { get; set; }
        public virtual string Hashcode { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }

        public Guid guid()
        {
            return this.SystemId;
        }
        public AccessInfo() { }
        /// <summary>
        /// Конструктор для объекта информации по записи о доступе. 
        /// </summary>
        /// <param name="Login">Логин доступа</param>
        /// <param name="Hashcode">Хешкод доступа</param>
        /// <param name="Password">Пароль доступа</param>
        /// <param name="Email">Email доступа</param>
        public AccessInfo(string Login, string Hashcode, string Password, string Email)
        {
            this.SystemId = Guid.NewGuid(); this.Login = Login; this.Hashcode = Hashcode; this.Password = Password; this.Email = Email;
        }
        public AccessInfo(Guid guid, string Login, string Hashcode, string Password, string Email)
        {
            this.SystemId = guid; this.Login = Login; this.Hashcode = Hashcode; this.Password = Password; this.Email = Email;
        }
    }
}

