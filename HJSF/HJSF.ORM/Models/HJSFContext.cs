using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HJSF.ORM.Models
{
    public partial class HJSFContext : DbContext
    {
        public HJSFContext()
        {
        }

        public HJSFContext(DbContextOptions<HJSFContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HjsfSysUser> HfjsSysUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=HJSF;User ID=sa;Password=123.com;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<HjsfSysUser>(entity =>
            {
                entity.ToTable("HJSF_SysUser");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Eamil)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LoginIp)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LoginTime).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
