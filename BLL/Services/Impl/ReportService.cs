using AutoMapper;
using BLL.DTO;
using BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRPS_AES.Entities;
using TRPS_AES.UnitOfWork;
using CLL.Security;
using CLL.Security.Identity;


namespace BLL.Services.Impl
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _database;
        private int pageSize = 10;

        public ReportService(IUnitOfWork unitOfWork)
        {
            if(unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }
            _database = unitOfWork;
        }

        /// <exception cref="MethodAccessException"></exception>
        public IEnumerable<ReportsDTO> GetReportDTO(int pageNumber)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Director) && userType != typeof(Administrator))
            {
                throw new MethodAccessException();
            }
            var userId = user.UserId;
            var reportsEntities = _database.Reports
                .Find(z => z.UserId == userId, pageNumber, pageSize); 
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Reports, ReportsDTO>());
            var mapper = new Mapper(mapperConfig);
            var reportsDto = mapper.Map<IEnumerable<Reports>, List<ReportsDTO>>(reportsEntities);
            return reportsDto;
        }

        public void AddReport(ReportsDTO reportDto)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();

            if (userType != typeof(Director) && userType != typeof(Administrator))
            {
                throw new MethodAccessException();
            }

            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ReportsDTO, Reports>());
            var mapper = new Mapper(mapperConfig);
            var reportEntity = mapper.Map<ReportsDTO, Reports>(reportDto);

            
            reportEntity.UserId = user.UserId;

            
            _database.Reports.Create(reportEntity);
            _database.Save();
        }
    }
}

