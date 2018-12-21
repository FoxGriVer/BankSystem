using System;
using System.Collections.Generic;
using System.Text;
using BankSystemModel;
using System.Linq;

namespace CoreRepository
{
    public class RecordCRUD: IRepository
    {
        BankSystemContext context;

        public RecordCRUD(BankSystemContext _context)
        {
            context = _context;
        }

        public RecordCRUD()
        {

        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }        

        public void CreateRecord(Record newRecord)
        {
            context.Records.Add(
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

        //public List<Record> GetAllAmmounts()
        //{
        //    var list = context.Records.ToList();
        //    int[] massOfNames = null;
        //    foreach (Record record in list)
        //    {
        //        massOfNames.Append(record.Ammount);
        //    }

        //    return massOfNames;
        //}

        public List<Record> GetAllRecords()
        {
            var list = context.Records.ToList();
            return list;
        }

        public Record GetRecordById(int? id)
        {
            var record = context.Records.FirstOrDefault(x => x.Id == id);
            return record;
        }

        public void UpdateRecord(Record changedRecord)
        {
            context.Records.Update(changedRecord);
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

        public List<Record> GetPeriod(DateTime startDate, DateTime endDate)
        {
            List<Record> list = context.Records.Where(x => (x.Date > startDate && x.Date < endDate) || (x.Date < startDate && x.Date > endDate)).ToList();
            return list;
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

    }
}
