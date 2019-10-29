'use strict';

var NavbarController = function ($scope, Auth, $state) {
    $scope.menu = [{ name: 'Home', state: 'home' }, { name: 'Login', state: 'login' }]
    $scope.loggedIn = Auth.isLoggedIn;
    $scope.hasRole = Auth.hasRole;
    $scope.inRole = Auth.inRole;
    $scope.logout = function () {
        $state.go('login');
        Auth.logout();
    }
}


angular.module('UTRGVApp')
  .controller('NavbarController', ['$scope', 'Auth', '$state', NavbarController]);
