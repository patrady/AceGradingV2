using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace AceGrading
{
    public class StringToDoubleValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (double.TryParse(value.ToString(), out double i))
                return new ValidationResult(true, null);

            return new ValidationResult(false, "Please enter a valid number value.");
        }
    }
}
