using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

namespace QLHS.Models.CustomAttributes
{
    public class TenKhoaValidationAttribute : ValidationAttribute
    {
        public TenKhoaValidationAttribute()
        {
            ErrorMessage = "Sai định dạng. Định dạng phải: 'Khóa' + ' ' + ' yyyy - yyyy' và yyyy đầu >= 2023 và yyyy sau bằng yyyy đầu + 4";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            string tenKhoa = value.ToString();

            if (Regex.IsMatch(tenKhoa, @"^Khóa \d{4}-\d{4}$"))
            {
                int startYear = int.Parse(tenKhoa.Substring(5, 4));
                int endYear = int.Parse(tenKhoa.Substring(10, 4));

                if (startYear >= 2023 && endYear == startYear + 4)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}