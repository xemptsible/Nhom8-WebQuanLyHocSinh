using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

namespace QLHS.Models.CustomAttributes
{
    public class MatKhauValidationAttribute : ValidationAttribute
    {
        public MatKhauValidationAttribute()
        {
            ErrorMessage = "Sai định dạng. Mật khẩu phải chứa ít nhất một kí tự thường, một kí tự in hoa và ít nhất một số.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            string matKhau = value.ToString();

            // Check if the password contains at least one lowercase letter, one uppercase letter, and at least one digit
            if (Regex.IsMatch(matKhau, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$"))
            {
                return true;
            }

            return false;
        }
    }
}