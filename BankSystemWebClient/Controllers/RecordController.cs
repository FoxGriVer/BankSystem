using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankSystemModel;
using DesktopRepository;
using BankSystemWebClient.Models;

namespace BankSystemWebClient.Controllers
{
    public class RecordController : Controller
    {
        private IRepository repository;

        public RecordController(IRepository _repository)
        {
            repository = _repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewAll()
        {
            return View(repository.GetAllRecords());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Record createdRecord)
        {
            repository.CreateRecord(createdRecord);
            return View("~/Views/Record/AddPost.cshtml");
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            var record = repository.GetRecordById(id);
            if (record != null)
            {
                return View(record);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Update(Record changedRecord)
        {
            repository.UpdateRecord(changedRecord);
            return RedirectToAction("ViewAll");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                repository.DeleteRecord(id);
                return RedirectToAction("ViewAll");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult RepresentReport()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RepresentReport(ReportModel reportModel)
        {
            var reportsInPeriod = repository.GetPeriod(reportModel.StartDate, reportModel.EndTime);
            ViewBag.BalanceForPeriod = repository.GetBalanceForPeriod(reportsInPeriod);
            ViewBag.BalanceForMonth = repository.GetBalanceForLastMonth();
            return View("~/Views/Record/RepresentReportList.cshtml", reportsInPeriod);
        }
    }
}