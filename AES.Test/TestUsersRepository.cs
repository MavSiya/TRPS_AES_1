using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRPS_AES.Entities;
using TRPS_AES.Repositories.Impl;

namespace AES.Test
{
    public class TestUsersRepository
    : BaseRepository<Users>
    {
        public TestUsersRepository(DbContext context)
        : base(context)
        {
        }
    }

}