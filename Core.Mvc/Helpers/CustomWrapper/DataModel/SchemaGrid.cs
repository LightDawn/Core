using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Mvc.Extensions;
using System.Web.Mvc;
using System.Reflection;
using Core.Mvc.Attribute.Filter;
using Core.Cmn.Extensions;
using Core.Mvc.Extensions.FilterRelated;
//using Core.Cmn.Extensions;

namespace Core.Mvc.Helpers.CustomWrapper.DataModel
{
    public class SchemaGrid : DataSourceSchema
    {
        public Type GridViewModelType { get; private set; }

        public SchemaGrid(Type modelMetaData)
        {
            GridViewModelType = modelMetaData;
        }

        protected override void Serialize(IDictionary<string, object> json)
        {
            base.Serialize(json);

            if (GridViewModelType != null)
            {
                json["searchCustomTypes"] = GetColumnWithClrRelatedTypeForSearch();
            }
           
           

        }

        private  Dictionary<string ,object> GetColumnWithClrRelatedTypeForSearch()
        {
            PropertyInfo[] propInfos = GridViewModelType.GetProperties();
            var fieldTypeInfos = new Dictionary<string ,object>();
            foreach (var item in propInfos)
            {
                     var customAttrs = item.GetCustomAttributes(true);
                     ModelFieldTypeInfo fieldTypeInfo = null;
                    
                     foreach (var attrItem in customAttrs)
                     {
                         if (attrItem is SearchRelatedTypeAttribute)
                         {
                             InitialiseFieldTypeInfo(ref fieldTypeInfo);
                             var searchRelatedAttr  = (attrItem as SearchRelatedTypeAttribute);
                             fieldTypeInfo.CustomType = searchRelatedAttr.CustomType;
                             fieldTypeInfo.ModelPropName = searchRelatedAttr.MainPropertyNameOfModel;
                             fieldTypeInfo.TrueEquivalent = searchRelatedAttr.TrueEquivalent;
                             fieldTypeInfo.FalseEquivalent = searchRelatedAttr.FalseEquivalent;
                             fieldTypeInfo.NavigationProperty = searchRelatedAttr.NavigationProperty;
                             //fieldTypeInfo.IdReplacement = searchRelatedAttr.IdReplacement;
                         }

                         else if (attrItem is SearchRelatedEnumInfoAttribute)
                         {
                             InitialiseFieldTypeInfo(ref fieldTypeInfo);
                             var SearchRelatedEnumAttr = (attrItem as SearchRelatedEnumInfoAttribute);
                             fieldTypeInfo.CustomType = SearchRelatedEnumAttr.CustomType;
                             fieldTypeInfo.ModelPropName = SearchRelatedEnumAttr.MainPropertyNameOfModel;
                             var enumType = SearchRelatedEnumAttr.EnumType;
                             fieldTypeInfo.EnumKeyValue = enumType.GetEnumKeyValuePairEquivalents();
                         }
                         
                     }
                     if (fieldTypeInfo != null)
                     {
                         fieldTypeInfos.Add(item.Name, fieldTypeInfo.ToJson());
                     }
            }

            SetExcludingFields(fieldTypeInfos);
            return fieldTypeInfos;
        }

        private void SetExcludingFields(Dictionary<string, object> fieldTypeInfos)
        {
            var viewModelNonAttrs = GridViewModelType.GetCustomAttributes(false);//.FirstOrDefault(attr => attr.AttributeType.Name == "SearchExcludingFieldsAttribute");
            foreach(var attrItem in viewModelNonAttrs)
            {
                if (attrItem is SearchExcludingFieldsAttribute)
                {
                    var attr = (attrItem as SearchExcludingFieldsAttribute);
                    fieldTypeInfos.Add("excludingFields", attr.FieldsToExcludeFromSearchable);
                }
            } 
        }

        private void InitialiseFieldTypeInfo(ref ModelFieldTypeInfo fieldTypeInfo)
        {
            if (fieldTypeInfo == null)
            {
                fieldTypeInfo = new ModelFieldTypeInfo();
            }
        }
    }
}
