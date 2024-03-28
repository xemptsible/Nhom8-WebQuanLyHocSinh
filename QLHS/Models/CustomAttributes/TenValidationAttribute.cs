using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

namespace QLHS.Models.CustomAttributes
{
    public class TenValidationAttribute : ValidationAttribute
    {
        public TenValidationAttribute()
        {
            ErrorMessage = "Sai định dạng. Tên không được chứa số, kí tự đặc biệt và phải bắt đầu bằng kí tự không khoảng trắng.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            string ten = value.ToString();

            if (Regex.IsMatch(ten, @"^[\p{L}\s]+$"))
            {
                if (!char.IsWhiteSpace(ten[0]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}