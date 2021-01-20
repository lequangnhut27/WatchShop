namespace WatchShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThuongHieu")]
    public partial class ThuongHieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThuongHieu()
        {
            DongHoes = new HashSet<DongHo>();
        }

        [Key]
        [StringLength(10)]
        public string MaTH { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Thương hiệu")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string TenTH { get; set; }

        [StringLength(20)]
        [DisplayName("Quốc gia")]
        public string QuocGia { get; set; }

        [StringLength(100)]
        [DisplayName("Ảnh")]
        public string AnhURL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DongHo> DongHoes { get; set; }
    }
}
