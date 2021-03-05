using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Morocoto.Domain.Models;

#nullable disable

namespace Morocoto.Domain.DbContexts
{
    public partial class MorocotoDbContext : DbContext
    {
        public MorocotoDbContext()
        {
        }

        public MorocotoDbContext(DbContextOptions<MorocotoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<BusinessAddress> BusinessAddresses { get; set; }
        public virtual DbSet<BusinessBill> BusinessBills { get; set; }
        public virtual DbSet<BusinessPhoneNumber> BusinessPhoneNumbers { get; set; }
        public virtual DbSet<BusinessType> BusinessTypes { get; set; }
        public virtual DbSet<BuyCredit> BuyCredits { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerTaxis> CustomerTaxes { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RequestState> RequestStates { get; set; }
        public virtual DbSet<SecurityQuestion> SecurityQuestions { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAddress> UserAddresses { get; set; }
        public virtual DbSet<UserPhoneNumber> UserPhoneNumbers { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Business>(entity =>
            { 

                entity.HasIndex(e => e.BusinessNumber, "Business_BusinessNumber_idx")
                    .IsUnique();

                entity.Property(e => e.BusinessCreditAvailable)
                    .HasColumnType("decimal(13, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BusinessName)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.HasOne(d => d.BusinessType)
                    .WithMany(p => p.Businesses)
                    .HasForeignKey(d => d.BusinessTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Business_BusinessTypeId_fk");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.Businesses)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Business_partnerId_fk");
            });

            modelBuilder.Entity<BusinessAddress>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.BusinessId })
                    .HasName("BusinessAddresses_pk");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Street1)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Street2)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.BusinessAddresses)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("BusinessAddresses_BusinessId_fk");
            });

            modelBuilder.Entity<BusinessBill>(entity =>
            {
                entity.Property(e => e.DateCreation).HasColumnType("datetime");

                entity.Property(e => e.PathFile)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.BusinessBills)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("BusinessBills_BusinessId_fk");
            });

            modelBuilder.Entity<BusinessPhoneNumber>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.BusinessId })
                    .HasName("BusinessPhoneNumbers_pk");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.BusinessPhoneNumbers)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("BusinessPhoneNumbers_BusinessId_fk");
            });

            modelBuilder.Entity<BusinessType>(entity =>
            {
                entity.HasIndex(e => e.BusinessType1, "BusinessType_idx")
                    .IsUnique();

                entity.Property(e => e.BusinessType1)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("BusinessType");
            });

            modelBuilder.Entity<BuyCredit>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.BusinessId, e.CustomerId })
                    .HasName("BuyCredits_Pk");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreditBought).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.CreditBoughtDate).HasColumnType("datetime");

                entity.Property(e => e.TransactionNumber)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.BuyCredits)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("BuyCredits_Customers_Fk");

                entity.HasOne(d => d.CustomerTax)
                    .WithMany(p => p.BuyCredits)
                    .HasForeignKey(d => d.CustomerTaxId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("BuyCredits_CustomerTaxesId");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.BuyCredits)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("BuyCredits_Business_Fk");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Customer)
                    .HasForeignKey<Customer>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Customers_UserId_FK");
            });

            modelBuilder.Entity<CustomerTaxis>(entity =>
            {
                entity.Property(e => e.Tax).HasColumnType("decimal(13, 2)");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.LogMessage)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UserDevice)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Partner>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Partner)
                    .HasForeignKey<Partner>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Partner_UserId_FK");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.RequestedCredit).HasColumnType("decimal(13, 2)");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RequestStates_BusinessId_fk");

                entity.HasOne(d => d.RequestState)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.RequestStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Requests_RequestStates_Fk");
            });

            modelBuilder.Entity<RequestState>(entity =>
            {
                entity.Property(e => e.RequestState1)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("RequestState");
            });

            modelBuilder.Entity<SecurityQuestion>(entity =>
            {
                entity.HasIndex(e => e.SecurityQuestion1, "SecurityQuestions_idx")
                    .IsUnique();

                entity.Property(e => e.SecurityQuestion1)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("SecurityQuestion");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CustomerSenderId, e.CustomerRecieverId })
                    .HasName("Transactions_pk");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ConfirmationNumber)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreditTransfered).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.DateCreation).HasColumnType("datetime");

                entity.HasOne(d => d.CustomerReciever)
                    .WithMany(p => p.TransactionCustomerRecievers)
                    .HasForeignKey(d => d.CustomerRecieverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Transactions_CustomerRecieverId_fk");

                entity.HasOne(d => d.CustomerSender)
                    .WithMany(p => p.TransactionCustomerSenders)
                    .HasForeignKey(d => d.CustomerSenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Transactions_CustomerSenderId_fk");

                entity.HasOne(d => d.CustomerTaxes)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CustomerTaxesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ConfirmationNumber");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "User_Email_idx")
                    .IsUnique();

                entity.HasIndex(e => e.IdentificationDocument, "User_IdentificationDocument_idx")
                    .IsUnique();

                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdentificationDocument)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.OsPhone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Pin)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("PIN");

                entity.Property(e => e.SecurityAnswer)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UserPhone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.SecurityQuestion)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.SecurityQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_SecurityQuestion_FK");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_UserType_FK");
            });

            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.UserId })
                    .HasName("UserAddresses_pk");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Street1)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Street2)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserAddresses_UserId_fk");
            });

            modelBuilder.Entity<UserPhoneNumber>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.UserId })
                    .HasName("UserPhoneNumbers_pk");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPhoneNumbers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserPhoneNumbers_UserId_FK");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.HasIndex(e => e.UserType1, "UserType_idx")
                    .IsUnique();

                entity.Property(e => e.UserType1)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("UserType");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
