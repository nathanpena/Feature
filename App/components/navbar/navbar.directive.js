'use strict';

angular.module('UTRGVApp')
  .directive('navbar', function () {
      return {
          templateUrl: 'App/components/navbar/navbar.html',
          restrict: 'E',
          controller: 'NavbarController',
          controllerAs: 'nav'
      }
  });
