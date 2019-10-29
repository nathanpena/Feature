'use strict';

var AdminController = function ($scope, Auth) {

}


angular.module('UTRGVApp')
.component('admin', {
    templateUrl: 'App/app/admin/admin.html',
    controller: ['$scope', 'Auth', AdminController]
});
