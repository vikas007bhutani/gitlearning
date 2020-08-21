using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SALEERP.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata;
//using Entities.Models;

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
        public virtual DbSet<CategorySeparate> CategorySeparate { get; set; }
        public virtual DbSet<DesignIntricacyComponentPlacementMarblecolor> DesignIntricacyComponentPlacementMarblecolor { get; set; }
        public virtual DbSet<ItemCategoryMaster> ItemCategoryMaster { get; set; }
        public virtual DbSet<Shape> Shape { get; set; }

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

            modelBuilder.Entity<CategorySeparate>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.Categoryid)
                    .HasName("IX_CategorySeparate_CategoryId");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_CategorySeparate_Id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.SrNo).HasColumnName("Sr_No");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<DesignIntricacyComponentPlacementMarblecolor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DESIGN_INTRICACY_COMPONENT_PLACEMENT_MARBLECOLOR");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MasterCompanyId).HasColumnName("MasterCompanyID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ItemCategoryMaster>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("ITEM_CATEGORY_MASTER");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("CATEGORY_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("CATEGORY_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Hscode)
                    .HasColumnName("HSCODE")
                    .HasMaxLength(50);

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<Shape>(entity =>
            {
                entity.HasIndex(e => e.ShapeId)
                    .HasName("ShapeIndex");

                entity.Property(e => e.ShapeId).ValueGeneratedNever();

                entity.Property(e => e.ShapeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });
            //builder.Entity<Invoice>().HasMany(c => c.Documents).WithOne(e => e.Invoice).IsRequired();

        }
    }
}
