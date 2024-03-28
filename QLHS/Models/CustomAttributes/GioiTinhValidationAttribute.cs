using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

namespace QLHS.Models.CustomAttributes
{
    public class GioiTinhValidationAttribute : ValidationAttribute
    {
        public GioiTinhValidationAttribute()
        {
            ErrorMessage = "Sai định dạng. Giới tính phải là 'Nam' hoặc 'Nữ'.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            string gioiTinh = value.ToString();

            if (gioiTinh.Equals("Nam", StringComparison.OrdinalIgnoreCase) || gioiTinh.Equals("Nữ", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}