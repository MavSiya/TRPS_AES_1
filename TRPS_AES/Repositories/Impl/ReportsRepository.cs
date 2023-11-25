using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRPS_AES.EF;
using TRPS_AES.Entities;
using TRPS_AES.Repositories.Interfaces;

namespace TRPS_AES.Repositories.Impl
{
    public class ReportsRepository : BaseRepository<Reports>, IReportsRepository 
    {
        internal ReportsRepository(AESContext context)
        : base(context)
        {
        }
    }
}
