using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLL.Security.Identity
{
    public class Director : User
    {
        public Director(int userId, string name, string surname, string position, string password, int cardId)
            : base(userId, name, surname, position, password, cardId, nameof(Director))
        {
        }
    }
}