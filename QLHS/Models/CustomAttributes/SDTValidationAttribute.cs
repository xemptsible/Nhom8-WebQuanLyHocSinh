using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

namespace QLHS.Models.CustomAttributes
{
    public class SDTValidationAttribute : ValidationAttribute
    {
        public SDTValidationAttribute()
        {
            ErrorMessage = "Sai định dạng. Số điện thoại không hợp lệ.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            string sdt = value.ToString();

            // Số điện thoại phải có 10 chữ số và bắt đầu bằng số 0
            if (Regex.IsMatch(sdt, @"^0\d{9}$"))
            {
                return true;
            }

            return false;
        }
    }
}