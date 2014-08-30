using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Mvc.Attribute.Validation
{
   
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
