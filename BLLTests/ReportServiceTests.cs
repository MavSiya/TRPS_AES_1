using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Services.Impl;
using BLL.Services.Interfaces;
using CLL.Security;
using CLL.Security.Identity;
using Moq;
using TRPS_AES.Entities;
using TRPS_AES.Repositories.Interfaces;
using TRPS_AES.UnitOfWork;
using Assert = Xunit.Assert;




namespace BLLTests
{
    public class ReportServiceTests
    {

        [Fact]
        public void Ctor_InputNull_ThrowArgumentNullException()
        {
            // Arrange
            IUnitOfWork nullUnitOfWork = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new ReportService(nullUnitOfWork));
        }

        [Fact]
       
        public void GetReports_UserIsAdmin_ThrowMethodAccessException()
         {
             // Arrange
             User user = new OrdinaryWorker(2, "testName3","testSurname3", "testPosition3","testPassword3",2);
             SecurityContext.SetUser(user);
             var mockUnitOfWork = new Mock<IUnitOfWork>();
             IReportService reportService = new ReportService(mockUnitOfWork.Object);

             // Act
             // Assert
             Assert.Throws<MethodAccessException>(() => reportService.GetReportDTO(0));
         }

        [Fact]
        public void GetReportsAsAdministrator_ReportFromDAL_CorrectMappingToReportDTO()
        {
            // Arrange
            User user = new Administrator(1, "testName2", "testSurname2", "testPosition2", "testPassword2", 1);
            SecurityContext.SetUser(user);
            var reportService = GetReportService();

            // Act
            var actualReportDto = reportService.GetReportDTO(0).First();

            // Assert
            Assert.True(
                actualReportDto.IdReport == 1
                && actualReportDto.UserId == 1
                && actualReportDto.Date == new DateTime(2023, 12, 7)
                && actualReportDto.ArriveTime == TimeSpan.FromHours(8)
                && actualReportDto.DepartureTime == TimeSpan.FromHours(16)
                );
        }

        [Fact]
        public void GetReportsAsDirector_ReportFromDAL_CorrectMappingToReportDTO()
        {
            // Arrange
            User user = new Director(1, "testName", "testSurname", "testPosition", "testPassword", 1);
            SecurityContext.SetUser(user);
            var reportService = GetReportService();

            // Act
            var actualReportDto = reportService.GetReportDTO(0).First();

            // Assert
            Assert.True(
                actualReportDto.IdReport == 1
                && actualReportDto.UserId == 1
                && actualReportDto.Date == new DateTime(2023, 12, 7)
                && actualReportDto.ArriveTime == TimeSpan.FromHours(8)
                && actualReportDto.DepartureTime == TimeSpan.FromHours(16)
                );
        }

        IReportService GetReportService()
        {
            var mockContext = new Mock<IUnitOfWork>();
            var expectedReport = new Reports() { IdReport = 1, UserId = 1, Date = new DateTime(2023, 12, 7), ArriveTime = TimeSpan.FromHours(8), DepartureTime = TimeSpan.FromHours(16) };
            var mockDbSet = new Mock<IReportsRepository>();
            mockDbSet.Setup(z =>
                z.Find(
                    It.IsAny<Func<Reports, bool>>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
            .Returns(
                    new List<Reports>() { expectedReport }
                    );
            mockContext
                .Setup(context =>
                    context.Reports)
                .Returns(mockDbSet.Object);

            IReportService reportService = new ReportService(mockContext.Object);

            return reportService;
        }

        [Fact]
        public void AddReport_AsAdministrator_SuccessfullyAdded()
        {
            // Arrange
            User user = new Administrator(1, "testName2", "testSurname2", "testPosition2", "testPassword2", 1);
            SecurityContext.SetUser(user);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockReportsRepository = new Mock<IReportsRepository>();

            mockUnitOfWork.Setup(u => u.Reports).Returns(mockReportsRepository.Object);

            IReportService reportService = new ReportService(mockUnitOfWork.Object);

            var reportDto = new ReportsDTO
            {
                IdReport = 2,
                UserId = 1,
                Date = new DateTime(2023, 12, 8),
                ArriveTime = TimeSpan.FromHours(9),
                DepartureTime = TimeSpan.FromHours(17)
            };

            // Act
            reportService.AddReport(reportDto);

            // Assert
            mockReportsRepository.Verify(r => r.Create(It.IsAny<Reports>()), Times.Once);
            mockUnitOfWork.Verify(u => u.Save(), Times.Once);
        }
    }
}
