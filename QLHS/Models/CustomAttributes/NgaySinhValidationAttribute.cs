using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

namespace QLHS.Models.CustomAttributes
{
    public class NgaySinhValidationAttribute : ValidationAttribute
    {
        public NgaySinhValidationAttribute()
        {
            ErrorMessage = "Sai định dạng. Ngày sinh phải là ngày hợp lệ với định dạng 'dd/mm/yyyy' và năm phải nhỏ hơn 2023.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            if (DateTime.TryParseExact(value.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                // Check if the year is less than 2023
                if (parsedDate.Year < 2023)
                {
                    return true;
                }
            }

            return false;
        }
    }
}