/// <reference path="../../../sampleproject/scripts/kendo/2013.1.514/jquery.min.js" />
/// <reference path="../../../sampleproject/scripts/kendo/2013.1.514/kendo.all.min.js" />
/// <reference path="../../../sampleproject/scripts/kendo/2013.1.514/kendo.aspnetmvc.min.js" />
/// <reference path="../qunit/qunit.js" />
/// <reference path="../../../sampleproject/views/viewpage1.html" />

var dom = $("div.k-rtl");
//test("TestKendoGrid", function () {
    dom.kendoGrid({
        autoBind: false,
        //dataSource: [{ SelectedRoleId: "1", Id: "5", FName: "behnam", LName: "alavi", UserName: "behnam.alavi@gmail.com" }]
        dataSource: { transport: { read: { url: "http://localhost:8056/api/userapi" } } }
        //dataSource: {
        //    type: "json" ,
        //    transport: {
        //        read: {
        //            url: "htp://localhost:8056/api/userapi"   //"htp://demos.kendoui.com/service/Northwind.svc/Orders"
        //        }
        //    },
        // serverPaging: true,
        // pageSize: 10
    });

//});

asyncTest("the grid creates a row for every item in the data source", 1, function () {
    var grid = dom.data("kendoGrid");
    grid.one("dataBound", function () {
        equal(dom.find(" tbody > tr").length, 1);
        start();
    });
    grid.dataSource.read();
});