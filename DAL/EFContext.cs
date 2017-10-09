﻿using DAL.Abstract;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace DAL
{
    public class EFContext : DbContext, IEFContext
    {
        public EFContext() : base("ConnectionHotel")
        {
            Database.SetInitializer<EFContext>(null);
        }

        public EFContext(string connString) : base(connString)
        {
            Database.SetInitializer<EFContext>(new DBInitializer());
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}