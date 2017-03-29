#region File Header & Copyright Notice
//Copyright 2017 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion

using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace OpenDayApplication.ValidationRules
{
    public class StringPropertyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null && value != string.Empty && Regex.IsMatch((string)value, @"^[a-zA-Z]+$"))
                return new ValidationResult(true, null);
            else
                return new ValidationResult(false, "Please enter not null value.");
        }
    }
}
