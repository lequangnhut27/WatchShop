namespace WatchShop.Data.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WebBanDongHoContext : DbContext
    {
        public WebBanDongHoContext()
            : base("name=WebBanDongHoContext")
        {
        }

        public virtual DbSet<AccountManager> AccountManagers { get; set; }
        public virtual DbSet<ChiTietGiamGia> ChiTietGiamGias { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<DoiTuong> DoiTuongs { get; set; }
        public virtual DbSet<DongHo> DongHoes { get; set; }
        public virtual DbSet<DongHoThoiTrang> DongHoThoiTrangs { get; set; }
        public virtual DbSet<DongHoThongMinh> DongHoThongMinhs { get; set; }
        public virtual DbSet<HinhThucThanhToan> HinhThucThanhToans { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiGiamGia> LoaiGiamGias { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ThuongHieu> ThuongHieux { get; set; }
        public virtual DbSet<TinhTrangHoaDon> TinhTrangHoaDons { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountManager>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<AccountManager>()
                .Property(e => e.Pass)
                .IsUnicode(false);

            modelBuilder.Entity<AccountManager>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<AccountManager>()
                .Property(e => e.SoDT)
                .IsUnicode(false);

            modelBuilder.Entity<AccountManager>()
                .Property(e => e.AnhURL)
                .IsUnicode(false);

            modelBuilder.Entity<AccountManager>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.AccountManagers)
                .Map(m => m.ToTable("ManagerRoles").MapLeftKey("ManagerId").MapRightKey("RoleId"));

            modelBuilder.Entity<ChiTietGiamGia>()
                .Property(e => e.MaGG)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietGiamGia>()
                .Property(e => e.MaLoaiGG)
                .IsUnicode(false);

            modelBuilder.Entity<DoiTuong>()
                .Property(e => e.MaDoiTuong)
                .IsUnicode(false);

            modelBuilder.Entity<DongHo>()
                .Property(e => e.MaTH)
                .IsUnicode(false);

            modelBuilder.Entity<DongHo>()
                .Property(e => e.MaGG)
                .IsUnicode(false);

            modelBuilder.Entity<DongHo>()
                .HasMany(e => e.ChiTietHoaDons)
                .WithRequired(e => e.DongHo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DongHo>()
                .HasOptional(e => e.DongHoThoiTrang)
                .WithRequired(e => e.DongHo);

            modelBuilder.Entity<DongHo>()
                .HasOptional(e => e.DongHoThongMinh)
                .WithRequired(e => e.DongHo);

            modelBuilder.Entity<DongHoThoiTrang>()
                .Property(e => e.MaDoiTuong)
                .IsUnicode(false);

            modelBuilder.Entity<DongHo>()
                .Property(e => e.AnhChinhURL)
                .IsUnicode(false);

            modelBuilder.Entity<DongHo>()
                .Property(e => e.Anh1URL)
                .IsUnicode(false);

            modelBuilder.Entity<DongHo>()
                .Property(e => e.Anh2URL)
                .IsUnicode(false);

            modelBuilder.Entity<DongHo>()
                .Property(e => e.Anh3URL)
                .IsUnicode(false);

            modelBuilder.Entity<DongHo>()
                .Property(e => e.Anh4URL)
                .IsUnicode(false);

            modelBuilder.Entity<HinhThucThanhToan>()
                .Property(e => e.MaHTTT)
                .IsUnicode(false);

            modelBuilder.Entity<HinhThucThanhToan>()
                .HasMany(e => e.HoaDons)
                .WithRequired(e => e.HinhThucThanhToan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaHTTT)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaTTHD)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.ChiTietHoaDons)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.SoDT)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SoDT)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.AnhURL)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.HoaDons)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiGiamGia>()
                .Property(e => e.MaLoaiGG)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiGiamGia>()
                .HasMany(e => e.ChiTietGiamGias)
                .WithRequired(e => e.LoaiGiamGia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleId)
                .IsUnicode(false);

            modelBuilder.Entity<ThuongHieu>()
                .Property(e => e.MaTH)
                .IsUnicode(false);

            modelBuilder.Entity<ThuongHieu>()
                .Property(e => e.AnhURL)
                .IsUnicode(false);

            modelBuilder.Entity<TinhTrangHoaDon>()
                .Property(e => e.MaTTHD)
                .IsUnicode(false);

            modelBuilder.Entity<TinhTrangHoaDon>()
                .HasMany(e => e.HoaDons)
                .WithRequired(e => e.TinhTrangHoaDon)
                .WillCascadeOnDelete(false);
        }
    }
}
