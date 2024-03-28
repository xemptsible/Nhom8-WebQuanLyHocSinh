using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

namespace QLHS.Models.CustomAttributes
{
    public class NamValidationAttribute : ValidationAttribute
    {
        public NamValidationAttribute()
        {
            ErrorMessage = "Năm phải lớn hơn hoặc bằng 2023.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            if (int.TryParse(value.ToString(), out int nam))
            {
                if (nam >= 2023)
                {
                    return true;
                }
            }

            return false;
        }
    }
}