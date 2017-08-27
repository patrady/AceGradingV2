using System.Windows.Controls;

namespace AceGrading
{

    public class StringToIntegerValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (int.TryParse(value.ToString(), out int i))
                return new ValidationResult(true, null);

            return new ValidationResult(false, "Please enter a valid number value.");
        }
    }


}
