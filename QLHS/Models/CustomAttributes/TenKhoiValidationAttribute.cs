using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

namespace QLHS.Models.CustomAttributes
{
    public class TenKhoiValidationAttribute : ValidationAttribute
    {
        public TenKhoiValidationAttribute()
        {
            ErrorMessage = "Sai định dạng. Định dạng phải: 'Khối' + ' ' + 'số >=1 || <=12'";
        }
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            string tenKhoi = value.ToString();

            if (Regex.IsMatch(tenKhoi, @"^Khối (1[0-2]|[1-9])$"))
            {
                return true;
            }

            return false;
        }
    }
}