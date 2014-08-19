using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Core.Mvc.Extensions
{
    /// <summary> 						            
    ///Created by :Tabandeh
    ///Created on Date :1392/7/13
    /// <remarks>Create Required validation for specified item</remarks>
    /// </summary>
    public class Required : ValidationBase
    {
        public override Dictionary<string, string> CreateRelatedValidation()
        {
            return new Dictionary<string, string> { { "data-val-required", Core.Resources.Messages.FillTheField } };
        }

    }

    public class PasswordChecker : ValidationBase
    {
        public override Dictionary<string, string> CreateRelatedValidation()
        {
            return new Dictionary<string, string> { 

                    {"data-val-length", Core.Resources.Messages.PasswordMustBeMoreThan5Character},

                    {"data-val-length-min", "5"},

                    {"data-val-regex", Core.Resources.Messages.PasswordMustBeContainCharactersAndDigits},

                    {"data-val-regex-pattern", @"\D+\d+|\d+\D+"}
           };
        }
    }

    public class StrengthChecker : ValidationAttribute
    {
        private int MinLenght { get; set; }

        private string Pattern { get; set; }

        public StrengthChecker(int minLenght = 5 , string pattern=@"\D+\d+|\d+\D+" )
        {
            this.MinLenght = minLenght;

            this.Pattern = pattern;

            this.ErrorMessage = Core.Resources.ExceptionMessage.PasswordIsNotValid;

        }

        public override bool IsValid(object value)
        {
            string str = (string)value;

            if (str.Length > this.MinLenght)
            {
                Regex rg = new Regex(this.Pattern);

                return rg.IsMatch(str);
            }

            return false;
        }
    }


    /// <summary> 						            
    ///Created by :Tabandeh
    ///Created on Date :1392/7/13
    /// <remarks>Create Range validation for specified item</remarks>
    /// </summary>
    public class Range : ValidationBase
    {
        private int Min { get; set; }
        private int Max { get; set; }
        public Range(int min, int max)
        {
            Max = max;
            Min = min;
        }

        /// <summary> 						            
        ///Created by :Tabandeh
        ///Created on Date :1392/7/13
        /// </summary>
        public override Dictionary<string, string> CreateRelatedValidation()
        {
            throw new NotImplementedException();

        }
    }

    /// <summary> 						            
    ///Created by :Tabandeh
    ///Created on Date :1392/7/13
    ///All custom validation are inherited from this class.
    /// </summary>
    public abstract class ValidationBase
    {
        public abstract Dictionary<string, string> CreateRelatedValidation();
    }

}
