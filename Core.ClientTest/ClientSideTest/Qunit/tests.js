/// <reference path="qunit.js" />
/// <reference path="../javascript1.js" />

test("hello test", function () {
    ok(1 == "1", "Passed!");
});

test("EQuality Test", function () {
    var value = returnHelloWord();
    equal("Hello", value, "Expect to return the word Hello");
});