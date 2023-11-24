using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRPS_AES.Entities;

namespace TRPS_AES.EF
{
     class AESContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Cards> Cards { get; set; }
        public DbSet<Reports> Reports { get; set; }

        public AESContext(DbContextOptions<AESContext> options) : base(options)
        {
        }

    }
}
