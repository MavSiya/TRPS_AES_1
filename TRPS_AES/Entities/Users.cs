using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPS_AES.Entities
{
     class Users
    {
        public int IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Director { get; set; }
        public string Position { get; set; }
        public string Password { get; set; }
        public string CardId { get; set; }
    }
}
