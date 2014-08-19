
FormJS = function () {
    this._jsObjectList = null;
    this._jsObjectInfoList = null;
    //--------------------------------------------------
    this._ready = function () {
        this._jsObjectList = new Array();
        this._createJSObjects();
        this._validator = null;

       // var tempThis = this;
       // $("form").submit(function () {
           
               // $("body").block({ message: null });
                //$.each(tempThis._jsObjectInfoList, function (id, info) {
                //    if (info.InfoType == Tools.JSClassInfoKind.InputBoxRP && info.Kind == "Money") {
                //        $("#" + info.Id).val($("#" + info.Id).rawVal());
                //    }
                   
                //});
           
            
       // });
        
        $(window).unload(function () { formJS._dispose() });
        this._callformJSUserFunc("onLoad");
        this._addEventHandler();
    };

    this._submit = function (eventArgs) {
        var validationResult = this._callformJSUserFunc("onBeforeValidation");
        if (typeof validationResult == "boolean" && !validationResult)
            return validationResult;

        validationResult = this._validator.onSubmitValidation(this);
        var ret;
        if (validationResult)
            ret = this._callformJSUserFunc("onBeforeSubmit", validationResult);
        if (typeof ret == "boolean")
            return validationResult && ret;
        else
            return validationResult;

    };
    this._dispose = function () {
        //this._removeEventHandler();
        $("form").off("submit");
        $.each(this._jsObjectList, function (id, obj) {
            obj.dispose();
        });
        this._callformJSUserFunc("onDispose");
    };

    //-------------------------------------------------
    this._createJSObjects = function () {
        if (this._jsObjectInfoList == null)
            return;
        var jsObjectList = this._jsObjectList;
        var tempObj = null;
        //$.each(this._jsObjectInfoList, function (id, info) {
        //    if (info.InfoType == Tools.JSClassInfoKind.InputBoxRP) {
        //        tempObj = new Tools.InputBoxRP(info);
        //        tempObj.initialize();
        //        jsObjectList.push(tempObj);
        //    }
        //    else if (info.InfoType == Tools.JSClassInfoKind.DropDownListX) {
        //        tempObj = new Tools.DropDownListX(info);
        //        tempObj.initialize();
        //        jsObjectList.push(tempObj);
        //    }
        //    
        //    else if (info.InfoType == Tools.JSClassInfoKind.CheckBoxX) {
        //        tempObj = new Tools.CheckBoxX(info);
        //        tempObj.initialize();
        //        jsObjectList.push(tempObj);
        //    }
        //    /
        //});


    };
}