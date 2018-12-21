using System;
using System.Collections.Generic;
using System.Text;
using BankSystemModel;
using System.Linq;

namespace MySqlRepository
{
    public class RecordCRUD: IRepository
    {
        MySqlDbContext context;

        public RecordCRUD(MySqlDbContext _context)
        {
            context = _context;
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void CreateRecord(Record newRecord)
        {
            context.RecordsWithDate.Add(
                    new Record
                    {
                        NameOfObject = newRecord.NameOfObject,
                        Ammount = newRecord.Ammount,
                        IsIncome = newRecord.IsIncome,
                        Date = newRecord.Date
                    }
                );
            context.SaveChanges();
        }

        public List<Record> GetAllRecords()
        {
            var list = context.RecordsWithDate.ToList();
            return list;
        }

        public Record GetRecordById(int? id)
        {
            var record = context.RecordsWithDate.FirstOrDefault(x => x.Id == id);
            return record;
        }

        public void UpdateRecord(Record changedRecord)
        {
            context.RecordsWithDate.Update(changedRecord);
            context.SaveChanges();
        }

        public void DeleteRecord(int? id)
        {
            Record record = context.RecordsWithDate.FirstOrDefault(x => x.Id == id);
            if (record != null)
            {
                context.RecordsWithDate.Remove(record);
                context.SaveChanges();
            }
        }

        public List<Record> GetPeriod(DateTime startDate, DateTime endDate)
        {
            List<Record> list = context.RecordsWithDate.Where(x => (x.Date > startDate && x.Date < endDate) || (x.Date < startDate && x.Date > endDate)).ToList();
            return list;
        }

        public int GetBalanceForPeriod(List<Record> RecordsWithDate)
        {
            int recordBalance = 0;
            foreach (Record rec in RecordsWithDate)
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
    }
}
