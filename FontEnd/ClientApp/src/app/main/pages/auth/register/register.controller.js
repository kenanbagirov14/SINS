(function () {
    'use strict';

    angular
        .module('app.pages.auth.register')
        .controller('RegisterController', RegisterController);

    /** @ngInject */
    function RegisterController($state,msApi) {
        // Data
        var vm = this;
        vm.userData = {};
        vm.registerUser = registerUser;
        // Methods
        function registerUser() {
            msApi.request('addUser@save', vm.userData,
                // SUCCESS
                function (response) {
                    console.log(response.output);
                      $state.go('app.pages_auth_login');

                },
                // ERROR
                function (response) {
                   vm.userData={}
                }
            );

        }
        //////////
    }
})();