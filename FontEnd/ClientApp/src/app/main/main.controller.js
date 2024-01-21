(function () {
  'use strict';

  angular
    .module('app.main')
    .controller('MainController', MainController)
    .service('MainService', MainService)
    .service('NotificationService', NotificationService)
  //  .service('offlineDataService', offlineDataService)
    .service('ACLService', ACLService);

  // function offlineDataService($injector, localStorageService) {
  //   if (!localStorageService.get(offlineData)) {
  //     localStorageService.set(offlineData, null);
  //   }
  //   var service = {
  //     setData: setOfflineData,
  //     getData: getOfflineData,
  //     clearData: clearOfflineData
  //   }
  //   var toastr;


  //   function setOfflineData(property, data) {
  //     var currentData = JSON.parse(localStorageService.get(offlineData));
  //     currentData[property] = data;
  //     localStorageService.set(offlineData, JSON.stringify(currentData));
  //   }
  //   function getOfflineData(property) {

  //     try {
  //       return JSON.parse(localStorageService.get(offlineData))[property];
  //     }
  //     catch (error) {
  //       return false;
  //     }
  //   }
  //   function clearOfflineData(property) {
  //     localStorageService.set(offlineData, null);
  //   }
  //   return service;

  // }

  function NotificationService($injector) {
    var service = {
      show: notify
    }
    var toastr;

    function getToaster() {
      if (!toastr) {
        toastr = $injector.get("$mdToast");
      }
      return toastr;
    }

    function notify(msg, css, timeout) {
      return getToaster().show(getToaster().simple({
        hideDelay: timeout || 4000,
        position: 'bottom left',
        content: msg,
        toastClass: css
      }));
    }
    return service;

  }

  function ACLService($injector, $filter) {
    var service = {
      check: isUserHasAccess
    }

    function isUserHasAccess(roles,authService) {
      //console.log(roles,authService.authentication.roles);
      //debugger;
      return $filter('intersect')(authService.authentication.roles, roles).length > 0
    }
    return service;
  }

  function MainService($q, $timeout) {

    var service = {
      init: autoCompleteMaker
    }

    function autoCompleteMaker(obj, prop, list) {
      obj[prop] = list;
      obj['selectedItem'] = null
      obj['searchText'] = null
      obj['querySearch'] = querySearch
      obj['selectedItemChange'] = selectedItemChange;
      return obj;
    }

    /**
     * Search for states... use $timeout to simulate
     * remote dataservice call.
     */
    function querySearch(query, prop, val) {
      //  console.log(prop);
      // console.log(query);
      // console.log(vm.userList.users.filter(createFilterFor(query)))
      var results = query ? prop.filter(createFilterFor(query, val)) : prop;
      var deferred = $q.defer();
      $timeout(function () {
        deferred.resolve(results);
      }, Math.random() * 1000, false);
      return deferred.promise;
    }

    /**
     * Create filter function for a query string
     */
    function createFilterFor(query, val) {
      var lowercaseQuery = query.toLocaleLowerCase();

      return function filterFn(prop) {
        //  console.log(prop[val]);
        //console.log(user.firstName + ' - ' + lowercaseQuery)
        return ((prop[val] || "").replace("İ", "i").toLocaleLowerCase().indexOf(lowercaseQuery) !== -1);
      };
    }

    function selectedItemChange(model, property, value) {
      model[property] = value;
      //value = null
      // console.log(model);
    }
    return service
  };
  /** @ngInject */
  function MainController($scope, $rootScope) {

    // if (!msHub.hub().proxy._.callbackMap.hasOwnProperty('remotecommand')) {
    //   msHub.hub().proxy.on('remotecommand', function (response) {
    //     if (response.command == "clear") {
    //       localStorage.clear();
    //       location.assign("/pages/auth/login");
    //     }
    //   });
    // }
    //console.log("in maing")
    // Data

    //////////

    // Remove the splash screen
    $scope.$on('$viewContentAnimationEnded', function (event) {
      if (event.targetScope.$id === $scope.$id) {
        $rootScope.$broadcast('msSplashScreen::remove');
      }
    });


  }
})();
