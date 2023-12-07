using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRPS_AES.Repositories.Interfaces;

namespace TRPS_AES.UnitOfWork
{

    public interface IUnitOfWork : IDisposable
    {
        IReportsRepository Reports { get; }
        IUsersRepository Users { get; }
        ICardsRepository Cards { get; }
        void Save();
    }

}