'use strict';

angular.module('UTRGVApp.auth', [
  'UTRGVApp.constants',
  'UTRGVApp.util',
  'ngResource',
  'ngCookies',
  'ui.router'
])
.config(['$httpProvider', function ($httpProvider) {
      $httpProvider.interceptors.push('authInterceptor');
}]);
