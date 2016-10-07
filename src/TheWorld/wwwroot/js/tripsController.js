(function() {

    "use strict";

    //getting the existing module
    angular.module("app-trips")
        .controller("tripsController", tripsController);

    function tripsController($http) {

        var vm = this;
        
        // retrieve from api - using http
        vm.trips = [];

        vm.newTrip = {};

        vm.errorMessage = "";

        $http.get("/api/trips")
            .then(function(response) {
                //Success
                angular.copy(response.data, vm.trips);
            }),function() {
                // failure
            vm.errorMessage = "Failed to load data: " + error;
        }

        vm.addTrip = function() {
            vm.trips.push({ name: vm.newTrip.name, created: new Date() });
            vm.newTrip = {};
        };
    }
})();