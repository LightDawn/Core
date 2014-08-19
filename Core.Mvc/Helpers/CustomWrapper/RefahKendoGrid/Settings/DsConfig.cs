using System;
using Core.Mvc.Helpers.CustomWrapper.DataModel;

namespace Core.Mvc.Helpers.RefahKendoGrid.Settings
{
    [Serializable()]
    public class DsConfig
    {
        public DsConfig()
        {
            ModelRP = new ModelRP();
            //SchemaRP = schemaRP;
            CrudRP = new CrudRP();
            ServerRelated = new ServerRelInfo();
        }
        public ModelRP ModelRP { get; private set; }
        //public SchemaRP SchemaRP { get; private set; }
        public CrudRP CrudRP { get; private set; }
        public ServerRelInfo ServerRelated { get; private set; }
    } 
}
