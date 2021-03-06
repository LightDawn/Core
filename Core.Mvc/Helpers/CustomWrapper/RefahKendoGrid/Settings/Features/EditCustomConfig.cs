﻿using Kendo.Mvc;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc.Html;
using System.Web.Script.Serialization;
using Core.Mvc.Helpers.RefahKendoGrid.Infrastructure;
using Core.Mvc.Helpers.CustomWrapper.Infrastructure;
using Newtonsoft.Json;

namespace Core.Mvc.Helpers.RefahKendoGrid.Settings.Features
{
    [Serializable()]
    public class EditCustomConfig : JsonObjectBase
    {
        public EditCustomConfig()
        {
            Template = new HtmlTemplateRP();
            Mode = GridEditMode.PopUp;
            Confirmation = false;
            EditWindow = new EditWindow();
        }
        public GridEditMode Mode { get; set; }
        public bool Confirmation { get; set; }
        public HtmlTemplateRP Template { get; private set; }
        public EditWindow EditWindow { get; set; }

        protected override void Serialize(IDictionary<string, object> json)
        {
            json["mode"] = Mode == GridEditMode.PopUp ? "popup" : "inline";
            json["template"] = string.Empty; 
            json["window"] = EditWindow.ToJson();
        }
    }
}
