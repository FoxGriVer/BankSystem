using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemModel
{
    public class Record
    {
        public int Id { get; set; }
        public string NameOfObject { get; set; }
        public int Ammount { get; set; }
        public DateTime Date { get; set; }
        public bool IsIncome { get; set; }
    }
}
