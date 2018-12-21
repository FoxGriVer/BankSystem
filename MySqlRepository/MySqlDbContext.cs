using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore;
using BankSystemModel;

namespace MySqlRepository
{
    public class MySqlDbContext: DbContext
    {
        public DbSet<Record> RecordsWithDate { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server = 54.36.121.209; Database = banksystemdb; UID = dotcomuser; Password = dotcomuser123; SslMode = none; Convert Zero Datetime = True; Allow Zero Datetime = True");
        }
    }
}
