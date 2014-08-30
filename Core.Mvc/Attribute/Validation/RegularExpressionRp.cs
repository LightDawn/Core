using System.ComponentModel.DataAnnotations;

namespace Core.Mvc.Attribute.Validation
{
   
   public class RegularExpressionRp : RegularExpressionAttribute
    {
       public RegularExpressionRp(string pattern) : base(pattern)
       {
       }
    }
}
