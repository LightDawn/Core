
using Core.Mvc.Helpers.RefahKendoGrid.Infrastructure;
using Core.Mvc.Helpers.RefahKendoGrid.Settings;
using Core.Mvc.Helpers.CustomWrapper.DataModel;
using Core.Mvc.Helpers.RefahKendoGrid.Settings.Features;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Core.Cmn.Extensions;
using Core.Mvc.ViewModel;
using Core.Mvc.Extensions;
using Core.Model;
using Core.Mvc.Helpers.RefahKendoGrid.Settings.ColumnConfig;
using System.Web.Mvc.Html;
using System.Web.Script.Serialization;
using Core.Service;
using Core.Mvc.Helpers.CustomWrapper.Infrastructure;
using Core.Mvc.Helpers.CustomWrapper.DataSource;
using Core.Mvc.Helpers.ElementAuthentication;

namespace Core.Mvc.Helpers.RefahKendoGrid
{
    /// <summary>
    ///کلاسی که وظیفه تولید گرید کندو را به عهده دارد. 
    /// </summary>
    [Serializable()]
    public class GridRP<T> where T : new() 
    {
        private Dictionary<string, object> _totalConfig;
        private List<Column> _columnsConfig;
        private DataSourceGridRP _dataSource;
        private Features _features;
        private ClientDependentFeature _clientDependency;
        private GridEvents Events;
        private Toolbar _toolbar;
        public string ID { get; private set; }
        private Type _modelType;
        //private HtmlHelper _helper;
        private ModelMetadata _modelMetaData;
        private string _removableStr;
        private string _width;
        private string _height;
        private bool _hasCommonAddOrEditScriptAppended;
        private AccessOperation _accessOperation;
        /// <summary>
        /// سازنده گرید 
        /// </summary>
        /// <param name="helper">متغیری که از نوع اچ تی ام ال هلپر است</param>
        /// <param name="gridId">شناسه گرید مربوطه</param>
        /// <param name="gridInfo">حاوی اطلاعات و ویژگیهای مربوط به گرید</param>
        /// <param name="clientDependency">  حاوی ویژگیهای مربوط به گرید که در سمت فایل تعریف گرید به کار می رود.</param>
        /// <param name="Width">طول گرید</param>
        /// <param name="Height">ارتفاع گرید</param>
        public GridRP(HtmlHelper helper, string gridId, GridInfo gridInfo , Type viewModelType = null , ClientDependentFeature clientDependency = null , string Width = null , string Height = null )
        {
            _modelMetaData = helper.ViewData.ModelMetadata;
            _accessOperation = new AccessOperation();
            BuildUniqueIDForGrid(gridId, helper);
            gridInfo.GridID = ID;
            _width = Width;
            _height = Height;
            _modelMetaData = helper.ViewData.ModelMetadata;
            //_helper = helper;
            _features = gridInfo.Features;
            _totalConfig = new Dictionary<string, object>();

            if (typeof(IViewModelBase).IsAssignableFrom(typeof(T)) && gridInfo.DataSource.ModelRP.ModelType == null && gridInfo.ColumnsInfo == null && viewModelType == null)
            {
                var modelType = typeof(T);
                gridInfo.ColumnsInfo = modelType.BiuldColumnsFromViewModel();
                _modelType = modelType;
            }

            else if (!typeof(IViewModelBase).IsAssignableFrom(typeof(T))  && viewModelType != null)
            {
                if (typeof(IViewModelBase).IsAssignableFrom(viewModelType))
                {
                    var modelType = viewModelType;
                    //gridInfo.ColumnsInfo = modelType.BiuldColumnsFromViewModel();
                    _modelType = modelType;
                }
                
            }

            //if (typeof(IViewModelBase).IsAssignableFrom(typeof(T)) && gridInfo.DataSource.ModelRP.ModelType != null && gridInfo.ColumnsInfo == null)
            //{
            //}
            //if (!typeof(IViewModelBase).IsAssignableFrom(typeof(T)) && gridInfo.DataSource.ModelRP.ModelType != null && gridInfo.ColumnsInfo == null)
            //{
            //}
            else if ((!typeof(IViewModelBase).IsAssignableFrom(typeof(T)) || gridInfo.DataSource.ModelRP.ModelType != null) && gridInfo.ColumnsInfo != null)
            {
                var modelType = typeof(T);
                _modelType = modelType;
            }
            else if ((typeof(IViewModelBase).IsAssignableFrom(typeof(T)) || gridInfo.DataSource.ModelRP.ModelType == null) && gridInfo.ColumnsInfo != null)
            {
                var modelType = typeof(T);
                _modelType = modelType;
            }
            else if (!typeof(IViewModelBase).IsAssignableFrom(typeof(T)) && gridInfo.DataSource.ModelRP.ModelType != null)
            {
                _modelType = gridInfo.DataSource.ModelRP.ModelType;
            }

            _features.SetCulture(gridInfo.GetCultureInfo());
            _features.EditableConfig.CustomConfig.Template.PartialViewModel = _modelType;
            _features.EditableConfig.CustomConfig.Template.CorrespondingHtmlHelper = helper;
            _columnsConfig = gridInfo.ColumnsInfo;
            gridInfo.DataSource.ModelRP.ModelType = _modelType;
            gridInfo.DataSource.CrudRP.Read.QueryStringItems = clientDependency.ReadQueryParams;
            gridInfo.DataSource.CrudRP.Read.ParamsFunction = clientDependency.PreReadFunction;
            _dataSource = BuildDataSourceObject(gridInfo.DataSource);
            DefineActionAuthority();
            MakeCUDUrls(_dataSource.Transport, gridInfo.DataSource.CrudRP);
            
            if (_features.ReadOnly)
            {
                _accessOperation.InsertableOrUpdatable = false;
            }

            if (!_features.ReadOnly && !_accessOperation.Insertable && !_accessOperation.Updatable)
            {
                _accessOperation.InsertableOrUpdatable = false;
            }

            if (clientDependency != null)
            {
                _clientDependency = clientDependency;
            }

            _toolbar = gridInfo.GridToolbar;
        }

        /// <summary>
        /// مجوز مربوط به عملیات گرید(ورود داده جدید ، ویرایش داده، و حذف) را تعیین می کند.
        /// </summary>
        private void DefineActionAuthority()
        {
            var url= this._dataSource.Transport.Read.Url.ToLower();
            _accessOperation.Insertable = _features.Insertable;
            _accessOperation.Updatable = _features.Updatable;
            _accessOperation.Removable = _features.Removable;
            UserAccessibleElement.DefineCrudActionAuthority(url, _accessOperation);
        }

        private void BuildUniqueIDForGrid(string gridId, HtmlHelper helper)
        {
            ID = gridId;
        }
        /// <summary>
        /// ساخت شیئ دیتا سورس 
        /// </summary>
        /// <param name="dsConfig"></param>
        /// <returns></returns>
        private DataSourceGridRP BuildDataSourceObject(DataSourceInfo dsConfig)
        {
            //dynamic modelMetaData = ModelMeat;
            DataSourceGridRP ds = null;
            if (typeof(T) == null)
            {
                ds = new DataSourceGridRP();
            }
            else
            {
                ds = new DataSourceGridRP(typeof(T));
            }
           
            ds.ID = this.ID;
            ds.ServerSorting = dsConfig.ServerRelated.ServerSorting == null ? true : dsConfig.ServerRelated.ServerSorting.Value;
            ds.ServerPaging = dsConfig.ServerRelated.ServerPaging == null ? true : dsConfig.ServerRelated.ServerSorting.Value;
            ds.ServerFiltering = dsConfig.ServerRelated.ServerFiltering == null ? true : dsConfig.ServerRelated.ServerSorting.Value;
            ds.Schema.Model = new ModelDescriptor(dsConfig.ModelRP.ModelType);
            ds.Events = ds.AssignDsEvents(dsConfig.DataSourceEvents);

            ds.DefineDataSourceModelKey(dsConfig);

            if (!string.IsNullOrEmpty(dsConfig.CrudRP.Read.Url))
            {
                ds.Transport .Read.Url = dsConfig.CrudRP.Read.Url;

                if (dsConfig.CrudRP.Read.QueryStringItems.Count > 0)
                {
                    ds.Transport.Read.Params = dsConfig.CrudRP.Read.QueryStringItems;
                }
                if (!string.IsNullOrEmpty(dsConfig.CrudRP.Read.ParamsFunction))
                {
                    ds.Transport.Read.ReadFunction = dsConfig.CrudRP.Read.ParamsFunction;
                }
            }

             //dsConfig.CrudRP.Read.Data


            return ds;
        }
        /// <summary>
        /// یو آر آل های مربوط عملیات درج، ویرایش و حذف ساخت
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="crudRP"></param>
        private void MakeCUDUrls(TransportBase ts, CrudRP crudRP)
        {
            if (!_features.ReadOnly)
            {
                if (_accessOperation.Insertable)
                {
                    ts.Create.Url = string.IsNullOrEmpty(crudRP.Insert.Url) ? crudRP.Read.Url : crudRP.Insert.Url;
                }

                if (_accessOperation.Updatable)
                {
                    ts.Update.Url = string.IsNullOrEmpty(crudRP.Update.Url) ? crudRP.Read.Url : crudRP.Update.Url;
                }

                if (_accessOperation.Removable)
                {
                    ts.Destroy.Url = string.IsNullOrEmpty(crudRP.Remove.Url) ? crudRP.Read.Url : crudRP.Remove.Url;
                }
            }
        }
        /// <summary>
        /// تولید و ارائه اسکریپت نهایی گرید .
        /// </summary>
        /// <returns></returns>
        internal MvcHtmlString Render()
        {
            var totalSerialized = SerializeConfig(_totalConfig);
            var gridScript = new StringBuilder((new JavaScriptGeneratorRP()).InitializeFor("#" + ID, "Grid", totalSerialized));
            var modalInitializationStr = string.Empty;
            TagBuilder initialScriptBuilder = new TagBuilder("script");
            initialScriptBuilder.SetInnerText(string.Format("var {0}_ns; {0}_isEventOnDataBoundAssigned=false , {0}_gridInitialized=true;", ID, "{", "}"));
            var refreshFunctionScript = string.Empty;
            var searchScript = string.Empty;

            if (_features.Refreshable)
            {
                refreshFunctionScript = string.Format(" $('#k_ref_btn_{2}').on('click' , function(e) {0}  e.preventDefault(); if(ns_Grid.GridOperations.ifRefreshCanApplyAccordingToFilter('{2}')) {{ ns_Grid.GridOperations.doWithInitialOrClearFilter('{2}' , false);  }} else {{ ns_Grid.GridOperations.doWithInitialOrClearFilter('{2}' , true);  }} {1});", "{", "}", ID);
            }

            var dataSourceEventScript = _dataSource.GetOnDataSourceErrorScriptTemplate(ID); 
            
            gridScript.Append(string.Format(" {0} {1} ", modalInitializationStr, refreshFunctionScript));
            
            var cssStyleAttr = string.Empty;
            if (_clientDependency != null)
            {
                if (_clientDependency.CssStyles.Any())
                {
                    if (!string.IsNullOrEmpty(_width))
                    {
                        if (string.IsNullOrEmpty(_clientDependency.CssStyles.Keys.FirstOrDefault(str => str == "width")))
                        {
                            _clientDependency.CssStyles.Add("width", _width + "px");
                        }
                    }

                    if (!string.IsNullOrEmpty(_height))
                    {
                        if (string.IsNullOrEmpty(_clientDependency.CssStyles.Keys.FirstOrDefault(str => str == "height")))
                        {
                            _clientDependency.CssStyles.Add("height", _height + "px");
                        }
                    }

                    if (_clientDependency.CssStyles.Count > 0)
                    {
                        cssStyleAttr = string.Format("style=\"{0}\"", ApplyInitialCssStyleOnGrid(_clientDependency.CssStyles));
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(_width))
                    {
                        _clientDependency.CssStyles.Add("width", _width + "px");
                    }

                    if (!string.IsNullOrEmpty(_height))
                    {
                        _clientDependency.CssStyles.Add("height", _height + "px");
                    }

                    if (_clientDependency.CssStyles.Count > 0)
                    {
                        cssStyleAttr = string.Format("style=\"{0}\"", ApplyInitialCssStyleOnGrid(_clientDependency.CssStyles));
                    }
                }

                if (!string.IsNullOrEmpty(_clientDependency.Events.OnLazyReadScript))//eval('{2}(e)');
                {
                    gridScript.Append(string.Format("$(document).ready(function() {0} eval('{2}(null);'); {1});", "{", "}", _clientDependency.Events.OnLazyReadScript));
                }
            }
            else
            {
                var styleDic = new Dictionary<string, string>();

                if (!string.IsNullOrEmpty(_width))
                {
                    styleDic.Add("width", _width + "px");
                }

                if (!string.IsNullOrEmpty(_height))
                {
                    styleDic.Add("height", _height + "px");
                }
                if (styleDic.Count > 0)
                {
                    cssStyleAttr = string.Format("style=\"{0}\"", ApplyInitialCssStyleOnGrid(styleDic));
                }
            }

            var cellModeDblClick = string.Empty;
            var rowModeDblClick = string.Empty;
            //if (!string.IsNullOrEmpty(_clientDependency.Events.OnDoubleClick) && _features.Selectability != Selectable.None)
            {
                cellModeDblClick = _features.Selectability == Selectable.Cell || _features.Selectability == Selectable.MultipleCells ?
                   string.Format("ns_Grid.GridOperations.onCellDblClick('{0}');" ,  _clientDependency.Events.OnDoubleClick)//string.Format("$(\"td[role='gridcell']\").on('dblclick' , function(e) {0}  if(typeof {3} == 'function') {0} eval(\"{3}\");  {1} else {0} var currentCell= $(this);  var eventArg = {0} doubleClickedCell {4} currentCell , wrappingRow {4} currentCell.closest(\"tr\") {1}; {3}(eventArg); {1}  {1}); ", "{", "}", ID, _clientDependency.Events.OnDoubleClick, ":")
                  : string.Empty;
                rowModeDblClick = _features.Selectability == Selectable.Row || _features.Selectability == Selectable.MultipleRows ?
                   string.Format("ns_Grid.GridOperations.onRowDblClick('{0}');" , _clientDependency.Events.OnDoubleClick)//string.Format("$(\"tr[role='row']\").on('dblclick' , function(e) {0}  if(typeof {3} == 'function') {0} eval(\"{3}\");  {1} else {0} var currentRow= $(this); var eventArg = {0} doubleClickedRow {4} currentRow {1}; eval(\"{3}\"); {1} {1});  ", "{", "}", ID, _clientDependency.Events.OnDoubleClick, ":")
                  : string.Empty;
            }
            var dblClickScript = string.Empty;//string.Format("<script> {0} </script>", cellModeDblClick + rowModeDblClick);
            var fialGridScript = gridScript.ToString();
            var finalString = string.Format("<{0} id=\"{2}\" {5} ><span>{9}{8}{7}{6}{4}</span><{1}>{3}</{1}></{0}>", "div", "script", ID, fialGridScript, !string.IsNullOrEmpty(_removableStr) ? _removableStr : string.Empty,
                cssStyleAttr,
                dataSourceEventScript,
                initialScriptBuilder.ToString(TagRenderMode.Normal), searchScript, dblClickScript);
            return MvcHtmlString.Create(finalString);
        }
        /// <summary>
        /// استایل های مربوط به گرید.
        /// </summary>
        /// <param name="cssStyles"></param>
        /// <returns></returns>
        private string ApplyInitialCssStyleOnGrid(Dictionary<string, string> cssStyles)
        {
            var cssString = new StringBuilder();
            if (cssStyles.Any())
            {
                foreach (var cssRule in cssStyles)
                {
                    cssString.Append(string.Format("{0}:{1}; ", cssRule.Key, cssRule.Value));
                }
            }

            if (cssString.Length > 0)
            {
                return cssString.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// سریال کردن داده های مربوط به گرید.
        /// </summary>
        /// <param name="totalConfig"></param>
        /// <returns></returns>
        private IDictionary<string, object> SerializeConfig(Dictionary<string, object> totalConfig)
        {
            if (_columnsConfig != null)
            {
                if (_columnsConfig.Count > 0)
                {

                    /*--------------------Auto Bind------------------- */
                    //Comment: When autoBind property is set to false,in order to get data from the 
                    //data source,the dataSource.read() method should be called inside any change 
                    //event handler.(this functionality is so useful in cases where multiple widgets are bound to the same dataSource.) 
                    totalConfig["autoBind"] = _features.AutoBind;

                    /*--------------------ColumnMenu------------------- */
                    if (_features.ColumnMenu.ColumnMenuEnabled)
                    {
                        totalConfig["columnMenu"] = _features.ColumnMenu.ToJson();
                        if (!string.IsNullOrEmpty(_features.ColumnMenu.ColumnMenuInit))
                        {
                            totalConfig["columnMenuInit"] = new ClientHandlerDescriptor { TemplateDelegate = obj => _features.ColumnMenu.ColumnMenuInit + "(e);" };
                        }
                    }

                    /*--------------------Columns Setup-------------- */

                    var colsList = new List<IDictionary<string, object>>();
                    _columnsConfig.ForEach(cc =>
                    {
                        cc.GridID = ID;
                        var dicItem = cc.ToJson();
                        colsList.Add(dicItem);
                    });

                    /*--------------------Toolbar-------------------- */

                    var modelTypeArr = _modelType.Name != "Object" ?   _modelType.ToString().Split('.') : null;
                    if (_toolbar != null)
                    {
                        var toolbarCommands = new List<IDictionary<string, object>>();
                        if (_toolbar.Commands != null && !_features.ReadOnly)
                        {
                            _toolbar.Commands.ForEach(tc =>
                            {
                                switch (tc.Name)
                                {
                                    case GCommandRP.Create:
                                        if (_accessOperation.Insertable)
                                        {
                                            tc.GridID = ID;
                                            tc.ModelType = modelTypeArr[modelTypeArr.Length-1];
                                            tc.AddEditTemplateAddress = _features.EditableConfig.CustomConfig.Template.Url;
                                            tc.CustomCommandID = "add_custom_template";
                                            if (!_hasCommonAddOrEditScriptAppended)
                                            {
                                                _hasCommonAddOrEditScriptAppended = true;
                                                tc.CommonAddOrEditScript = tc.GetCustomAddEditScript();
                                            }
                                            else
                                            {
                                                tc.CommonAddOrEditScript = string.Empty;
                                            }
                                            tc.AddCallerScript = tc.GetAddEditScriptEventListener(GCommandRP.Create);
                                            var dicItem = tc.ToJson();
                                            toolbarCommands.Add(dicItem);
                                        }
                                        break;
                                    case GCommandRP.Delete:
                                        if (_accessOperation.Removable)
                                        {
                                            tc.GridID = ID;
                                            var dicItem = tc.ToJson();
                                            toolbarCommands.Add(dicItem);
                                        }
                                        break;
                                    case GCommandRP.Edit:
                                        if (_accessOperation.Updatable)
                                        {
                                            tc.GridID = ID;
                                           
                                            tc.ModelType = modelTypeArr[modelTypeArr.Length - 1];
                                            tc.AddEditTemplateAddress = _features.EditableConfig.CustomConfig.Template.Url;
                                            tc.CustomCommandID = "edit_custom_template";
                                            if (!_hasCommonAddOrEditScriptAppended)
                                            {
                                                _hasCommonAddOrEditScriptAppended = true;
                                                tc.CommonAddOrEditScript = tc.GetCustomAddEditScript(); 
                                            }
                                            else
                                            {
                                                tc.CommonAddOrEditScript = string.Empty;
                                            }
                                            tc.EditCallerScript = tc.GetAddEditScriptEventListener(GCommandRP.Edit);
                                            var dicItem = tc.ToJson();
                                            toolbarCommands.Add(dicItem);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            });
                        }

                        var customActions = _clientDependency.CustomActions;
                        if (customActions.Count > 0)
                        {
                            customActions.ForEach(ca=>{
                                   if(string.IsNullOrEmpty(ca.Template))
                                   if (!string.IsNullOrEmpty(ca.CommandText) && !string.IsNullOrEmpty(ca.ClickEventHandler) && !string.IsNullOrEmpty(ca.CustomActionUniqueName))
                                   {
                                       if (VerifyCustomActionAuthorization(ca.CustomActionUniqueName.Split('#')[0]))
                                       {
                                           var customCommand = new ColumnCommand() { Name = GCommandRP.Custom };
                                           customCommand.CustomCommandID = ca.ID;
                                           customCommand.GridID = ID;
                                           customCommand.ClickHandler = ca.ClickEventHandler;
                                           customCommand.Text = ca.CommandText;
                                           customCommand.CommandIconRelativePath = ca.IconRelativePath;
                                           customCommand.CssClass = ca.CssClass;
                                           toolbarCommands.Add(customCommand.ToJson());
                                       }
                                   }
                                   else
                                   {
                                      if(!string.IsNullOrEmpty(ca.CustomActionUniqueName)){
                                          var customCommand = new ColumnCommand() { Name = GCommandRP.Custom };
                                          customCommand.Template = ca.Template;
                                          toolbarCommands.Add(customCommand.ToJson());
                                      }
                                   }
                            });
                        }

                        if (_features.Searchable)
                        {
                            var searchCommand = new ColumnCommand { Name = GCommandRP.Search };
                            searchCommand.GridID = ID;
                            var dicItem = searchCommand.ToJson();
                            toolbarCommands.Add(dicItem);
                        }

                        if (_accessOperation.UserGuideIncluded && modelTypeArr != null && _features.UserGuideIncluded)
                        {
                            var uGuideCommand = new ColumnCommand { Name = GCommandRP.UserGuide };
                            uGuideCommand.GridID = ID;
                            uGuideCommand.ClickHandler = "Home/CreateHelpView";
                            var viewModelModelIndex = modelTypeArr[modelTypeArr.Length - 1].IndexOf("ViewModel");
                            uGuideCommand.ModelType = modelTypeArr[modelTypeArr.Length - 1].Substring(0, viewModelModelIndex);
                            uGuideCommand.CommandIconRelativePath = "Content/Images/Help.png";
                            uGuideCommand.Text = "راهنما";
                            toolbarCommands.Add(uGuideCommand.ToJson());

                        }

                        if (_features.Refreshable)
                        {
                            var refreshableCommand = _toolbar.Commands.FirstOrDefault(com => com.Name == GCommandRP.Refresh);
                            if (refreshableCommand != null)
                            {
                                refreshableCommand.GridID = ID;
                                toolbarCommands.Add(refreshableCommand.ToJson());
                            }
                        } 

                        if (toolbarCommands.Count > 0)
                        {
                            totalConfig["toolbar"] = toolbarCommands;
                        }
                    }

                    totalConfig["columns"] = colsList;

                    /*--------------------Paging-------------- */

                    if (_features.Paging)
                    {
                        totalConfig["pageable"] = _features.PagingInfo.ToJson();                    
                    }

                    /*--------------------Filterable-------------- */

                    var filterConfig = _features.Filter;
                    if (filterConfig.FilterAffection)
                    {
                        totalConfig["filterable"] = filterConfig.ToJson();
                    }

                    /*--------------------Navigatable-------------- */

                    var nav = _features.Navigatable;

                    totalConfig["navigatable"] = nav;

                    /*--------------------Sortable-------------- */
                    if (_features.Sortability.IsSortable)
                    {
                        totalConfig["sortable"] = _features.Sortability.IsSortable;// _features.Sortability.ToJson();
                    }

                    /*--------------------Groupable-------------- */
                    if (_features.Grouping)
                    {
                        totalConfig["groupable"] = _features.GroupingInfo.ToJson();
                    }
                    else
                    {
                        totalConfig["groupable"] = false;
                    }

                    /*-------------------Reorderable-------------- */

                    totalConfig["reorderable"] = _features.Reorderable;

                    /*------------------Scrollable --------------- */

                    totalConfig["scrollable"] = _features.Scrollability.IsScrollable;

                    /*-----------------Resizable--------------- */

                    totalConfig["resizable"] = _features.Resizable;

                    /*--------------------Aggregates-------------- */


                    /*--------------------Editable-------------- */
                    //This property is very important in Editing(delete , edit)
                    var edConfig = _features.EditableConfig;

                    if (edConfig.CustomConfig != null)
                    {
                        totalConfig["editable"] = edConfig.CustomConfig.ToJson();
                    }
                    else
                    {
                        totalConfig["editable"] = edConfig.Editable;
                    }

                    /*--------------------Selectable-------------- */
                    totalConfig["selectable"] = DefineSelectableString(_features.Selectability);
                    if (_clientDependency != null)
                    {
                        DefineEventHandlers(totalConfig);
                    }

                    /*--------------------Data Source-------------- */

                    totalConfig["dataSource"] = _dataSource.ToJson();
                    var model = _dataSource.Schema.Model;
                }
            }
            return totalConfig;
        }

        /// <summary>
        /// مجوز مربوط به عملکرد اضافی
        /// </summary>
        /// <param name="customActionUniqueName"></param>
        /// <returns></returns>
        private bool VerifyCustomActionAuthorization(string customActionUniqueName)
        {
            return UserAccessibleElement.HasCustomActionAuthorized(customActionUniqueName);
        }
        /// <summary>
        /// رویداد های مربوط به گرید .
        /// </summary>
        /// <param name="totalConfig"></param>
        private void DefineEventHandlers(Dictionary<string, object> totalConfig)
        {
            BuildOnDataBindingEventHandler(totalConfig);
            BuildOnEditEventHandler(totalConfig);
            BuildOnChangeEventHandler(totalConfig);
            BuildOnSaveEventHandler(totalConfig);
            BuildOnCancelEventHandler(totalConfig);
            BuildOnDataBoundEventHandler(totalConfig);
        }

        
        #region Grid Event Handlers 
        
        private void BuildOnChangeEventHandler(Dictionary<string, object> totalConfig)
        {
            if (!string.IsNullOrEmpty(_clientDependency.Events.OnChange))
            {
                var onChangeStr = string.Format("function {3}_onChange(e) {0} {2} {1}", "{", "}", _clientDependency.Events.OnChange + "(e);", ID);
                totalConfig["change"] = new ClientHandlerDescriptor { TemplateDelegate = obj => onChangeStr };
            }
        }

        private void BuildOnEditEventHandler(Dictionary<string, object> totalConfig)
        {
            if (!_features.ReadOnly)
            {
                if (_accessOperation.InsertableOrUpdatable)
                {
                    var initialOnEditCode = string.Format("ns_Grid.GridOperations.onEditEventHandler(e);"); 
                    var afterOnDataBindingCode = string.Empty;
                    var onEditStr = string.Format("function {2}_onEdit(e) {{  e.preventDefault(); {0} {1}  }}", initialOnEditCode, !string.IsNullOrEmpty(_clientDependency.Events.OnEdit) ? _clientDependency.Events.OnEdit + "(e);" : string.Empty, ID);
                    totalConfig["edit"] = new ClientHandlerDescriptor { TemplateDelegate = obj => onEditStr };
                }
            }
        }

        private void BuildOnDataBindingEventHandler(Dictionary<string, object> totalConfig)
        {
            if (!_features.ReadOnly)
            {
                    var afterOnDataBindingCode = string.Empty;
                    if (!string.IsNullOrEmpty(_clientDependency.Events.OnDataBinding))
                    {
                        afterOnDataBindingCode = string.Format("if(typeof {0} == 'function') eval(\"{0}\");  else {0}(event);", _clientDependency.Events.OnDataBinding);
                    }
                    var onDataBoundingInitialStrEdit = string.Format("var grd =$('#{0}');", ID);
                    var onDataBoundingStrDelete = string.Empty;
                    var completeBoundStr = string.Format("function {0}_onDataBinding(event) {{ {1} {2} }}", ID , onDataBoundingInitialStrEdit, afterOnDataBindingCode);
                    totalConfig["dataBinding"] = new ClientHandlerDescriptor { TemplateDelegate = obj => completeBoundStr };
            }
        }

        private void BuildOnDataBoundEventHandler(Dictionary<string, object> totalConfig)
        {
            var cellModeDblClick = string.Empty;
            var rowModeDblClick = string.Empty;

            if (!string.IsNullOrEmpty(_clientDependency.Events.OnDoubleClick) && _features.Selectability != Selectable.None)
            {
                cellModeDblClick = _features.Selectability == Selectable.Cell || _features.Selectability == Selectable.MultipleCells ? //(" ns_Grid.GridOperations.onCellDblClick('' , {0});", _clientDependency.Events.OnDoubleClick)//
                   GetCellDoubleClickScript(_clientDependency.Events.OnDoubleClick)
                  : string.Empty;
                rowModeDblClick = _features.Selectability == Selectable.Row || _features.Selectability == Selectable.MultipleRows ?
                   GetRowDoubleClickScript(_clientDependency.Events.OnDoubleClick) // string.Format(" ns_Grid.GridOperations.onRowDblClick('' ,  {0});", _clientDependency.Events.OnDoubleClick)//
                  : string.Empty;
                //$("td[role='gridcell']").on('dblclick' , function(e) {  if(typeof onDoubleClick == 'function')  eval(onDoubleClick);  else { var currentCell= $(this);  var eventArg = { doubleClickedCell : currentCell , wrappingRow : currentCell.closest("tr") }; eval(onDoubleClick + '(eventArg);'); }  });
            }
            var onDataBoundScript = string.Empty;
            if (!string.IsNullOrEmpty(_clientDependency.Events.OnDataBound))
            {
                onDataBoundScript = GetOnDataBoundScript(_clientDependency.Events.OnDataBound , "event");
            }
            totalConfig["dataBound"] = new ClientHandlerDescriptor { TemplateDelegate = obj =>
                string.Format("function {0}_onDataBound(event) {{  {1}  {2} {3} }}", ID, cellModeDblClick + rowModeDblClick, onDataBoundScript , getKeyboardNavigation())
            };
        }

        public string GetOnDataBoundScript(string dataBoundCustomCode ,string args)
        {
            return string.Format("ns_Grid.GridOperations.onDataBoundCustomCode({0} , {1});", dataBoundCustomCode, args);
        }

        public string getKeyboardNavigation()
        {
            return string.Format("ns_Grid.GridOperations.supressAnyKeyEvent('{0}');" , ID);
        }

        private string GetRowDoubleClickScript(string rowDblClick)
        {
            return string.Format("ns_Grid.GridOperations.onRowDblClick(\"{0}\" , '{1}');" , rowDblClick , ID); 
        }

        private string GetCellDoubleClickScript(string cellDblClick)
        {
            return string.Format("ns_Grid.GridOperations.onCellDblClick(\"{0}\" , '{1}');", cellDblClick , ID);
        }

        private string GetOnInitScript(string onInit)
        {
            return string.Format("ns_Grid.GridOperations.onGridInit(\"{0}\");", onInit);
        }

        private void BuildOnCancelEventHandler(Dictionary<string, object> totalConfig)
        {

        }

        private void BuildOnSaveEventHandler(Dictionary<string, object> totalConfig)
        {
            var onSaveFinalEventStr = string.Empty;

            if (!string.IsNullOrEmpty(_clientDependency.Events.OnSave))
            {
                onSaveFinalEventStr = string.Format("function {2}_onSave(e) {0}  {3} {4}(e);  {1}", "{", "}", ID, string.Empty, _clientDependency.Events.OnSave);
            }
            else
            {
                onSaveFinalEventStr = string.Format("function {2}_onSave(e) {0}  {3} {1}", "{", "}", ID, string.Empty);
            }
            totalConfig["save"] = new ClientHandlerDescriptor { TemplateDelegate = obj => onSaveFinalEventStr };

        }

        #endregion
        /// <summary>
        /// مود انتخاب گرید
        /// </summary>
        /// <param name="selectable"></param>
        /// <returns></returns>
        private string DefineSelectableString(Selectable selectable)
        {
            var selectableStr = string.Empty;
            switch (selectable)
            {
                case Selectable.Row:
                    selectableStr = "row";
                    break;
                case Selectable.Cell:
                    selectableStr = "cell";
                    break;
                case Selectable.MultipleRows:
                    selectableStr = "multiple row";
                    break;
                case Selectable.MultipleCells:
                    selectableStr = "multiple cell";
                    break;
                case Selectable.None:
                    selectableStr = "none";
                    break;
                default:
                    selectableStr = "row";
                    break;
            }
            return selectableStr;
        }
    }
}
