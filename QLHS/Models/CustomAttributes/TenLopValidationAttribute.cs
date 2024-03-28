using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

namespace QLHS.Models.CustomAttributes
{
    public class TenLopValidationAttribute : ValidationAttribute
    {
        public TenLopValidationAttribute()
        {
            ErrorMessage = "Sai định dạng. Định dạng phải là 'số/số' và số phải nằm trong khoảng từ 1 đến 12.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            string tenLop = value.ToString();

            string[] parts = tenLop.Split('/');
            if (parts.Length == 2)
            {
                if (int.TryParse(parts[0], out int so1) && int.TryParse(parts[1], out int so2))
                {
                    if (so1 >= 1 && so1 <= 12 && so2 >= 1 && so2 <= 12)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}