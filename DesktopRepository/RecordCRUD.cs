using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using BankSystemModel;

namespace DesktopRepository
{
    public class RecordCRUD: IRepository
    {
        private static MySqlConnection connection { get; set; }
        public List<Record> ListOfRecords { get; set; }

        public RecordCRUD(MySqlConnection _connection)
        {
            //connection = _connection;
            //ListOfRecords = new List<Record>();
        }

        public RecordCRUD() // для 2 лабы
        {
            MysqlDbContext.Initialize();
            connection = MysqlDbContext.ShareConnetction();
            ListOfRecords = new List<Record>();
        }

        //public void Initialize() // Для 3 лабы
        //{
        //    MysqlDbContext.Initialize();
        //    connection = MysqlDbContext.ShareConnetction();
        //    ListOfRecords = new List<Record>();
        //}

        public List<Record> GetAllRecords()
        {
            //connection = MysqlDbContext.ShareConnetction();
            //Initialize();
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `Records`", connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListOfRecords.Add(new Record()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    NameOfObject = reader["NameOfObject"].ToString(),
                    IsIncome = Convert.ToBoolean(reader["IsIncome"]),
                    Date = DateTime.ParseExact(reader["Date"].ToString(), "yyyy-d-M", System.Globalization.CultureInfo.InvariantCulture),
                    Ammount = Convert.ToInt32(reader["Ammount"])
                });
            }
            connection.Close();
            return ListOfRecords;
        }

        public List<Record> GetPeriod(DateTime startDate, DateTime endDate)
        {
            List<Record> listOfRecordsInPeriod = GetAllRecords().Where(x => (x.Date > startDate && x.Date < endDate) || (x.Date < startDate && x.Date > endDate)).ToList();
            return listOfRecordsInPeriod;
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

        public void DeleteRecord(int? id)
        {
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM `Records` WHERE `Records`.`Id` = " + id + " ", connection);
            var ans = cmd.ExecuteNonQuery();
            connection.Close();
            //return Convert.ToBoolean(ans);
        }

        public void UpdateRecord(Record _updatedRecord)
        {
            connection.Open();
            string dateString = "" + _updatedRecord.Date.Year + "-" + _updatedRecord.Date.Day + "-" + _updatedRecord.Date.Month + "";
            MySqlCommand cmd = new MySqlCommand("UPDATE `banksystemdb`.`Records` SET `NameOfObject` = '" + _updatedRecord.NameOfObject + "', `Ammount` = '" + _updatedRecord.Ammount + "', `Date` = '" + dateString + "', `IsIncome` = " + _updatedRecord.IsIncome + " WHERE `Records`.`Id` = '" + _updatedRecord.Id + "';", connection);
            var ans = cmd.ExecuteNonQuery();
            connection.Close();
            //return Convert.ToBoolean(ans);
        }

        public void CreateRecord(Record _newRecord)
        {
            connection.Open();
            string dateString = "" + _newRecord.Date.Year + "-" + _newRecord.Date.Day + "-" + _newRecord.Date.Month + "";
            MySqlCommand cmd = new MySqlCommand("INSERT INTO `Records` (`Id`, `NameOfObject`, `Ammount`,`IsIncome`, `Date` ) VALUES (NULL, '" + _newRecord.NameOfObject + "', '" + _newRecord.Ammount + "', " + _newRecord.IsIncome + ", '" + dateString + "');", connection);
            var ans = cmd.ExecuteNonQuery();
            connection.Close();
        }

        public Record GetRecordById(int? _id)
        {
            throw new NotImplementedException();
        }

    }
}
