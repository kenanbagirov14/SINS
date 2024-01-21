(function () {
    'use strict';

    angular
        .module('app.request')
        .controller('PhoneCardController', PhoneCardController);

    /** @ngInject */
    function PhoneCardController(
        $mdDialog,
        $filter,
        $rootScope,
        $mdMenu,
        msApi,
        Task,
        AuthData,
        // NotificationService
        event
    ) {
        var vm = this;
        //vm.customerRequestFilter = { customerRequestTypeId: id }
        vm.title = 'Telefon Kartatekası' + ": " + Task.customerRequest.customerNumber
        // Methods     
        vm.closeDialog = closeDialog;
        vm.loadPhoneCard = loadPhoneCard;
        vm.phoneCard = {};
        function loadPhoneCard() {
            msApi.request('findPhoneNumber@save', { number: Task.customerRequest.customerNumber },
                // SUCCESS
                function (response) {
                    try {
                        vm.phoneCard = response.output;
                    }
                    catch (err) {
                        console.log(err);
                    }
                    //requestForm.createdDate = new Date();
                    // vm.requests.unshift(response);
                    //console.log(response.data)
                },
                // ERROR
                function (response) {
                    console.error(response.data)
                }
            );
        }
        loadPhoneCard();
        // console.log(vm.departmentList)
        // //////////
        // function changeTypeRequest(selectedId) {
        //     var thisType = $filter('filter')(vm.requestTypesFilter, { id: selectedId })[0];
        //     vm.requestForm.placeHolder.requestType = thisType.name;
        //     //console.log(typeof thisType)
        //     if (thisType.childRequestTypes && thisType.childRequestTypes.length) {
        //         //var selectedTypeName = $filter('filter')(vm.requestTypesFilter, { id: selectedId })[0].name;
        //         vm.typeRequest = { name: thisType.name, status: false };
        //         vm.requestTypesFilter = thisType.childRequestTypes;
        //         //console.log(vm.requestTypesFilter);
        //     }
        //     else {
        //         // vm.typeRequest = "Müraciətin tipi";
        //         // vm.requestTypesFilter = RequestTypes;
        //     }
        //     //console.log(selectedTypeChild);

        // }

        // function showDepartmentChild(event, id) {
        //     //console.log(id);
        //     vm.isDepartmentChildShown[id] = !(vm.isDepartmentChildShown[id] | false);
        //     //console.log(vm.isDepartmentChildShown[id]);
        // }
        // function mouseupEvent(event, count, dep, deps) {
        //     //vm.departmentList.selectedItemChange(vm.requestForm, vm.fromType, department.id); vm.fromName = department.name
        //     // console.log(count);

        //     // if (count > 0) {
        //     //     vm.showDepartmentChild(event, id)
        //     // } else {
        //     vm.departmentList.selectedItemChange(vm.requestForm, vm.fromType, dep.id);
        //     vm.fromName = dep.name;
        //     vm.requestForm.placeHolder.department = dep.name;
        //     $mdMenu.hide()
        //     // }

        // }
        // vm.mouseupEventShow = mouseupEventShow;
        // function mouseupEventShow(event, count, id, name) {
        //     // console.log(count);

        //     // if (count > 0) {
        //     vm.showDepartmentChild(event, id)
        //     // } else {
        //     //    vm.departmentList.selectedItemChange(vm.newTaskForm, 'departmentId', id);
        //     //    vm.fromName = name;
        //     //    $mdMenu.hide()
        //     //}
        //     //console.log(vm.newTaskForm.departmentId)

        // }
        // /**
        //  * Save task
        //  */
        // function saveRequest(requestForm) {
        //     requestForm.startDate = moment(requestForm.startDate || new Date()).add(0, 'hours').format();
        //     // console.log(requestForm);
        //     // return;
        //     msApi.request('addRequest@save', requestForm,
        //         // SUCCESS
        //         function (response) {
        //             if (response.isSuccess) {
        //                 NotificationService.show('Müraciət əlavə olundu', 'success')
        //             } else {
        //                 response.errorList.forEach(function (element) {
        //                     NotificationService.show(element.text, 'error')
        //                 }, this);
        //             }
        //             try {
        //                 var _requestExist = $filter('filter')(vm.requests, { id: response.output.id });
        //                 if (_requestExist.length < 1) {
        //                     $rootScope.$evalAsync(function () {
        //                         response.output.customerRequestType = { name: requestForm.placeHolder.requestType }
        //                         response.output.department = { name: requestForm.placeHolder.department }
        //                         vm.requests.unshift(response.output);
        //                     })
        //                 }


        //                 if (requestForm.createTask) {
        //                     OpenTaskDialog(event, response.output);
        //                 }
        //             }
        //             catch (err) {
        //                 console.log(err);
        //             }
        //             //requestForm.createdDate = new Date();
        //             // vm.requests.unshift(response);
        //             //console.log(response.data)
        //         },
        //         // ERROR
        //         function (response) {
        //             console.error(response.data)
        //         }
        //     );
        //     closeDialog();
        // }

        // /**
        //  * Delete task
        //  */
        // function deleteTask() {
        //     var confirm = $mdDialog.confirm()
        //         .title('Are you sure?')
        //         .content('The Task will be deleted.')
        //         .ariaLabel('Delete Task')
        //         .ok('Delete')
        //         .cancel('Cancel')
        //         .targetEvent(event);

        //     $mdDialog.show(confirm).then(function () {
        //         // Dummy delete action
        //         for (var i = 0; i < vm.tasks.length; i++) {
        //             if (vm.tasks[i].id === vm.task.id) {
        //                 vm.tasks[i].deleted = true;
        //                 break;
        //             }
        //         }
        //     }, function () {
        //         // Cancel Action
        //     });
        // }

        /**
         * Close dialog
         */
        function closeDialog() {
            $mdDialog.hide();
        }
    }
})();