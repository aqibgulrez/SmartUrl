using Microsoft.EntityFrameworkCore;
using SmartUrl.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUrl.Repository.SQL
{
    public class SQLApiContext : DbContext
    {
        public SQLApiContext(DbContextOptions<SQLApiContext> options) : base(options)
        {

        }

        public DbSet<SmartUrlEntity> SmartUrl { get; set; }

  
    }
}
