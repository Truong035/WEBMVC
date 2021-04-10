namespace webMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class De_Thi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public De_Thi()
        {
            CauHoiDeThis = new HashSet<CauHoiDeThi>();
            Da_SVLuaChon = new HashSet<Da_SVLuaChon>();
        }

        [StringLength(50)]
        public string Ma_SV { get; set; }

        public long? Ma_BoDe { get; set; }

        public bool? TrangThai { get; set; }

        [Key]
        public long MaDeThi { get; set; }

        public virtual Bo_De Bo_De { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CauHoiDeThi> CauHoiDeThis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Da_SVLuaChon> Da_SVLuaChon { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
