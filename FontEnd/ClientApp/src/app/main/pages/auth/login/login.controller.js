(function () {
  'use strict';

  angular
    .module('app.pages.auth.login')
    .controller('LoginController', LoginController);

  /** @ngInject */
  function LoginController(api, authService, $state, $filter, $stateParams, __env, lf) {
    //console.log(lf)

    var vm = this
    vm.baseRoutes = {
      "routetask": "task",
      "routerequest": "request",
      "routecallcenter": "callcenter",
      "routereport": "report"
    }
    vm.token = $stateParams.token;
    // Data
    vm.loginError = {
      data: "",
      show: false
    }
    //.logOut();
    vm.sendDataStatus = false;
    vm.startLogin = startLogin
    //  vm.login = login;
    //////////

    // functions
    vm.loginError = {
      data: null,
      show: false
    }
    vm.progress = false;
    //return;
    //console.log(vm.token)
    //debugger;
    function startLogin(type) {
      vm.progress = true;
      lf.clear().then(function () {  
        location.assign(__env.apiUrl + "common/auth?authTypeId=" + type);      
      }).catch(function (err) {        
        console.log(err);
      });     
      return;
    }

    if ($stateParams.login == "false" && vm.token) {
      vm.sendDataStatus = false;
      vm.loginError = {
        data: err ? err.message : "error occured",
        show: true
      }
    } else if (vm.token) {
      loginUser(true);
    }

    function loginUser(firstAttempt) {

      vm.progress = true;
      authService.login(vm.token).then(function (response, error) {
          //console.log(authService.authentication);
          // if (!msHub.hub().proxy._.callbackMap.hasOwnProperty('remotecommand')) {
          //   msHub.hub().proxy.on('remotecommand', function (response) {
          //     if (response.command == "clear") {
          //       localStorage.clear();
          //       location.assign("/pages/auth/login");
          //     }
          //   });
          // }
          //console.log(authService.authentication.roles.indexOf("Operator"))
          // var defaultRouteIndex = $filter('includes')('route', authService.authentication.roles);
          // if (defaultRouteIndex > -1) {
          //   var defaultRoute = authService.authentication.roles[defaultRouteIndex].replace('route', '').toLowerCase()
          //   $state.go('app.' + defaultRoute);
          // }
          //if (defaultRouteIndex > -1) {
          // for (var s = 0; s < authService.authentication.roles.length; s++) {
          //   var thisRoute = authService.authentication.roles[s].toLowerCase();
          //   console.log(thisRoute);
          //   if (thisRoute in vm.baseRoutes) {
          //     $state.go('app.' + vm.baseRoutes[thisRoute]);
          //     break;
          //   }
          // }

          for (var k in vm.baseRoutes) {
            //console.log(authService.authentication.roles.map(function (x) { return x.toLowerCase() }));
            //console.log((authService.authentication.roles.map(function (x) { return x.toLowerCase() })).indexOf(k) > -1);
            if ((authService.authentication.roles.map(function (x) {
                return x.toLowerCase()
              })).indexOf(k) > -1) {
              $state.go('app.' + vm.baseRoutes[k]);
              return;
            }
          }
          //}
          //else {
          $state.go('app.pages_error_unauthorize');
          //}

        },
        function (err) {
          if (firstAttempt) {
            loginUser(false)
            return;
          }
          vm.sendDataStatus = false;
          vm.loginError = {
            data: err ? err.message : "error occured",
            show: true
          }
        });
      //$state.go('app.request');
    }
  }
})();
