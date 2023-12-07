using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRPS_AES.Entities;
using TRPS_AES.Repositories.Impl;
using TRPS_AES.Repositories.Interfaces;
using TRPS_AES.UnitOfWork;

namespace TRPS_AES.EF
{
    public class EFUnitOfWork
 : IUnitOfWork
    {
        private AESContext db;
        private ReportsRepository reportsRepository;
        private UsersRepository usersRepository;
        private CardsRepository cardsRepository;
        public EFUnitOfWork(DbContextOptions<AESContext> options)
        {
            db = new AESContext(options);
        }
        public IReportsRepository Reports
        {
            get
            {
                if (reportsRepository == null)
                    reportsRepository = new ReportsRepository(db);
                return reportsRepository;
            }
        }
        public IUsersRepository Users
        {
            get
            {
                if (usersRepository == null)
                    usersRepository = new UsersRepository(db);
                return usersRepository;
            }
        }

        public ICardsRepository Cards
        {
            get
            {
                if (cardsRepository == null)
                    cardsRepository = new CardsRepository(db);
                return cardsRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}