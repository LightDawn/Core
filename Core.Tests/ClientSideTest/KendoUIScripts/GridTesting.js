/// <reference path="../../../sampleproject/scripts/kendo/2013.1.514/jquery.min.js" />
/// <reference path="../../../sampleproject/scripts/kendo/2013.1.514/kendo.all.min.js" />
/// <reference path="../../../sampleproject/scripts/kendo/2013.1.514/kendo.aspnetmvc.min.js" />
/// <reference path="../qunit/qunit.js" />
/// <reference path="../../../sampleproject/views/viewpage1.html" />


var dom = $("div.k-rtl");
test("the grid creates a row for every item in the data source", function () {
    //var dom = $("div.k-rtl");
    dom.kendoGrid({
        dataSource: [{ SelectedRoleId: "1", Id: "5", FName: "behnam", LName: "alavi", UserName: "behnam.alavi@gmail.com" }]//[{ foo: "foo" }]
    });
    equal(dom.find("tbody > tr").length, 1);
    //equal($("#grid tbody > tr").length, 1);
});

//$("#kjsimpleGrid").kendoGrid({
//    groupable: true, scrollable: true,
//    sortable: true, pageable: true, filterable: true,
//    dataSource: {
//        transport: { read: { url: "/Grid/GetEmployees" } }
//    },
//    columns: [
//        { title: "Id", field: "EmployeeID", width: 80 },
//        { title: "Last", field: "LastName", width: 100 },
//        { title: "First", field: "FirstName", width: 100 },
//        { title: "Title", field: "Title", width: 200 },
//        { title: "City", field: "City", width: 200 },
//        { title: "Region", field: "Region" }]
//});

//public JsonResult GetEmployees()

//{

//    var _emps =

//        (from e in _db.Employees

//            select new

//    {

//                EmployeeID = e.EmployeeID,

//                LastName = e.LastName,

//                FirstName = e.FirstName,

//                Title = e.Title,

//                TitleOfCourtesy = e.TitleOfCourtesy,

//                BirthDate = e.BirthDate,

//                HireDate = e.HireDate,

//                Address = e.Address,

//                City = e.City,

//                Region = (e.Region == null ? "": e.Region)

//}).ToList();

//return Json(_emps, JsonRequestBehavior.AllowGet);

//}

