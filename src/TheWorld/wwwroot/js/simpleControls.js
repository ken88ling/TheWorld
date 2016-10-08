(function() {

    "use strict";

    angular.module("simpleControls", [])
        .directive("waitCursor", waitCursor);

    function waitCursor() {
        return {
            scope: {
              show: "=displayWhen"  
            },
            retrict:"E",//Restrict E say's restrict it to only the element style
            templateUrl:"/views/waitCursor.html"
        };
    }

})();