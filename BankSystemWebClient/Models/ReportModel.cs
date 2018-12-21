using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankSystemWebClient.Models
{
    public class ReportModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndTime { get; set; }
        public int SumAmmount { get; set; }
    }
}
