(function () {
  'use strict';

  angular
    .module('app.pages.error.unauthorize')
    .controller('UnauthorizeController', UnauthorizeController);

  /** @ngInject */
  function UnauthorizeController($state, $stateParams, __env) {
    var vm = this
    vm.token = $stateParams.token;
    // Data
    vm.loginError = {
      data: "",
      show: false
    }
    vm.goToHome = goToHome;
    //.logOut();
    vm.sendDataStatus = false;
    //  vm.login = login;
    //////////

    // functions
    vm.loginError = {
      data: null,
      show: false
    }    //return;


    function goToHome() {
      window.localStorage.clear();
      window.localStorage.subversion = window.__env.subversion;
      $state.go('app.pages_auth_login');
    }

  }
})();
