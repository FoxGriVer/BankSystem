using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.ComponentModel;
using BankSystemModel;

namespace WebRepository
{
    public class RecordCRUD: IRepository
    {
        public static BankSystemContext context;

        public RecordCRUD()
        {
            context = new BankSystemContext();
        }
        
        public void Initialize()
        {            
            context = new BankSystemContext();
        }

        public BindingList<Record> GetRecordsForBinding()
        {
            context.Records.Load();
            return context.Records.Local.ToBindingList();
        }        

        public List<Record> GetAllRecords()
        {
            return context.Records.ToList();
        }

        public List<Record> GetPeriod(DateTime startDate, DateTime endDate)
        {
            List<Record> listOfRecordsInPeriod =  context.Records.Where(x => (x.Date > startDate && x.Date < endDate) || (x.Date < startDate && x.Date > endDate)).ToList();
            return listOfRecordsInPeriod;
        }

        public void CreateRecord(Record newRecord)
        {
            Record _newRecord = new Record
            {
                NameOfObject = newRecord.NameOfObject,
                Ammount = newRecord.Ammount,
                IsIncome = newRecord.IsIncome,
                Date = newRecord.Date
            };
            var tempRecord = context.Records.FirstOrDefault(x => x.Id == newRecord.Id);
            if (tempRecord != null)
            {
                context.Records.Add(_newRecord);
            }            
            context.SaveChanges();
        }

        public void DeleteRecord(int? id)
        {
            Record record = context.Records.FirstOrDefault(x => x.Id == id);
            if (record != null)
            {
                context.Records.Remove(record);
                context.SaveChanges();
            }
        }

        public void UpdateRecord(Record updatedRecord)
        {
            Record record = context.Records.FirstOrDefault(x => x.Id == updatedRecord.Id);
            if (record != null)
            {                
                context.SaveChanges();
            }
        }

        public int GetBalanceForPeriod(List<Record> records)
        {
            int recordBalance = 0;
            foreach (Record rec in records)
            {
                if (rec.IsIncome == true)
                    recordBalance += rec.Ammount;
                else
                    recordBalance -= rec.Ammount;
            }
            return recordBalance;
        }

        public int GetBalanceForLastMonth()
        {
            var today = DateTime.Today;
            var lastMonth = today.AddDays(-31);
            return GetBalanceForPeriod(GetPeriod(lastMonth, today));
        }

        public Record GetRecordById(int? _id)
        {
            return context.Records.FirstOrDefault(x => x.Id == _id);
        }

    }
}
