using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Selfnet.EntityFrameworkCore
{
    public class SelfnetDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...

        public SelfnetDbContext(DbContextOptions<SelfnetDbContext> options) 
            : base(options)
        {

        }

        /// <summary>
        /// struct func
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //this.RegisterModel(modelBuilder);
            //base.OnModelCreating(modelBuilder);
        }

    }
}
