using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLL.Security.Identity
{
    public abstract class User
    {
        public User(int userId, string name, string surname, string password, string position, string userType)
        {
            UserId = userId;
            Name = name;
            Surname = surname;
            Password = password;
            Position = position;
            UserType = userType;
        }
        public int UserId { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Position { get; }
        protected string UserType { get; }
        public string Password { get; }
    }
}
