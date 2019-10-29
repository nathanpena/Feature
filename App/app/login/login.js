'use strict';

angular.module('UTRGVApp')
  .config(['$stateProvider', function ($stateProvider) {
      $stateProvider
        .state('login', {
            url: '/login',
            template: '<login></login>',
            data: {
                pageTitle: 'Login'
            }
        });
  }]);
