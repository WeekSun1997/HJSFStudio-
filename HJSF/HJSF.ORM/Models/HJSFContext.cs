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

        public virtual DbSet<HjsfSysUser> HjsfSysUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=HJSF;User ID=sa;Password=123.com;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HjsfSysUser>(entity =>
            {
                entity.ToTable("HJSF_SysUser");

                entity.Property(e => e.Address)
                    .HasMaxLength(105)
                    .IsUnicode(false)
                    .HasComment("地址");

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasComment("添加时间");

                entity.Property(e => e.CreateUserId).HasComment("添加人ID");

                entity.Property(e => e.CreateUserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("添加人名称");

                entity.Property(e => e.Eamil)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasComment("电子邮箱");

                entity.Property(e => e.LoginCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("登录账号");

                entity.Property(e => e.OpenId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("微信ID");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("密码");

                entity.Property(e => e.Phone)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasComment("手机号");

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateUserId).HasComment("修改人ID");

                entity.Property(e => e.UpdateUserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("修改人名称");

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("用户姓名");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
