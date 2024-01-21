(function () {
    'use strict';

    angular
        .module('app.pages.auth.login', [])
        .config(config)
        .factory('authService', ['$http', '$q', '$window', 'api', 'lf', function ($http, $q, $window, api, lf) {
            //console.log(lf)

            var serviceBase = api.baseUrl;
            var authServiceFactory = {};

            var _authentication = {
                isAuth: false,
                userName: "",
                roles: [],
                userId: 0
            };

            var _saveRegistration = function (registration) {
                _logOut();
                return $http.post(serviceBase + 'account/register', registration).then(function (response) {
                    return response;
                });
            };

            var _login = function (token) {
                //localStorageService.remove('authorizationData');
                //   var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
                //var data = "token=" + token;
                var data = {};

                var deferred = $q.defer();
                $http.post(serviceBase + 'user/findByToken', data, { headers: { 'Authorization': 'Bearer ' + token } }).success(function (response) {
                    //debugger;
                    var userData = response.output;
                    if ($window.localStorage.userName !== userData.userName) {
                        console.log("have to deleted")
                        lf.clear().then(function () {
                            // Run this code once the database has been entirely deleted.
                            console.log('Database is now empty.');
                            deferred.resolve();
                        }).catch(function (err) {
                            // This code runs if there were any errors
                            console.log(err);
                        });
                    }
                    //response.roles = "[\"admin\"]";
                    //console.log(response);
                    $window.localStorage.setItem('authorizationData', angular.toJson({ token: token, userName: userData.userName, userId: userData.id, roles: angular.toJson(userData.roles), departments: angular.toJson(userData.departments), departmentId: userData.departmentId }));
                    //console.log(localStorageService.get('authorizationData'));
                    $window.localStorage.userName = userData.userName
                    _authentication.isAuth = true;
                    _authentication.userName = userData.userName;
                    _authentication.userId = userData.id;
                    _authentication.roles = userData.roles;
                    _authentication.departments = userData.departments;
                    _authentication.departmentId = userData.departmentId;
                    _authentication.token = token;
                    // console.log(response)
                    deferred.resolve(response);

                    //  console.log(response);

                }).error(function (err) {
                    _logOut();
                    deferred.reject(err);
                });


                //console.log(deferred.promise);
                return deferred.promise;
            };

            var _logOut = function () {
                var reUserName = $window.localStorage.userName
                //console.log(reUserName)
                $window.localStorage.clear();
                $window.localStorage.subversion = $window.__env.subversion;
                $window.localStorage.userName = reUserName
                //localStorage.clear(); 
                _authentication.isAuth = false;
                _authentication.userName = "";
                _authentication.roles = [];
                _authentication.departments = [];
                _authentication.departmentId = null;
                _authentication.userId = 0;
                // $location.path('/login')
                //console.log($location.path());

            };

            var _fillAuthData = function () {

                var authData = angular.fromJson($window.localStorage.getItem('authorizationData'));
                //console.log(authData.roles);
                if (authData) {
                    _authentication.isAuth = true;
                    _authentication.userName = authData.userName;
                    _authentication.userId = authData.userId;
                    _authentication.token = authData.token;
                    _authentication.roles = angular.fromJson(authData.roles);
                    _authentication.departments = angular.fromJson(authData.departments);
                    _authentication.departmentId = authData.departmentId;
                }
            }

            authServiceFactory.saveRegistration = _saveRegistration;
            authServiceFactory.login = _login;
            authServiceFactory.logOut = _logOut;
            authServiceFactory.fillAuthData = _fillAuthData;
            authServiceFactory.authentication = _authentication;
            // console.dir(_authentication);

            return authServiceFactory;
        }]);

    /** @ngInject */
    function config($stateProvider, $translatePartialLoaderProvider, msApiProvider) {
        // State
        $stateProvider.state('app.pages_auth_login', {
            url: '/pages/auth/login/?token&login',
            views: {
                'main@': {
                    templateUrl: 'app/core/layouts/content-only.html',
                    controller: 'MainController as vm'
                },
                'content@app.pages_auth_login': {
                    templateUrl: 'app/main/pages/auth/login/login.html',
                    controller: 'LoginController as vm'
                }
            },

            bodyClass: 'login'
        });

        msApiProvider.register('userFindByToken', ['user/findByToken']);
        // Translation
        $translatePartialLoaderProvider.addPart('app/main/pages/auth/login');


    }

})();