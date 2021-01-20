namespace WatchShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietGiamGia")]
    public partial class ChiTietGiamGia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChiTietGiamGia()
        {
            DongHoes = new HashSet<DongHo>();
        }

        [Key]
        [StringLength(10)]
        public string MaGG { get; set; }

        [Required]
        [StringLength(10)]
        public string MaLoaiGG { get; set; }

        public float SoLieuGiamGia { get; set; }

        [StringLength(100)]
        [DisplayName("Giảm giá")]
        [DisplayFormat(NullDisplayText = "Không giảm")]
        public string MoTa { get; set; }

        public virtual LoaiGiamGia LoaiGiamGia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DongHo> DongHoes { get; set; }
    }
}
