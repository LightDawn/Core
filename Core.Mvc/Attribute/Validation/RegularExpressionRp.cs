using System.ComponentModel.DataAnnotations;

namespace Core.Mvc.Attribute.Validation
{
    /// <summary>
    //Create By:pouyan
    //Date:1392/04/18
    //Description:This Class is Custom Attribute For RegularExpression
    /// </summary>
   public class RegularExpressionRp : RegularExpressionAttribute
    {
       public RegularExpressionRp(string pattern) : base(pattern)
       {
       }
    }
}
