using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Core.Mvc.Attribute.Validation;

namespace Core.Mvc.Attribute
{
    public  static class RegisterAttribute
    {
        /// <summary>
        //Created By:pouyan
        //Date:1392/04/15
        //Description:This Class Registers All Custom Attribute Validation ...It Calls In Application_Start
        /// </summary>

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


