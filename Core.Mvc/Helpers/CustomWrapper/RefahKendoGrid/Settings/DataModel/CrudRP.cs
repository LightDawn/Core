﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Mvc.Helpers.RefahKendoGrid.Settings.DataModel;

namespace Core.Mvc.Helpers.RefahKendoGrid.Settings
{
    [Serializable()]
    public class CrudRP
    {
        public CrudInfo Read { get; private set; }
        public CrudInfoEdit Update { get; private set; }
        public CrudInfoEdit Remove { get; private set; }
        public CrudInfo Insert { get; private set; }
        public CrudRP()
        {
            Read = new CrudInfo();
            Update = new CrudInfoEdit();
            Remove = new CrudInfoEdit();
            Insert = new CrudInfo();
        }
    }
}
