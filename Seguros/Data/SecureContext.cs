using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Seguros.Models
{
    public class SecureContext : DbContext
    {
        public SecureContext()
        {
        }

        public SecureContext (DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Policy> Policy { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Type> Type { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=secure;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<Client>().HasData(
                new Client() { Id = 1, Name = "Jane", LastName = "Austen", Email = "jane@prueba.com" },
                new Client() { Id = 2, Name = "Charles", LastName = "Dickens", Email = "charles@prueba.com" },
                new Client() { Id = 3, Name = "Miguel", LastName = "Cervantes", Email = "miguel@prueba.com" });


            modelBuilder.Entity<Type>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Percentage)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Type>().HasData(
                new Type() { Id = 1, Percentage = 10, Value = "Terremoto" },
                new Type() { Id = 2, Percentage = 50, Value = "Incendio" },
                new Type() { Id = 3, Percentage = 80, Value = "Pérdida" });

            modelBuilder.Entity<Policy>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.TypeId)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Period)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Danger)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Policy>().HasData(
                new Policy() {
                    Id = 1,
                    Name = "Prueba1",
                    ClientId = 1,
                    Danger = Danger.Alto,
                    Description = "Se está haciendo una prueba",
                    Period = 12,
                    TypeId = 1,
                    Price = 1000,
                    Date = new DateTime()
                });

        }
    }
}
