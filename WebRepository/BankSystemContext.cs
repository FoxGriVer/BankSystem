using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using BankSystemModel;

namespace WebRepository
{
    public class BankSystemContext: DbContext
    {
        public DbSet<Record> Records { get; set; }

        public BankSystemContext() : base("DefaultConnection")
        {

        }
    }
}
