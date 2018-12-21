using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BankSystemModel;


namespace CoreRepository.Tests
{
    public class CoreRepoTests
    {
        private BankSystemContext context = new BankSystemContext();
        private MySqlRepository.MySqlDbContext mysqlContext = new MySqlRepository.MySqlDbContext();

        private List<BankSystemModel.Record> GetTestRecords()
        {
            var records = new List<BankSystemModel.Record>
            {
                new BankSystemModel.Record { Id = 1, NameOfObject = "TestObject1", Ammount = 111, Date = DateTime.Now.Date, IsIncome = true },
                new BankSystemModel.Record { Id = 2, NameOfObject = "TestObject2", Ammount = 222, Date = DateTime.Now.Date, IsIncome = true },
                new BankSystemModel.Record { Id = 3, NameOfObject = "TestObject3", Ammount = 333, Date = DateTime.Now.Date, IsIncome = true }
            };
            return records;
        }

        [Fact]
        public void CreateRecordWithIncomeOperation()
        {
            var testRecord = GetTestRecords()[0];
            var repo = new RecordCRUD(context);

            repo.CreateRecord(testRecord);

            Assert.NotNull(testRecord);
            Assert.Equal("TestObject1",testRecord.NameOfObject);
            Assert.True(testRecord.IsIncome);
        }

        [Fact]
        public void GetAllRecordsIsNotEmptyAndReturnsListOfRecords()
        {
            var repo = new RecordCRUD(context);
            var testRecord = GetTestRecords()[2];

            var result = repo.GetAllRecords();

            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<BankSystemModel.Record>>(result);
            Assert.DoesNotContain<BankSystemModel.Record>(testRecord, result);
        }

        [Fact]
        public void GetRecordByIdIfIdDoesNotExist()
        {
            var repo = new RecordCRUD(context);

            var result = repo.GetRecordById(null);

            Assert.Null(result);
        }

        [Fact]
        public void GetRecordByIdIfIdExists()
        {
            var repo = new RecordCRUD(context);
            var testRecord = repo.GetAllRecords()[0];

            var result = repo.GetRecordById(testRecord.Id);

            Assert.IsNotType<string>(result);
            Assert.IsType<BankSystemModel.Record>(result);
            Assert.Same(testRecord, result);
        }

        [Fact]
        public void GetPeriodContainsAPartOfAllRecords()
        {
            var repo = new RecordCRUD(context);
            var allRecords = repo.GetAllRecords();
            var testStartDate = new DateTime(2017, 01, 01);
            var testEndDate = new DateTime(2018, 12, 12);

            var result = repo.GetPeriod(testStartDate, testEndDate);

            foreach (var res in result)
            {
                Assert.Contains<BankSystemModel.Record>(res, allRecords);
            }                        
        }

        [Fact]
        public void GetBalanceForPeriodReturnsInt()
        {
            var repo = new RecordCRUD(context);
            var testStartDate = new DateTime(2017, 01, 01);
            var testEndDate = new DateTime(2018, 12, 12);       
            var recordsForThePeriod = repo.GetPeriod(testStartDate, testEndDate);

            var result = repo.GetBalanceForPeriod(recordsForThePeriod);

            Assert.IsNotType<BankSystemModel.Record>(result);
            Assert.IsType<int>(result);
            //Assert.Equal(-4246, result); //баланс за данный период отрицательный
        }

        [Fact]
        public void MySqlCreateRecordWithIncomeOperation()
        {
            var testRecord = GetTestRecords()[0];
            var repo = new MySqlRepository.RecordCRUD(mysqlContext);

            repo.CreateRecord(testRecord);

            Assert.NotNull(testRecord);
            Assert.Equal("TestObject1", testRecord.NameOfObject);
            Assert.True(testRecord.IsIncome);
        }

        [Fact]
        public void MySqlGetRecordByIdIfIdExists()
        {
            var repo = new MySqlRepository.RecordCRUD(mysqlContext);
            var testRecord = repo.GetAllRecords()[0];

            var result = repo.GetRecordById(testRecord.Id);

            Assert.IsNotType<string>(result);
            Assert.IsType<BankSystemModel.Record>(result);
            Assert.Same(testRecord, result);
        }

        [Fact]
        public void MySqlGetPeriodContainsAPartOfAllRecords()
        {
            var repo = new MySqlRepository.RecordCRUD(mysqlContext);
            var allRecords = repo.GetAllRecords();
            var testStartDate = new DateTime(2017, 01, 01);
            var testEndDate = new DateTime(2018, 12, 12);

            var result = repo.GetPeriod(testStartDate, testEndDate);

            foreach (var res in result)
            {
                Assert.Contains<BankSystemModel.Record>(res, allRecords);
            }
        }

        [Fact]
        public void MySqlGetAllRecordsIsNotEmptyAndReturnsListOfRecords()
        {
            var repo = new MySqlRepository.RecordCRUD(mysqlContext);
            var testRecord = GetTestRecords()[2];

            var result = repo.GetAllRecords();

            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<BankSystemModel.Record>>(result);
            Assert.DoesNotContain<BankSystemModel.Record>(testRecord, result);
        }

    }
}
