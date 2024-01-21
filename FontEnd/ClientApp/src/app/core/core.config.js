(function () {
    'use strict';

    angular
        .module('app.core')
        .config(config)
        // .factory('authInterceptorService', ['$q', 'localStorageService', '$window', function ($q, localStorageService, $window) {

        //     var authInterceptorServiceFactory = {};

        //     var _request = function (config) {

        //         config.headers = config.headers || {};

        //         var authData = localStorageService.get('authorizationData');
        //         if (authData) {
        //             config.headers['auth-token'] = authData.token;
        //         }
        //         config.headers['Content-Type'] = 'application/json';
        //         //console.log(config)
        //         return config;
        //     }

        //     var _responseError = function (rejection) {
        //         //console.log(rejection);
        //         if (rejection.status == 401) {
        //             $window.location.pathname = '/pages/auth/login';
        //             //  $location.path('/pages/auth/login');
        //         }
        //         return $q.reject(rejection);
        //     }

        //     authInterceptorServiceFactory.request = _request;
        //     authInterceptorServiceFactory.responseError = _responseError;

        //     return authInterceptorServiceFactory;
        // }]);
    /** @ngInject */
    function config($ariaProvider, $logProvider, msScrollConfigProvider, sinsConfigProvider) {
        // Enable debug logging
        $logProvider.debugEnabled(true);

        /*eslint-disable */
        // ng-aria configuration
        $ariaProvider.config({
            tabindex: false
        });

        // Nis theme configurations
        sinsConfigProvider.config({
            'disableCustomScrollbars': false,
            'disableCustomScrollbarsOnMobile': false,
            'disableMdInkRippleOnMobile': true
        });

        // msScroll configuration
        msScrollConfigProvider.config({
            wheelPropagation: true
        });

        /*eslint-enable */

    }
})();