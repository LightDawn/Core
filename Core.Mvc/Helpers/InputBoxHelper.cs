using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;
using System.Linq.Expressions;
using System;
using System.Text;
using Kendo.Mvc.UI;
using Core.Mvc.Extensions;
using Core.Cmn.Extensions;
namespace Core.Mvc.Helpers
{

    public static class InputBoxHelper
    {

        #region textbox
        public static MvcHtmlString TextBoxRP(this HtmlHelper helper, string id, string name, string cssClass, string style, bool readOnly, Dictionary<string, object> htmlAttributes)
        {

            htmlAttributes = htmlAttributes ?? new Dictionary<string, object>();
            htmlAttributes.Add("id", id);
            if (style.HasValue() )
            {
                htmlAttributes.Add("Style", style);
            }
            if (readOnly)
            {
                htmlAttributes.Add("readOnly", readOnly);
            }
            if (cssClass.HasValue())
            {
                htmlAttributes.Add("class", cssClass);
            }

            HtmlModifier.ManageHtmlAttributes(htmlAttributes, StyleKind.TextBox);

            return System.Web.Mvc.Html.InputExtensions.TextBox(helper, name, string.Empty, htmlAttributes);
        }
        public static MvcHtmlString TextBoxRP(this HtmlHelper helper, string id)
        {
            return helper.TextBoxRP(id, id, string.Empty, string.Empty, false, null);
        }

        public static MvcHtmlString TextBoxRP(this HtmlHelper helper, string id, Dictionary<string, object> htmlAttributes)
        {
            return helper.TextBoxRP(id, id, string.Empty, string.Empty, false, htmlAttributes);
        }

        public static MvcHtmlString TextBoxRP(this HtmlHelper helper, string id, string name)
        {
            return helper.TextBoxRP(id, name, string.Empty, string.Empty, false, null);
        }

        public static MvcHtmlString TextBoxRP(this HtmlHelper helper, string id, string name, string cssClass)
        {
            return helper.TextBoxRP(id, name, cssClass, string.Empty, false, null);
        }

        public static MvcHtmlString TextBoxRP(this HtmlHelper helper, string id, string style, Dictionary<string, object> htmlAttributes)
        {
            return helper.TextBoxRP(id, id, string.Empty, style, false, htmlAttributes);
        }
        public static MvcHtmlString TextBoxRP(this HtmlHelper helper, string id, bool readOnly, Dictionary<string, object> htmlAttributes)
        {
            return helper.TextBoxRP(id, id, string.Empty, string.Empty, readOnly, htmlAttributes);
        }
        public static MvcHtmlString TextBoxRP(this HtmlHelper helper, string id, string name, string cssClass, string style, bool readOnly)
        {
            return helper.TextBoxRP(name, id, cssClass, style, readOnly, null);
        }



        #endregion

        #region textboxFor<TModel, TProperty>
        public static MvcHtmlString TextBoxRPFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TProperty>> expression, string format, object htmlAttributes)
        {

            Dictionary<string, object> attributes = new Dictionary<string, object>(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            HtmlModifier.ManageHtmlAttributes(attributes, StyleKind.TextBox);

            return System.Web.Mvc.Html.InputExtensions.TextBoxFor<TModel, TProperty>(helper, expression, format, attributes);
        }
        public static MvcHtmlString TextBoxRPFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TProperty>> expression)
        {
            return System.Web.Mvc.Html.InputExtensions.TextBoxFor<TModel, TProperty>(helper, expression, HtmlModifier.ManageHtmlAttributes(StyleKind.TextBox));
        }
        public static MvcHtmlString TextBoxRPFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TProperty>> expression, Dictionary<string, object> htmlAttributes)
        {
            Dictionary<string, object> attributes = new Dictionary<string, object>(htmlAttributes);
            HtmlModifier.ManageHtmlAttributes(attributes, StyleKind.TextBox);
            return System.Web.Mvc.Html.InputExtensions.TextBoxFor<TModel, TProperty>(helper, expression, attributes);
        }
        public static MvcHtmlString TextBoxRPFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            Dictionary<string, object> attributes = new Dictionary<string, object>(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            HtmlModifier.ManageHtmlAttributes(attributes, StyleKind.TextBox);
            return System.Web.Mvc.Html.InputExtensions.TextBoxFor<TModel, TProperty>(helper, expression, attributes);
        }
        public static MvcHtmlString TextBoxRPFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TProperty>> expression, string format, Dictionary<string, object> htmlAttributes)
        {
            Dictionary<string, object> attributes = new Dictionary<string, object>(htmlAttributes);
            HtmlModifier.ManageHtmlAttributes(attributes, StyleKind.TextBox);
            return System.Web.Mvc.Html.InputExtensions.TextBoxFor<TModel, TProperty>(helper, expression, format, attributes);
        }
        #endregion

        #region textArea
        public static MvcHtmlString TextAreaRP(this HtmlHelper helper, string id, string name, int rows, int columns, Dictionary<string, object> htmlAttributes, string style, string cssClass, bool readOnly)
        {
            htmlAttributes = htmlAttributes ?? new Dictionary<string, object>();
            htmlAttributes.Add("id", id);
            if (!string.IsNullOrEmpty(style))
            {
                htmlAttributes.Add("Style", style);
            }
            if (readOnly)
            {
                htmlAttributes.Add("readOnly", readOnly);
            }
            if (!string.IsNullOrEmpty(cssClass))
            {
                htmlAttributes.Add("class", cssClass);
            }

            HtmlModifier.ManageHtmlAttributes(htmlAttributes, StyleKind.TextBox);

            return System.Web.Mvc.Html.TextAreaExtensions.TextArea(helper, name, string.Empty, rows, columns, htmlAttributes);
        }

        public static MvcHtmlString TextAreaRP(this HtmlHelper helper, string id, int rows, int columns)
        {
            return helper.TextAreaRP(id, id, rows, columns, null, string.Empty, string.Empty, false);
        }

        public static MvcHtmlString TextAreaRP(this HtmlHelper helper, string id, int rows, int columns, Dictionary<string, object> htmlAttributes)
        {
            return helper.TextAreaRP(id, id, rows, columns, htmlAttributes, string.Empty, string.Empty, false);
        }

        public static MvcHtmlString TextAreaRP(this HtmlHelper helper, string id, int rows, int columns, Dictionary<string, object> htmlAttributes, bool readOnly)
        {
            return helper.TextAreaRP(id, id, rows, columns, htmlAttributes, string.Empty, string.Empty, readOnly);

        }
        public static MvcHtmlString TextAreaRP(this HtmlHelper helper, string id, int rows, int columns, string cssClass)
        {
            return helper.TextAreaRP(id, id, rows, columns, null, string.Empty, cssClass, false);
        }
        public static MvcHtmlString TextAreaRP(this HtmlHelper helper, string id, int rows, int columns, string cssClass, bool readOnly)
        {
            return helper.TextAreaRP(id, id, rows, columns, null, string.Empty, cssClass, readOnly);
        }

        public static MvcHtmlString TextAreaRP(this HtmlHelper helper, string id, string name, int rows, int columns)
        {
            return helper.TextAreaRP(id, name, rows, columns, null, string.Empty, string.Empty, false);
        }

        public static MvcHtmlString TextAreaRP(this HtmlHelper helper, string id, string name, int rows, int columns, bool readOnly)
        {
            return helper.TextAreaRP(id, name, rows, columns, null, string.Empty, string.Empty, readOnly);
        }
        public static MvcHtmlString TextAreaRP(this HtmlHelper helper, string id, string name, int rows, int columns, Dictionary<string, object> htmlAttributes)
        {
            return helper.TextAreaRP(id, name, rows, columns, htmlAttributes);

        }


        #endregion

        #region textAreaRPFor<TModel,TProperty>
        public static MvcHtmlString TextAreaRPFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TProperty>> expression)
        {
            return System.Web.Mvc.Html.TextAreaExtensions.TextAreaFor<TModel, TProperty>(helper, expression, HtmlModifier.ManageHtmlAttributes(StyleKind.TextBox));
        }
        public static MvcHtmlString TextAreaRPFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TProperty>> expression, Dictionary<string, object> htmlAttributes)
        {
            HtmlModifier.ManageHtmlAttributes(htmlAttributes, StyleKind.TextBox);
            
            return System.Web.Mvc.Html.TextAreaExtensions.TextAreaFor<TModel, TProperty>(helper, expression, htmlAttributes);
        }
        public static MvcHtmlString TextAreaRPFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            Dictionary<string, object> attributes = new Dictionary<string, object>(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            
            HtmlModifier.ManageHtmlAttributes(attributes, StyleKind.TextBox);
            
            return System.Web.Mvc.Html.TextAreaExtensions.TextAreaFor<TModel, TProperty>(helper, expression, attributes);
        }
        public static MvcHtmlString TextAreaRPFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TProperty>> expression, int rows, int columns, object htmlAttributes)
        {
            Dictionary<string, object> attributes = new Dictionary<string, object>(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            
            HtmlModifier.ManageHtmlAttributes(attributes, StyleKind.TextBox);
           
            return System.Web.Mvc.Html.TextAreaExtensions.TextAreaFor<TModel, TProperty>(helper, expression, rows, columns, attributes);
        }
        #endregion

        #region PasswordRP
        public static MvcHtmlString PasswordRP(this HtmlHelper helper, string id, string name, string cssClass, string style, bool readOnly, Dictionary<string, object> htmlAttributes)
        {
            htmlAttributes = htmlAttributes ?? new Dictionary<string, object>();
            htmlAttributes.Add("id", id);
            if (!string.IsNullOrEmpty(style))
            {
                htmlAttributes.Add("Style", style);
            }
            if (readOnly)
            {
                htmlAttributes.Add("readOnly", readOnly);
            }
            if (!string.IsNullOrEmpty(cssClass))
            {
                htmlAttributes.Add("class", cssClass);
            }

            HtmlModifier.ManageHtmlAttributes(htmlAttributes, StyleKind.TextBox);

            return System.Web.Mvc.Html.InputExtensions.Password(helper, name, string.Empty, htmlAttributes);
        }
        public static MvcHtmlString PasswordRP(this HtmlHelper helper, string id)
        {
            return helper.PasswordRP(id, id, string.Empty, string.Empty, false, null);
        }

        public static MvcHtmlString PasswordRP(this HtmlHelper helper, string id, Dictionary<string, object> htmlAttributes)
        {
            return helper.PasswordRP(id, id, string.Empty, string.Empty, false, htmlAttributes);
        }

        public static MvcHtmlString PasswordRP(this HtmlHelper helper, string id, string name)
        {
            return helper.PasswordRP(id, name, string.Empty, string.Empty, false, null);
        }

        public static MvcHtmlString PasswordRP(this HtmlHelper helper, string id, string name, string cssClass)
        {
            return helper.PasswordRP(id, name, cssClass, string.Empty, false, null);
        }

        public static MvcHtmlString PasswordRP(this HtmlHelper helper, string id, string style, Dictionary<string, object> htmlAttributes)
        {
            return helper.PasswordRP(id, id, string.Empty, style, false, htmlAttributes);
        }

        public static MvcHtmlString PasswordRP(this HtmlHelper helper, string id, string name, string cssClass, string style, bool readOnly)
        {
            return helper.PasswordRP(name, id, cssClass, style, readOnly, null);
        }
        #endregion

        #region PasswordRPFor<TModel, TProperty>

        public static MvcHtmlString PasswordRPFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TProperty>> expression
            , Dictionary<string, object> htmlAttributes, bool checkStrength)
        {
            htmlAttributes = htmlAttributes ?? new Dictionary<string, object>();

            HtmlModifier.ManageHtmlAttributes(htmlAttributes, StyleKind.TextBox);

            if (checkStrength)
            {
                htmlAttributes.Add("onkeyup", "strenghtChecker(this)");
                htmlAttributes.Add("onblur", "hideChecker($(this).next('span'))");
            }

            var memberExp = expression.Body as MemberExpression;

            var strengthChecker = memberExp.Member.GetCustomAttributes(typeof(StrengthChecker), true);

            if (strengthChecker.Length > 0)
            {
                PasswordChecker validation = new PasswordChecker();

                foreach (var item in validation.CreateRelatedValidation())
                {
                    htmlAttributes.Add(item.Key, item.Value);
                }

            }

            var htmlPassword = System.Web.Mvc.Html.InputExtensions.PasswordFor(helper, expression, htmlAttributes);

            TagBuilder stateBuilder = new TagBuilder("span");

            return MvcHtmlString.Create(htmlPassword.ToHtmlString() + stateBuilder);
        }

        public static MvcHtmlString PasswordRPFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TProperty>> expression)
        {
            return PasswordRPFor<TModel, TProperty>(helper, expression, null, false);
        }

        public static MvcHtmlString PasswordRPFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TProperty>> expression, bool checkStrength)
        {
            return PasswordRPFor<TModel, TProperty>(helper, expression, null, checkStrength);
        }

        public static MvcHtmlString PasswordRPFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            Dictionary<string, object> attributes = new Dictionary<string, object>(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            return PasswordRPFor<TModel, TProperty>(helper, expression, attributes, false);
        }
        #endregion

        #region HiddenRP
        public static MvcHtmlString HiddenRP(this HtmlHelper helper, string name, string value, IDictionary<string, object> htmlAttributes, string cssClass)
        {
            htmlAttributes.Add("Style", cssClass);

            return System.Web.Mvc.Html.InputExtensions.Hidden(helper, name, value, htmlAttributes);
        }
        public static MvcHtmlString HiddenRP(this HtmlHelper helper, string name, string value, object htmlAttributes)
        {
            return System.Web.Mvc.Html.InputExtensions.Hidden(helper, name, value, htmlAttributes);
        }
        public static MvcHtmlString HiddenRP(this HtmlHelper helper, string name, string value)
        {
            return System.Web.Mvc.Html.InputExtensions.Hidden(helper, name, value);
        }
        public static MvcHtmlString HiddenRP(this HtmlHelper helper, string name)
        {
            return System.Web.Mvc.Html.InputExtensions.Hidden(helper, name);
        }
        #endregion

        #region HiddenRPFor<TModel, TProperty>
        public static MvcHtmlString HiddenRPFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TProperty>> expression)
        {
            return System.Web.Mvc.Html.InputExtensions.HiddenFor(helper, expression);
        }
        public static MvcHtmlString HiddenRPFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            return System.Web.Mvc.Html.InputExtensions.HiddenFor(helper, expression, htmlAttributes);
        }
        public static MvcHtmlString HiddenRPFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return System.Web.Mvc.Html.InputExtensions.HiddenFor(helper, expression, htmlAttributes);
        }
        #endregion

        #region RadioButtonRP
        public static MvcHtmlString RadioButtonRP(this HtmlHelper helper, string name, string value, bool isChecked, IDictionary<string, object> htmlAttributes, string cssClass)
        {
            htmlAttributes.Add("Style", cssClass);

            return System.Web.Mvc.Html.InputExtensions.RadioButton(helper, name, value, isChecked, htmlAttributes);
        }
        public static MvcHtmlString RadioButtonRP(this HtmlHelper helper, string name, string value, bool isChecked, object htmlAttributes)
        {
            return System.Web.Mvc.Html.InputExtensions.RadioButton(helper, name, value, isChecked, htmlAttributes);
        }
        public static MvcHtmlString RadioButtonRP(this HtmlHelper helper, string name, string value, bool isChecked)
        {
            return System.Web.Mvc.Html.InputExtensions.RadioButton(helper, name, value, isChecked);
        }
        public static MvcHtmlString RadioButtonRP(this HtmlHelper helper, string name, string value)
        {
            return System.Web.Mvc.Html.InputExtensions.RadioButton(helper, name, value);
        }
        #endregion

        #region RadioButtonRPFor<TModel, TProperty>
        public static MvcHtmlString RadioButtonRPFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TProperty>> expression, object value)
        {
            return System.Web.Mvc.Html.InputExtensions.RadioButtonFor(helper, expression, value);
        }
        public static MvcHtmlString RadioButtonRPFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TProperty>> expression, object value, IDictionary<string, object> htmlAttributes)
        {
            return System.Web.Mvc.Html.InputExtensions.RadioButtonFor(helper, expression, value, htmlAttributes);
        }
        public static MvcHtmlString RadioButtonRPFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TProperty>> expression, object value, object htmlAttributes)
        {
            return System.Web.Mvc.Html.InputExtensions.RadioButtonFor(helper, expression, value, htmlAttributes);
        }
        #endregion

        #region CheckBoxRP
        public static MvcHtmlString CheckBoxRP(this HtmlHelper helper, string name, string value, bool isChecked, IDictionary<string, object> htmlAttributes, string cssClass)
        {
            htmlAttributes.Add("Style", cssClass);

            return System.Web.Mvc.Html.InputExtensions.CheckBox(helper, name, isChecked, htmlAttributes);
        }
        public static MvcHtmlString CheckBoxRP(this HtmlHelper helper, string name, string value, bool isChecked, object htmlAttributes)
        {
            return System.Web.Mvc.Html.InputExtensions.CheckBox(helper, name, isChecked, htmlAttributes);
        }
        public static MvcHtmlString CheckBoxRP(this HtmlHelper helper, string name, bool isChecked)
        {
            return System.Web.Mvc.Html.InputExtensions.CheckBox(helper, name, isChecked);
        }
        public static MvcHtmlString CheckBoxRP(this HtmlHelper helper, string name)
        {
            return System.Web.Mvc.Html.InputExtensions.CheckBox(helper, name);
        }
        #endregion

        #region CheckBoxRPFor<TModel, TProperty>

        public static MvcHtmlString CheckBoxRPFor<TModel>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, bool>> expression,string id, Dictionary<string, object> htmlAttributes)
        {
            htmlAttributes = htmlAttributes ?? new Dictionary<string, object>();

            if (id.HasValue())
            {
                htmlAttributes.Add("id", id);
            }

            HtmlModifier.ManageHtmlAttributes(htmlAttributes, StyleKind.checkBox);

            return System.Web.Mvc.Html.InputExtensions.CheckBoxFor(helper, expression, htmlAttributes);
        }

        public static MvcHtmlString CheckBoxRPFor<TModel>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, bool>> expression)
        {
            return CheckBoxRPFor<TModel>(helper, expression,string.Empty, null);
        }

        public static MvcHtmlString CheckBoxRPFor<TModel>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, bool>> expression, Dictionary<string, object> htmlAttributes)
        {
            return CheckBoxRPFor<TModel>(helper, expression, string.Empty, htmlAttributes);
        }
        public static MvcHtmlString CheckBoxRPFor<TModel>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, bool>> expression, object htmlAttributes)
        {
            Dictionary<string, object> attributes = new Dictionary<string, object>(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            
            return CheckBoxRPFor<TModel>(helper, expression,string.Empty, attributes);
        }

        #endregion

        #region CheckBoxListRP
        public static MvcHtmlString CheckBoxListRP(this HtmlHelper helper, string name, List<CheckBoxListInfo> checkBoxList, IDictionary<string, object> htmlAttributes)
        {

            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("The argument must have a value", "name");
            if (checkBoxList == null)
                throw new ArgumentNullException("listInfo");
            if (checkBoxList.Count < 1)
                throw new ArgumentException("The list must contain at least one value", "checkBoxList");


            StringBuilder totalTags = new StringBuilder();

            foreach (CheckBoxListInfo info in checkBoxList)
            {
                TagBuilder builder = new TagBuilder("input");
                if (info.IsChecked)
                    builder.MergeAttribute("checked", "checked");
                builder.MergeAttributes<string, object>(htmlAttributes);
                builder.MergeAttribute("type", "checkbox");
                builder.MergeAttribute("value", info.Value);
                builder.MergeAttribute("name", name);
                builder.SetInnerText(info.Text);
                totalTags.Append(builder + "<br/>");
            }

            return MvcHtmlString.Create(totalTags.ToString());

        }
        public static MvcHtmlString CheckBoxListRP(this HtmlHelper helper, string name, MultiSelectList checkBoxList, IDictionary<string, object> htmlAttributes)
        {

            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("The argument must have a value", "name");
            if (checkBoxList == null)
                throw new ArgumentNullException("listInfo");

            StringBuilder totalTags = new StringBuilder();

            foreach (SelectListItem info in checkBoxList)
            {
                TagBuilder builder = new TagBuilder("input");
                if (info.Selected)
                    builder.MergeAttribute("checked", "checked");
                if (htmlAttributes.Count > 0)
                    builder.MergeAttributes<string, object>(htmlAttributes);
                builder.MergeAttribute("type", "checkbox");
                builder.MergeAttribute("value", info.Value);
                builder.MergeAttribute("name", name);
                builder.SetInnerText(info.Text);
                totalTags.Append(builder + "<br/>");
            }

            return MvcHtmlString.Create(totalTags.ToString());

        }

        public static MvcHtmlString CheckBoxListRP(this HtmlHelper helper, string name, MultiSelectList checkBoxList)
        {
            return CheckBoxListRP(helper, name, checkBoxList, null);
        }
        #endregion

        #region MoneyTextBoxRP
        public static MvcHtmlString MoneyTextBoxRP(this HtmlHelper helper,string id, string name, Dictionary<string, object> htmlAttributes,
                                                       bool showSpinner, string placeHolder, decimal? defaultValue)
        {
            Kendo.Mvc.UI.Fluent.NumericTextBoxBuilder<decimal> numericTextbox = helper.Kendo()
                                                                                .NumericTextBox<decimal>()
                                                                                .Name(name);

            numericTextbox = numericTextbox.Value(defaultValue);

            htmlAttributes = htmlAttributes ?? new Dictionary<string, object>();

            if (id.HasValue())
            {
                htmlAttributes.Add("id", id);
            }

            if (htmlAttributes.Count> 0)
            {
                numericTextbox = numericTextbox.HtmlAttributes(htmlAttributes);
            }

            if (placeHolder.HasValue())
            {
                numericTextbox = numericTextbox.Placeholder(placeHolder);
            }

            numericTextbox = numericTextbox.Format("c0").Spinners(false);


            TagBuilder container = new TagBuilder("span");
            var numericTextboxStr = numericTextbox.ToHtmlString();
            container.AddCssClass((numericTextboxStr.Contains("data-val-required") ? StyleKind.RequiredInput : StyleKind.OptionalInput));
            //    var script =string.Format(@"<script> $(document).ready(function(){{   var numeric = $('#{0}').kendoNumericTextBox().data('kendoNumericTextBox');"+
            //@"numeric.wrapper.find('.k-numeric-wrap').find('.k-select').hide();" +
            //@"numeric.wrapper.find('.k-numeric-wrap').addClass('k-textbox').removeClass('k-numeric-wrap');" +
            //@"numeric.wrapper.find('.k-textbox').find('.k-formatted-value').removeClass('k-input');}}); </script>", Name);

            //if(numericTextbox.ToHtmlString().Contains("data-val-required"))
            //green= border-left-color: rgb(24, 165, 57);
            //var script = "<script>jQuery(function(){  $('#" + Name + "').parent().addClass('" +
            //    (numericTextbox.ToHtmlString().Contains("data-val-required")?StyleKind.RequiredInput:StyleKind.OptionalInput)
            //    +"');});</script>";
            //container.InnerHtml = numericTextbox.ToHtmlString()+script;
            container.InnerHtml = numericTextboxStr;


            return MvcHtmlString.Create(container.ToString());
        }

        public static MvcHtmlString MoneyTextBoxRP(this HtmlHelper helper, string name, Dictionary<string, object> htmlAttributes)
        {
            return MoneyTextBoxRP(helper,string.Empty, name, htmlAttributes, false, null, null);
        }
        public static MvcHtmlString MoneyTextBoxRP(this HtmlHelper helper, string name)
        {
            return MoneyTextBoxRP(helper,string.Empty, name, null, false, null, null);
        }



        #endregion

#region TimePicker
        public static MvcHtmlString TimePickerRP(this HtmlHelper helper, string id, string min, string max ,string changeEvent, string openEvent, string closeEvent, int interval
            , Dictionary<string, object> htmlAttributes, string defaultValue)
        {
            Kendo.Mvc.UI.Fluent.TimePickerBuilder timePicker = helper.Kendo().TimePicker()
                .Name(id)
                .Min(min)
                .Max(max)
                .Value(defaultValue)
                .Interval(interval)
                .Events(t => 
                   t.Change(changeEvent)
                   .Open(openEvent)
                   .Close(closeEvent))
                .HtmlAttributes(htmlAttributes);

            return MvcHtmlString.Create(timePicker.ToHtmlString());
        }

        public static MvcHtmlString TimePickerRP(this HtmlHelper helper, string id, DateTime min, DateTime max, Dictionary<string, object> htmlAttributes, DateTime defaultValue)
        {
            Kendo.Mvc.UI.Fluent.TimePickerBuilder timePicker = helper.Kendo().TimePicker()
                .Name(id)
                .Min(min)
                .Max(max)
                .Value(defaultValue)
                .HtmlAttributes(htmlAttributes);

            return MvcHtmlString.Create(timePicker.ToHtmlString());
        }

        public static MvcHtmlString TimePickerRP(this HtmlHelper helper, string id, string min, string max, int interval)
        {
            Kendo.Mvc.UI.Fluent.TimePickerBuilder timePicker = helper.Kendo().TimePicker()
                .Name(id)
                .Max(max)
                .Min(min)
                .Interval(interval);

            return MvcHtmlString.Create(timePicker.ToHtmlString());
        }

        public static MvcHtmlString TimePickerRP(this HtmlHelper helper, string id, DateTime min, DateTime max, int interval)
        {
            Kendo.Mvc.UI.Fluent.TimePickerBuilder timePicker = helper.Kendo().TimePicker()
                .Name(id)
                .Max(max)
                .Min(min)
                .Interval(interval);

            return MvcHtmlString.Create(timePicker.ToHtmlString());
        }

        public static MvcHtmlString TimePickerRP(this HtmlHelper helper, string id, int interval)
        {
            Kendo.Mvc.UI.Fluent.TimePickerBuilder timePicker = helper.Kendo().TimePicker()
                .Name(id)
                .Interval(interval);

            return MvcHtmlString.Create(timePicker.ToHtmlString());
        }

        public static MvcHtmlString TimePickerRP(this HtmlHelper helper, string id, int interval,string changeEvent="", string openEvent="", string closeEvent="")
        {
            Kendo.Mvc.UI.Fluent.TimePickerBuilder timePicker = helper.Kendo().TimePicker()
               .Name(id)
               .Interval(interval)
               .Events(t => 
                   t.Change(changeEvent)
                   .Open(openEvent)
                   .Close(closeEvent));

            return MvcHtmlString.Create(timePicker.ToHtmlString());
        }

       
        

        public static MvcHtmlString TimePickerRP(this HtmlHelper helper, string id)
        {
            Kendo.Mvc.UI.Fluent.TimePickerBuilder timePicker = helper.Kendo().TimePicker()
                .Name(id);

            return MvcHtmlString.Create(timePicker.ToHtmlString());
        }

        



#endregion
    }


    public enum InputKind
    {
        // text ,
        submit,//IMP
        button,//IMP
        // checkbox,
        // radio,
        // hidden,
        // password,
        //image,
        //reset,
        //file,
        //color,
        //date,
        //datetime,
        //email,
        //month,
        //number,
        //range,
        //search,
        //tel,
        //time,
        //url,
        Money
        //week="week"

    }

    public class CheckBoxListInfo
    {
        //public CheckBoxListInfo(string value, string text, bool isChecked)
        //{
        //    this.Value = value;
        //    this.Text = text;
        //    this.IsChecked = isChecked;
        //}

        public string Value { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }
}
