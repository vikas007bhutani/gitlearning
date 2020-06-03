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
        public virtual DbSet<LanguagesMaster> LanguagesMaster { get; set; }
        public virtual DbSet<LoginRoles> LoginRoles { get; set; }
        public virtual DbSet<MirrorAgent> MirrorAgent { get; set; }
        public virtual DbSet<MirrorDetails> MirrorDetails { get; set; }
        public virtual DbSet<PayDetails> PayDetails { get; set; }
        public virtual DbSet<PoolMaster> PoolMaster { get; set; }
        public virtual DbSet<Roleclaim> Roleclaim { get; set; }
        public virtual DbSet<SaleDetails> SaleDetails { get; set; }
        public virtual DbSet<SeriesMaster> SeriesMaster { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }
        public virtual DbSet<VehicleDetails> VehicleDetails { get; set; }
        public virtual DbSet<VehicleMaster> VehicleMaster { get; set; }

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
            modelBuilder.Entity<AgentContact>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.ToTable("Agent_Contact", "agent");

                entity.Property(e => e.ContactId).HasColumnName("contact_id");

                entity.Property(e => e.AgentId).HasColumnName("agent_id");

                entity.Property(e => e.Createdby)
                    .HasColumnName("createdby")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Createddatetime)
                    .HasColumnName("createddatetime")
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

                entity.Property(e => e.Updateddatetime)
                    .HasColumnName("updateddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentContact)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Agent_Contact_Agent_User");
            });

            modelBuilder.Entity<AgentMaster>(entity =>
            {
                entity.HasKey(e => e.AgentId)
                    .HasName("PK_Agent");

                entity.ToTable("Agent_Master", "mr");

                entity.Property(e => e.AgentId).HasColumnName("agent_id");

                entity.Property(e => e.AgentCode)
                    .HasColumnName("agent_code")
                    .HasMaxLength(10);

                entity.Property(e => e.AgentType)
                    .HasColumnName("agent_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

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
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<AgentUser>(entity =>
            {
                entity.HasKey(e => e.AgentId);

                entity.ToTable("Agent_User", "acc");

                entity.Property(e => e.AgentId).HasColumnName("agent_id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(500);

                entity.Property(e => e.AgentCode)
                    .HasColumnName("agent_code")
                    .HasMaxLength(50);

                entity.Property(e => e.AgentTypeId).HasColumnName("agent_type_id");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(50);

                entity.Property(e => e.status)
                  .HasColumnName("status")
                  .HasMaxLength(20);

                entity.Property(e => e.Contractformalities)
                    .HasColumnName("contractformalities")
                    .HasMaxLength(500);

                entity.Property(e => e.Contractstartdate)
                    .HasColumnName("contractstartdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(50);

                entity.Property(e => e.Createdby).HasColumnName("createdby");

                entity.Property(e => e.Createddatetime)
                    .HasColumnName("createddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Updateddatetime)
                  .HasColumnName("updateddatetime")
                  .HasColumnType("datetime")
                  .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Panno)
                    .HasColumnName("panno")
                    .HasMaxLength(50);

                entity.Property(e => e.Parcheeid).HasColumnName("parcheeid");

                entity.Property(e => e.Website)
                    .HasColumnName("website")
                    .HasMaxLength(100);

                entity.HasOne(d => d.AgentType)
                    .WithMany(p => p.AgentUser)
                    .HasForeignKey(d => d.AgentTypeId)
                    .HasConstraintName("FK_Agent_User_Agent_type");

                entity.HasOne(d => d.CreatedbyNavigation)
                    .WithMany(p => p.AgentUser)
                    .HasForeignKey(d => d.Createdby)
                    .HasConstraintName("FK_Agent_User_login_User");
            });

            modelBuilder.Entity<BankDetails>(entity =>
            {
                entity.ToTable("Bank_Details", "agent");

                entity.Property(e => e.AccountNo)
                    .HasColumnName("Account_No")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AgentId).HasColumnName("Agent_Id");

                entity.Property(e => e.BankId).HasColumnName("Bank_Id");

                entity.Property(e => e.Createddatetime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("Is_Active");

                entity.Property(e => e.Updateddatetime).HasColumnType("datetime");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.BankDetails)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_agent_user_Bank_Details");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.BankDetails)
                    .HasForeignKey(d => d.BankId)
                    .HasConstraintName("FK_Bank_Details_Bank_master");
            });

            modelBuilder.Entity<BankMaster>(entity =>
            {
                entity.HasKey(e => e.Bankid);

                entity.ToTable("Bank_Master", "comm");

                entity.Property(e => e.Bankid).HasColumnName("bankid");

                entity.Property(e => e.Bankaddress)
                    .HasColumnName("bankaddress")
                    .HasMaxLength(500);

                entity.Property(e => e.Bankcode)
                    .HasColumnName("bankcode")
                    .HasMaxLength(10);

                entity.Property(e => e.Bankname)
                    .HasColumnName("bankname")
                    .HasMaxLength(100);

                entity.Property(e => e.Createdby)
                    .HasColumnName("createdby")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Createddatetime)
                    .HasColumnName("createddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ifsc)
                    .HasColumnName("IFSC")
                    .HasMaxLength(20);

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Updateddatetime)
                    .HasColumnName("updateddatetime")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<CommissionDetails>(entity =>
            {
                entity.HasKey(e => e.CommId);

                entity.ToTable("Commission_Details", "mr");

                entity.Property(e => e.CommId).HasColumnName("comm_id");

                entity.Property(e => e.AgentId).HasColumnName("agent_id");

                entity.Property(e => e.CommAmount)
                    .HasColumnName("comm_amount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CommDate)
                    .HasColumnName("comm_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.CommPecentage).HasColumnName("comm_pecentage");

                entity.Property(e => e.Createdby)
                    .HasColumnName("createdby")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Createddatetime)
                    .HasColumnName("createddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.MirrorId).HasColumnName("mirror_id");

                entity.Property(e => e.SaleId).HasColumnName("sale_id");

                entity.Property(e => e.Updateddatetime)
                    .HasColumnName("updateddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<CountriesMaster>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PK_Countries");

                entity.ToTable("Countries_Master", "mr");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.CountryCode)
                    .HasColumnName("country_code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
                    .HasColumnName("country_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

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
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<LanguagesMaster>(entity =>
            {
                entity.HasKey(e => e.LanguageId)
                    .HasName("PK_Languages");

                entity.ToTable("Languages_Master", "mr");

                entity.Property(e => e.LanguageId).HasColumnName("language_id");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LanguageCode)
                    .HasColumnName("language_code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LanguageName)
                    .HasColumnName("language_name")
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
                entity.HasKey(e => e.RoleId);

                entity.ToTable("LoginRoles", "acc");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.RoleDescripton)
                    .HasColumnName("role_descripton")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RoleName)
                    .HasColumnName("role_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<MirrorAgent>(entity =>
            {
                entity.ToTable("Mirror_Agent", "mr");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Agentid).HasColumnName("agentid");

                entity.Property(e => e.parchiamount)
                    .HasColumnName("parchiamount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Createdby)
                    .HasColumnName("createdby")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Createddatetime)
                    .HasColumnName("createddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.isparchi)
                  .HasColumnName("isparchi");
                  

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Mirrorid).HasColumnName("mirrorid");

                entity.Property(e => e.Updateddatetime)
                    .HasColumnName("updateddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Mirror)
                    .WithMany(p => p.MirrorAgent)
                    .HasForeignKey(d => d.Mirrorid)
                    .HasConstraintName("FK_Mirror_Agent_Mirror_details");
            });

            modelBuilder.Entity<MirrorDetails>(entity =>
            {
                entity.HasKey(e => e.MirrorId);

                entity.ToTable("Mirror_Details", "mr");

                entity.Property(e => e.MirrorId).HasColumnName("mirror_id");

                entity.Property(e => e.Countryid).HasColumnName("countryid");

                entity.Property(e => e.Createdby)
                    .HasColumnName("createdby")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Createddatetime)
                    .HasColumnName("createddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DemoPersonId).HasColumnName("demo_person_id");

                entity.Property(e => e.DriverId).HasColumnName("driver_id");

                entity.Property(e => e.ExcursionAgentId).HasColumnName("excursion_agent_id");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Languageid).HasColumnName("languageid");

                entity.Property(e => e.MirrorDate)
                    .HasColumnName("mirror_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Pax).HasColumnName("pax");

                entity.Property(e => e.PoolId).HasColumnName("pool_id");

                entity.Property(e => e.PrincipleAgentId).HasColumnName("principle_agent_id");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(500);

                entity.Property(e => e.SeriesId).HasColumnName("series_id");

                entity.Property(e => e.StatusId)
                    .HasColumnName("status_id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TourEscortId).HasColumnName("tour_escort_id");

                entity.Property(e => e.TourGuideId).HasColumnName("tour_guide_id");

                entity.Property(e => e.TourLeaderId).HasColumnName("tour_leader_id");

                entity.Property(e => e.Updateddatetime)
                    .HasColumnName("updateddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CreatedbyNavigation)
                    .WithMany(p => p.MirrorDetails)
                    .HasForeignKey(d => d.Createdby)
                    .HasConstraintName("FK_Mirror_Details_User_login");
            });

            modelBuilder.Entity<PayDetails>(entity =>
            {
                entity.HasKey(e => e.PayId);

                entity.ToTable("Pay_Details", "mr");

                entity.Property(e => e.PayId).HasColumnName("pay_id");

                entity.Property(e => e.AgentId).HasColumnName("agent_id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CommId).HasColumnName("comm_id");

                entity.Property(e => e.Createdby)
                    .HasColumnName("createdby")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Createddatetime)
                    .HasColumnName("createddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MirrorId).HasColumnName("mirror_id");

                entity.Property(e => e.PayDate)
                    .HasColumnName("pay_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Updateddatetime)
                    .HasColumnName("updateddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.PayDetails)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.Cascade)
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

            modelBuilder.Entity<PoolMaster>(entity =>
            {
                entity.HasKey(e => e.PoolId);

                entity.ToTable("Pool_Master", "mr");

                entity.Property(e => e.PoolId).HasColumnName("pool_id");

                entity.Property(e => e.Createdby)
                    .HasColumnName("createdby")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Createddatetime)
                    .HasColumnName("createddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PoolDesc)
                    .HasColumnName("pool_desc")
                    .HasMaxLength(500);

                entity.Property(e => e.PoolEndDate)
                    .HasColumnName("pool_end_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PoolName)
                    .HasColumnName("pool_name")
                    .HasMaxLength(100);

                entity.Property(e => e.PoolStartDate)
                    .HasColumnName("pool_start_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Updateddatetime)
                    .HasColumnName("updateddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Roleclaim>(entity =>
            {
                entity.HasKey(e => e.ClaimId);

                entity.ToTable("roleclaim", "acc");

                entity.Property(e => e.ClaimId).HasColumnName("claim_id");

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

            modelBuilder.Entity<SaleDetails>(entity =>
            {
                entity.HasKey(e => e.SaleId);

                entity.ToTable("Sale_Details");

                entity.Property(e => e.SaleId).HasColumnName("sale_id");

                entity.Property(e => e.CardChargePercentage)
                    .HasColumnName("Card_Charge_Percentage")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Createdby)
                    .HasColumnName("createdby")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Createddatetime)
                    .HasColumnName("createddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Gst)
                    .HasColumnName("GST")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.HdchargeAmount)
                    .HasColumnName("HDcharge_amount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Igst)
                    .HasColumnName("IGST")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PaidByCard)
                    .HasColumnName("paid_by_card")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaidByCash)
                    .HasColumnName("paid_by_cash")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaidInStore)
                    .HasColumnName("paid_in_store")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PayLater)
                    .HasColumnName("pay_later")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SaleAmount)
                    .HasColumnName("sale_amount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SaleDate)
                    .HasColumnName("sale_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Updateddatetime)
                    .HasColumnName("updateddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<SeriesMaster>(entity =>
            {
                entity.HasKey(e => e.SeriesId);

                entity.ToTable("Series_Master", "comm");

                entity.Property(e => e.SeriesId).HasColumnName("series_id");

                entity.Property(e => e.Createdby)
                    .HasColumnName("createdby")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Createddatetime)
                    .HasColumnName("createddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SeriesCode)
                    .HasColumnName("series_code")
                    .HasMaxLength(50);

                entity.Property(e => e.SeriesName)
                    .HasColumnName("series_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Updateddatetime)
                    .HasColumnName("updateddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("UserLogin", "acc");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("created_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.UpdatedDatetime)
                    .HasColumnName("updated_datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserEmail)
                    .HasColumnName("user_email")
                    .HasMaxLength(100);

                entity.Property(e => e.UserName)
                    .HasColumnName("user_name")
                    .HasMaxLength(100);

                entity.Property(e => e.UserPass)
                    .HasColumnName("user_pass")
                    .HasMaxLength(100);

                entity.Property(e => e.UserPhone)
                    .HasColumnName("user_phone")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VehicleDetails>(entity =>
            {
                entity.ToTable("Vehicle_Details", "agent");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AgentId).HasColumnName("agent_id");

                entity.Property(e => e.Createdby)
                    .HasColumnName("createdby")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Createddatetime)
                    .HasColumnName("createddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Updateddatetime)
                    .HasColumnName("updateddatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.Property(e => e.VehicleNo)
                    .HasColumnName("vehicle_no")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.VehicleDetails)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Vehicle_Details_Agent_User");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleDetails)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Vehicle_Details_Vehicle_Master");
            });

            modelBuilder.Entity<VehicleMaster>(entity =>
            {
                entity.HasKey(e => e.VehicleId)
                    .HasName("PK_Vehicle");

                entity.ToTable("Vehicle_Master", "mr");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

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
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.VehicleType)
                    .HasColumnName("vehicle_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
