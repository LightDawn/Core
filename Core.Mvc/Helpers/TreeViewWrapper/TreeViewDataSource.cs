using Core.Mvc.Helpers.CustomWrapper.DataModel;
using Core.Mvc.Helpers.CustomWrapper.DataSource;
using Core.Mvc.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Core.Mvc.Helpers
{
    public class TreeViewDataSource : DataSourceBase
    {
        //private dynamic _model;

        // private TreeViewDataSourceSchema Schema;

        private DataSourceInfo _dataSourceInfo;


        public TreeViewDataSource(TreeInfo info, string name, dynamic modelValues)
        {
            this.ID = name;

            //  this._model = modelValues;

            this._dataSourceInfo = info.DataSource;

            initializeSchema();

             base.Type = null;

            base.Events = base.AssignDsEvents(_dataSourceInfo.DataSourceEvents ?? new Dictionary<DataSourceEvent, object>());

            _dataSourceInfo.CrudRP.Read.Data = info.FunctionName;

            base.Transport = CreateTransportObject(_dataSourceInfo.CrudRP);
        }

        private void initializeSchema()
        {
            // this.Schema = new TreeViewDataSourceSchema(); 

            base.Schema.Total = null;

            base.Schema.Data = null;

            // var modelSchema = new ViewModel.Descriptor(_dataSourceInfo.ModelRP.ModelType, _model);
            // var modelSchema = new Kendo.Mvc.UI.ModelDescriptor(_model.GetType() );
            //  base.Schema.Model = new ViewModel.Descriptor(_dataSourceInfo.ModelRP.ModelType, _model);
            base.Schema.Model = new Kendo.Mvc.UI.ModelDescriptor(_dataSourceInfo.ModelRP.ModelType);

            // base.Schema.Model.Id = new DataKeyRP(_dataSourceInfo.ModelRP.ModelIdName);

            base.Schema.Model.Id = (!string.IsNullOrEmpty(_dataSourceInfo.ModelRP.ModelIdName)) ?
                new DataKeyRP(_dataSourceInfo.ModelRP.ModelIdName) : new DataKeyRP("Id");

        }

        private TransportBase CreateTransportObject(CrudRP info)
        {
            var transport = new TreeViewTransport();

            if (string.IsNullOrEmpty(info.Read.Url))
            {
                throw new Exception(Core.Resources.ExceptionMessage.URLCanNotBeEmpty);
            }

            transport.Read.Url = info.Read.Url;

            if (!string.IsNullOrEmpty(info.Read.Data))
            {
                transport.Read.Data = new Kendo.Mvc.ClientHandlerDescriptor { HandlerName = info.Read.Data };
            }

            transport.Create.Url = string.IsNullOrEmpty(info.Insert.Url) ? transport.Read.Url : info.Insert.Url;

            transport.Update.Url = string.IsNullOrEmpty(info.Update.Url) ? transport.Read.Url : info.Update.Url;

            transport.Destroy.Url = string.IsNullOrEmpty(info.Remove.Url) ? transport.Read.Url : info.Remove.Url;

            return transport;
        }

        public override object BuildDefaultOnRequestEndEvent(string customOnRequestEndFunction)
        {

            var OnRequestEndFunc = string.Format("function {0}_requestEnd(args) {{  if( args.response != undefined ) $('#{0}').prev('.k-grouping-header').hide();" +
                                                    " {1} " +
                                                    "}}"
                                                    , this.ID, customOnRequestEndFunction != string.Empty ? "eval('" + customOnRequestEndFunction + "(args);');" : string.Empty);

            return OnRequestEndFunc;
        }

        public override object BuildDefaultOnErrorEvent(string customOntErrorFunction)
        {
            //var OnErrorFunc = string.Format("function {0}_error(args) {{ debugger; var dataSource= args.sender; var data= args.sender._data;" +
            //    " $.each(data, function(index, model){{ " +
            //    "if (model.isNew()|| model.dirty) {{dataSource.cancelChanges(model);}} " +
            //    "}} ); " +
            //    "TreeView.refresh({0});" +
            //                                         " {1} " +
            //                                         "}}"
            //                                         , this.ID, customOntErrorFunction != string.Empty ? "eval('" + customOntErrorFunction + "(args);');" : string.Empty);

           
            var OnErrorFunc = string.Format("function {0}_error(args) {{ TreeView.error(args); " +      
                                                     " {1} " +
                                                     "}}"
                                                     , this.ID, customOntErrorFunction != string.Empty ? "eval('" + customOntErrorFunction + "(args);');" : string.Empty);

            return OnErrorFunc;
        }

        public override object BuildDefaultOnRequestStartEvent(string customOnRequestStartFunction)
        {
            var OnRequestStartFunc = string.Format("function {0}_requestStart(args) {{ " +
                                                    " {1} " +
                                                    "}}"
                                                    , this.ID, customOnRequestStartFunction != string.Empty ? "eval('" + customOnRequestStartFunction + "(args);');" : string.Empty);

            return OnRequestStartFunc;
        }

        //protected override void Serialize(IDictionary<string, object> json)
        //{
        //    if (_model is TreeViewModelBase)
        //    {
        //        _model.ToJson();
        //    }
        //    base.Serialize(json);


        //}
    }
}
