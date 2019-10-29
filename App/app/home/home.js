 'use strict';

angular.module('UTRGVApp')
  .config(['$stateProvider', function ($stateProvider) {
      $stateProvider
        .state('home', {
            url: '/',
            template: '<home></home>',
            authenticate: true,
            data: {
                pageTitle: 'Home'
            }
        });
  }]);
