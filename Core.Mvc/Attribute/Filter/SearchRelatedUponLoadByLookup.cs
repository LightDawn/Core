using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mvc.Attribute.Filter
{
    public class SearchRelatedUponLoadByLookupAttribute : System.Attribute 
    {

        public SearchRelatedUponLoadByLookupAttribute(
            string   lookUpName, 
            string   lookupGridName , 
            string   lookupRelatedId ,
            string   lookupViewModel , 
            string   iViewInfoProperty ,
            string[] lookUpGridFor , 
            string[] lookUpTreeFor , 
            string[] isMultiSelectFor)
        {
            
        }
    }
}
