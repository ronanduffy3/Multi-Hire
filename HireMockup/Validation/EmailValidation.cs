using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HireMockup.Validation
{
    public class EmailValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // Is a valid email address?
            var pattern =
                @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*";
            if (!Regex.IsMatch((string)value, pattern))
            {
                var msg = $"{value} is not a valid email address.";
                return new ValidationResult(false, msg);
            }

            // Email address is valid
            return new ValidationResult(true, null);
        }

        public static Boolean ValidateEmail(string email)
        {
            // Is a valid email address?
            var pattern =
                @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*";
            if (!Regex.IsMatch((string)email, pattern))
            {
                var msg = $"{email} is not a valid email address.";
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
