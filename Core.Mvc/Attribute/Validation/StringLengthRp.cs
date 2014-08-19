﻿using System.ComponentModel.DataAnnotations;

namespace Core.Mvc.Attribute.Validation
{
    /// <summary>
    //Create By:pouyan
    //Date:1392/04/18
    //Description:This Class is Custom Attribute For StringLength
    /// </summary>
    public class StringLengthRp : StringLengthAttribute
    {
        public StringLengthRp(int maximumLength) : base(maximumLength)
        {
        }
    }
}
