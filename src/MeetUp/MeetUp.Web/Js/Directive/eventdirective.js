var myApp = angular.module('eventdirective', ['ngSanitize']);
myApp.directive('eventdirective', ['$compile', function ($compile) {
    return {
        restrict: 'E',    // 'E' element 'A' attribute 'C' class 'M' html comment
        replace: true,
        templateUrl: '/AngularEvents/Event',
        scope: {
            occasion: "=occasion"  // '=' passes in model '&' passes in function '@' string
        },
        link: function(scope, element, attrs, controllers) {

        }
    };
}]);