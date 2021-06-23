using FlexateWebApi.Domain.Model;
using FlexateWebApi.Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexateWebApi.Infrastructure
{
    public class Context : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<PersonCar> PersonCar { get; set; }
        public DbSet<PersonOffice> PersonOffice { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
