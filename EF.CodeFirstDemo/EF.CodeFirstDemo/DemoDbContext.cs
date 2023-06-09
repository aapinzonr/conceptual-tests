using EF.CodeFirstDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.CodeFirstDemo
{
    public class DemoDbContext : DbContext
    {
        //public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        //{
        //}

        public DbSet<DemoProduct> DemoProduct { get; set; }

        public DbSet<DemoBuy> DemoBuy { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=DemoDb;Trusted_Connection=True;Encrypt=False");
        }
    }
}
