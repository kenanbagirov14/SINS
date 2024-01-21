// (function ()
// {
//     'use strict';

//     angular
//         .module('app.navigation')
//         .controller('NavigationController', NavigationController);

//     /** @ngInject */
//     function NavigationController($scope,__env)
//     {
//         var vm = this;
//         vm.env = __env;

//         // Data
//         vm.bodyEl = angular.element('body');
//         vm.folded = true;
//         vm.msScrollOptions = {
//             suppressScrollX: false
//         };

//         // Methods
//         $scope.toggleMsNavigationFolded = toggleMsNavigationFolded;

//         //////////

//         /**
//          * Toggle folded status
//          */
//         function toggleMsNavigationFolded()
//         {
//             vm.folded = !vm.folded;
//         }

//         // Close the mobile menu on $stateChangeSuccess
//         $scope.$on('$stateChangeSuccess', function ()
//         {
//             vm.bodyEl.removeClass('ms-navigation-horizontal-mobile-menu-active');
//         });
//     }

// })();