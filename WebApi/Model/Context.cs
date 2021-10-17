using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model
{
    public class Context:DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-PUNH1KOK\SQLEXPRESS;Database=Webapi;Trusted_Connection=True;");
        }

        public DbSet<Person> people{ get; set; }

    }
}
