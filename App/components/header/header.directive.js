'use strict';

angular.module('UTRGVApp')
  .directive('header', function () {
      return {
          templateUrl: 'App/components/header/header.html',
          restrict: 'E'
      }
  });
