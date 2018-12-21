using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankSystemModel;

namespace BankSystemWebClient.Models
{
    public class ReportResponse
    {
        public int BalanceForMonth { get; set; }
        public int BalanceForPeriod { get; set; }
        public List<Record> RecordsForPeriod { get; set; }
    }
}
