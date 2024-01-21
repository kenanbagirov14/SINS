(function () {
    'use strict';

    angular
        .module('app.request')
        .controller('AssignDialogController', AssignDialogController);

    /** @ngInject */
    function AssignDialogController($mdDialog, Task, UserList, msApi, event) {
        var vm = this;

        // Data
        vm.title = 'İstifadəçi əlavə et';
        vm.task = Task;
        // Methods
        vm.assignUser = assignUser;
        vm.closeDialog = closeDialog;
        //////////

        /**
         * assign user
         */
        function assignUser(ev, item) {
            vm.task.executorUserId = item.value;
            msApi.request('updateTask@save', vm.task,
                // SUCCESS
                function (response) {
                    //requestForm.createdDate = new Date();
                    vm.task = response.output;
                    //console.log(response.data)
                },
                // ERROR
                function (response) {
                    console.error(response.data)
                }
            );
            closeDialog();
        }


        /**
         * Close dialog
         */
        function closeDialog() {
            $mdDialog.hide();
        }



        // list of `state` value/display objects
        vm.states = loadAll();
        vm.querySearch = querySearch;

        // ******************************
        // Template methods
        // ******************************

        vm.cancel = function ($event) {
            $mdDialog.cancel();
        };
        vm.finish = function ($event) {
            $mdDialog.hide();
        };

        // ******************************
        // Internal methods
        // ******************************

        /**
         * Search for states... use $timeout to simulate
         * remote dataservice call.
         */
        function querySearch(query) {
            return query ? vm.states.filter(createFilterFor(query)) : vm.states;
        }

        /**
         * Build `states` list of key/value pairs
         */
        function loadAll() {

            return UserList.map(function (state) {
                return {
                    value: state.id,
                    display: state.userName
                };
            });
        }

        /**
         * Create filter function for a query string
         */
        function createFilterFor(query) {
           // console.log(query)
            var lowercaseQuery = angular.lowercase(query);

            return function filterFn(state) {
               // console.log(state);
                return (angular.lowercase(state.display).indexOf(lowercaseQuery) === 0);
            };

        }
        /**
             * Close dialog
             */
        function closeDialog() {
            $mdDialog.hide();
        }

    }
})();