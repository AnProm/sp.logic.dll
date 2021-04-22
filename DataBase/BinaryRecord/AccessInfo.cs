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
    }
}

