using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Core.Mvc.Attribute.Validation;

namespace Core.Mvc.Attribute
{
    public  static class RegisterAttribute
    {
      

        public static void SetRegisterAttribute()
        {

            DataAnnotationsModelValidatorProvider.RegisterAdapter(
                typeof(RangeRp),
                typeof(RangeAttributeAdapter));

            DataAnnotationsModelValidatorProvider.RegisterAdapter(
                typeof(RequiredRp),
                typeof(RequiredAttributeAdapter));

            DataAnnotationsModelValidatorProvider.RegisterAdapter(
                typeof(StringLengthRp),
                typeof(StringLengthAttributeAdapter));
        }



       
    }



    
}


