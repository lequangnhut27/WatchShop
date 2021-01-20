namespace WatchShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHoaDon")]
    public partial class ChiTietHoaDon
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHD { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDH { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0} VNĐ")]
        public int? Gia { get; set; }

        [DisplayName("Số lượng")]
        public int SoLuong { get; set; }

        public virtual DongHo DongHo { get; set; }

        public virtual HoaDon HoaDon { get; set; }
    }
}
