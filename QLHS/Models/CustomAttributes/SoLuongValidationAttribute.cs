using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

namespace QLHS.Models.CustomAttributes
{
    public class SoLuongValidationAttribute : ValidationAttribute
    {
        public SoLuongValidationAttribute()
        {
            ErrorMessage = "Số lượng phải lớn hơn 1.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            if (int.TryParse(value.ToString(), out int soLuong))
            {
                if (soLuong > 1 && soLuong <=50)
                {
                    return true;
                }
            }

            return false;
        }
    }
}