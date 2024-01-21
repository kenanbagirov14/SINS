(function () {
    'use strict';

    angular
        .module('app.task')
        .controller('MultiTaskDialogController', MultiTaskDialogController);

    /** @ngInject */
    function MultiTaskDialogController(
        $mdDialog,
        $mdMenu,
        $mdConstant,
        $filter,
        SourceList,
        ThisRequest,
        RequestTypes,
        AutoComplete,
        InjuryTypes,
        ThisTask,
        Projects,
        Tasks,
        NotificationService,
        msApi,
        OpenProjectDialog,
        authService,
        event) {
        var vm = this;
        vm.newTaskForm = {};
        // Data
        //  console.log(SourceList)
        vm.title = 'Yeni tapşırıqlar';
        vm.newRequest = false;
        vm.sourceList = SourceList.list;
        vm.fromType = SourceList.from
        vm.thisRequest = ThisRequest;
        vm.requestTypesFilter = RequestTypes;
        vm.task = ThisTask;
        vm.projectList = Projects;
        vm.userList = AutoComplete.userList;
        vm.departmentList = AutoComplete.departmentList;
        vm.tasks = Tasks;
        vm.taskDialogDepartmentList = {}
        vm.taskDialogUserList = {}
        vm.openProjectDialog = OpenProjectDialog;
        vm.taskDialogDepartmentList.searchText = null;
        vm.taskDialogDepartmentList.searchText = null;
        vm.taskDialogUserList.selectedItem = null;
        vm.taskDialogUserList.selectedItem = null;

        vm.taskDialogUserList.selectedItemChange = selectedItemChange;
        vm.keys = [$mdConstant.KEY_CODE.ENTER, $mdConstant.KEY_CODE.COMMA, $mdConstant.KEY_CODE.TAB];

        vm.mouseupEvent = mouseupEvent;
        vm.mouseupEventShow = mouseupEventShow;
        //console.log(vm.sourceList)
        vm.injuryTypes = InjuryTypes;
        var now = new Date();
        console.log(now);
        vm.taskForm = {
            'startDate': now,
            'startDateTimeStamp': moment(now).toDate().getTime(),
            'endDate': moment(now).add(vm.thisRequest.customerRequestType.executionDay, 'd').toDate(),
            'CustomerRequestId': vm.thisRequest.id,
            'sourceTypeId': vm.thisRequest.sourceTypeId,
            'description': 'İstismar işləri'
            // 'departmentId':vm.thisRequest.customerRequestType.departmentId

        }
        // if (vm.thisRequest.id) {
        //     vm.taskForm['description'] = (vm.thisRequest.department ? vm.thisRequest.department.name + ' / ' : "") + vm.thisRequest.customerRequestType.name;
        //     vm.taskForm['note'] = $filter('htmlToPlaintext')(vm.thisRequest.text || '');
        // }
        vm.taskForm[vm.fromType] = vm.thisRequest[vm.fromType] || null
        vm.taskForm.departmentId = vm.thisRequest.customerRequestType.departmentId || null;
        vm.taskForm.mainTaskId = vm.task.id || null;
        vm.taskForm.sourceTypeId = 1;
        vm.taskForm.tag = []
        vm.taskForm.attachment = []
        vm.taskForm.mainTask = [];
        vm.taskFileSelected = taskFileSelected;
        vm.fileUrl = msApi.fileUrl;
        vm.removeFile = removeFile
        vm.addNewTask = addNewTask;
        vm.removeNewTask = removeNewTask;
        vm.showDepartmentChild = showDepartmentChild;
        vm.isDepartmentChildShown = [];
        // console.log(vm.thisRequest);
        // console.log(vm.taskForm)
        // Methods
        vm.saveTask = saveTask;
        vm.deleteTask = deleteTask;
        vm.closeDialog = closeDialog;
        vm.addNewTag = addNewTag;
        console.log(vm.userList);
        // console.log(vm);
        //////////
        // vm.toDay = new Date();

        // vm.minEndDate = vm.taskForm.startDate
        // vm.dateChanged = dateChanged

        // function dateChanged(date) {
        //     vm.minEndDate = moment(date).toDate();
        //     if (date > vm.taskForm.endDate) {
        //         vm.taskForm.endDate = vm.minEndDate;
        //     }
        // }
        function showDepartmentChild(event, id) {
            //console.log(id);
            vm.isDepartmentChildShown[id] = !(vm.isDepartmentChildShown[id] | false);
            //console.log(vm.isDepartmentChildShown[id]);
            return false;
        }
        vm.selectedDepartmentIds = [];
        function mouseupEvent(event, count, dep, departments) {
            // console.log(count);

            // if (count > 0) {
            //     vm.showDepartmentChild(event, id)
            // } else {
            vm.departmentList.selectedItemChange(vm.newTaskForm, 'departmentId', dep.id);
            vm.fromName = dep.name;
            $mdMenu.hide()
            vm.selectedDepartmentIds = [];
            vm.selectedDepartmentIds = recursiveDepartment(departments, vm.selectedDepartmentIds)
            vm.selectedDepartmentIds.push(dep.id);
            console.log(vm.selectedDepartmentIds);
            //}
            //console.log(vm.newTaskForm.departmentId)

        }
        function recursiveDepartment(departments, array) {
            for (var i = 0; i < departments.length; i++) {
                array.push(departments[i].id);
                if (departments[i].childDepartments.length > 0) {
                    array.concat(recursiveDepartment(departments[i].childDepartments, array))
                }
            }
            return array;
        }

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
        function selectedItemChange(model, property, value) {
            model[property] = value;
        }
        function addNewTag(chip) {
            return {
                name: chip
            };
        }
        function removeFile(index) {
            vm.taskForm.attachment.splice(index, 1)
        }

        function addNewTask(data) {
            // return;
            data.note = vm.taskForm.description

            var executorUserId = 1;
            var departmentId = 1;
            if (vm.isProjectActive) {
                var projectName = null;
                try {
                    projectName = JSON.parse(vm.taskForm.project).name
                } catch (err) {
                    console.log(err);
                }
                data.projectName = projectName
            }

            data.endDate = moment(vm.taskForm.endDate).add(4, "h").toDate();
            try {
                executorUserId = JSON.parse(data.executorUser).id;
                departmentId = JSON.parse(data.executorUser).departmentId;
            } catch (err) {
                console.log(err);
            }
            vm.newTaskForm.executorUserId = executorUserId
            vm.newTaskForm.departmentId = departmentId
            // console.log(data);
            vm.taskForm.mainTask.push(data);
            vm.newTaskForm = {};
            vm.taskDialogDepartmentList.searchText = null;
            vm.taskDialogDepartmentList.selectedItem = null;
            vm.taskDialogUserList.searchText = null;
            vm.taskDialogUserList.selectedItem = null;
            vm.newTaskForm = null;
        }
        function removeNewTask(data) {
            vm.taskForm.mainTask.splice(vm.taskForm.mainTask.indexOf(data), 1);
        }
        console.log(authService);
        /**
         * Save task
         */
        function saveTask(taskForm) {
            if (vm.newTaskForm != null && vm.newTaskForm.executorUser != null && vm.newTaskForm.description.length > 3) {
                this.addNewTask(vm.newTaskForm)
            }
            var requestData = {
                "customerRequestTypeId": window.__env.multiTaskId, // ümumi tapşırıqlar
                "departmentId": authService.authentication.departmentId, //todo user deparmtnet
                "customerName": authService.authentication.userName,
                "customerNumber": null,
                "startDate": moment(vm.taskForm.startDate).add(4, "h").toDate(),
                "endDate": moment(vm.taskForm.endDate).add(4, "h").toDate(),
                "text": vm.taskForm.description,
                "description": vm.taskForm.description,
                "sourceTypeId": 1,
                "mainTask": vm.taskForm.mainTask
            }
            msApi.request('addRequest@save', requestData,
                // SUCCESS
                function (response) {
                    if (response.isSuccess) {
                        try {
                            for (var i = 0; i < response.output.mainTask.length; i++) {
                                vm.tasks.unshift(response.output.mainTask[i]);
                            }
                        } catch (err) {
                            console.log(err);
                        }
                        NotificationService.show('Tapşırıqlar əlavə olundu', 'success');
                        // vm.taskForm = {
                        //     'startDate': new Date(),
                        //     'startDateTimeStamp': new Date().getTime(),
                        //     'endDate': moment(new Date()).add(vm.thisRequest.customerRequestType.executionDay, 'd').toDate(),
                        //     'CustomerRequestId': vm.thisRequest.id,
                        //     'sourceTypeId': vm.thisRequest.sourceTypeId,
                        //     // 'departmentId':vm.thisRequest.customerRequestType.departmentId

                        // }
                    } else {
                        response.errorList.forEach(function (element) {
                            NotificationService.show('error', element.text)
                        }, this);
                    }

                    // if (vm.thisRequest.id != null)
                    //     vm.thisRequest.mainTask.push(response.output);

                    // if (vm.taskForm.mainTaskId) {
                    //     vm.task.childTasks.push(response.output);
                    // }
                    // if (vm.thisRequest.id != null)
                    //     vm.thisRequest.mainTask.push(response);
                    // else
                    //     vm.tasks.unshift(response)
                    //console.log(vm.tasks)
                },
                // ERROR
                function (response) {
                    console.error(response.data)
                    NotificationService.show('Bu tapşırığı artiq yaratmısız yenisi üçün 4 saat gözləməlisiz', 'error');
                }
            );
            closeDialog();
        }

        function taskFileSelected(e, reader, file, fileList, fileOjects, fileObj) {
            vm.taskForm.isProgress = true;
            //vm.commentFileName = file.filename;
            //console.log(file)
            var newFile = {
                // relationalId: vm.selectedTask.id,
                fileType: 1,
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
                    vm.taskForm.attachment.push(response.output)
                    console.log(vm.taskForm);
                    vm.taskForm.isProgress = false
                },
                // ERROR
                function (response) {
                    console.error(response.data)
                    vm.taskForm.isProgress = false
                }
            );
            // console.log(newFile);
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
                // for (var i = 0; i < vm.tasks.length; i++) {
                //     if (vm.tasks[i].id === vm.task.id) {
                //         vm.tasks[i].deleted = true;
                //         break;
                //     }
                // }
            }, function () {
                // Cancel Action
            });
        }

        /**
         * Close dialog
         */
        function closeDialog() {
            $mdDialog.hide();
        }
        vm.jsonResult = function (str) {
            if (str) {
                try {
                    return JSON.parse(JSON.parse(str).description);
                } catch (err) {
                    console.log(err)
                    return [];
                }
            }
            else {
                return null;
            }
        }

        vm.userFilter = function (items) {
            //(filterByArray :  'departmentId':  vm.selectedDepartmentIds && filterByArray  : 'userName' : JSON.parse(vm.taskForm.project).description)
            return function (user) {
                var firstCase = false;
                console.log(vm.selectedDepartmentIds)
                firstCase = $filter('filterByArray')(items, user.departmentId, vm.selectedDepartmentIds)
                return firstCase;
            };
        };





















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
