﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLHS.Models
{
    using QLHS.Models.CustomAttributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class KhoaHoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhoaHoc()
        {
            this.HocSinhs = new HashSet<HocSinh>();
        }

        public int MaKhoa { get; set; }
        [TenKhoaValidation]
        [Required(ErrorMessage = "Tên Khóa là trường bắt buộc.")]
        public string TenKhoa { get; set; }

        public object Clone()
        {
            return new KhoaHoc
            {
                MaKhoa = this.MaKhoa,
                TenKhoa = this.TenKhoa
            };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HocSinh> HocSinhs { get; set; }
    }
}