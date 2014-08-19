using System.Collections.Generic;
using System.Globalization;
using Core.Mvc.ViewModels;
using System;
using Core.Mvc.Helpers.CustomWrapper.DataSource;
using Core.Mvc.Helpers.CustomWrapper.DataModel;

namespace Core.Mvc.Helpers.RefahKendoGrid.Settings
{
    [Serializable()]
    public class GridInfo : IViewInfo
    {
        private CultureInfo _cultureInfo;
        public GridInfo()
            : this(null,null)
        { }
        public GridInfo(AccessOperation crudOperation = null, Toolbar gridToolbar = null)
        {
            //_cultureInfo = new CultureInfo(culture);
            //ID = Id;
            
            if (crudOperation == null)
            {
                CRUDOperation = new AccessOperation();
            }
            else
            {
                CRUDOperation = crudOperation;
            }
            GridToolbar = gridToolbar;
            if (GridToolbar == null)
            {
                GridToolbar = new Toolbar(true);
            }
            else
            {
                GridToolbar.CRUDOperation = CRUDOperation;
                if (GridToolbar != null && GridToolbar.Commands != null && GridToolbar.Commands.Count > 0)
                {
                    GridToolbar.Commands = GridToolbar.GetDefaultCommandList(GridToolbar.Commands);
                }
            } 
        }

        public string Width { get; set; }
        public string Hieght { get; set; }
        public List<Column> ColumnsInfo { get; set; }
        public DataSourceInfo DataSource { get; set; }
        public RefahKendoGrid.Features Features { get; set; }
        internal Toolbar GridToolbar { get; set; }

        public string GridID { get; internal set; }


        public RefahKendoGrid.ClientDependentFeature ClientDependentFeatures { get; internal set; }

        internal AccessOperation CRUDOperation { get; set; }

        internal CultureInfo GetCultureInfo()
        {
            return _cultureInfo;
        }

        //private static string _gridID;
        //public static string GridID { get { return _gridID; } set { lock (value) { _gridID = value; } } }
    }
}
