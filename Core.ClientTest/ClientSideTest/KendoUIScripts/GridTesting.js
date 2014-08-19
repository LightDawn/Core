/// <reference path="../../../sampleproject/scripts/jquery-1.8.2.min.js" />
/// <reference path="../../../sampleproject/scripts/kendo.all.min.js" />
/// <reference path="../../../sampleproject/scripts/kendo.aspnetmvc.min.js" />
/// <reference path="../qunit/qunit.js" />
/// <reference path="http://localhost:8056/ViewPage1" />


var dom = $("div.k-rtl");
test("the grid creates a row for every item in the data source", function () {
    //var dom = $("div.k-rtl");
    dom.kendoGrid({
        dataSource: [{ foo: "foo" }]
    });
    equal(dom.find("tbody > tr").length, 1);
    //equal($("#grid tbody > tr").length, 1);
});
   
