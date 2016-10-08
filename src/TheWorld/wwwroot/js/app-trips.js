(function () {

    "use strict";


    //Create the module
    angular.module("app-trips", ["simpleControls", "ngRoute"])
    .config(function ($routeProvider) {

        $routeProvider.when("/",
        {
            controller: "tripsController",//this it's going to be used for that individual view
            controllerAs: "vm", //for data binding
            templateUrl: "/views/tripsView.html"//represent what is actual view
        });

        $routeProvider.when("/editor/:tripName",
        {
            controller: "tripEditorController",
            controllerAs: "vm",
            templateUrl: "/views/tripEditorView.html"
        });

        $routeProvider.otherwise({ redirectTo: "/" });
    });

})();