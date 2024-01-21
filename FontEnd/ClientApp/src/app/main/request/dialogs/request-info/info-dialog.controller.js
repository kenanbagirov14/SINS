(function () {
    'use strict';

    angular
        .module('app.request')
        .controller('RequestInfoDialogController', RequestInfoDialogController);

    /** @ngInject */
    function RequestInfoDialogController($mdDialog,
        $filter,
        $rootScope,
        SourceList,
        RequestTypes,
        Requests,
        RequestStatusList,
        ThisRequest,
        NotificationService,
        Caller, AuthData, msApi, event) {
        var vm = this;
        vm.fileUrl = msApi.fileUrl;
        vm.formDisabled = true;
        // Data
        //console.log(RequestTypes);
        vm.sourceList = SourceList.list;
        vm.requests = Requests;
        //console.log(Requests); 
        vm.requestTypesFilter = RequestTypes;
        vm.requestStatusList = RequestStatusList
        //vm.sourceType = SourceType;
        vm.fromType = SourceList.from;
        vm.setRequestStatus = setRequestStatus;
        vm.caller = Caller;
        vm.authData = AuthData;
        //console.log(vm.authData.roles.indexOf("User"));
        vm.typeRequest = { name: "Müraciətin tipi", status: true };
        vm.changeTypeRequest = changeTypeRequest
        //vm.customerRequestFilter = { customerRequestTypeId: id }
        vm.title = 'Ətraflı baxış '// + vm.sourceType.name;

        vm.requestForm = ThisRequest;
        vm.requestForm.startDate = moment(ThisRequest.startDate).toDate()
        // Methods
        vm.saveRequest = saveRequest;
        vm.deleteTask = deleteTask;
        vm.closeDialog = closeDialog;
        //console.log(vm.requestForm);
        //////////
        function changeTypeRequest(selectedId) {
            var thisType = $filter('filter')(vm.requestTypesFilter, { id: selectedId })[0];
            //console.log(typeof thisType)
            if (thisType.childRequestTypes && thisType.childRequestTypes.length) {
                //var selectedTypeName = $filter('filter')(vm.requestTypesFilter, { id: selectedId })[0].name;
                vm.typeRequest = { name: thisType.name, status: false };
                vm.requestTypesFilter = thisType.childRequestTypes;
                //console.log(vm.requestTypesFilter);
            }
            else {
                // vm.typeRequest = "Müraciətin tipi";
                // vm.requestTypesFilter = RequestTypes;
            }
            //console.log(selectedTypeChild);

        }
        /**
         * Save task
         */
        function saveRequest(requestForm) {

            msApi.request('updateRequest@save', requestForm,
                // SUCCESS
                function (response) {
                    if (response.isSuccess) {
                        NotificationService.show('Müraciət dəyişdirildi', 'success')
                        if (response.output.requestStatus.hasOwnProperty("updatedDate"))
                            response.output.requestStatus.createdDate = response.output.requestStatus.updatedDate
                    } else {
                        response.errorList.forEach(function (element) {
                            NotificationService.show(element.text, 'error')
                        }, this);
                    }

                    vm.requestForm = response.output;
                    var _requestExist = $filter('filter')(vm.requests, { id: response.output.id });
                    console.log(_requestExist);
                    if (_requestExist.length > 0) {
                        vm.requests[vm.requests.indexOf(_requestExist[0])] = response.output;
                    }
                },
                // ERROR
                function (response) {
                    console.error(response.output)
                }
            );

            closeDialog();
        }

        /**
         * Delete task
         */
        function deleteTask() {
            var confirm = $mdDialog.confirm()
                .title('Are you sure?')
                .content('The Task will be deleted.')
                .ariaLabel('Delete Task')
                .ok('Delete')
                .cancel('Cancel')
                .targetEvent(event);

            $mdDialog.show(confirm).then(function () {
                // Dummy delete action
                for (var i = 0; i < vm.tasks.length; i++) {
                    if (vm.tasks[i].id === vm.task.id) {
                        vm.tasks[i].deleted = true;
                        break;
                    }
                }
            }, function () {
                // Cancel Action
            });
        }

        function setRequestStatus(request, statusId) {
            var requestHistory = { CustomerRequestId: request.id, RequestStatusId: statusId };
            msApi.request('updateRequestStatusHistory@save', requestHistory,
                // SUCCESS
                function (response) {
                    //requestForm.createdDate = new Date();
                    vm.requestForm.requestStatus.id = statusId;
                    //console.log(response.data)
                },
                // ERROR
                function (response) {
                    console.error(response)
                }
            );
        }


        /**
         * Close dialog
         */
        function closeDialog() {
            $mdDialog.hide();
        }
    }
})();