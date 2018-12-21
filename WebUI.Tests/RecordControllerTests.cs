using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankSystemWebClient.Controllers;
using BankSystemWebClient.Models;
using BankSystemModel;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Tests
{
    public class RecordControllerTests
    {

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

        private ReportModel GetTestReportModel()
        {
            return new ReportModel { EndTime = DateTime.Now.Date, StartDate = DateTime.Now.Date, SumAmmount = 12000 };
        }

        [Fact]
        public void IndexReturnsAViewResult()
        {
            var mock = new Mock<IRepository>();
            var controller = new RecordController(mock.Object);

            var result = controller.Index();

            Assert.IsNotType<EmptyResult>(result); // проверяет что возврощается не объект типа EmptyResult
        }

        [Fact]
        public void ViewAllReturnsAViewResultWithAListOfRecords()
        {
            //arrange 
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.GetAllRecords()).Returns(GetTestRecords());
            var controller = new RecordController(mock.Object);

            //act
            var result = controller.ViewAll();

            //assert
            var viewResult = Assert.IsType<ViewResult>(result); // является ли возврощаемый объект типа ViewResult
            var model = Assert.IsAssignableFrom<IEnumerable<BankSystemModel.Record>>(viewResult.Model); // передается ли в представление в качестве модели объект IEnumerable<Record>
            Assert.NotEmpty(model);
            Assert.Equal(GetTestRecords().Count, model.Count()); // проверяется кол-во передаваемых объектов в модель представления
        }

        [Fact]
        public void AddGetReturnsAViewResult()
        {
            var mock = new Mock<IRepository>();
            var controller = new RecordController(mock.Object);

            var result = controller.Add();

            Assert.IsType<ViewResult>(result); // проверяет что возврощается не объект типа EmptyResult
        }

        [Fact]
        public void AddPostRequestAddRecord()
        {
            var mock = new Mock<IRepository>();
            var controller = new RecordController(mock.Object);
            var newRecord = GetTestRecords()[0];

            var result = controller.Add(newRecord);

            mock.Verify(repo => repo.CreateRecord(newRecord)); // проверяет, что данный метод запускается в репозиторие
        }

        [Fact]
        public void UpdateGetReturnsNotFoundWhenRecordNotFound() // проверяет когда такого id несущетсвует
        {
            int testRecordId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.GetRecordById(testRecordId)).Returns(null as BankSystemModel.Record);
            var controller = new RecordController(mock.Object);

            var result = controller.Update(testRecordId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void UpdateGetReturnsViewResultWithRecord() // проверяет, что при обновлении записи, данная запись передается в view 
        {
            int testRecordId = 2;
            var testRecord = GetTestRecords().FirstOrDefault(x => x.Id == testRecordId);
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.GetRecordById(testRecordId)).Returns(testRecord);
            var controller = new RecordController(mock.Object);

            var result = controller.Update(testRecordId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<BankSystemModel.Record>(viewResult.ViewData.Model); // проверка, что переданная записиь данного типа
            Assert.Equal(testRecord.Id, model.Id);
            Assert.Equal(testRecord.NameOfObject, model.NameOfObject);
            Assert.Equal(testRecord.Ammount, model.Ammount);
        }

        [Fact]
        public void UpdatePostRedirectsToAnontherAction()
        {
            int testRecordId = 2;
            var testRecord = GetTestRecords().FirstOrDefault(x => x.Id == testRecordId);
            var mock = new Mock<IRepository>();
            var controller = new RecordController(mock.Object);

            var result = controller.Update(testRecord);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ViewAll", redirectToActionResult.ActionName);
            mock.Verify(repo => repo.UpdateRecord(testRecord));
        }

        [Fact]
        public void RepresentReportPostShowsBalanceAndReturnResultWithRecords()
        {
            var testReportModel = GetTestReportModel();
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.GetPeriod(testReportModel.StartDate, testReportModel.EndTime)).Returns(GetTestRecords());
            var controller = new RecordController(mock.Object);

            var result = controller.RepresentReport(testReportModel);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<BankSystemModel.Record>>(viewResult.Model);
            mock.Verify(repo => repo.GetPeriod(testReportModel.StartDate, testReportModel.EndTime));
            mock.Verify(repo => repo.GetBalanceForLastMonth());
            Assert.Contains("RepresentReportList", viewResult.ViewName); // проерка на какое представление переходит
        }

    }
}
