namespace WatchShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DoiTuong")]
    public partial class DoiTuong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DoiTuong()
        {
            DongHoThoiTrangs = new HashSet<DongHoThoiTrang>();
        }

        [Key]
        [StringLength(10)]
        public string MaDoiTuong { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Đối tượng sử dụng")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string TenDoiTuong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DongHoThoiTrang> DongHoThoiTrangs { get; set; }
    }
}
