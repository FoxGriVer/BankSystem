using System;
using System.Collections.Generic;
using System.Text;
using BankSystemModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CoreRepository
{
    public class BankSystemContext: DbContext
    {
        public DbSet<Record> Records { get; set; }

        //для сервиса внедрения
        //public BankSystemContext(DbContextOptions<BankSystemContext> options) : base(options)
        //{
        //    Database.EnsureCreated();
        //}

        public BankSystemContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BankSystemDb;Trusted_Connection=True;");
        }

    }
}
