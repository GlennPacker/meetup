var myApp = angular.module('userprofiledirective', []);
myApp.directive('userprofiledirective', ['$compile', function ($compile) {
    return {
        restrict: 'E',
        //replace: true,
        transclude: true,
        templateUrl: '/AngularEvents/UserProfile',
        scope: {
            user: "=user",
            height: "@?",
            width: "@?"
        }
    };
}]);