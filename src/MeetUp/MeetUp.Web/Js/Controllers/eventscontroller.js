var myControllers = angular.module('eventscontroller', []);

myControllers.controller('eventscontroller', ['$scope', '$http', '$q', function ($scope, $http, $q) {
    $scope.events = [];
    $scope.msg = 'Loading Data';
    $scope.wait = true;

    $scope.getevents = function () {
        var url = 'api/v1/ApiEvents';
        $http.get(url).success(function (data, status, headers, config) {
            $scope.localdata(data);
        }).error(function (error) {
            $scope.msg = 'Oops somthing went wrong';
            $scope.wait = false;
        });
    }

    $scope.localdata = function(data) {
        $scope.msg = '';
        $scope.events = data;
        $scope.wait = false;
        // check for new data 
        $scope.geteventupdates();
    }

    $scope.geteventupdates = function () {
        var url = 'api/v1/Events/Update';
        $http.get(url).success(function (data, status, headers, config) {
            $scope.events = data;
        }).error(function (error) {
            // dont' do anything just go with the local db data as it looks like meetup api is down.
        });
    }

    $scope.getevents();
}]);