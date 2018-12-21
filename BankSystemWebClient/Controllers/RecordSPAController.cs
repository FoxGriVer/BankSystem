using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankSystemModel;
using BankSystemWebClient.Models;

namespace BankSystemWebClient.Controllers
{
    [Produces("application/json")]
    [Route("api/RecordSPA")]
    public class RecordSPAController : Controller
    {
        private IRepository repository;

        public RecordSPAController(IRepository _repository)
        {
            repository = _repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IEnumerable<Record> GetRecords()
        {
            return repository.GetAllRecords();
        }

        [HttpPost]
        public IActionResult AddRecord([FromBody]Record newRecord)
        {
            repository.CreateRecord(newRecord);
            return Ok();
        }

        [HttpGet("GetRecord/{id}")]
        public IActionResult GetRecordById(int? id)
        {
            var record = repository.GetRecordById(id);
            if (record != null)
            {
                return Ok(record);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRecord([FromBody]Record updatedRecord)
        {
            repository.UpdateRecord(updatedRecord);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if(id != null)
            {
                repository.DeleteRecord(id);
                return Ok();
            }
            return NotFound();
        }

        [HttpPost("GetFiltredRecords")]
        public ReportResponse GetFiltredRecords([FromBody] DatesForFilter datesForFilter)
        {
            ReportResponse reportResponse = new ReportResponse();
            reportResponse.RecordsForPeriod = repository.GetPeriod(datesForFilter.StartDate, datesForFilter.EndDate);
            reportResponse.BalanceForMonth = repository.GetBalanceForLastMonth();
            reportResponse.BalanceForPeriod = repository.GetBalanceForPeriod(reportResponse.RecordsForPeriod);
            return reportResponse;
        }

        [HttpGet("GetFiltredRecords")]
        public IActionResult GetFiltredRecords()
        {
            return Ok();
        }

    }
}