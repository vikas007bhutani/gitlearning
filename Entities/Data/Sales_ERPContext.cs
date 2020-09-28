using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SALEERP.Models;

namespace SALEERP.Data
{
    public partial class Sales_ERPContext : DbContext
    {
        public Sales_ERPContext()
        {
        }

        public Sales_ERPContext(DbContextOptions<Sales_ERPContext> options)
            : base(options)
        {
        }


        public virtual DbSet<AgentContact> AgentContact { get; set; }
        public virtual DbSet<AgentMaster> AgentMaster { get; set; }
        public virtual DbSet<AgentUser> AgentUser { get; set; }
        public virtual DbSet<BankDetails> BankDetails { get; set; }
        public virtual DbSet<BankMaster> BankMaster { get; set; }
        public virtual DbSet<CommissionDetails> CommissionDetails { get; set; }
        public virtual DbSet<CountriesMaster> CountriesMaster { get; set; }
        public virtual DbSet<CustomerDetails> CustomerDetails { get; set; }
        public virtual DbSet<LanguagesMaster> LanguagesMaster { get; set; }
        public virtual DbSet<LoginRoles> LoginRoles { get; set; }
        public virtual DbSet<MirrorAgent> MirrorAgent { get; set; }
        public virtual DbSet<MirrorDetails> MirrorDetails { get; set; }
        public virtual DbSet<MirrorList> MirrorList { get; set; }
        public virtual DbSet<OrderItemDetails> OrderItemDetails { get; set; }
        public virtual DbSet<OrderMaster> OrderMaster { get; set; }
        public virtual DbSet<OrderPayment> OrderPayment { get; set; }
        public virtual DbSet<PayDetails> PayDetails { get; set; }
        public virtual DbSet<PayMode> PayMode { get; set; }
        public virtual DbSet<PoolMaster> PoolMaster { get; set; }
        public virtual DbSet<Roleclaim> Roleclaim { get; set; }
        public virtual DbSet<SeriesMaster> SeriesMaster { get; set; }
        public virtual DbSet<SpecialEdition> SpecialEdition { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }
        public virtual DbSet<VehicleDetails> VehicleDetails { get; set; }
        public virtual DbSet<VehicleMaster> VehicleMaster { get; set; }
        public virtual DbSet<CurrencyMaster> CurrencyMaster { get; set; }
        public virtual DbSet<SpecialAdditionDetails> SpecialAdditionDetails { get; set; }
        public virtual DbSet<CarpetNumber> CarpetNumber { get; set; }
        public virtual DbSet<V_FinishedItemDetail> V_FinishedItemDetail { get; set; }
        public virtual DbSet<CategorySeparate> CategorySeparate { get; set; }
        public virtual DbSet<DesignIntricacyComponentPlacementMarblecolor> DesignIntricacyComponentPlacementMarblecolor { get; set; }
        public virtual DbSet<ItemCategoryMaster> ItemCategoryMaster { get; set; }
        public virtual DbSet<Shape> Shape { get; set; }
        public virtual DbSet<Nationality> Nationality { get; set; }
        public virtual DbSet<ItemMaster> ItemMaster { get; set; }
        public virtual DbSet<Size> Size { get; set; }
        public virtual DbSet<CardTypeMaster> CardTypeMaster { get; set; }
        public virtual DbSet<PayLaterMaster> PayLaterMaster { get; set; }
        public virtual DbSet<PayMethodMaster> PayMethodMaster { get; set; }
        public virtual DbSet<Directstockpack> Directstockpack { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-T8NF0AF\\SQLEXPRESS;Database=Sales_ERP;Trusted_Connection=True;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Size>(entity =>
            {
                entity.HasIndex(e => new { e.SizeId, e.Shapeid })
                    .HasName("SizeIndex");

                entity.Property(e => e.SizeId).ValueGeneratedNever();

                entity.Property(e => e.Actualfullareasqyd)
                    .HasColumnType("decimal(18, 4)")
                    .HasComputedColumnSql("([dbo].[F_getfullareasqyd]([areaft],[mastercompanyid]))");

                entity.Property(e => e.LengthFt).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ProdLengthFt).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ProdSizeFt).HasMaxLength(50);

                entity.Property(e => e.ProdSizeMtr).HasMaxLength(50);

                entity.Property(e => e.ProdWidthFt).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.SizeFt).HasMaxLength(50);

                entity.Property(e => e.SizeInch).HasMaxLength(50);

                entity.Property(e => e.SizeMtr).HasMaxLength(50);

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.WidthFt).HasColumnType("decimal(10, 2)");
            });
            modelBuilder.Entity<ItemMaster>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.ToTable("ITEM_MASTER");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("IX_ITEM_MASTER");

                entity.HasIndex(e => e.ItemType)
                    .HasName("ITEM_MASTER_UnitId");

                entity.HasIndex(e => e.MasterCompanyid)
                    .HasName("IX_ITEM_MASTER_MastercompanyId");

                entity.Property(e => e.ItemId)
                    .HasColumnName("ITEM_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");

                entity.Property(e => e.Ismultipleconstruction)
                    .HasColumnName("ISMULTIPLECONSTRUCTION")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ItemCode)
                    .IsRequired()
                    .HasColumnName("ITEM_CODE")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasColumnName("ITEM_NAME")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Itemstatusbypassonfolio)
                    .HasColumnName("ITEMSTATUSBYPASSONFOLIO")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Katiwithexportsize)
                    .HasColumnName("KATIWITHEXPORTSIZE")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitTypeId).HasColumnName("UnitTypeID");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });
            modelBuilder.Entity<Nationality>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("nationality");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nationality1)
                    .HasColumnName("nationality")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Priority).HasColumnName("priority");
            });
            modelBuilder.Entity<AgentContact>(entity =>
            {
                entity.ToTable("AgentContact", "agent");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AgentId).HasColumnName("agent_id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(20);

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(20);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentContact)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_AgentContact_AgentUser");
            });

            modelBuilder.Entity<AgentMaster>(entity =>
            {
                entity.ToTable("AgentMaster", "mr");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<AgentUser>(entity =>
            {
                entity.ToTable("AgentUser", "acc");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(500);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(50);

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(50);

                entity.Property(e => e.ContractFormalities)
                    .HasColumnName("contract_formalities")
                    .HasMaxLength(500);

                entity.Property(e => e.ContractStartdate)
                    .HasColumnName("contract_startdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.country_id).HasColumnName("country_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Panno)
                    .HasColumnName("panno")
                    .HasMaxLength(50);

                entity.Property(e => e.ParcheeId).HasColumnName("parchee_id");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Website)
                    .HasColumnName("website")
                    .HasMaxLength(100);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.AgentUser)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Agent_User_Agent_type");
            });

            modelBuilder.Entity<BankDetails>(entity =>
            {
                entity.ToTable("BankDetails", "agent");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountNo)
                    .HasColumnName("account_no")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AgentId).HasColumnName("agent_id");

                entity.Property(e => e.BankId).HasColumnName("bank_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(200);

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.BankDetails)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_BankDetails_AgentUser");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.BankDetails)
                    .HasForeignKey(d => d.BankId)
                    .HasConstraintName("FK_BankDetails_BankMaster");
            });

            modelBuilder.Entity<BankMaster>(entity =>
            {
                entity.ToTable("BankMaster", "comm");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(500);

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ifsc)
                    .HasColumnName("IFSC")
                    .HasMaxLength(20);

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<CommissionDetails>(entity =>
            {
                entity.ToTable("CommissionDetails", "mr");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AgentId).HasColumnName("agent_id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.MirrorId).HasColumnName("mirror_id");

                entity.Property(e => e.Pecentage).HasColumnName("pecentage").HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SaleId).HasColumnName("sale_id");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<CountriesMaster>(entity =>
            {
                entity.ToTable("CountriesMaster", "mr");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.Priority).HasColumnName("priority");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<CustomerDetails>(entity =>
            {
                entity.ToTable("Customer_Details", "sales");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(300);

                entity.Property(e => e.Airport)
                    .HasColumnName("airport")
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(50);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(50);

                entity.Property(e => e.nationality)
                   .HasColumnName("nationality")
                   .HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PassportNo)
                    .HasColumnName("passport_no")
                    .HasMaxLength(50);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(50);

                entity.Property(e => e.TeleCountryCode)
                    .HasColumnName("tele_country_code")
                    .HasMaxLength(10);

                entity.Property(e => e.MobCountryCode)
                   .HasColumnName("mob_country_code")
                   .HasMaxLength(10);

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(20);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(10);

                entity.Property(e => e.countryid)
                  .HasColumnName("countryid")
                  .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Zipcode)
                    .HasColumnName("zipcode")
                    .HasMaxLength(20);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.CustomerDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Customer_Details_Order_Master");
            });

            modelBuilder.Entity<LanguagesMaster>(entity =>
            {
                entity.ToTable("LanguagesMaster", "mr");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<LoginRoles>(entity =>
            {
                entity.ToTable("LoginRoles", "acc");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Descripton)
                    .HasColumnName("descripton")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<MirrorAgent>(entity =>
            {
                entity.ToTable("MirrorAgent", "mr");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AgentId).HasColumnName("agent_id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsParchi).HasColumnName("is_parchi");

                entity.Property(e => e.MirrorId).HasColumnName("mirror_id");

                entity.Property(e => e.ParchiAmount)
                    .HasColumnName("parchi_amount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Mirror)
                    .WithMany(p => p.MirrorAgent)
                    .HasForeignKey(d => d.MirrorId)
                    .HasConstraintName("FK_Mirror_Agent_Mirror_details");
            });

            modelBuilder.Entity<MirrorDetails>(entity =>
            {
                entity.ToTable("MirrorDetails", "mr");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LanguageId).HasColumnName("language_id");

                entity.Property(e => e.Pax).HasColumnName("pax");

                entity.Property(e => e.PoolId).HasColumnName("pool_id");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(500);

                entity.Property(e => e.SeriesId).HasColumnName("series_id");

                entity.Property(e => e.StatusId)
                    .HasColumnName("status_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.MirrorDetails)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_MirrorDetails_country");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MirrorDetails)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_Mirror_Details_User_login");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.MirrorDetails)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_MirrorDetails_language");

                entity.HasOne(d => d.Pool)
                    .WithMany(p => p.MirrorDetails)
                    .HasForeignKey(d => d.PoolId)
                    .HasConstraintName("FK_MirrorDetails_pool");

                entity.HasOne(d => d.Series)
                    .WithMany(p => p.MirrorDetails)
                    .HasForeignKey(d => d.SeriesId)
                    .HasConstraintName("FK_MirrorDetails_series");
            });

            modelBuilder.Entity<MirrorList>(entity =>
            {
                entity.HasKey(e => e.MirrorId);

                entity.ToTable("Mirror_List", "sales");

                entity.Property(e => e.MirrorId)
                    .HasColumnName("mirror_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .IsRequired()
                    .HasColumnName("updated_datetime")
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<OrderItemDetails>(entity =>
            {
                entity.ToTable("Order_Item_Details", "sales");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(50);

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasMaxLength(20);

                entity.Property(e => e.ConversionRate)
                    .HasColumnName("conversion_rate")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrencyType).HasColumnName("currency_type");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ItemDesc)
                    .HasColumnName("item_desc")
                    .HasMaxLength(100);

                entity.Property(e => e.ItemType)
                    .HasColumnName("item_type")
                    .HasComment("tobemade|stand|stock");

                entity.Property(e => e.SaleType)
                  .HasColumnName("sale_type")
                  .HasComment("used for custom sale|normal sale|cash sale");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.OrderType)
                    .HasColumnName("order_type")
                    .HasComment("cash memo||order form");

                entity.Property(e => e.OrderTypePrefix)
                    .HasColumnName("order_type_prefix")
                    .HasMaxLength(10);

                entity.Property(e => e.Photo)
                    .HasColumnName("photo")
                    .HasMaxLength(200);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PriceInr)
                    .HasColumnName("price_inr")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.Shape)
                    .HasColumnName("shape")
                    .HasMaxLength(20);

                entity.Property(e => e.Size)
                    .HasColumnName("size")
                    .HasMaxLength(20);

                entity.Property(e => e.width)
                    .HasColumnName("width")
                    .HasMaxLength(20);

                entity.Property(e => e.height)
                    .HasColumnName("height")
                    .HasMaxLength(20);

                entity.Property(e => e.StockId)
                    .HasColumnName("stock_id")
                    .HasMaxLength(50);

                entity.Property(e => e.TransId)
                    .HasColumnName("trans_id")
                    .HasMaxLength(50);

                entity.Property(e => e.Unit).HasColumnName("unit");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItemDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Order_Item_Details_order_master");
            });

            modelBuilder.Entity<OrderMaster>(entity =>
            {
                entity.ToTable("Order_Master", "sales");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DelieveryType)
                    .HasColumnName("delievery_type")
                    .HasComment("used for portdelievery or homedelivery");

                entity.Property(e => e.DeliveryFrom)
                    .HasColumnName("delivery_from")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeliveryTo)
                    .HasColumnName("delivery_to")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(500);

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MirrorId).HasColumnName("mirror_id");

                entity.Property(e => e.PortType)
                    .HasColumnName("port_type")
                    .HasComment("used for seaport|airport");

                entity.Property(e => e.PortName)
                   .HasColumnName("port_name")
                   .HasMaxLength(100);

                entity.Property(e => e.PassportNo)
                  .HasColumnName("passport_no")
                  .HasMaxLength(50);

                entity.Property(e => e.SaleDate)
                    .HasColumnName("sale_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

              

                entity.Property(e => e.TransactionId)
                    .HasColumnName("transaction_id")
                    .HasMaxLength(100);

                entity.Property(e => e.Unit).HasColumnName("unit");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.salestatus)
                    .HasColumnName("sale_status");

                entity.HasOne(d => d.Mirror)
                    .WithMany(p => p.OrderMaster)
                    .HasForeignKey(d => d.MirrorId)
                    .HasConstraintName("FK_Order_Master_Mirror_List");
            });

            modelBuilder.Entity<OrderPayment>(entity =>
            {
                entity.ToTable("Order_Payment", "sales");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AmoutHd)
                    .HasColumnName("amout_hd")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CardType)
                    .HasColumnName("card_type")
                    .HasComment("visa|amex|pdc");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Gst)
                    .HasColumnName("GST")
                    .HasColumnType("decimal(18, 2)");
                entity.Property(e => e.CurrencyType).HasColumnName("currency_type");

                entity.Property(e => e.Igst)
                    .HasColumnName("IGST")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PayDate)
                    .HasColumnName("pay_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PayMode)
                    .HasColumnName("pay_mode")
                    .HasComment("cash|Card|Paylater");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnName("update_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderPayment)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Order_Payment_Order_Master");
            });

            modelBuilder.Entity<PayDetails>(entity =>
            {
                entity.ToTable("PayDetails", "mr");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AgentId).HasColumnName("agent_id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CommId).HasColumnName("comm_id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.pay_mode)
                    .HasColumnName("pay_mode")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.payment_by)
                  .HasColumnName("payment_by")
                  .HasMaxLength(50);

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MirrorId).HasColumnName("mirror_id");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.PayDetails)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_Pay_Details_Agent_User");

                entity.HasOne(d => d.Comm)
                    .WithMany(p => p.PayDetails)
                    .HasForeignKey(d => d.CommId)
                    .HasConstraintName("FK_Pay_Details_Commission_Details");

                entity.HasOne(d => d.Mirror)
                    .WithMany(p => p.PayDetails)
                    .HasForeignKey(d => d.MirrorId)
                    .HasConstraintName("FK_Pay_Details_Mirror");
            });
            modelBuilder.Entity<PayMode>(entity =>
            {
                entity.ToTable("PayMode", "comm");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Createddatetime)
                    .HasColumnName("createddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Mode)
                    .HasColumnName("mode")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });
            modelBuilder.Entity<PoolMaster>(entity =>
            {
                entity.ToTable("PoolMaster", "mr");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(500);

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Roleclaim>(entity =>
            {
                entity.ToTable("roleclaim", "acc");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Roleclaim)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_roleclaim_loginrole");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Roleclaim)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_roleclaim_userlogin");
            });

            modelBuilder.Entity<SeriesMaster>(entity =>
            {
                entity.ToTable("SeriesMaster", "comm");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<SpecialEdition>(entity =>
            {
                entity.ToTable("SpecialEditionMaster", "sales");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.ToTable("UserLogin", "acc");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(100);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<VehicleDetails>(entity =>
            {
                entity.ToTable("VehicleDetails", "agent");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AgentId).HasColumnName("agent_id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.VehicleDetails)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_Vehicle_Details_Agent_User");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleDetails)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Vehicle_Details_Vehicle_Master");
            });

            modelBuilder.Entity<VehicleMaster>(entity =>
            {
                entity.ToTable("VehicleMaster", "mr");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<CurrencyMaster>(entity =>
            {
                entity.ToTable("CurrencyMaster", "comm");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.IsActive)
                   .HasColumnName("is_active")
                   .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Priority).HasColumnName("priority");

                entity.Property(e => e.Rate)
                    .HasColumnName("rate")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Symbol)
                    .HasColumnName("symbol")
                    .HasMaxLength(10);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });
            modelBuilder.Entity<SpecialAdditionDetails>(entity =>
            {
                entity.ToTable("special_addition_details", "sales");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");

                entity.Property(e => e.SpecialAdditionDesc)
                    .HasColumnName("special_addition_desc")
                    .HasMaxLength(50);

                entity.Property(e => e.SpecialAdditionId).HasColumnName("special_addition_id");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });
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
               
                entity.HasKey(e => e.StockNo)
                   .HasName("PK__Carpet_Number__210CC53E");

                entity.HasIndex(e => e.Companyid)
                    .HasName("CarpetNumber_CompanyId");

                entity.HasIndex(e => e.CurrentProStatus)
                    .HasName("ix_Currentprostatus");

                entity.HasIndex(e => e.IssRecStatus)
                    .HasName("ix_issrecstatus");

             

                entity.HasIndex(e => e.OrderId)
                    .HasName("CarpetNumber_orderid");

                entity.HasIndex(e => e.PackingDetailId)
                    .HasName("CarpetNumber_PackingDetailid");

                entity.HasIndex(e => e.PackingId)
                    .HasName("CarpetNumber_Packingid");

                entity.HasIndex(e => e.Postfix)
                    .HasName("IX_CarpetNumber_2");

                entity.HasIndex(e => e.ProcessRecDetailId)
                    .HasName("ix_ProcessrecDetailid");

                entity.HasIndex(e => e.ProcessRecId)
                    .HasName("ix_Processrecid");

                entity.HasIndex(e => e.StockNo)
                    .HasName("IX_CarpetNumber")
                    .IsUnique();

               

                entity.HasIndex(e => e.TypeId)
                    .HasName("CarpetNumber_Typeid");

                entity.Property(e => e.StockNo).ValueGeneratedNever();

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasMaxLength(500);

                entity.Property(e => e.Confirm)
                    .HasColumnName("CONFIRM")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Confirmdate)
                    .HasColumnName("confirmdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Damaged).HasColumnName("damaged");

                entity.Property(e => e.Godownid).HasColumnName("godownid");

                entity.Property(e => e.Ismobsend).HasColumnName("ismobsend");

                entity.Property(e => e.item_finished_id).HasColumnName("Item_Finished_Id");

                entity.Property(e => e.Locationid)
                    .HasColumnName("LOCATIONID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OldStockNo).HasMaxLength(50);

                entity.Property(e => e.PackDate)
                    .HasColumnName("Pack_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PackSource)
                    .HasColumnName("pack_source")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PackingDetailId).HasColumnName("PackingDetailID");

                entity.Property(e => e.PackingId).HasColumnName("PackingID");

                entity.Property(e => e.Prefix).HasMaxLength(10);

                entity.Property(e => e.Price).HasColumnType("decimal(28, 2)");

                entity.Property(e => e.ProcessRecDetailId).HasColumnName("Process_Rec_Detail_Id");

                entity.Property(e => e.ProcessRecId).HasColumnName("Process_Rec_id");

                entity.Property(e => e.RecDate)
                    .HasColumnName("Rec_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.TStockNo)
                    .IsRequired()
                    .HasColumnName("TStockNo")
                    .HasMaxLength(50);

                entity.Property(e => e.Userid).HasColumnName("userid");


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
            modelBuilder.Entity<CardTypeMaster>(entity =>
            {
                entity.ToTable("CardTypeMaster", "comm");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CardCode)
                    .HasColumnName("card_code")
                    .HasMaxLength(10);

                entity.Property(e => e.CardDesc)
                    .HasColumnName("card_desc")
                    .HasMaxLength(100);

                entity.Property(e => e.CardName)
                    .HasColumnName("card_name")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<PayLaterMaster>(entity =>
            {
                entity.ToTable("PayLaterMaster", "comm");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TypeCode)
                    .HasColumnName("type_code")
                    .HasMaxLength(10);

                entity.Property(e => e.TypeDesc)
                    .HasColumnName("type_desc")
                    .HasMaxLength(100);

                entity.Property(e => e.TypeName)
                    .HasColumnName("type_name")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<PayMethodMaster>(entity =>
            {
                entity.ToTable("PayMethodMaster", "comm");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PayCode)
                    .HasColumnName("pay_code")
                    .HasMaxLength(10);

                entity.Property(e => e.PayDesc)
                    .HasColumnName("pay_desc")
                    .HasMaxLength(100);

                entity.Property(e => e.PayName)
                    .HasColumnName("pay_name")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Directstockpack>(entity =>
            {
                entity.HasKey(e => e.DId);

                entity.ToTable("DIRECTSTOCKPACK");

                entity.Property(e => e.DId).HasColumnName("D_ID");

                entity.Property(e => e.Dateadded)
                    .HasColumnName("DATEADDED")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Remark)
                    .HasColumnName("REMARK")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Stockno).HasColumnName("STOCKNO");

                entity.Property(e => e.Userid).HasColumnName("USERID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
