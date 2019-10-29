(function (angular, undefined) {
    'use strict';

    angular.module('UTRGVApp.constants', [])

    .constant('appConfig', { userRoles: ['Faculty', 'Commitee', 'Chair', 'Dean', 'Admin'] })
    .factory('currentAcademicYear', function () {
    	return function(){
        var year = new Date().getFullYear();
        var month = new Date().getMonth() + 1;
        if (month < 6)
            year--;

        return year;
    	}
    });
})(angular);