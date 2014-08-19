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
    public class Pageability : JsonObjectBase
    {
        //private CultureInfo _cultureInfo;
        public Pageability(CultureInfo cultureInfo)
        {
            PageSizesConfig = new PageSizesConfig(cultureInfo);
            PageMessages = new PageMessage(this , cultureInfo);
            AcceptsPageConfig = true;
            PageSize = 20;
            ApplyRefresh = false;
            PageInfo = true;
            AcceptsInput = false;
        }

        public bool AcceptsPageConfig { get; set; }
        public int PageSize { get; set; }
        public PageMessage PageMessages { get; set; }
        public bool ApplyRefresh { get; set; }
        public bool PageInfo { get; set; }
        public bool AcceptsInput { get; set; }//It Makes sense in existence of Property Page of PageMessages Class
        public PageSizesConfig PageSizesConfig { get; set; }

        protected override void Serialize(IDictionary<string, object> json)
        {
            json["refresh"] = ApplyRefresh;
            json["pageSize"] = PageSize == 0 ? 2: PageSize;
            json["messages"] = PageMessages.ToJson();
        }
    }
}

