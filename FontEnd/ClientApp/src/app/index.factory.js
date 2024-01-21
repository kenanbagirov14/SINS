(function () {
  'use strict';

  angular
    .module('sins')
    .factory('authInterceptorService', factoryBlock);

  /** @ngInject */
  function factoryBlock($q, $window, NotificationService) {

    var authInterceptorServiceFactory = {};

    var _request = function (config) {

      //debugger;
      config.headers = config.headers || {};
      var authData = angular.fromJson($window.localStorage.getItem('authorizationData'));
      if (authData) {
        config.headers.Authorization = 'Bearer ' + authData.token;
      }

      return config;
    }

    var _responseError = function (rejection) {
      // console.log(rejection.status);
      if (rejection.status === -1) {
        NotificationService.show("network problem", 'error', 5000);
      } else if (rejection.status === 401) {
        $window.location = "/pages/auth/login";
      } else if (rejection.status === 500) {
        //    $window.location.reload();
      }
      return $q.reject(rejection);
    }

    // var _response = function (response) {

    //   if (response.data.isSuccess != undefined) {
    //     try {
    //       if (response.data.isSuccess) {
    //         NotificationService.show('success', 'success')
    //       } else {
    //         response.data.errorList.forEach(function (element) {
    //           NotificationService.show(element.text, 'error')
    //         }, this);
    //       }
    //     } catch (err) {
    //       NotificationService.show('An error occured', err)
    //     }
    //   }
    //   return response
    // }

    authInterceptorServiceFactory.request = _request;
    //authInterceptorServiceFactory.response = _response;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
  }
})();
