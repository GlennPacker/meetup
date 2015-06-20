﻿var myApp = angular.module('eventdirective', []);
myApp.directive('eventdirective', function ($compile) {
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
});