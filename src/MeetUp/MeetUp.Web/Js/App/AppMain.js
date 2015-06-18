var myApp = angular.module('appmain', ['ngRoute', 'eventscontroller']);

myApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
      when('/list', {
          templateUrl: '/AngularEvents/list',
          controller: 'eventscontroller'
      }).
      otherwise({
          redirectTo: '/list'
      });
}]);