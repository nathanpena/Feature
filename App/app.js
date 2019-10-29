'use strict';

angular.module('UTRGVApp', 
		['UTRGVApp.auth',
		 'UTRGVApp.constants',
		 'UTRGVApp.modalService',
		 'ngFileUpload',
		 'angularUtils.directives.dirPagination',
		 'ui.bootstrap',
		 'ngCookies',
		 'ui.router',
		 'ngSanitize',
		 'textAngular',
		 'angularSpinner'])


.directive('title', ['$rootScope', '$timeout',
  function($rootScope, $timeout) {
      return {
          link: function() {

              var listener = function(event, toState) {

                  $timeout(function() {
                      $rootScope.title = (toState.data && toState.data.pageTitle) 
                      ? toState.data.pageTitle 
                      : 'UTRGV';
                  });
              };

              $rootScope.$on('$stateChangeSuccess', listener);
          }
      };
  }
])

.config(['$urlRouterProvider', '$locationProvider', function ($urlRouterProvider, $locationProvider) {
    $urlRouterProvider
    .otherwise('/login');

    $locationProvider.html5Mode(true);
}]);
