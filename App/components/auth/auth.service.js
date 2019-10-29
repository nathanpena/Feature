'use strict';

(function() {

    function AuthService($location, $http, $cookies, $q, appConfig, Util, User) {
        var safeCb = Util.safeCb;
        var currentUser = {};
        var userRoles = appConfig.userRoles || [];

        if ($cookies.get('token') && $location.path() !== '/logout') {
            currentUser = User.get();
        }

        var Auth = {

            /**
             * Authenticate user and save token
             *
             * @param  {Object}   user     - login info
             * @param  {Function} callback - optional, function(error, user)
             * @return {Promise}
             */
            login : function(email, password, callback) {
                return $http.post('api/account/login', {
                    email: email,
                    password: password
                })
                  .then(function(res) {
                      $cookies.put('token', res.data, {path:'/'});
                      currentUser = User.get();
                      return currentUser.$promise;
                  })
                  .then(function (user) {
                      safeCb(callback)(null, user);
                      return user;
                  })
                  .catch(function(err) {
                      Auth.logout();
                      safeCb(callback)(err.data);
                      return $q.reject(err.data);
                  });
    },

    /**
     * Delete access token and user info
     */
    logout : function() {
        $cookies.remove('token');
        currentUser = {};
    },
    /**
     * Gets all available info on a user
     *   (synchronous|asynchronous)
     *
     * @param  {Function|*} callback - optional, funciton(user)
     * @return {Object|Promise}
     */
    getCurrentUser : function(callback) {
        if (arguments.length === 0) {
            return currentUser;
        }

        var value = (currentUser.hasOwnProperty('$promise')) ?
          currentUser.$promise : currentUser;
        return $q.when(value)
          .then(function(user)  {
              safeCb(callback)(user);
              return user;
          }, function()  {
              safeCb(callback)({});
              return {};
          });
    },
    /**
     * Check if a user is logged in
     *   (synchronous|asynchronous)
     *
     * @param  {Function|*} callback - optional, function(is)
     * @return {Bool|Promise}
     */
    isLoggedIn : function(callback) {
        if (arguments.length === 0) {
            return currentUser.hasOwnProperty('role');
        }

        return Auth.getCurrentUser(null)
          .then(function(user) {
              var is = user.hasOwnProperty('role');
              safeCb(callback)(is);
              return is;
          });
    },

    /**
     * Check if a user has a specified role or higher
     *   (synchronous|asynchronous)
     *
     * @param  {String}     role     - the role to check against
     * @param  {Function|*} callback - optional, function(has)
     * @return {Bool|Promise}
     */
    hasRole : function(role, callback) {
        var hasRole = function(r, h) {
            return userRoles.indexOf(r) >= userRoles.indexOf(h);
        };

        if (arguments.length < 2) {
            return hasRole(currentUser.role, role);
        }

        return Auth.getCurrentUser(null)
          .then(function(user) {
              var has = (user.hasOwnProperty('role')) ?
                hasRole(user.role, role) : false;
              safeCb(callback)(has);
              return has;
          });
    },


    inRole : function(role, callback){
        var hasRole = function(r, h) {
            return r == h || r == 'admin';
        };

        if (arguments.length < 2) {
            return hasRole(currentUser.role, role);
        }
      
        return Auth.getCurrentUser(null)
         .then(function(user) {
             var has = (user.hasOwnProperty('role')) ?
               hasRole(user.role, role) : false;
             safeCb(callback)(has);
             return has;
         });

    },


    /**
     * Check if a user is an admin
     *   (synchronous|asynchronous)
     *
     * @param  {Function|*} callback - optional, function(is)
     * @return {Bool|Promise}
     */
    isAdmin : function() {
        return Auth.hasRole
          .apply(Auth, [].concat.apply(['admin'], arguments));
    },

    /**
     * Get auth token
     *
     * @return {String} - a token string used for authenticating
     */
    getToken : function() {
        return $cookies.get('token');
    }
};

return Auth;
}

angular.module('UTRGVApp.auth')
  .factory('Auth', ['$location', '$http', '$cookies', '$q', 'appConfig', 'Util', 'User', AuthService]);

})();
