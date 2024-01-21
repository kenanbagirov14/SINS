(function () {
    'use strict';

    angular
        .module('app.request')
        .controller('RequestDialogController', RequestDialogController);

    /** @ngInject */
    function RequestDialogController($mdDialog,
        $filter,
        $rootScope,
        $mdMenu,
        SourceList,
        RequestTypes,
        Requests,
        SourceType,
        ThisRequest,
        UserDepartments,
        AuthData,
        Caller,
        msApi,
        AutoComplete,
        OpenTaskDialog,
        NotificationService,
        lf,
        // LoadMoreIsInProgress,
        event) {
        var vm = this;
        // Data
        //console.log(RequestTypes);
        vm.sourceList = SourceList.list;
        vm.requests = Requests;
        vm.requestTypesFilter = [];
        vm.sourceType = SourceType;
        vm.fromType = SourceList.from;
        vm.caller = Caller;
        vm.mouseupEvent = mouseupEvent
        vm.mouseupEventShow = mouseupEventShow;
        vm.numberBlur = numberBlur;

        vm.requestFileSelected = requestFileSelected;
        vm.removeFile = removeFile;

        lf.getItem("offlineRequestTypesFilter_todo" + vm.sourceType.id, function (err, data) { //TODO rename
            if (data) vm.requestTypesFilter = data;
            else {
                msApi.request('customRequestTypes@query',
                    // SUCCESS
                    function (response) {
                        vm.requestTypesFilter = response.output;
                        lf.setItem("offlineRequestTypesFilter_" + vm.sourceType.id, response.output)
                    },
                    // ERROR
                    function (err) {
                        console.error(err)
                    }
                )
            }
        }) // TODO bir

        // msApi.request('customRequestTypes@query',
        //   // SUCCESS
        //   function (response) {
        //     vm.requestTypesFilter = response.output;
        //     lf.setItem("offlineRequestTypesFilter_" + vm.sourceType.id, response.output)
        //   },
        //   // ERROR
        //   function (err) {
        //     console.error(err)
        //   }
        // ) //TODO bir


        // console.log(vm.sourceType);
        function mouseupEventShow(event, count, id, name) {
            // console.log(count);

            // if (count > 0) {
            vm.showDepartmentChild(event, id)
            // } else {
            //    vm.departmentList.selectedItemChange(vm.newTaskForm, 'departmentId', id);
            //    vm.fromName = name;
            //    $mdMenu.hide()
            //}
            //console.log(vm.newTaskForm.departmentId)

        }
        vm.typeRequest = {
            name: "Müraciətin tipi",
            status: true
        };
        vm.changeTypeRequest = changeTypeRequest
        //vm.customerRequestFilter = { customerRequestTypeId: id }
        vm.title = 'Yeni ' + vm.sourceType.name;

        vm.requestForm = ThisRequest;
        // Methods
        vm.saveRequest = saveRequest;
        vm.deleteTask = deleteTask;
        vm.closeDialog = closeDialog;
        vm.departmentList = AutoComplete.departmentList;
        vm.departmentList.searchText = null;
        vm.departmentList.selectedItem = null;
        vm.showDepartmentChild = showDepartmentChild;
        vm.isDepartmentChildShown = [];
        vm.requestForm.placeHolder = {};
        vm.requestForm.attachment = []
        // console.log(vm.departmentList)
        //////////
        function changeTypeRequest(selectedId) {
            var thisType = $filter('filter')(vm.requestTypesFilter, {
                id: selectedId
            })[0];
            vm.requestForm.placeHolder.requestType = thisType.name;
            //console.log(typeof thisType)
            if (thisType.childRequestTypes && thisType.childRequestTypes.length) {
                //var selectedTypeName = $filter('filter')(vm.requestTypesFilter, { id: selectedId })[0].name;
                vm.typeRequest = {
                    name: thisType.name,
                    status: false
                };
                vm.requestForm.placeHolder.from = thisType.name;
                vm.requestTypesFilter = thisType.childRequestTypes;
                //console.log(vm.requestTypesFilter);
            } else {
                // vm.typeRequest = "Müraciətin tipi";
                // vm.requestTypesFilter = RequestTypes;
            }
            //console.log(selectedTypeChild);

        }

        function showDepartmentChild(event, id) {
            //console.log(id);
            vm.isDepartmentChildShown[id] = !(vm.isDepartmentChildShown[id] | false);
            //console.log(vm.isDepartmentChildShown[id]);
        }

        function numberBlur(num) {
            num = String(num);
            if (vm.sourceType.id == 2 && num.length > 8) {
                //console.log(vm.departmentList.departments);
                //var dep = $filter('filter')(vm.departmentList.departments, { regionPrefix: num.substring(0, 4) })[0];
                //mouseupEvent(null, 0, dep, null)
            }
        }

        if (vm.sourceType.id == 4) {
            var userDepartment = $filter('filter')(UserDepartments, { id: AuthData.departmentId })[0];
            vm.departmentList.selectedItemChange(vm.requestForm, vm.fromType, userDepartment.id);
            vm.fromName = userDepartment.name;
            vm.requestForm.customerNumber = userDepartment.phone;
            vm.requestForm.customerName = AuthData.userName;
            vm.requestForm.placeHolder.department = userDepartment.name;
        }

        function mouseupEvent(event, count, dep, deps) {
            //vm.departmentList.selectedItemChange(vm.requestForm, vm.fromType, department.id); vm.fromName = department.name
            // console.log(count);

            // if (count > 0) {
            //     vm.showDepartmentChild(event, id)
            // } else {
            vm.departmentList.selectedItemChange(vm.requestForm, vm.fromType, dep.id);
            vm.fromName = dep.name;
            //console.log(dep);
            vm.requestForm.placeHolder.department = dep.name;

            if (vm.sourceType.id == 2) {
                vm.requestForm.customerNumber = dep.regionPrefix; // Number(dep.regionPrefix+String(vm.requestForm.customerNumber).substr(4) |;
            }
            $mdMenu.hide()
            // }

        }
        vm.mouseupEventShow = mouseupEventShow;

        function mouseupEventShow(event, count, id, name) {
            // console.log(count);

            // if (count > 0) {
            vm.showDepartmentChild(event, id)
            // } else {
            //    vm.departmentList.selectedItemChange(vm.newTaskForm, 'departmentId', id);
            //    vm.fromName = name;
            //    $mdMenu.hide()
            //}
            //console.log(vm.newTaskForm.departmentId)

        }
        /**
         * Save task
         */
        function saveRequest(requestForm) {
            console.log(requestForm);

            requestForm.startDate = moment(requestForm.startDate || new Date()).add(0, 'hours').format();
            // console.log(requestForm);
            // return;
            msApi.request('addRequest@save', requestForm,
                // SUCCESS
                function (response) {
                    console.log(response)
                    if (response.isSuccess) {
                        NotificationService.show(response.output.id + " nömrəli müraciət əlavə edildi", 'success')
                    } else {
                        response.errorList.forEach(function (element) {
                            console.log("else")
                            //NotificationService.show(element.text, 'error')
                        }, this);
                    }
                    try {
                        $rootScope.$evalAsync(function () {
                            response.output.customerRequestType = {
                                name: requestForm.placeHolder.requestType
                            }
                            response.output.department = {
                                name: requestForm.placeHolder.department
                            }
                            vm.requests.unshift(response.output);
                        })

                        if (requestForm.createTask) {
                            OpenTaskDialog(event, response.output);
                        }
                    } catch (err) {
                        console.log(err);
                        console.log("err");

                    }
                    //requestForm.createdDate = new Date();
                    // vm.requests.unshift(response);
                    //console.log(response.data)
                },
                // ERROR
                function (response) {
                    console.error(response.data)
                    NotificationService.show(response.data.errorList[0].text, 'error');
                    alert(response.data.errorList[0].text);

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

        function requestFileSelected(e, reader, file, fileList, fileOjects, fileObj) {
            vm.requestForm.isProgress = true;
            //vm.commentFileName = file.filename;
            //console.log(file)
            var newFile = {
                // relationalId: vm.selectedTask.id,
                fileType: 2,
                mediaType: fileObj.filetype,
                container: fileObj.base64,
                description: fileObj.filename,
                extension: fileObj.filename.split('.').pop()
            }
            msApi.request('addAttachment@save', newFile,
                // SUCCESS
                function (response) {
                    // requestForm.createdDate = new Date();
                    // task.taskStatusId = statusId;
                    // console.log(response);
                    // vm.filesForUpload.push(response);
                    vm.requestForm.attachment.push(response.output)
                    // console.log(vm.requestForm);
                    vm.requestForm.isProgress = false
                },
                // ERROR
                function (response) {
                    //console.error(response.data)
                    vm.requestForm.isProgress = false
                }
            );
            // console.log(newFile);
        }

        function removeFile(index) {
            vm.requestForm.attachment.splice(index, 1)
        }
        /**
         * Close dialog
         */
        function closeDialog() {
            $mdDialog.hide();
        }
    }
})();
