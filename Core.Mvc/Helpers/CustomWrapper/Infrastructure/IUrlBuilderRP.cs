using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mvc.Helpers.RefahKendoGrid.Infrastructure
{
    public interface IUrlBuilderRP
    {
         string ControllerName { get; set; }

         string ActionName { get; set; }

         string Url { get; set; }
    }
}
