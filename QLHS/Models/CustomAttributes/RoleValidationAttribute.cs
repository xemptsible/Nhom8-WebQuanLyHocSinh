using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

namespace QLHS.Models.CustomAttributes
{
    public class RoleValidationAttribute : ValidationAttribute
    {
        public RoleValidationAttribute()
        {
            ErrorMessage = "Sai định dạng. Vai Trò phải là 'Admin' hoặc 'GV'.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            string gioiTinh = value.ToString();

            if (gioiTinh.Equals("Admin", StringComparison.OrdinalIgnoreCase) || gioiTinh.Equals("GV", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}