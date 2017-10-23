using DAL.Abstract;
using DAL.Entity;
using DAL.Entity.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace DAL
{
    public class EFContext : IdentityDbContext<AppUser>, IEFContext
    {
        public EFContext() : base("HotelDB")
        {
            Database.SetInitializer<EFContext>(null);
        }

        public static EFContext Create()
        {
            return new EFContext();
        }

        public EFContext(string connString) : base(connString)
        {
            Database.SetInitializer<EFContext>(new DBInitializer());
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        #region User Security
        //public DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
        #endregion

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}