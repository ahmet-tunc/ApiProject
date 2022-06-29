using CaseStudy.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.DataAccess.Concrete
{
    public class AppDbContext:DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseOracle(@"Data Source=THEUATDB;User ID=AHMET.TUNC;Password=Ahmt789;");
        //    //base.OnConfiguring(optionsBuilder);
        //}


        public DbSet<Log> Logs { get; set; }
    }
}
