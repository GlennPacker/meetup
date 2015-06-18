var myControllers = angular.module('eventscontroller', []);

myControllers.controller('eventscontroller', ['$scope', '$http', '$q', function ($scope, $http, $q) {
    $scope.events = [];
    $scope.msg = 'Loading Data';
    $scope.wait = true;

    $scope.getevents = function () {
        var url = 'api/v1/apievents';
        $http.get(url).success(function (data, status, headers, config) {
            $scope.msg = '';
            $scope.events = data.value;
            $scope.wait = false;
        }).error(function (error) {
            $scope.msg = 'Oops somthing went wrong';
            $scope.wait = false;
        });
    }
    $scope.getevents();
}]);