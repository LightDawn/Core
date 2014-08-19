using Core.Mvc.Helpers.CustomWrapper.DataSource;
using Core.Mvc.Helpers.RefahKendoGrid.Settings.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mvc.Helpers.RefahKendoGrid.Settings
{
    [Serializable()]
    public class DataSourceInfo 
    {
        public DataSourceInfo()
        {
            ModelRP = new ModelRP();
            CrudRP = new CrudRP();
            ServerRelated = new ServerRelInfo();
            ServerRelated.DSType = DataSourceType.Ajax;
            DataSourceEvents = new Dictionary<DataSourceEvent, object>();
        }
        public ModelRP ModelRP { get; private set; }
        //public SchemaRP SchemaRP { get; private set; }
        public CrudRP CrudRP { get; private set; }
        public ServerRelInfo ServerRelated { get; private set; }
        public Dictionary<DataSourceEvent, object> DataSourceEvents { get; private set; }



        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

}
