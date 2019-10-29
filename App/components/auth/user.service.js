'use strict';

(function () {

    function UserResource($resource) {
        return $resource('api/users/:id/:controller', {
            id: '@_id'
        }, {

            get: {
                method: 'GET',
                params: {
                    id: 'me'
                }
            },
            find: {
                method: 'GET',
                url: 'api/users/find/:email',
                isArray :true
            }
        });
    }

    angular.module('UTRGVApp.auth')
      .factory('User', ['$resource', UserResource]);

})();
