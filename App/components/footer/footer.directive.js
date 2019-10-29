'use strict';

angular.module('UTRGVApp')
  .directive('footer', function () {
      return {
          templateUrl: 'App/components/footer/footer.html',
          restrict: 'E'
      }
  });
