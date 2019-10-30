using System.Globalization;
using System.Windows.Controls;

namespace UI.Layer.Validation
{
    public class StringEmptyValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!string.IsNullOrEmpty(value + ""))
                return ValidationResult.ValidResult;
            return new ValidationResult(false, "");
        }
    }
}