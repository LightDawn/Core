using System.ComponentModel.DataAnnotations;

namespace Core.Mvc.Attribute.Validation
{
  
    public class StringLengthRp : StringLengthAttribute
    {
        public StringLengthRp(int maximumLength) : base(maximumLength)
        {
        }
    }
}
