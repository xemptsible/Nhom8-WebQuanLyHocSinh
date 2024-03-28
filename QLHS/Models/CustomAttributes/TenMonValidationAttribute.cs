using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

namespace QLHS.Models.CustomAttributes
{
    public class TenMonValidationAttribute : ValidationAttribute
    {
        public TenMonValidationAttribute()
        {
            ErrorMessage = "Sai định dạng. Tên môn không được chứa số, kí tự đặc biệt và phải bắt đầu bằng kí tự không khoảng trắng.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            string tenMon = value.ToString();

            if (Regex.IsMatch(tenMon, @"^[\p{L}\s]+$"))
            {
                if (!char.IsWhiteSpace(tenMon[0]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}