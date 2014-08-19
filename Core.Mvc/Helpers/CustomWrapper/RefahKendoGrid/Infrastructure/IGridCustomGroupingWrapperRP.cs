using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mvc.Helpers.RefahKendoGrid.Infrastructure
{
    public interface IGridCustomGroupingWrapperRP
    {
        IEnumerable GroupedEnumerable { get; }
    }
}
