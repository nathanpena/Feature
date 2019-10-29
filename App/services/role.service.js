'use strict';

(function () {

    function RoleResource($resource) {
        return $resource('/api/roles/:id', {
            id: '@id'
        });
    }

    angular.module('UTRGVApp')
      .factory('Role', ['$resource', RoleResource]);

})();
