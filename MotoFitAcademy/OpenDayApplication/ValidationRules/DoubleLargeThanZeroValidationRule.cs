#region File Header & Copyright Notice
//Copyright 2017 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion

using System.Globalization;
using System.Windows.Controls;

namespace OpenDayApplication.ValidationRules
{
    public class DoubleLargeThanZeroValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double d;
            bool result = double.TryParse(value.ToString(), out d);
            if (result && d>0)
                return new ValidationResult(true, null);
            else
                return new ValidationResult(false, "Please enter correct value.");
        }
    }
}
