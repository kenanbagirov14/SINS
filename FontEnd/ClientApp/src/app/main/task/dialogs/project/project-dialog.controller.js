(function () {
    'use strict';

    angular
        .module('app.task')
        .controller('NewProjectDialogController', NewProjectDialogController);

    /** @ngInject */
    function NewProjectDialogController(
        $q,
        $timeout,
        $mdDialog,
        $mdMenu,
        $mdConstant,
        $filter,
        Departments,
        Projects,
        Project,
        msApi,
        event) {
        var vm = this;
        // Data
        //  console.log(SourceList)
        vm.title = 'Yeni Layihə';
        vm.departments = Departments;
        vm.projects = Projects;
        vm.newProjectForm = {};
        vm.saveProject = saveProject;
        vm.closeDialog = closeDialog;
        vm.mouseupEvent = mouseupEvent;
        vm.showDepartmentChild = showDepartmentChild;
        if (Project != undefined) {
            vm.newProjectForm = Project
        }

        /**
         * Save task
         */
        function saveProject(newProjectForm) {
            newProjectForm.departmentId = [];

            angular.forEach(newProjectForm.departmentIds, function (value, key) {
                if (value) this.push(key);
            }, newProjectForm.departmentId);

            newProjectForm.description = newProjectForm.departmentId.join(",");
            delete newProjectForm.departmentId;
            delete newProjectForm.departmentIds;
            var postUrl = newProjectForm.id != undefined ? 'updateProject@save' : 'addProject@save';
            //console.log(vm.userList.selectedItem)
            // vm.userList.selectedItem = null;
            // console.log(vm.userList.selectedItem)
            //  return;
            msApi.request(postUrl, newProjectForm,
                // SUCCESS
                function (response) {
                    Projects.output.push(response.output);
                    window.localStorage.offlineProjects = JSON.stringify(Projects.output);

                },
                // ERROR
                function (response) {
                    console.error(response.data)
                }
            );
            closeDialog();
        }
        vm.isDepartmentChildShown = [];
        function showDepartmentChild(event, id) {
            //console.log(id);
            vm.isDepartmentChildShown[id] = !(vm.isDepartmentChildShown[id] | false);
            //console.log(vm.isDepartmentChildShown[id]);
            return false;
        }
        function mouseupEvent(event, count, id, name) {
            // console.log(count);

            if (count > 0) {
                vm.showDepartmentChild(event, id)
            } else {
                // vm.newProjectForm.departmentId = id;
                // vm.defaultDepartment = name;
                $mdMenu.hide()
            }

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