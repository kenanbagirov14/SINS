(function () {
    'use strict';

    angular
        .module('app.task')
        .controller('NewDepartmentDialogController', NewDepartmentDialogController);

    /** @ngInject */
    function NewDepartmentDialogController(
        $q,
        $timeout,
        $mdDialog,
        $mdConstant,
        $filter,
        Departments,
        Department,
        Users,
        msApi,
        event) {
        var vm = this;
        // Data
        //  console.log(SourceList)
        vm.title = 'Yeni Şöbə';
        vm.departments = Departments;
        vm.newDepartmentForm = {};
        vm.users = Users;
        vm.saveDepartment = saveDepartment;
        vm.closeDialog = closeDialog;
        if (Department != undefined) {
            vm.newDepartmentForm = Department
        }

        /**
         * Save task
         */
        function saveDepartment(newDepartmentForm) {
            var postUrl = newDepartmentForm.id != undefined ? 'updateDepartment@save' : 'addDepartment@save';
            //console.log(vm.userList.selectedItem)
            // vm.userList.selectedItem = null;
            // console.log(vm.userList.selectedItem)
            //  return;
            msApi.request(postUrl, newDepartmentForm,
                // SUCCESS
                function (response) {
                    console.log(response)
                },
                // ERROR
                function (response) {
                    console.error(response)
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























        //--------------------------------------------------------------- temp ---//


        // var pendingSearch, cancelSearch = angular.noop;
        // var lastSearch;

        // vm.allContacts = loadContacts();
        // vm.contacts = [vm.allContacts[0]];
        // vm.asyncContacts = [];
        // vm.filterSelected = true;

        // vm.querySearch = querySearch;
        // vm.delayedQuerySearch = delayedQuerySearch;

        // /**
        //  * Search for contacts; use a random delay to simulate a remote call
        //  */
        // function querySearch(criteria) {
        //     return criteria ? vm.allContacts.filter(createFilterFor(criteria)) : [];
        // }

        // /**
        //  * Async search for contacts
        //  * Also debounce the queries; since the md-contact-chips does not support this
        //  */
        // function delayedQuerySearch(criteria) {
        //     if (!pendingSearch || !debounceSearch()) {
        //         cancelSearch();

        //         return pendingSearch = $q(function (resolve, reject) {
        //             // Simulate async search... (after debouncing)
        //             cancelSearch = reject;
        //             $timeout(function () {

        //                 resolve(vm.querySearch(criteria));

        //                 refreshDebounce();
        //             }, Math.random() * 500, true)
        //         });
        //     }

        //     return pendingSearch;
        // }

        // function refreshDebounce() {
        //     lastSearch = 0;
        //     pendingSearch = null;
        //     cancelSearch = angular.noop;
        // }

        // /**
        //  * Debounce if querying faster than 300ms
        //  */
        // function debounceSearch() {
        //     var now = new Date().getMilliseconds();
        //     lastSearch = lastSearch || now;

        //     return ((now - lastSearch) < 300);
        // }

        // /**
        //  * Create filter function for a query string
        //  */
        // function createFilterFor(query) {
        //     var lowercaseQuery = angular.lowercase(query);

        //     return function filterFn(contact) {
        //         return (contact._lowername.indexOf(lowercaseQuery) != -1);
        //     };

        // }

        // function loadContacts() {
        //     var contacts = [
        //         'Orxan Rzazade',
        //         'Ismayil Ismayilov',
        //         'Yusif Shixelizade',
        //         'Ceyhun Ismayilzade',
        //         'Etibar Quliyev',
        //         'Megan Smith'
        //     ];

        //     return contacts.map(function (c, index) {
        //         var cParts = c.split(' ');
        //         var email = cParts[0][0].toLowerCase() + '.' + cParts[1].toLowerCase() + '@example.com';
        //         var hash = email;

        //         var contact = {
        //             name: c,
        //             email: email,
        //             image: '//www.gravatar.com/avatar/' + hash + '?s=50&d=retro'
        //         };
        //         contact._lowername = contact.name.toLowerCase();
        //         return contact;
        //     });
        // }

    }
})();