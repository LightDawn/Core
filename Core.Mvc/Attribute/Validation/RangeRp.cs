using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Mvc.Attribute.Validation
{
    /// <summary>
    //Create By:pouyan
    //Date:1392/04/18
    //Description:This Class is Custom Attribute For Range
    /// </summary>
    public class RangeRp : RangeAttribute
    {

        public RangeRp(int minimum, int maximum) : base(minimum, maximum)
        {
        }

        public RangeRp(double minimum, double maximum) : base(minimum, maximum)
        {
        }

        public RangeRp(Type type, string minimum, string maximum) : base(type, minimum, maximum)
        {
        }
    }

    
}
