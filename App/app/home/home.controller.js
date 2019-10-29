'use strict';

var HomeController = function ($scope, Auth) {

}


angular.module('UTRGVApp')
.component('home', {
    templateUrl: 'App/app/home/home.html',
    controller: ['$scope', 'Auth', HomeController]
});
