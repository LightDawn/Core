﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc;

namespace Core.Mvc.Attribute.Filter
{
    public class ModelFieldTypeInfo : JsonObject
    {
        public ModelFieldTypeInfo()
        {
        }
        //public string ViewModelPropName { get; set; }
        public string ModelPropName { get; set; }
        public string CustomType { get; set; }
        public string FalseEquivalent { get; set; }
        public string TrueEquivalent { get; set; }
        public bool IdReplacement { get; set; }
        public string NavigationProperty { get; set; }
        public Dictionary<int,  string> EnumKeyValue { get; set; }

        protected override void Serialize(IDictionary<string, object> json)
        {
            //json["vmPropName"] = ViewModelPropName;
            if (!string.IsNullOrEmpty(ModelPropName))
            {
                json["mdlPropName"] = ModelPropName;
            }

            if (!string.IsNullOrEmpty(CustomType))
            {
                json["custType"] = CustomType;
            }

            if (!string.IsNullOrEmpty(FalseEquivalent))
            {
                json["falseEqui"] = FalseEquivalent;
            }

            if (!string.IsNullOrEmpty(TrueEquivalent))
            {
                json["trueEqui"] = TrueEquivalent;
            }

            if (EnumKeyValue != null && EnumKeyValue.Count > 0)
            {
                json["enumDic"] = EnumKeyValue;
            }

            if (IdReplacement)
            {
                json["IdReplacement"] = IdReplacement;
            }

            if (!string.IsNullOrEmpty(NavigationProperty))
            {
                json["navProp"] = NavigationProperty;
            }

        }
    }
}
