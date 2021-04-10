using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace webMVC.Models
{
    public partial class TracnghiemonlineDB : DbContext
    {
        public TracnghiemonlineDB()
            : base("name=TracnghiemonlineDB")
        {
        }

        public virtual DbSet<Bo_De> Bo_De { get; set; }
        public virtual DbSet<CauHoi> CauHois { get; set; }
        public virtual DbSet<CauHoiDeThi> CauHoiDeThis { get; set; }
        public virtual DbSet<Da_SVLuaChon> Da_SVLuaChon { get; set; }
        public virtual DbSet<Dap_AN> Dap_AN { get; set; }
        public virtual DbSet<De_Thi> De_Thi { get; set; }
        public virtual DbSet<Kho_CauHoi> Kho_CauHoi { get; set; }
        public virtual DbSet<MonHoc> MonHocs { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bo_De>()
                .Property(e => e.NoiDung)
                .IsUnicode(false);

            modelBuilder.Entity<Bo_De>()
                .Property(e => e.Ma_NguoiTao)
                .IsFixedLength();

            modelBuilder.Entity<Bo_De>()
                .Property(e => e.LinkTai)
                .IsUnicode(false);

            modelBuilder.Entity<Bo_De>()
                .HasMany(e => e.CauHois)
                .WithRequired(e => e.Bo_De)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<De_Thi>()
                .Property(e => e.Ma_SV)
                .IsFixedLength();

            modelBuilder.Entity<Kho_CauHoi>()
                .HasMany(e => e.CauHois)
                .WithRequired(e => e.Kho_CauHoi)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kho_CauHoi>()
                .HasMany(e => e.CauHoiDeThis)
                .WithOptional(e => e.Kho_CauHoi)
                .HasForeignKey(e => e.MaCauHoi);

            modelBuilder.Entity<MonHoc>()
                .HasMany(e => e.Bo_De)
                .WithOptional(e => e.MonHoc)
                .HasForeignKey(e => e.Ma_Mon);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.TaiKhoan1)
                .IsFixedLength();

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MatKhau)
                .IsFixedLength();

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.Bo_De)
                .WithOptional(e => e.TaiKhoan)
                .HasForeignKey(e => e.Ma_NguoiTao);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.De_Thi)
                .WithOptional(e => e.TaiKhoan)
                .HasForeignKey(e => e.Ma_SV);
        }
    }
}
