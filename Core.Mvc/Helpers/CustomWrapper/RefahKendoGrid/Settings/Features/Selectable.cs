using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mvc.Helpers.RefahKendoGrid
{
    [Flags]
    [Serializable()]
    public enum Selectable
    {
        Row,
        Cell,
        MultipleRows,
        MultipleCells,
        None
    }
}
