using Core.Model;
using Core.Mvc.Extensions;
using Core.Mvc.Helpers.RefahKendoGrid;
using Core.Mvc.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;


namespace Core.Mvc.Helpers
{
    /// <summary> 						            
    ///Created by :Tabandeh
    ///Created on Date :1392/5/16
    ///cerate lookup by providing textbox for display text and HiddenField for keeping value.
    ///lookup have two kind(tree, grid).look up result have two kind(textbox , multiselect).
    ///if developers need to set validation for lookup ,they can set validationRP array 
    /// </summary>
    public static class LookUpHelper
    {
        private static string _Id;
        /// <summary> 						            
        ///Created by :Tabandeh
        ///Created on Date :1392/7/20
        /// <remarks>property for return Id from  HtmlModifier.ModifyId(_Id) method.and set value to _Id</remarks>
        /// </summary>
        private static string Id
        {
            get
            {
                return HtmlModifier.ModifyId(_Id);
            }
            set { _Id = value; }
        }

        private static string Title { get; set; }

        #region textbox for grid result
        // /// <summary> 						            
        /////Created by :Tabandeh
        /////Created on Date :  1392/5/16
        ///// <remarks>textbox for grid result</remarks>
        ///// </summary>
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"> Specify ViewModel </typeparam>
        /// <param name="helper">Represents support for rendering html controls in a view</param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="gridInfo">Specify grid info such as datasourceinfo , toolbar , general features and commands config</param>
        /// <param name="propertyNameForValue"></param>
        /// <param name="propertyNameForDisplay"></param>
        /// <param name="propertyNameForBinding"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="readOnly"></param>
        /// <param name="lookupHtmlAttributes"></param>
        /// <param name="clientDependentFeature">Specify all the client related features of the grid are included(declared in Core.Mvc.Helpers.RefahKendoGrid.ClientDependentFeature())</param>
        /// <param name="validationRP">Specify validation type <example>new Core.Mvc.Extensions.Required()</example></param>
        /// <returns>MvcHtmlString.Create(container.ToString());</returns>
        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string name, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName,
            string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, object htmlAttributes,
            bool readOnly, Dictionary<string, object> lookupHtmlAttributes, ClientDependentFeature clientDependentFeature, params  ValidationBase[] validationRP) where TModel : IViewModelBase, new()
        {

            Id = id;
           // Title = title;

            TagBuilder container = new TagBuilder("span");
            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                container.MergeAttributes(attributes);
            }

            TagBuilder containerState = new TagBuilder("span");


            MvcHtmlString textbox;
            lookupHtmlAttributes = lookupHtmlAttributes ?? new Dictionary<string, object>();

            //---value
            TagBuilder hiddenValue = new TagBuilder("input");
            hiddenValue.MergeAttribute("type", "hidden");
            hiddenValue.MergeAttribute("id", propertyNameForBinding);

            if (!string.IsNullOrEmpty(name))
            {
                hiddenValue.MergeAttribute("name", name);
            }

            hiddenValue.MergeAttribute("data-bind", string.Format("value:{0}", propertyNameForBinding));

            if (validationRP != null && validationRP.Count()>0)
            {
                hiddenValue.MergeAttributes(CreateValidationForlookup(lookupHtmlAttributes, validationRP));
            }

            textbox = helper.TextBoxRP(Id, readOnly, lookupHtmlAttributes);


            string gridID = string.Format("lookupGrid_{0}", Id);

            gridInfo.GridID = gridID;

            gridInfo.ClientDependentFeatures = clientDependentFeature;
        
            var lookupInfo = new Lookup.Grid
            {
                Title = title,

                LookupName = Id,

                GridID = gridID,

                ViewModel = typeof(TModel).AssemblyQualifiedName,

                ViewModelProperty = viewModelPropName,

                UseMultiSelect = false,

                PropertyNameForDisplay = propertyNameForDisplay,

                PropertyNameForValue = propertyNameForValue,

                PropertyNameForBinding = propertyNameForBinding

            };

            //----create Lookup
            containerState.InnerHtml = textbox.ToHtmlString()

                + hiddenValue.ToString(TagRenderMode.SelfClosing)

                + CreatLookupButton(lookupInfo);
              
            container.InnerHtml = containerState.ToString();
            
            return MvcHtmlString.Create(container.ToString());
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string title, RefahKendoGrid.Settings.GridInfo gridInfo,string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, string.Empty, title, gridInfo, viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, null, false, null, null, null);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string title, RefahKendoGrid.Settings.GridInfo gridInfo,string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, params  ValidationBase[] validationRP) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, string.Empty, title, gridInfo, viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, null, false, null, null, validationRP);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string title, RefahKendoGrid.Settings.GridInfo gridInfo,string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, Dictionary<string, object> lookupTextBoxHtmlAttributes) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, string.Empty, title, gridInfo, viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, null, false, lookupTextBoxHtmlAttributes, null, null);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, ClientDependentFeature clientDependentFeature) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, string.Empty, title, gridInfo, viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, null, false, null, clientDependentFeature, null);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, Dictionary<string, object> lookupTextBoxHtmlAttributes, ClientDependentFeature clientDependentFeature) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, string.Empty, title, gridInfo, viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, null, false, lookupTextBoxHtmlAttributes, clientDependentFeature, null);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, Dictionary<string, object> lookupTextBoxHtmlAttributes, params  ValidationBase[] validationRP) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, string.Empty, title, gridInfo, viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, null, false, lookupTextBoxHtmlAttributes, null, validationRP);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string name, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, name, title, gridInfo, viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, null, false, null, null, null);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string name, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, params  ValidationBase[] validationRP) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, name, title, gridInfo, viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, null, false, null, null, validationRP);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string name, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, Dictionary<string, object> lookupTextBoxHtmlAttributes) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, name, title, gridInfo, viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, null, false, lookupTextBoxHtmlAttributes, null, null);
        }
        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string name, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, Dictionary<string, object> lookupTextBoxHtmlAttributes, params  ValidationBase[] validationRP) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, name, title, gridInfo, viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, null, false, lookupTextBoxHtmlAttributes, null, validationRP);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string name, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, bool readOnly) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, name, title, gridInfo, viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, null, readOnly, null, null, null);
        }


        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string name, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, bool readOnly, params  ValidationBase[] validationRP) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, name, title, gridInfo, viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, null, readOnly, null, null, validationRP);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string name, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, bool readOnly, Dictionary<string, object> lookupTextBoxHtmlAttributes) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, name, title, gridInfo, viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, null, readOnly, lookupTextBoxHtmlAttributes, null, null);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string name, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, bool readOnly, Dictionary<string, object> lookupTextBoxHtmlAttributes, params  ValidationBase[] validationRP) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, name, title, gridInfo, viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, null, readOnly, lookupTextBoxHtmlAttributes, null, validationRP);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, bool readOnly, params  ValidationBase[] validationRP) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, string.Empty, title, gridInfo,viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, null, readOnly, null, null, validationRP);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, bool readOnly) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, string.Empty, title, gridInfo, viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, null, readOnly, null, null);
        }


        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, bool readOnly, Dictionary<string, object> lookupTextBoxHtmlAttributes, params  ValidationBase[] validationRP) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, string.Empty, title, gridInfo, viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, null, readOnly, lookupTextBoxHtmlAttributes, null, validationRP);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, bool readOnly, Dictionary<string, object> lookupTextBoxHtmlAttributes) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, string.Empty, title, gridInfo, viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, null, readOnly, lookupTextBoxHtmlAttributes, null, null);
        }
        #endregion

        #region multi select for grid result
        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string name, string title, RefahKendoGrid.Settings.GridInfo gridInfo , string viewModelPropName
            , string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, bool isMultiSelect, object htmlAttributes, bool readOnly
            , Dictionary<string, object> lookupHtmlAttributes, ClientDependentFeature clientDependentFeature, params  ValidationBase[] validationRP) where TModel : IViewModelBase, new()
        {
           
            Id = id;
           
            TagBuilder container = new TagBuilder("span");

            container.AddCssClass("rp-lookup");

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

                container.MergeAttributes(attributes);
            }

            TagBuilder containerState = new TagBuilder("span");



            //-----multi select
            Kendo.Mvc.UI.Fluent.MultiSelectBuilder multiSelect = helper.MultiSelectRP(propertyNameForBinding, propertyNameForDisplay, propertyNameForValue, Kendo.Mvc.UI.FilterType.Contains);

            lookupHtmlAttributes = lookupHtmlAttributes ?? new Dictionary<string, object>();

            Dictionary<string, string> _htmlAttributes = null;

            if (validationRP != null && validationRP.Count() >0)
            {

                _htmlAttributes = CreateValidationForlookup(lookupHtmlAttributes, validationRP);
            }

            else
            {
                _htmlAttributes = lookupHtmlAttributes.ToDictionary(t => t.Key, t => (string)t.Value);
            }

            if (readOnly)
            {
                _htmlAttributes.Add("readOnly", "true");
            }

            if (!string.IsNullOrEmpty(Id))
            {
                _htmlAttributes.Add("id", Id);
            }

            if (!string.IsNullOrEmpty(name))
            {
                _htmlAttributes.Add("name", name);
            }

            multiSelect.HtmlAttributes(_htmlAttributes.ToDictionary(t => t.Key, t => (object)t.Value));

            var multiselectHtml = multiSelect.ToHtmlString();

            containerState.AddCssClass(multiselectHtml.Contains("data-val-required") ? StyleKind.RequiredInput : StyleKind.OptionalInput);

           
            string gridID = string.Format("lookupGrid_{0}", Id);

            gridInfo.GridID = gridID;

            gridInfo.ClientDependentFeatures = clientDependentFeature;
            
            var lookupInfo = new Lookup.Grid
            {
                Title = title,

                LookupName = Id,

                GridID = gridID,

                ViewModel = typeof(TModel).AssemblyQualifiedName,

                ViewModelProperty = viewModelPropName,

                UseMultiSelect = true,

                PropertyNameForDisplay = propertyNameForDisplay,

                PropertyNameForValue = propertyNameForValue,

                PropertyNameForBinding = propertyNameForBinding

            };

            containerState.InnerHtml = multiselectHtml
                                     + CreatLookupButton(lookupInfo);

            container.InnerHtml = containerState.ToString();
            
            //helper.RenderPartial("~/Views/LookUp/_GridLookUp.cshtml", new Lookup.Grid
            //{
            //    GridInfo = gridInfo,
            //    GridID = gridID,
            //    ClientDependentFeatures = clientDependentFeature,
            //    LookupName = Id,
            //    UseMultiSelect = isMultiSelect,
            //    PropertyNameForDisplay = propertyNameForDisplay,
            //    PropertyNameForValue = propertyNameForValue,
            //    PropertyNameForBinding = propertyNameForBinding
            //});

            return MvcHtmlString.Create(container.ToString());
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, bool isMultiSelect, bool readOnly) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, string.Empty, title, gridInfo,viewModelPropName, propertyNameForValue, propertyNameForDisplay, id, isMultiSelect, null, readOnly, null, null);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName,string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, bool isMultiSelect, bool readOnly) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, string.Empty, title, gridInfo,viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, isMultiSelect, null, readOnly, null, null);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string title, RefahKendoGrid.Settings.GridInfo gridInfo,string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, bool isMultiSelect, bool readOnly, Dictionary<string, object> lookupHtmlAttributes) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, string.Empty, title, gridInfo,viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, isMultiSelect, null, readOnly, lookupHtmlAttributes, null);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string title, RefahKendoGrid.Settings.GridInfo gridInfo,string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, bool isMultiSelect, bool readOnly, Dictionary<string, object> lookupHtmlAttributes) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, string.Empty, title, gridInfo,viewModelPropName, propertyNameForValue, propertyNameForDisplay, id, isMultiSelect, null, readOnly, lookupHtmlAttributes, null);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName,string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, bool isMultiSelect, bool readOnly, Dictionary<string, object> lookupHtmlAttributes, ClientDependentFeature clientDependentFeature) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, string.Empty, title, gridInfo,viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, isMultiSelect, null, readOnly, lookupHtmlAttributes, clientDependentFeature, null);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string title, RefahKendoGrid.Settings.GridInfo gridInfo,string viewModelPropName, string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, bool isMultiSelect, bool readOnly, ClientDependentFeature clientDependentFeature) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, string.Empty, title, gridInfo,viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, isMultiSelect, null, readOnly, null, clientDependentFeature, null);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string name, string title, RefahKendoGrid.Settings.GridInfo gridInfo, string viewModelPropName,string propertyNameForValue, string propertyNameForDisplay, string propertyNameForBinding, bool isMultiSelect, bool readOnly) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, name, title, gridInfo,viewModelPropName, propertyNameForValue, propertyNameForDisplay, propertyNameForBinding, isMultiSelect, null, readOnly, null, null, null);
        }

        #endregion

        #region text box for tree
        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string name, string title, TreeInfo treeInfo,string viewModelPropName,string propertyNameForBinding
            , object htmlAttributes, bool readOnly, Dictionary<string, object> lookupHtmlAttributes, params  ValidationBase[] validationRP)
            where TModel : IViewModelBase, new()
        {
           
            Id = id;
       
            TagBuilder container = new TagBuilder("span");

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

                container.MergeAttributes(attributes);
            }

            TagBuilder containerState = new TagBuilder("span");

            //-----textbox
            MvcHtmlString textbox;

            lookupHtmlAttributes = lookupHtmlAttributes ?? new Dictionary<string, object>();
            
            //---value
            TagBuilder hiddenValue = new TagBuilder("input");

            hiddenValue.MergeAttribute("type", "hidden");

            hiddenValue.MergeAttribute("id", propertyNameForBinding);

            if (!string.IsNullOrEmpty(name))
            {
                hiddenValue.MergeAttribute("name", name);
            }

            hiddenValue.MergeAttribute("data-bind", string.Format("value:{0}", propertyNameForBinding));

            if (validationRP != null)
            {
                hiddenValue.MergeAttributes(CreateValidationForlookup(lookupHtmlAttributes, validationRP));
            }

            textbox = helper.TextBoxRP(Id, readOnly, lookupHtmlAttributes);

            string treeID = string.Format("lookupTree_{0}", Id);

            treeInfo.Name = treeID;

            var lookupInfo = new Lookup.Tree
            {
                Title = title,

                LookupName = Id,

                TreeID = treeID,

                ViewModel = typeof(TModel).AssemblyQualifiedName,

                ViewModelProperty = viewModelPropName,

                UseMultiSelect = false,

                PropertyNameForDisplay = treeInfo.DataTextField,

                PropertyNameForBinding = propertyNameForBinding

            };

            
            //-----create lookup----
            containerState.InnerHtml = textbox.ToHtmlString()
                + hiddenValue
                + CreatLookupButton(lookupInfo);

            container.InnerHtml = containerState.ToString();
                        
            return MvcHtmlString.Create(container.ToString());
        }

        #region text box for tree overrides
        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string title, TreeInfo treeInfo,string viewModelPropName, string propertyNameForBinding, Dictionary<string, object> lookupTextBoxHtmlAttributes) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, string.Empty, title, treeInfo,viewModelPropName, propertyNameForBinding, null, false, lookupTextBoxHtmlAttributes, null);
        }


        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string name, string title, TreeInfo treeInfo,string viewModelPropName, string propertyNameForBinding) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, name, title, treeInfo,viewModelPropName, propertyNameForBinding, null, false, null, null);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string name, string title, TreeInfo treeInfo,string viewModelPropName, string propertyNameForBinding, Dictionary<string, object> lookupTextBoxHtmlAttributes) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, name, title, treeInfo,viewModelPropName, propertyNameForBinding, null, false, lookupTextBoxHtmlAttributes, null);
        }


        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string title, TreeInfo treeInfo,string viewModelPropName, string propertyNameForBinding) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, string.Empty, title, treeInfo,viewModelPropName, propertyNameForBinding, null, false, null, null);
        }
         #endregion

        #endregion

        #region multi select for tree
        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string name, string title, TreeInfo treeInfo, string viewModelPropName, string propertyNameForBinding, bool isMultiSelect,
            object htmlAttributes, bool readOnly, Dictionary<string, object> lookupHtmlAttributes, params  ValidationBase[] validationRP) where TModel : IViewModelBase, new()
        {

            Id = id;

            TagBuilder container = new TagBuilder("span");

            container.AddCssClass("rp-lookup");

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

                container.MergeAttributes(attributes);
            }

            TagBuilder containerState = new TagBuilder("span");

            //-----MultiSelect

            Kendo.Mvc.UI.Fluent.MultiSelectBuilder multiSelect = helper.MultiSelectRP(propertyNameForBinding, treeInfo.DataTextField, treeInfo.DataSource.ModelRP.ModelIdName, Kendo.Mvc.UI.FilterType.Contains);

            lookupHtmlAttributes = lookupHtmlAttributes ?? new Dictionary<string, object>();

            Dictionary<string, string> _htmlAttributes = null;

            if (validationRP != null && validationRP.Count() > 0)
            {

                _htmlAttributes = CreateValidationForlookup(lookupHtmlAttributes, validationRP);
            }

            else
            {
                _htmlAttributes = lookupHtmlAttributes.ToDictionary(t => t.Key, t => (string)t.Value);
            }

            if (readOnly)
            {
                _htmlAttributes.Add("readOnly", "true");
            }

            if (!string.IsNullOrEmpty(Id))
            {
                _htmlAttributes.Add("id", Id);
            }

            if (!string.IsNullOrEmpty(name))
            {
                _htmlAttributes.Add("name", name);
            }

            multiSelect.HtmlAttributes(_htmlAttributes.ToDictionary(t => t.Key, t => (object)t.Value));

            var multiSelectHtml = multiSelect.ToHtmlString();

            containerState.AddCssClass(multiSelectHtml.Contains("data-val-required") ? StyleKind.RequiredInput : StyleKind.OptionalInput);

            string treeID = string.Format("lookupTree_{0}", Id);

            treeInfo.Name = treeID;

            var lookupInfo = new Lookup.Tree
            {
                Title = title,

                LookupName = Id,

                TreeID = treeID,
                // ViewModel = typeof(TModel).FullName,
                ViewModel = typeof(TModel).AssemblyQualifiedName,
                
                ViewModelProperty = viewModelPropName,

                UseMultiSelect = true,

                PropertyNameForDisplay = treeInfo.DataTextField,

                PropertyNameForValue = treeInfo.DataSource.ModelRP.ModelIdName,

                PropertyNameForBinding = propertyNameForBinding

            };


            //-----Create lookup----
            containerState.InnerHtml = multiSelectHtml + CreatLookupButton(lookupInfo);

            container.InnerHtml = containerState.ToString();

            container.InnerHtml = containerState.ToString();

            return MvcHtmlString.Create(container.ToString());

        }

        #region multi select for tree overrides
        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string title, TreeInfo treeInfo, string viewModelPropName, string propertyNameForBinding, bool readOnly, bool isMultiSelect) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, string.Empty, title, treeInfo,viewModelPropName, propertyNameForBinding, isMultiSelect, null, readOnly,null,null);
        }

        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string name, string title, TreeInfo treeInfo, string viewModelPropName, string propertyNameForBinding, bool readOnly, bool isMultiSelect) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, name, title, treeInfo,viewModelPropName, propertyNameForBinding, isMultiSelect, null, readOnly,null, null);
        }
        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string name, string title, TreeInfo treeInfo, string viewModelPropName, string propertyNameForBinding, bool readOnly, bool isMultiSelect, Dictionary<string, object> lookupHtmlAttributes, params  ValidationBase[] validationRP) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, name, title, treeInfo, viewModelPropName, propertyNameForBinding, isMultiSelect, null, readOnly, lookupHtmlAttributes, validationRP);
        }
        public static MvcHtmlString LookUpRP<TModel>(this HtmlHelper helper, string id, string name, string title, TreeInfo treeInfo, string viewModelPropName, string propertyNameForBinding, bool readOnly, bool isMultiSelect, object htmlAttributes) where TModel : IViewModelBase, new()
        {
            return LookUpRP<TModel>(helper, id, name, title, treeInfo, viewModelPropName, propertyNameForBinding, isMultiSelect, htmlAttributes, readOnly, null, null);
        }
        

        #endregion

        #endregion


        //private static void CreateValidationForlookup(TagBuilder tagForModel, Dictionary<string, object> lookupHtmlAttributes, ValidationBase[] validationRP)
        //{
        //    foreach (var item in validationRP)
        //    {
        //        switch (item.GetType().Name)
        //        {
        //            case "Required":
        //                {
        //                    lookupHtmlAttributes.Add("Style", "border-left-color:rgb(236,99,22);border-left-width:5px;");

        //                    break;
        //                }
        //        }

        //        tagForModel.MergeAttributes(item.CreateRelatedValidation());

        //    }
        //}
        private static Dictionary<string, string> CreateValidationForlookup(Dictionary<string, object> lookupHtmlAttributes, ValidationBase[] validationRP)
        {
            IEnumerable<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            Dictionary<string, string> res = new Dictionary<string, string>();
            foreach (var item in validationRP)
            {
                switch (item.GetType().Name)
                {
                    case "Required":
                        {
                            lookupHtmlAttributes.Add("Style", "border-left-color:rgb(236,99,22);border-left-width:5px;");
                            break;
                        }
                }
                result = item.CreateRelatedValidation().Concat(result);

            }
            result.ToList().ForEach(item => res.Add(item.Key, item.Value));
            return res;
        }

        //old
        //private static TagBuilder CreatLookupButton()
        //{
        //    TagBuilder button = new TagBuilder("span");
        //    button.MergeAttribute("id", string.Format("btnRP_{0}", Id));
        //    button.AddCssClass(string.Format("{0} {1}", StyleKind.RighSpace, StyleKind.Button));
        //    TagBuilder icon = new TagBuilder("span");
        //    icon.AddCssClass(StyleKind.Icons.LookUP);
        //    button.InnerHtml = icon.ToString() + ShowLookupScript();
        //    return button;
        //}
        
      
        
        //private static string ShowLookupScript()
        //{
        //    var script = @"<script>  $(document).ready(function () {  " +
        //       "$('#" + string.Format("btnRP_{0}", Id) + "').click(function () { var temp =$(this);  if( temp.attr('disabled')!== 'disabled') "
        //          + "LookUp.show('" + Title + "','lkp_" + Id + "_Div'" + @" );"
        //           + "else return false;    });} );</script>";
        //    return script;
        //}

        //----------------------------new
        private static TagBuilder CreatLookupButton(Lookup.Grid lookInfo)
        {
            TagBuilder button = new TagBuilder("span");
            button.MergeAttribute("id", string.Format("btnRP_{0}", Id));
            button.AddCssClass(string.Format("{0} {1}", StyleKind.RighSpace, StyleKind.Button));
            TagBuilder icon = new TagBuilder("span");
            icon.AddCssClass(StyleKind.Icons.LookUP);
            button.InnerHtml = icon.ToString() + ShowLookupScript(lookInfo);
            return button;
        }

        private static TagBuilder CreatLookupButton(Lookup.Tree lookInfo)
        {
            TagBuilder button = new TagBuilder("span");
            button.MergeAttribute("id", string.Format("btnRP_{0}", Id));
            button.AddCssClass(string.Format("{0} {1}", StyleKind.RighSpace, StyleKind.Button));
            TagBuilder icon = new TagBuilder("span");
            icon.AddCssClass(StyleKind.Icons.LookUP);
            button.InnerHtml = icon.ToString() + ShowLookupScript(lookInfo);
            return button;
        }
      

      

        private static string ShowLookupScript(Lookup.Grid info)
        {
            var script = @"<script>  $(document).ready(function () {  " +
               "$('#" + string.Format("btnRP_{0}", Id) + "').click(function () { var temp =$(this);  if( temp.attr('disabled')!== 'disabled') "
                  + string.Format("Lookup.loadGrid('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},{10}",
                                       info.Title, 
                                       info.ViewModel,
                                       info.ViewModelProperty,
                                       info.LookupName,
                                       info.GridID ,
                                       info.PropertyNameForDisplay,
                                       info.PropertyNameForValue,
                                       info.PropertyNameForBinding,
                                       info.UseMultiSelect,
                                       info.Width,
                                       info.Height
                                        ) + @" );"
                   + "else return false;    });} );</script>";
            return script;
        }

        private static string ShowLookupScript(Lookup.Tree info)
        {
            var script = @"<script>  $(document).ready(function () {  " +
               "$('#" + string.Format("btnRP_{0}", Id) + "').click(function () { var temp =$(this);  if( temp.attr('disabled')!== 'disabled') "
                  + string.Format("Lookup.loadTree('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},{9}",
                                       info.Title,
                                       info.ViewModel,
                                       info.ViewModelProperty,
                                       info.LookupName,
                                       info.TreeID,
                                       info.PropertyNameForDisplay,
                                       info.PropertyNameForBinding,
                                       info.UseMultiSelect,
                                       info.Width,
                                       info.Height
                                        ) + @" );"
                   + "else return false;    });} );</script>";
            return script;
        }
    }
}
