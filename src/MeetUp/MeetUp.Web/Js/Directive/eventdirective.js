var myApp = angular.module('eventdirective', ['ngSanitize']);
myApp.directive('eventdirective', [
    '$compile', '$http', function($compile, $http) {
        return {
            restrict: 'E', // 'E' element 'A' attribute 'C' class 'M' html comment
            replace: true,
            templateUrl: '/AngularEvents/Event',
            scope: {
                occasion: "=occasion" // '=' passes in model '&' passes in function '@' string
            },
            link: function (scope, element, attrs, controllers) {
                scope.showrsvp = false;
                scope.updateevent = true;
                scope.showmap = false;
                scope.showtripadvisor = false;
                scope.showphotos = false;
                scope.updatedoccasion = false;      // use a timestamp hmm??

                scope.rsvps = function() {
                    // show profiles
                    scope.showrsvp = !scope.showrsvp;
                    scope.hidepanelsexcept('rsvp');
                    if (scope.showrsvp && !scope.updatedoccasion) {
                        scope.getevent();
                    }
                }


                scope.getevent = function () {
                    var url = '/api/v1/Event/Update?id=' + scope.occasion.Id + '&force=' + scope.occasion.forceUpdate; // update event forced if soon
                    $http.get(url).success(function (data, status, headers, config) {
                        scope.occasion = data;
                        scope.updatedoccasion = true;
                    }).error(function (error) {
                        // can't update but have old data so no error.
                    });
                }

                scope.tripadvisor = function() {
                    // show tripadvisor
                    scope.showtripadvisor = !scope.showtripadvisor;
                    scope.hidepanelsexcept('tripadvisor');

                    // update trip advisor
                }

                scope.photos = function() {
                    // show photos
                    scope.showphotos = !scope.showphotos;
                    scope.hidepanelsexcept('photos');

                    // get photos from prvious times at venues
                }

                scope.mappanel = function() {
                    // get map data
                    scope.showmap = !scope.showmap;
                    scope.hidepanelsexcept('map');
                }

                scope.forceupdate = function() {
                    scope.mom2 = moment(scope.occasion.JavascriptDateTime);
                    return moment().isBetween(moment(scope.mom2.add(-1, 'days')), moment(scope.mom2.add(2, 'days')));
                }

                //$scope.eventmoment = moment(occasion.);

                scope.hidepanelsexcept = function(panel) {
                    if (panel != 'rsvp') scope.showrsvp = false;
                    if (panel != 'map') scope.showmap = false;
                    if (panel != 'tripadvisor') scope.showtripadvisor = false;
                    if (panel != 'photos') scope.showphotos = false;
                }
            }
        };
    }
]);