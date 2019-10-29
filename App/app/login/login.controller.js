'use strict';

var LoginController = function ($scope, Auth, $state) {
    $scope.user = { email: '@utrgv.edu' };
    $scope.hideFooter = true;
    $scope.login = function (form) {
        $scope.loading = true;
        Auth.login($scope.user.email, $scope.user.password)
        .then(function (data) {
            console.log(data);
            if (data.role == "Admin")
                $state.go('admin');
            else
                $state.go('home');

        })
        .catch(function (err) {
            $scope.err = err;
        })
        .finally(function () {
            $scope.loading = false;
        })
    }
}


angular.module('UTRGVApp')
.component('login', {
    templateUrl: 'App/app/login/login.html',
    controller: ['$scope', 'Auth', '$state', LoginController]
});
