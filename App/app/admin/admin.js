'use strict';

angular.module('UTRGVApp')
  .config(['$stateProvider', function ($stateProvider) {
      $stateProvider
        .state('admin', {
            url: '/admin',
            template: '<admin></admin>',
            authenticate: 'Admin',
            data: {
                pageTitle: 'Admin Dashboard'
            }
        });
  }]);
