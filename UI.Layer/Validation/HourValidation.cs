using System;
using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace UI.Layer.Validation
{
    public class HourValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (Regex.IsMatch(value + "", "^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$"))
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, "");
                }
            }
            catch (Exception)
            {
                return new ValidationResult(false, "");
            }
        }
    }
}