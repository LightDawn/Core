using Core.Mvc.Helpers.CustomWrapper.Infrastructure;
using Core.Mvc.Helpers.RefahKendoGrid.Infrastructure;
using Kendo.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mvc.Helpers.RefahKendoGrid.Settings.Features
{
    [Serializable()]
    public class FilterOperatorEnum : JsonObjectBase
    {
        public FilterOperatorEnum(CultureInfo cultureInfo)
        {

        }


        protected override void Serialize(IDictionary<string, object> json)
        {
            json["eq"] = "برابر با";
            json["neq"] = "نا برابر با";
        }
    }
}
