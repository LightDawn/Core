/// <reference path="../../../sampleproject/scripts/jquery-1.8.2.min.js" />
/// <reference path="../../../sampleproject/scripts/kendo.all.min.js" />
/// <reference path="../../../sampleproject/scripts/kendo.aspnetmvc.min.js" />
/// <reference path="../qunit/qunit.js" />



var dom = $("div.k-rtl");
dom.kendoGrid({
    autoBind: false,
    dataSource: {
        type: "odata",
        transport: {
            read: {
                url: "http://demos.kendoui.com/service/Northwind.svc/Orders"
            }
        },
        serverPaging: true,
        pageSize: 10
    }
});

asyncTest("the grid creates a row for every item in the data source", 1, function () {
    var grid = dom.data("kendoGrid");

    grid.one("dataBound", function () {
        equal(dom.find(" tbody > tr").length, 10);
        start();
    });
    grid.dataSource.read();
});