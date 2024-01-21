(function () {
    'use strict';

    angular
        .module('app.request')
        .controller('RequestTaskDialogController', RequestTaskDialogController);

    /** @ngInject */
    function RequestTaskDialogController(
        $q,
        $timeout,
        $mdDialog,
        $mdMenu,
        $mdConstant,
        $filter,
        $state,
        SourceList,
        ThisRequest,
        RequestTypes,
        AutoComplete,
        UserDepartments,
        InjuryTypes,
        ThisTask,
        Projects,
        OpenProjectDialog,
        NotificationService,

        msApi,
        event) {
        var vm = this;
        // Data
        //  console.log(SourceList)
        vm.title = 'Yeni tapşırıq';
        vm.newRequest = false;
        vm.sourceList = SourceList.list;
        vm.fromType = SourceList.from
        vm.thisRequest = ThisRequest;
        vm.requestTypesFilter = RequestTypes;
        vm.task = ThisTask;
        vm.projectList = Projects;
        vm.userList = AutoComplete.userList;
        vm.userList.searchText = null;
        vm.userList.selectedItem = null;
        vm.departmentList = UserDepartments;//AutoComplete.departmentList;
        vm.departmentList.searchText = null;
        vm.departmentList.selectedItem = null;
        vm.keys = [$mdConstant.KEY_CODE.ENTER, $mdConstant.KEY_CODE.COMMA, $mdConstant.KEY_CODE.TAB];

        //   console.log('state in task', $state.current.name)
        vm.injuryTypes = InjuryTypes;
        vm.taskForm = {
            'startDate': new Date(),
            'startDateTimeStamp': new Date().getTime(),
            'endDate': moment(new Date()).add(vm.thisRequest.customerRequestType.executionDay, 'd').toDate(),
            'CustomerRequestId': vm.thisRequest.id,
            'sourceTypeId': vm.thisRequest.sourceTypeId
            // 'departmentId':vm.thisRequest.customerRequestType.departmentId

        }
        if (vm.thisRequest.id) {
            //console.log($state.current.name);
            ///console.log(vm.thisRequest);
            if ($state.current.name == 'app.callcenter') {
                vm.taskForm['description'] = vm.thisRequest.customerNumber + " | " + vm.thisRequest.customerRequestType.name;
                vm.taskForm['note'] = $filter('htmlToPlaintext')("Əlaqə: " + vm.thisRequest.contactNumber + "<br/>" + (vm.thisRequest.text || ''));
                try {
                    var dep;
                    var prefixLength = 4;
                    if (String(vm.thisRequest.customerNumber).substring(0, 2) == "12" || String(vm.thisRequest.customerNumber).substring(0, 2) == "18") {
                        prefixLength = 2;
                    }
                    else if (String(vm.thisRequest.customerNumber).substring(0, 4) == "2225" || String(vm.thisRequest.customerNumber).substring(0, 4) == "2226") {
                        prefixLength = 3;
                    }
                    // console.log('substr', String(vm.thisRequest.customerNumber).substring(0, 4));
                    //   console.log(vm.departmentList);
                    var prefix = String(vm.thisRequest.customerNumber).substring(0, prefixLength);
                    for (var i = 0; i <= vm.departmentList.length; i++) {
                        if (vm.departmentList[i].departmentPrefix == prefix) {
                            dep = vm.departmentList[i];
                            break;
                        }
                    }
                    //var dep = $filter('filter')(vm.departmentList, { departmentPrefix: String(vm.thisRequest.customerNumber).substring(0, prefixLength) })
                    //console.log('substring=>', dep);

                    vm.taskForm.departmentId = dep.id;
                    vm.fromName = dep.name;
                    vm.selectedDepartmentIds = [];
                    vm.selectedDepartmentIds.push(dep.id);

                    var user = $filter('filter')(vm.userList.users, { departmentId: dep.id })[0];
                    vm.taskForm.executorUserId = user.id;
                    $mdMenu.hide()
                    //console.log(vm.selectedDepartmentIds)
                }
                catch (err) {
                    vm.taskForm.departmentId = undefined
                    vm.fromName = undefined
                    vm.taskForm.executorUserId = undefined
                    console.log('istifadeci ve ya rayon tapilmadi')
                    console.log(err);
                }


            }
            else {
                vm.taskForm['description'] = vm.thisRequest.customerRequestTypeId == window.__env.multiTaskId ? vm.thisRequest.text : ((vm.thisRequest.department ? vm.thisRequest.department.name + ' / ' : "") + vm.thisRequest.customerRequestType.name);
                vm.taskForm['note'] = vm.thisRequest.customerRequestTypeId == window.__env.multiTaskId ? "" : $filter('htmlToPlaintext')(vm.thisRequest.text || '');
                vm.taskForm.departmentId = vm.thisRequest.customerRequestType.departmentId || null;
            }
        }
        vm.taskForm[vm.fromType] = vm.thisRequest[vm.fromType] || null
        vm.taskForm.mainTaskId = vm.task.id || null;
        vm.taskForm.sourceTypeId = 1;
        vm.taskForm.tag = []
        vm.taskForm.attachment = []
        vm.taskFileSelected = taskFileSelected;
        vm.fileUrl = msApi.fileUrl;
        vm.openProjectDialog = OpenProjectDialog;
        vm.removeFile = removeFile
        // console.log(vm.thisRequest);
        // console.log(vm.taskForm)
        // Methods
        vm.saveTask = saveTask;
        vm.deleteTask = deleteTask;
        vm.closeDialog = closeDialog;
        vm.addNewTag = addNewTag;
        vm.mouseupEvent = mouseupEvent;
        vm.mouseupEventShow = mouseupEventShow;
        vm.showDepartmentChild = showDepartmentChild;


        // console.log(vm);
        //////////
        vm.toDay = new Date();

        vm.minStartDate = new Date(
            vm.toDay.getFullYear(),
            vm.toDay.getMonth(),
            vm.toDay.getDate()
        );
        vm.minEndDate = new Date(
            vm.toDay.getFullYear(),
            vm.toDay.getMonth(),
            vm.toDay.getDate()
        );
        vm.dateChanged = dateChanged
        vm.taskForm.startDate = vm.minStartDate;

        function dateChanged(date) {
            vm.minEndDate = new Date(
                date.getFullYear(),
                date.getMonth(),
                date.getDate()
            );
            if (date > vm.taskForm.endDate) {
                vm.taskForm.endDate = vm.minEndDate;
            }
        }

        function addNewTag(chip) {
            return {
                name: chip
            };
        }
        function removeFile(index) {
            vm.taskForm.attachment.splice(index, 1)
        }
        /**
         * Save task
         */
        function saveTask(taskForm) {

            /////////////////////////////////////           
            var storagelengt = window.localStorage.length;
            var nowTime = new Date();
            nowTime = nowTime.getTime();
            var h = 4;

            if (storagelengt > 0) {

                for (var i = 0; i < localStorage.length; i++) {
                    var _key = localStorage.key(i);

                    var delStorage = _key.substring(0, 2);
                    if (delStorage == "T_") {
                        var del = localStorage.getItem(localStorage.key(i));
                        if (nowTime - del > h * 60 * 60 * 1000) {

                            localStorage.removeItem(_key);
                        }
                    }
                }
            }
            ///////////////////////////////////////
            //console.log(vm.userList.selectedItem)
            vm.userList.selectedItem = null;
            // console.log(vm.userList.selectedItem)
            //  return;
            var storage = localStorage.getItem("T_" + taskForm.CustomerRequestId + "|" + taskForm.description)

            if (storage == null) {
                var StorageKey = "T_" + taskForm.CustomerRequestId + "|" + taskForm.description;

                localStorage.setItem(StorageKey, nowTime); ///????

                msApi.request('addTask@save', taskForm,
                    // SUCCESS
                    function (response) {
                        if (response.isSuccess) {
                            NotificationService.show('Tapşırıq əlavə olundu', 'success')
                        } else {
                            response.errorList.forEach(function (element) {
                                NotificationService.show(element.text, 'error')
                            }, this);
                        }

                        if (vm.thisRequest.id != null)
                            vm.thisRequest.mainTask.push(response.output);

                        if (vm.taskForm.mainTaskId) {
                            vm.task.childTasks.push(response.output);
                        }
                        // if (vm.thisRequest.id != null)
                        //     vm.thisRequest.mainTask.push(response);
                        // else
                        //     vm.tasks.unshift(response)
                        //console.log(vm.tasks)
                    },
                    // ERROR
                    function (response) {
                        console.error(response.data)
                    }
                );
            }
            else {
                alert('Tapsiriq movcuddur yeni tapsiriq üçün 4(dörd) saat gözlenilmelidir', 'error');
                NotificationService.show('Tapsiriq movcuddur yeni tapsiriq üçün 4 (dörd) saat gözlənilmelidir', 'error');
            }
            closeDialog();
        }

        vm.isDepartmentChildShown = [];
        function showDepartmentChild(event, id) {
            //console.log(id);
            vm.isDepartmentChildShown[id] = !(vm.isDepartmentChildShown[id] | false);
            //console.log(vm.isDepartmentChildShown[id]);
            return false;
        }

        // function mouseupEvent(event, count, id, name) {
        //     // console.log(count);

        //     if (count > 0) {
        //         vm.showDepartmentChild(event, id)
        //     } else {
        //         // vm.departmentList.selectedItemChange(vm.taskForm, 'departmentId', id);
        //         vm.taskForm.departmentId = id;
        //         vm.fromName = name;
        //         $mdMenu.hide()
        //     }
        //     //console.log(vm.newTaskForm.departmentId)

        // }


        vm.selectedDepartmentIds = [];
        function mouseupEvent(event, count, dep, departments) {
            // console.log(count);

            // if (count > 0) {
            //     vm.showDepartmentChild(event, id)
            // } else {
            //vm.departmentList.selectedItemChange(vm.newTaskForm, 'departmentId', dep.id);
            vm.taskForm.departmentId = dep.id;
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
