using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SALEERP.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata;
using Entities.Models;

namespace SALEERP.Data
{
    public class ExportErpDbContext : DbContext
    {
        public ExportErpDbContext(DbContextOptions<ExportErpDbContext> options) : base(options)
        {
        }

        //public DbSet<Term> Term { get; set; }
        //public DbSet<BuyingAgent> BuyingAgent { get; set; }
        //public DbSet<CustomerInfo> CustomerInfo { get; set; }
        //public DbSet<TransMode> TransMode { get; set; }
        //public DbSet<GoodsReceipt> GoodsReceipt { get; set; }
        //public DbSet<CompanyInfo> CompanyInfo { get; set; }
        //public DbSet<CurrencyInfo> CurrencyInfo { get; set; }
        //public DbSet<CountryMaster> CountryMaster { get; set; }
        //public DbSet<ShippingAgency> ShippingAgency { get; set; }
        //public DbSet<Session> Session { get; set; }
        public virtual DbSet<CarpetNumber> CarpetNumber { get; set; }
        public virtual DbSet<V_FinishedItemDetail> V_FinishedItemDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<V_FinishedItemDetail>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("V_FinishedItemDetail");
            });

            modelBuilder.Entity<V_FinishedItemDetail>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<CarpetNumber>(entity =>
            {
                entity.HasNoKey();
            });
            //builder.Entity<Invoice>().HasMany(c => c.Documents).WithOne(e => e.Invoice).IsRequired();

        }
    }
}
