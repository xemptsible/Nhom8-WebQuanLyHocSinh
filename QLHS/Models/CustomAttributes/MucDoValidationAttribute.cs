using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

namespace QLHS.Models.CustomAttributes
{
    public class MucDoValidationAttribute : ValidationAttribute
    {
        public MucDoValidationAttribute()
        {
            ErrorMessage = "Số lượng phải lớn hơn 1 hoặc bé hơn 3.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            if (int.TryParse(value.ToString(), out int soLuong))
            {
                if (soLuong >= 1 && soLuong <=3)
                {
                    return true;
                }
            }

            return false;
        }
    }
}