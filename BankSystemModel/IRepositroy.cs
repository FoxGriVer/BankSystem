using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemModel
{
    public interface IRepository
    {
        void CreateRecord(Record record);
        void DeleteRecord(int? id);
        void UpdateRecord(Record record);
        Record GetRecordById(int? id);
        List<Record> GetAllRecords();
        List<Record> GetPeriod(DateTime startDate, DateTime endDate);
        int GetBalanceForPeriod(List<Record> listOfRecords);
        int GetBalanceForLastMonth();
        //void Initialize();        
    }
}
