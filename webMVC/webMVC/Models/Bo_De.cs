namespace webMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bo_De
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bo_De()
        {
            CauHois = new HashSet<CauHoi>();
            De_Thi = new HashSet<De_Thi>();
        }

        [Key]
        public long Ma_BoDe { get; set; }

        [Column(TypeName = "text")]
        public string NoiDung { get; set; }

        [StringLength(50)]
        public string Ma_NguoiTao { get; set; }

        public long? Ma_Mon { get; set; }

        public int? SoCau { get; set; }

        [StringLength(20)]
        public string ThoiGianThi { get; set; }

        public bool? Xoa { get; set; }

        public DateTime? NgayTao { get; set; }

        public int? LuotThich { get; set; }

        public int? LuotTai { get; set; }

        public int? Luuothi { get; set; }

        [Column(TypeName = "text")]
        public string LinkTai { get; set; }

        public virtual MonHoc MonHoc { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CauHoi> CauHois { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<De_Thi> De_Thi { get; set; }
    }
}
