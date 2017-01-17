using Microsoft.EntityFrameworkCore;
using CrmHub.Domain.Models;
using System.Linq;
using System;
using System.Reflection;

namespace CrmHub.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //modelBuilder.Entity<Client>()
            //.HasIndex(p => new { p.Company.Id, p.CNPJ });

            //modelBuilder.Entity<FieldMappingValue>()
            //.HasIndex(p => new { p.Client.Id, p.LabelHub, p.LabelCrm });
        }

        protected void UpdateDatesEntries()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    entry.Property("CreatedAt").IsModified = false;
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("UpdatedAt") != null))
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                    entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
            }
        }

        public override int SaveChanges()
        {
            UpdateDatesEntries();
            return base.SaveChanges();
        }

        public virtual DbSet<Crm> Crm { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Vendor> Vendor { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<AttributeEntity> AttributeEntity { get; set; }
        public virtual DbSet<FieldMappingValue> FieldMappingValue { get; set; }
    }
}
