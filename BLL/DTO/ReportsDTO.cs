using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ReportsDTO
    {
        public int IdReport { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan ArriveTime { get; set; }
        public TimeSpan DepartureTime { get; set; }
    }
}
