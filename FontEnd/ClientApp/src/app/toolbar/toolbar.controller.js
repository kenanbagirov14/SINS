(function () {
    'use strict';

    angular
        .module('app.toolbar')
        .controller('ToolbarController', ToolbarController);

    /** @ngInject */
    function ToolbarController($rootScope, $location, $q, $state, $timeout, $mdSidenav, $translate, $mdDialog, msNavigationService, authService, ACLService, msHub) {
        var vm = this;
        vm.hasAccess = hasAccess;
        $rootScope.pageTitle = "";
        // $rootScope.notificationCount;      
        vm.about = openAboutDialog;
        // Data
        $rootScope.global = {
            search: ''
        };

        vm.bodyEl = angular.element('body');


        // Methods
        vm.toggleSidenav = toggleSidenav;
        vm.logout = logout;
        vm.changeLanguage = changeLanguage;
        vm.setUserStatus = setUserStatus;
        vm.toggleHorizontalMobileMenu = toggleHorizontalMobileMenu;
        vm.toggleMsNavigationFolded = toggleMsNavigationFolded;
        vm.search = search;
        vm.searchResultClick = searchResultClick;
        vm.userName = window.localStorage.userName;

        vm.isAllNotificationLoaded = false;
        //////////


        function hasAccess(role) {
            return ACLService.check([role], authService)
        }
        /**
         * Toggle sidenav
         *
         * @param sidenavId
         */
        function toggleSidenav(sidenavId) {
            if (sidenavId == 'quick-panel') {
                $rootScope.notificationCount = 0;
                $rootScope.findNotification({ status: false }, false, vm.isAllNotificationLoaded);
                vm.isAllNotificationLoaded = true;
            }
            $mdSidenav(sidenavId).toggle();
        }

        /**
         * Sets User Status
         * @param status
         */
        function setUserStatus(status) {
            vm.userStatus = status;
        }

        /**
         * Logout Function
         */
        function logout() {
            authService.logOut();
            msHub.hub().stop();
            $location.path("/pages/auth/login");
        }

        /**
         * Change Language
         */
        function changeLanguage(lang) {
            vm.selectedLanguage = lang;

            /**
             * Show temporary message if user selects a language other than English
             *
             * angular-translate module will try to load language specific json files
             * as soon as you change the language. And because we don't have them, there
             * will be a lot of errors in the page potentially breaking couple functions
             * of the template.
             *
             * To prevent that from happening, we added a simple "return;" statement at the
             * end of this if block. If you have all the translation files, remove this if
             * block and the translations should work without any problems.
             */
            if (lang.code !== 'en') {
                var message = 'Sins supports translations through angular-translate module, but currently we do not have any translations other than English language. If you want to help us, send us a message through ThemeForest profile page.';

                // $mdToast.show({
                //     template: '<md-toast id="language-message" layout="column" layout-align="center start"><div class="md-toast-content">' + message + '</div></md-toast>',
                //     hideDelay: 7000,
                //     position: 'top right',
                //     parent: '#content'
                // });

                return;
            }

            // Change the language
            $translate.use(lang.code);
        }

        /**
         * Toggle horizontal mobile menu
         */
        function toggleHorizontalMobileMenu() {
            vm.bodyEl.toggleClass('ms-navigation-horizontal-mobile-menu-active');
        }

        /**
         * Toggle msNavigation folded
         */
        function toggleMsNavigationFolded() {
            msNavigationService.toggleFolded();
        }

        /**
         * Search action
         *
         * @param query
         * @returns {Promise}
         */
        function search(query) {
            var navigation = [],
                flatNavigation = msNavigationService.getFlatNavigation(),
                deferred = $q.defer();

            // Iterate through the navigation array and
            // make sure it doesn't have any groups or
            // none ui-sref items
            for (var x = 0; x < flatNavigation.length; x++) {
                if (flatNavigation[x].uisref) {
                    navigation.push(flatNavigation[x]);
                }
            }

            // If there is a query, filter the navigation;
            // otherwise we will return the entire navigation
            // list. Not exactly a good thing to do but it's
            // for demo purposes.
            if (query) {
                navigation = navigation.filter(function (item) {
                    if (angular.lowercase(item.title).search(angular.lowercase(query)) > -1) {
                        return true;
                    }
                });
            }

            // Fake service delay
            $timeout(function () {
                deferred.resolve(navigation);
            }, 1000);

            return deferred.promise;
        }

        /**
         * Search result click action
         *
         * @param item
         */
        function searchResultClick(item) {
            // If item has a link
            if (item.uisref) {
                // If there are state params,
                // use them...
                if (item.stateParams) {
                    $state.go(item.state, item.stateParams);
                }
                else {
                    $state.go(item.state);
                }
            }
        }

        function openAboutDialog(ev) {

            $mdDialog.show({
                controller: 'AboutController',
                controllerAs: 'vm',
                templateUrl: 'app/toolbar/dialogs/about/about-dialog.html',
                //  parent: angular.element($document.find('#request-container')),
                targetEvent: ev,
                clickOutsideToClose: true,
                locals: {
                    event: ev
                }
            });
        }

    }

})();