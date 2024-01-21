(function () {
    'use strict';

    angular
        .module('app.task')
        .controller('TaskController', TaskController);

    /** @ngInject */
    function TaskController($scope,
        $mdSidenav,
        $mdMenu,
        // Tasks,
        // InjuryTypes,
        // UserList,
        // Departments,
        // UserDepartments,
        // Regions,
        // Projects,
        // SourceTypes,
        // //Tasks,
        // TaskStatusList,
        msUtils,
        $mdDialog,
        $mdConstant,
        $document,
        $stateParams,
        $state,
        $rootScope,
        $q,
        $filter,
        $timeout,
        $interval,
        msHub,
        msApi,
        msNavigationService,
        NotificationService,
        authService,
        MainService,
        ACLService,
        lf

    ) {
        var vm = this;
        vm.tasks = []// Tasks.output; //[]
        vm.hub = msHub.hub();

        //debugger;
        if (!ACLService.check(['routeTask'], authService)) {
            //$state.go('app.pages_error_unauthorize');
            location.assign('/pages/error/unauthorize')
            return;
        }
        $timeout.cancel(window.__env.timeout);

        var autoComplete = {};
        var userList = {}
        var departmentList = {}
        vm.hasAccess = hasAccess;
        vm.isDisabled = isDisabled;
        //console.log(MainService);
        $rootScope.pageTitle = "Tapşırıqlar";
        $rootScope.activePage = 'task';

        // Navigation
        msNavigationService.clearNavigation();
        // Data 


        vm.sourceList = {}
        vm.openSidebar = openSidebar;
        vm.closeSidebar = closeSidebar;
        vm.closeMenu = closeMenu;
        vm.closeChildSidebar = closeChildSidebar;
        vm.openChildSidebar = openChildSidebar;
        vm.mainTask = null;
        vm.clearFilter = clearFilter;
        vm.authData = authService.authentication;
        vm.injuryTypesList = {};
        vm.realInjuryTypesList = {};
        vm.stateParams = $stateParams
        vm.isThisSelected = isThisSelected;
        vm.addNewTag = addNewTag;
        vm.inLineEdit = inLineEdit;
        vm.setTaskStatus = setTaskStatus;
        vm.goToRequest = goToRequest;
        vm.deleteTask = deleteTask
        vm.showPhoneCard = showPhoneCard;
        vm.prossesing = false;
        vm.taskStatusList = {};
        vm.hourList = {};
        vm.projectList = {};
        vm.filterData = {};
        vm.filterService = filterService
        vm.openProjectDialog = openProjectDialog;
        vm.pagination = {
            pageNumber: 1,
            pageSize: 20
        };
        vm.fileUrl = msApi.fileUrl;
        vm.loadMoreComment = loadMoreComment;
        vm.updateRating = updateRating;

        vm.excelExport = excelExport;


        vm.userListArray = [];
        vm.departmentListArray = [];
        vm.isOpenSidebar = false;
        vm.assignUser = assignUser;
        // vm.addSubtask = addSubtask;
        vm.openMultiTaskDialog = openMultiTaskDialog;
        vm.openChildTaskDialog = openChildTaskDialog
        vm.thisTabSelected = thisTabSelected;
        vm.filterChange = filterChange;
        vm.findTask = findTask;
        vm.loadMore = loadMore;
        vm.selectedTask = {};
        vm.selectedChild = {};
        vm.sourceType = {};
        vm.taskComments = { main: [], child: [] };
        vm.compareDate = compareDate;

        vm.findSettings = findSettings;
        vm.saveFilter = saveFilter;
        vm.delFilter = delFilter;
        vm.userTaskSettings = {}
        //vm.checkUserRole = checkUserRole;
        vm.loadProjects = loadProjects;
        vm.isAdmin = isAdmin;
        vm.Now = new Date();
        vm.filter = {};
        vm.selectedTab = 0;
        vm.totalTasks = 0;
        //vm.filesForUpload=[];
        vm.newComment = {
            attachment: []
        };
        vm.addTaskComment = addTaskComment;
        vm.commentFileSelected = commentFileSelected;

        vm.isDepartmentChildShown = [];
        vm.showDepartmentChild = showDepartmentChild;

        vm.downloadFile = downloadFile;
        vm.keys = [$mdConstant.KEY_CODE.ENTER, $mdConstant.KEY_CODE.COMMA, $mdConstant.KEY_CODE.TAB];

        vm.filterData = {
            id: vm.stateParams.id,
            description: vm.stateParams.title,
            projectName: vm.stateParams.project ? vm.stateParams.project.split(",") : undefined,
            startDateFrom: vm.stateParams.start ? new Date(vm.stateParams.start) : undefined,
            startDateTo: vm.stateParams.end ? new Date(vm.stateParams.end) : undefined,
            taskStatusId: vm.stateParams.taskstatus ? vm.stateParams.taskstatus.split(",") : undefined,
            afterHour: vm.stateParams.hour ? vm.stateParams.hour.split(",") : undefined,
            // executorUser: vm.stateParams.executor,
            department: vm.stateParams.department ? vm.stateParams.department.split(",") : undefined,
            executorUserId: vm.stateParams.executor ? vm.stateParams.executor.split(",") : undefined
        }
        // Tasks,
        // InjuryTypes,
        // UserList,
        // Departments,
        // UserDepartments,
        // Regions,
        // Projects,
        // SourceTypes,
        // //Tasks,
        // TaskStatusList,
        var Projects

        vm.prossesing = true;
        vm.loadMoreIsInProgress = true;
        lf.getItem("tasks", function (err, data) {
            //console.log('err', err)
            // console.log('tasks', data)
            if (data) {
                vm.tasks = data
                vm.prossesing = vm.loadMoreIsInProgress = false;
                window.__env.timeout = $timeout(findTask, 15 * 1000, 0, true, vm.filterData, true, true)
            } else {
                findTask(vm.filterData, false);
            }
        })
        var loadNeccessaryData = [
            // lf.getItem("Tasks", function (err, data) {
            //   if (data) Tasks = data
            //   else msApi.request('findTask@save',{},
            //     function (response) {
            //       Tasks = response.output;
            //       lf.setItem("Tasks", Tasks)
            //     }
            //   )
            // }),
            lf.getItem("InjuryTypes", function (err, data) {
                if (data) fullFillInjuryTypes(data);
                else {
                    msApi.request('injuryTypes@query',
                        function (response) {
                            fullFillInjuryTypes(response.output);
                            lf.setItem("InjuryTypes", response.output)
                        }
                    )
                }
            }),
            lf.getItem("UserList", function (err, data) {
                if (data) fullFillUserList(data)
                else {
                    msApi.request('getUser@query',
                        function (response) {
                            fullFillUserList(response.output);
                            lf.setItem("UserList", response.output)
                        }
                    )
                }
            }),
            lf.getItem("Departments", function (err, data) {
                if (data) fullFillTaskDepartments(data)
                else {
                    msApi.request('Departments@query',
                        function (response) {
                            fullFillTaskDepartments(response.output)
                            lf.setItem("Departments", response.output)
                        }
                    )
                }
            }),
            lf.getItem("UserDepartments", function (err, data) {
                if (data) {
                    vm.userDepartmets = data
                    autoComplete.departmentList = MainService.init(departmentList, 'departments', vm.userDepartmets);
                    autoComplete.departmentList.array = vm.departmentListArray;
                    vm.departmentList = autoComplete.departmentList;
                    vm.departmentList.searchText = null;
                    vm.departmentList.selectedItem = null;
                }
                else {
                    msApi.request('UserDepartments@save', {},
                        function (response) {
                            vm.userDepartmets = response.output;
                            autoComplete.departmentList = MainService.init(departmentList, 'departments', vm.userDepartmets);

                            autoComplete.departmentList.array = vm.departmentListArray;
                            vm.departmentList = autoComplete.departmentList;
                            vm.departmentList.searchText = null;
                            vm.departmentList.selectedItem = null;
                            lf.setItem("UserDepartments", vm.userDepartmets)
                        }
                    )
                }
            }),
            lf.getItem("Regions", function (err, data) {
                if (data) {
                    vm.sourceList[2] = {
                        list: data,
                        from: "regionId"
                    }
                }
                else {
                    msApi.request('Regions@query',
                        function (response) {
                            vm.sourceList[2] = {
                                list: response.output,
                                from: "regionId"
                            }
                            lf.setItem("Regions", response.output)
                        }
                    )
                }
            }),
            lf.getItem("Projects", function (err, data) {
                if (data) Projects = data
                else {
                    Projects = [];
                    msApi.request('Projects@query',
                        function (response) {
                            Projects = response.output;
                            lf.setItem("Projects", Projects)
                        }
                    )
                }
            }),
            lf.getItem("SourceTypes", function (err, data) {
                if (data) vm.sourceTypes = data
                else {
                    msApi.request('sourceTypes@query',
                        function (response) {
                            vm.sourceTypes = response.output;
                            lf.setItem("SourceTypes", vm.sourceTypes)
                        }
                    )
                }
            }),
            lf.getItem("TaskStatusList", function (err, data) {
                if (data) fullFillTaskStatusList(data)
                else {
                    msApi.request('TaskStatusList@query',
                        function (response) {
                            fullFillTaskStatusList(response.output);
                            lf.setItem("TaskStatusList", response.output)
                        }
                    )
                }
            })

        ]

        Promise.all(loadNeccessaryData).then(function (all) {



            // vm.departmentList = Departments;




            //Departments = UserDepartments;
            //console.log(Departments);



            Projects.forEach(function (element) {
                vm.projectList[element.id] = element.name;
            }, this);
            //      window.__env.intervals.push($interval(findTask, 399 * 1000, 0, true, vm.filterData, true, true))

            findSettings({ userId: vm.authData.userId, type: 1 })


        })

        window.__env.intervals.forEach(function (element) {
            $interval.cancel(element);
        });
        window.__env.intervals.push($interval(findTask, 399 * 1000, 0, true, vm.filterData, true, true))


        function fullFillUserList(data) {
            data.forEach(function (user) {
                vm.userListArray[user.id] = user.userName;
            }, this);
            autoComplete.userList = MainService.init(userList, 'users', data);
            autoComplete.userList.array = vm.userListArray;
            vm.userList = userList;
            vm.userListChild = userList;
            vm.userList.selectedItemChange = selectedItemChange;
            vm.userListChild.selectedItemChange = selectedItemChange;
        }

        function fullFillTaskStatusList(data) {
            data.forEach(function (element) {
                vm.taskStatusList[element.id] = element.name;
            }, this);
        }
        function fullFillTaskDepartments(data) {
            data.forEach(function (department) {
                vm.departmentListArray[department.id] = department.name;
            }, this);
            vm.sourceList[1] = {
                list: data,
                from: "departmentId"
            }
        }
        function fullFillInjuryTypes(data) {
            vm.injuryTypes = data;
            data.forEach(function (injury) {
                vm.injuryTypesList[injury.id] = injury.name;
            }, this);
            vm.realInjuryTypesList = vm.injuryTypesList //TODO remove this
        }
        if (!vm.hub.methods.hasOwnProperty('remotetaskupdate')) {
            vm.hub.on('remotetaskupdate', function (response) {
                try {
                    console.log(response)
                    $rootScope.$evalAsync(function () {
                        var oldTask = $filter('filter')(vm.tasks, { id: response.newData.id })[0];
                        response.newData.endDate = new Date(response.newData.endDate);
                        //console.log(oldTask); //671397
                        vm.tasks[vm.tasks.indexOf(oldTask)] = response.newData;
                        if (vm.selectedTask.id == response.newData.id) {
                            vm.selectedTask = response.newData;
                            findComment('main', {
                                mainTaskId: vm.selectedTask.id,
                                pageNumber: 1,
                                pageSize: 3
                            });
                            findHistory({
                                mainTaskId: vm.selectedTask.id,
                                pageNumber: 1,
                                pageSize: 20
                            })
                        }
                    })
                    NotificationService.show(response.newData.id + " nömrəli tapşırıq yeniləndi", 'success')
                    //openRequestDialog(null, { id: response.Local, name: "Local" }, { number: response.CallerNumber });
                    //console.log(response);
                } catch (err) {
                    console.log(response)
                    console.log(err)
                }
            });
        }
        if (!vm.hub.methods.hasOwnProperty('remotetaskadd')) {
            vm.hub.on('remotetaskadd', function (response) {
                try {
                    if (response.newData.generatedUserId != vm.authData.userId) {
                        $rootScope.$evalAsync(function () {
                            if (response.newData.mainTaskId) {
                                //vm.tasks[response.oldData] = response.newData;
                            } else {
                                //var _taskExist = $filter('filter')(vm.tasks, { id: response.newData.id });
                                vm.tasks.unshift(response.newData);

                            }
                        })
                        NotificationService.show(response.newData.id + " nömrəli tapşırıq əlavə edildi", 'success')
                        //openRequestDialog(null, { id: response.Local, name: "Local" }, { number: response.CallerNumber });
                        //console.log(response);
                    }
                } catch (err) {
                    console.log(response)
                    console.log(err)
                }
            });
        }
        if (!vm.hub.methods.hasOwnProperty('remotetaskremove')) {
            vm.hub.on('remotetaskremove', function (response) {
                console.log(response);
                try {
                    $rootScope.$evalAsync(function () {
                        if (response.newData.mainTaskId) {
                            //vm.tasks[response.oldData] = response.newData;
                        } else {
                            var _taskExist = $filter('filter')(vm.tasks, { id: response.newData.id })[0];
                            console.log(_taskExist);
                            console.log(response);
                            vm.tasks.splice(vm.tasks.indexOf(_taskExist), 1);
                        }
                    })
                    NotificationService.show(response.newData.id + " nömrəli tapşırıq silindi", 'success')
                    //openRequestDialog(null, { id: response.Local, name: "Local" }, { number: response.CallerNumber });
                    //console.log(response);
                } catch (err) {
                    console.log(response)
                    console.log(err)
                }
            });
        }
        if (!vm.hub.methods.hasOwnProperty('remotecommentadd')) {
            vm.hub.on('remotecommentadd', function (response) {
                try {
                    $rootScope.$apply(function () {
                        // if (response.newData.mainTask) {
                        //     vm.taskComments['child'].unshift(response.newData);
                        // }
                        // else {
                        vm.taskComments['main'].unshift(response.newData);
                        //}
                    })
                } catch (err) {
                    console.log(response)
                    console.log(err)
                }
            });
        }

        function filterDepartment() {
            //console.log(authService.authentication.departments, "173");
            //return [];
            var tempArray = [];
            Departments.forEach(function (department) {
                if (department.alias && authService.authentication.departments.indexOf(department.alias.toLowerCase()) > -1) {
                    this.push(department);
                }
            }, tempArray);
            return tempArray
        }
        //vm.filter.departmentIds = vm.filterData.department
        function hasAccess(role) {
            return ACLService.check([role], authService)
        }

        function isDisabled() {
            if (vm.selectedTask == null || !vm.selectedTask.hasOwnProperty("taskStatusId"))
                return false;
            else
                return vm.selectedTask.taskStatusId == 3;
        }
        function showDepartmentChild(event, id) {
            console.log(id);
            vm.isDepartmentChildShown[id] = !(vm.isDepartmentChildShown[id] | false);
            console.log(vm.isDepartmentChildShown[id]);
            return false;
        }

        function showPhoneCard(ev, task) {
            //  thisRequest ? vm.card[thisRequest.id] = true : false;
            $mdDialog.show({
                controller: 'PhoneCardController',
                controllerAs: 'vm',
                templateUrl: 'app/main/request/dialogs/request/phone-card.html',
                //  parent: angular.element($document.find('#task-container')),
                targetEvent: ev,
                clickOutsideToClose: true,
                locals: {
                    event: ev,
                    Task: task,
                    AuthData: vm.authData
                    //SourceType: vm.sourceType
                }
            });
        }

        function newFunction() {
            return 'remotetaskadd';
        }

        function goToRequest(requestId) {
            $state.go('app.request', { id: requestId });
        }
        function inLineEdit(model, property, value) {
            // console.log(property);
            vm.startAssignDate = false;
            vm.startAssignDateChild = false;
            vm.startAssignUser = false;
            vm.startAssignUserChild = false;
            vm.startAssignTitle = false;
            vm.startAssignNote = false;
            // if (property == 'realInjuryTypeId') {
            //     vm.selectedTask.realInjuryTypeId = value
            //     return;
            // } //TODO remove this

            //console.log(model)
            updateTask(model, property, value);
        }

        function addNewTag(chip) {
            return {
                name: chip
            };
        }

        function downloadFile(filePath) {
            console.log(msApi.fileUrl + filePath);
        }

        function filterService(filter, parse) {
            if (parse != undefined) filter = angular.fromJson(filter);
            console.log("--------------------------")
            console.log(filter);
            console.log("--------------------------")

            console.log("filterAfterHour==>" + filter.afterHours);
            filter.afterHour = [];
            angular.forEach(filter.afterHours, function (value, key) {
                if (value) this.push(key);
            }, filter.afterHour);
            // console.log(filter);
            if (filter.afterHour.length < 1) delete filter.afterHour;
            //delete filter.AfterHour;


            // console.log(filter.taskStatusIds);
            filter.taskStatusId = [];
            angular.forEach(filter.taskStatusIds, function (value, key) {
                if (value) this.push(key);
            }, filter.taskStatusId);
            // console.log(filter);
            if (filter.taskStatusId.length < 1) delete filter.taskStatusId;
            //delete filter.taskStatusIds;


            filter.projectName = [];
            angular.forEach(filter.projectIds, function (value, key) {
                if (value) this.push(key);
            }, filter.projectName);
            // console.log(filter);
            if (filter.projectName.length < 1) delete filter.projectName;


            filter.departmentId = [];
            angular.forEach(filter.departmentIds, function (value, key) {
                if (value) this.push(Number(key));
            }, filter.departmentId);
            // console.log(filter);
            if (filter.departmentId.length < 1) delete filter.departmentId;

            filter.executorUserId = [];
            angular.forEach(filter.executorUsers, function (value, key) {
                if (value) this.push(Number(key));
            }, filter.executorUserId);
            // console.log(filter);
            if (filter.executorUserId.length < 1) delete filter.executorUserId;

            // console.log(filter.departmentIds)
            vm.loadMoreIsInProgress = true;
            vm.pagination.pageNumber = 1;

            vm.filterData = filter;

            if (filter.startDateFrom) vm.filterData.startDateFrom = moment(filter.startDateFrom).format();
            if (filter.startDateTo) vm.filterData.startDateTo = new Date(moment(filter.startDateTo).toDate().getTime() + 1 * 24 * 59 * 60 * 1000);
            //console.log(vm.filterData.startDateTo);

            $state.go('app.task', {
                id: filter.id,
                title: filter.description,
                project: filter.projectName ? filter.projectName.join(",") : undefined,
                taskstatus: filter.taskStatusId ? filter.taskStatusId.join(",") : undefined,
                afterHour: filter.afterHour ? filter.afterHour.join(",") : undefined,
                //executor: filter.executorUserId,
                department: filter.departmentId ? filter.departmentId.join(",") : undefined,
                executor: filter.executorUserId ? filter.executorUserId.join(",") : undefined,
                start: filter.startDateFrom ? $filter("date")(filter.startDateFrom, "yyyy-MM-dd") : undefined,
                end: filter.startDateTo ? $filter("date")(filter.startDateTo, "yyyy-MM-dd") : undefined,
            }, { notify: false });

            findTask(vm.filterData);


        }

        function clearFilter() {
            vm.filterData = {};
            vm.filter = {};
            vm.pagination = {
                pageNumber: 1,
                pageSize: 20
            };
            $state.go('app.task', {
                id: undefined,
                title: undefined,
                project: undefined,
                taskstatus: undefined,
                afterHour: undefined,
                executor: undefined,
                department: undefined
            }, { notify: false });
            findTask(vm.filterData);
            //    delete vm.filter.findByTitle 
            //    delete vm.filter.findByExecutor
            //    delete vm.filter.findById
        }
        /**
         * add new user to task
         * @param ev
         * @param task
         */
        function assignUser(ev, task) {
            console.log(task)
            $mdDialog.show({
                controller: 'AssignDialogController',
                controllerAs: 'vm',
                templateUrl: 'app/main/request/dialogs/user/assign-dialog.html',
                // parent: angular.element($document.find('#task')),
                targetEvent: ev,
                clickOutsideToClose: true,
                locals: {
                    Task: task,
                    UserList: UserList,
                    event: ev
                }
            });
        }

        /**
             * findSettings
             * @param {*json} data 
             */
        function findSettings(data) {
            lf.getItem("userTaskSettings", function (err, result) {
                if (result) vm.userTaskSettings = result
                else msApi.request('findSettings@save', data,
                    // SUCCESS
                    function (response) {
                        vm.userTaskSettings = response.output
                        lf.setItem("userTaskSettings", vm.userTaskSettings)
                    }
                );
            })

        }
        /**
        * findSettings
        * @param {*json} data 
        */
        function saveFilter(ev, filter) {
            if (!filter) {
                return;
            }
            // Appending dialog to document.body to cover sidenav in docs app
            var confirm = $mdDialog.prompt()
                .title('Mövcud çeşidləməni yaddaşda saxla')
                //.textContent('Çeşidləmə üçün ad təyin edin')
                .placeholder('Çeşidləmə üçün ad təyin edin')
                .ariaLabel('Filter name')
                .initialValue('My Filter ' + (new Date().getTime()))
                .targetEvent(ev)
                //.required(true)
                .ok('Saxla!')
                .cancel('Ləğv et');

            $mdDialog.show(confirm).then(function (result) {
                msApi.request('addSettings@save', { name: result, type: 1, settings: angular.toJson(filter) },
                    // SUCCESS
                    function (response) {
                        console.log(response)
                        vm.userTaskSettings.push({ name: result, settings: angular.toJson(filter), id: response.output.id });
                        localStorage.offlineUserTaskSettings = angular.toJson({ output: vm.userTaskSettings });
                    },
                    // ERROR
                    function (response) {
                        console.error(response.output)
                    }
                );
            }, function () {
                $scope.status = 'You didn\'t name your dog.';
            });
        }
        function delFilter(ev, set) {
            console.log(set);
            var confirm = $mdDialog.confirm()
                .title('Bu çeşidləməni silmək istədiyinizdən əminsiniz?')
                .htmlContent('<b>#' + set.id + " Adı: " + set.name)
                .ariaLabel('delete settings')
                .targetEvent(ev)
                .ok('Bəli')
                .cancel('Xeyr');

            $mdDialog.show(confirm).then(function () {
                msApi.request('delSettings@save', { id: set.id },
                    // SUCCESS
                    function (response) {
                        vm.userTaskSettings.splice(vm.userTaskSettings.indexOf(set), 1);
                        localStorage.offlineUserTaskSettings = angular.toJson({ output: vm.userTaskSettings });
                    },
                    // ERROR
                    function (response) {
                        console.error(response.output)
                    }
                );
            }, function () {
            });
        }


        /**
         * thisTabSelected
         * @param {event object} ev 
         * @param {object} sourceType 
         */
        function thisTabSelected(ev, sourceType) {
            // msNavigationService.saveItem('filtr', {
            //     title: 'Sorğu mənbəyi',
            //     group: true,
            //     weight: 2
            // });
            vm.selectedTab = sourceType.id;
            vm.filterChange(sourceType.id, sourceType.name, "sourceTypeId");
        }


        function isThisSelected(task) {
            //    console.log(task.id);
            //    console.log(vm.stateParamsId)
            task.id == vm.stateParams.id ? vm.openSidebar(task) : null //todo open this sidebar
            //    console.log(task.id)
            //   console.log(vm.stateParamsId)
        }

        function commentFileSelected(e, reader, file, fileList, fileOjects, fileObj) {
            vm.newComment.isProgress = true;
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
                    vm.newComment.attachment.push(response.output)
                    console.log(vm.newComment);
                    vm.newComment.isProgress = false
                },
                // ERROR
                function (response) {
                    console.error(response.data)
                    vm.newComment.isProgress = false
                }
            );
            // console.log(newFile);
        }


        /**
         * filterChange
         * @param {*} objectIndex 
         * @param {*} objectReadableName 
         */
        function filterChange(objectIndex, objectReadableName, objectName) {
            //vm.requests = [];
            // vm.filterData = {};
            vm.pagination = {
                pageNumber: 1,
                pageSize: 20
            };
            //vm.sourceType;
            // if (objectIndex === -1) {
            //     findRequest(obj);
            // }
            // else {
            // vm.listType = objectReadableName; //todo show all filter's name
            vm.filterData[objectName] = objectIndex;
            findTask(vm.filterData);
            // vm.filterIds = typeIndex.id;
            // }


            vm.selectedRequests = [];

        }

        function setTaskStatus(task, statusId) {
            var taskHistory = {
                MainTaskId: task.id,
                TaskStatusId: statusId
            };
            msApi.request('updateTaskStatusHistory@save', taskHistory,
                // SUCCESS
                function (response) {
                    //requestForm.createdDate = new Date();
                    task.taskStatusId = statusId;
                    findHistory({
                        mainTaskId: task.id,
                        pageNumber: 1,
                        pageSize: 20
                    })
                    //console.log(response.data)
                },
                // ERROR
                function (response) {
                    console.error(response.data)
                }
            );
        }
        /**
         * open side bar info panel
         * @param {object} task 
         */
        function openSidebar(task) {
            vm.startAssignUser = false;
            vm.startAssignTitle = false;
            vm.startAssignNote = false;
            vm.allCommentLoaded = false;
            if ((vm.selectedTask) && vm.selectedTask.id == task.id)
                vm.isOpenSidebar = !vm.isOpenSidebar
            else
                vm.isOpenSidebar = true;
            vm.selectedTask = task;
            vm.selectedTask.history = [];
            // //console.log(vm.selectedTask.rating.senderPoint);
            // //console.log(vm.selectedTask.rating.senderPoint);
            findComment('main', {
                mainTaskId: task.id,
                pageNumber: 1,
                pageSize: 3
            });
            findHistory({
                mainTaskId: task.id,
                pageNumber: 1,
                pageSize: 20
            })
            getRefreshTask(task.id);
            vm.selectedTask.endDate = vm.selectedTask.endDate ? new Date(vm.selectedTask.endDate) : null
            vm.selectedTask.startDate = vm.selectedTask.startDate ? new Date(vm.selectedTask.startDate) : null
        }

        function loadMoreComment(taskId) {
            findComment('main', {
                mainTaskId: taskId
            });
            vm.allCommentLoaded = true;
        }

        function openChildSidebar(task) {

            vm.mainTask = vm.selectedTask;
            vm.selectedChild = task;


            if (vm.selectedChild.id == task.id)
                vm.isOpenChildTask = !vm.isOpenChildTask
            else
                vm.isOpenChildTask = true;

            // for (var key in task) {
            //   vm.selectedChild[key] = task[key];
            // }
            vm.selectedChild.endDate = vm.selectedTask.endDate ? new Date(vm.selectedTask.endDate) : null
            vm.selectedChild.startDate = vm.selectedTask.startDate ? new Date(vm.selectedTask.startDate) : null
        }
        /**
         * 
         */
        function closeSidebar() {
            vm.isOpenSidebar = false;
            closeChildSidebar();
        }
        function closeMenu() {
            $mdMenu.hide();
        }
        function closeChildSidebar() {
            // vm.selectedTask = vm.mainTask;
            //vm.mainTask = null;
            vm.isOpenChildTask = false;
        }


        function selectedItemChange(model, property, value) {
            model[property] = value;
            vm.userList.selectedItem = null;
            vm.userListChild.selectedItem = null;
            vm.userList.searchText = null;
            vm.userListChild.searchText = null;
            model['executorUser'] = null;
            vm.startAssignUser = false;
            inLineEdit(model, property, value);
            //value = null
            // console.log(model);
        }

        function openChildTaskDialog(ev, task) {
            vm.card = [];
            //  thisRequest ? vm.card[thisRequest.id] = true : false;
            $mdDialog.show({
                multiple: true,
                skipHide: true,
                controller: 'RequestTaskDialogController',
                controllerAs: 'vm',
                bindToController: true,
                templateUrl: 'app/main/request/dialogs/task/task-dialog.html',
                targetEvent: ev,
                clickOutsideToClose: true,
                locals: {
                    SourceList: vm.sourceList[1],
                    ThisRequest: {
                        id: null,
                        sourceTypeId: vm.sourceType.sourceTypeId,
                        customerRequestType: {
                            departmentId: null
                        }
                    },
                    RequestTypes: null,
                    ThisTask: task,
                    UserList: userList,
                    Tasks: vm.tasks,
                    InjuryTypes: vm.injuryTypes,
                    Projects: null,
                    AutoComplete: autoComplete,
                    UserDepartments: UserDepartments,
                    OpenProjectDialog: vm.openProjectDialog,
                    event: ev
                }
            });
        }


        function openMultiTaskDialog(ev, task) {
            vm.card = [];
            //  thisRequest ? vm.card[thisRequest.id] = true : false;
            $mdDialog.show({
                multiple: true,
                skipHide: true,
                controller: 'MultiTaskDialogController',
                controllerAs: 'vm',
                bindToController: true,
                templateUrl: 'app/main/task/dialogs/task/task-dialog.html',
                //  parent: angular.element($document.find('#task-container')),
                targetEvent: ev,
                clickOutsideToClose: true,
                locals: {
                    SourceList: vm.sourceList[1],
                    ThisRequest: {
                        id: null,
                        sourceTypeId: vm.sourceType.sourceTypeId,
                        customerRequestType: {
                            departmentId: null
                        }
                    },
                    RequestTypes: null,
                    ThisTask: task,
                    UserList: userList,
                    Tasks: vm.tasks,
                    InjuryTypes: vm.injuryTypes,
                    Projects: Projects,
                    AutoComplete: autoComplete,
                    OpenProjectDialog: vm.openProjectDialog,
                    event: ev
                }
            });
        }

        function openProjectDialog(ev, project) {
            vm.card = [];
            //  thisRequest ? vm.card[thisRequest.id] = true : false;
            $mdDialog.show({
                skipHide: true,
                controller: 'NewProjectDialogController',
                controllerAs: 'vm',
                templateUrl: 'app/main/task/dialogs/project/project-dialog.html',
                //  parent: angular.element($document.find('#task-container')),
                targetEvent: ev,
                clickOutsideToClose: true,
                locals: {
                    Departments: Departments,
                    Projects: Projects,
                    event: ev,
                    Project: project
                }
            });
        }

        function openDepartmentDialog(ev, department) {
            $mdDialog.show({
                controller: 'NewDepartmentDialogController',
                controllerAs: 'vm',
                templateUrl: 'app/main/task/dialogs/department/department-dialog.html',
                //  parent: angular.element($document.find('#task-container')),
                targetEvent: ev,
                clickOutsideToClose: true,
                locals: {
                    Departments: Departments,
                    event: ev,
                    Department: department,
                    Users: UserList
                }
            });
        }

        function deleteThis(ev, thisObj, postUrl) {
            // Appending dialog to document.body to cover sidenav in docs app
            var confirm = $mdDialog.confirm()
                .title('Diqqət')
                .textContent('Siz bu obyekti silmək istədiyinizdən əminsiniz?')
                .ariaLabel('delete object')
                .targetEvent(ev)
                .ok('Bəli, sil')
                .cancel('Xeyr, qalsın');

            $mdDialog.show(confirm).then(function () {
                msApi.request(postUrl, thisObj,
                    // SUCCESS
                    function (response) {
                        // Projects.slice(project)
                    },
                    // ERROR
                    function (response) {
                        console.error(response.data)
                    }
                );
            }, function () {
                //todo
            });
        }

        function findTask(data, append, withInterval) {
            //console.log('data',data);

            if (withInterval == undefined) {
                withInterval = false;
                vm.findIsInProgress = !append;
                vm.prossesing = true;
                data = angular.merge({}, data, vm.pagination, {
                    'ascId': vm.ascId
                });
            } else {
                data = angular.merge({}, vm.filterData, { pageNumber: 1, pageSize: vm.pagination.pageNumber * vm.pagination.pageSize }, {
                    'ascId': vm.ascId
                });
            }
            //console.log(data);

            console.log("start task find request")
            msApi.request('findTask@save', data,
                // SUCCESS
                function (response) {
                    if (withInterval) {
                        vm.tasks = response.output
                        vm.totalTasks = response.totalCount;
                    }
                    else {
                        // var _selectedTask = $filter('filter')(response.output, { id: vm.stateParams.id });
                        // if (_selectedTask.length > 0)
                        //   vm.tasks.splice(vm.tasks.indexOf(_selectedTask[0]), 1)
                        vm.prossesing = false;
                        vm.findIsInProgress = false;
                        vm.loadMoreIsInProgress = false;
                        //requestForm.createdDate = new Date();
                        closeSidebar();
                        if (append)
                            vm.tasks = vm.tasks.concat(response.output);
                        else
                            vm.tasks = response.output

                        vm.totalTasks = response.totalCount;
                    }
                    lf.setItem("tasks", vm.tasks.slice(0, vm.pageSize))
                },
                // ERROR
                function (response) {
                    vm.prossesing = false
                    vm.findIsInProgress = false;
                    vm.loadMoreIsInProgress = false;
                    //  console.error(response.errorList)
                }
            );
        }

        function findComment(property, data) {
            msApi.request('findComment@save', data,
                // SUCCESS
                function (response) {
                    //requestForm.createdDate = new Date();
                    //closeSidebar();
                    vm.taskComments[property] = response.output
                    // console.log(vm.taskComments[property]);
                    //console.log(response.data)
                },
                // ERROR
                function (response) {
                    console.error(response.data)
                }
            );
        }
        function findHistory(data) {
            msApi.request('findHistory@save', data,
                // SUCCESS
                function (response) {
                    console.log(response);
                    vm.selectedTask.history = response.output;
                    //requestForm.createdDate = new Date();
                    //closeSidebar();
                    //vm.taskComments[property] = response.output
                    // console.log(vm.taskComments[property]);
                    //console.log(response.data)
                },
                // ERROR
                function (response) {
                    console.error(response.data)
                }
            );
        }
        function getRefreshTask(id) {
            msApi.request('findTask@save', { id: id },
                // SUCCESS
                function (response) {
                    // console.log(response);
                    // vm.selectedTask = response.output
                    //requestForm.createdDate = new Date();
                    //closeSidebar();
                    //vm.taskComments[property] = response.output
                    // console.log(vm.taskComments[property]);
                    //console.log(response.data)
                },
                // ERROR
                function (response) {
                    console.error(response)
                }
            );
        }
        function loadProjects() {
            msApi.request('Projects@query',
                // SUCCESS
                function (response) {
                    if (response.isSuccess) {
                        Projects = response.output;
                        Projects.forEach(function (element) {
                            vm.projectList[element.id] = element.name;
                        }, this);
                    }
                    //console.log(response.data)
                },
                // ERROR
                function (response) {
                    console.error(response.data)
                }
            );
        }
        function updateTask(model, property, value) {
            vm.prossesing = true;
            if (property == 'executorUserId') { //todo if new user
                model['taskStatusId'] = 5;
            }
            //console.log(value);
            model[property] = value;
            //console.log(task);
            msApi.request('updateTask@save', model,
                // SUCCESS
                function (response) {
                    if (response.isSuccess) {
                        NotificationService.show('Tapşırıq  #' + response.output.id + ' yeniləndi', 'success')
                    } else {
                        response.errorList.forEach(function (element) {
                            NotificationService.show(element.text, 'error')
                        }, this);
                    }
                    //requestForm.createdDate = new Date();
                    vm.prossesing = false
                    vm.selectedTask = response.output;
                    findHistory({
                        mainTaskId: vm.selectedTask.id,
                        pageNumber: 1,
                        pageSize: 20
                    })
                    //console.log(response.data)
                },
                // ERROR
                function (response) {
                    vm.prossesing = false;
                    console.error(response.data)
                }
            );
        }

        function loadMore() {
            vm.loadMoreIsInProgress = true;
            vm.pagination.pageNumber++;
            findTask(vm.filterData, true);
            //console.log('test')
        }

        function addTaskComment(taskId, comment) {
            vm.newComment.mainTaskId = taskId;
            // console.log(vm.newComment);
            // var newComment = { mainTaskId: taskId, content: comment.content };
            // newComment.file = vm.
            //console.log(task);
            msApi.request('addTaskComment@save', vm.newComment,
                // SUCCESS
                function (response) {
                    if (response.isSuccess) {
                        NotificationService.show('Şərh əlavə edildi', 'success');
                        vm.taskComments['main'].unshift(response.output);
                    } else {
                        response.errorList.forEach(function (element) {
                            NotificationService.show(element.text, 'error')
                        }, this);
                    }
                    vm.newComment = {
                        file: []
                    };
                    // if (response.output.mainTask) {
                    //   vm.taskComments['child'].unshift(response.output);
                    // }
                    // else {
                    //   vm.taskComments['main'].unshift(response.output);
                    // }
                    //.createdDate = new Date();
                    //vm.selectedTask = response;
                    console.log(response)
                },
                // ERROR
                function (response) {
                    console.error(response.data)
                }
            );
        }

        function compareDate(first, second) {
            return (new Date(first.toString())).getTime() + 1000 * 60 * 60 * 24 < (new Date(second.toString()).getTime());
        }

        function updateRating(ratingId, value, taskId) {
            if (angular.isNumber(ratingId)) {
                msApi.request('updateRating@save', {
                    id: ratingId,
                    taskPoint: value
                },
                    // SUCCESS
                    function (response) {
                        if (response.isSuccess) {
                            NotificationService.show('success', 'success')
                        } else {
                            response.errorList.forEach(function (element) {
                                NotificationService.show(element.text, 'error')
                            }, this);
                        }
                        //requestForm.createdDate = new Date();
                        // request.requestStatus.id = statusId;
                        console.log(response.output)
                    },
                    // ERROR
                    function (response) {
                        console.error(response)
                    }
                );
            } else {
                msApi.request('addRating@save', {
                    taskId: taskId,
                    taskPoint: value
                },
                    // SUCCESS
                    function (response) {
                        //requestForm.createdDate = new Date();
                        // request.requestStatus.id = statusId;
                        console.log(response)
                    },
                    // ERROR
                    function (response) {
                        console.error(response)
                    }
                );
            }

        }

        function checkAssignUser() {
            return ACLService.check(["assign_user"], authService);
        }
        function checkAssignDeadline() {
            return ACLService.check(["assign_deadline"], authService);
        }
        function isAdmin() {
            return ACLService.check(["Admin"], authService);
        }

        function deleteTask(ev, task) {
            var confirm = $mdDialog.confirm()
                .title('Bu tapşırığı silmək istədiyinizdən əminsiniz?')
                .htmlContent('<b>#' + task.id + " Mövzu:" + task.description + '</b>' + ' silinəcək.')
                .ariaLabel('delete task')
                .targetEvent(ev)
                .ok('Bəli')
                .cancel('Xeyr');

            $mdDialog.show(confirm).then(function () {
                msApi.request('delTask@save', { id: task.id },
                    // SUCCESS
                    function (response) {
                        vm.tasks.splice(vm.tasks.indexOf(task), 1);
                    },
                    // ERROR
                    function (response) {
                        console.error(response.output)
                    }
                );
            }, function () {
            });

        }

        function excelExport(data, append, withInterval) {

            if (withInterval == undefined) {
                withInterval = false;
                vm.findIsInProgress = !append;
                vm.prossesing = true;
                data = angular.merge({}, data, { pageNumber: 0, pageSize: 0 }, {
                    'ascId': vm.ascId
                });
            } else {
                data = angular.merge({}, vm.filterData, { pageNumber: 0, pageSize: 0 }, {
                    'ascId': vm.ascId
                });
            }
            ////test http method;

            //$http({
            //    method: "GET",
            //    url: "MainTask/findall",

            //}).then(function (respons) {
            //    console.log(respons)
            //})


            console.log("start task find request")
            msApi.request('findTask@save', data,
                // SUCCESS
                function (response) {
                    if (withInterval) {
                        var a = response.output
                        vm.totalTasks = response.totalCount;
                    }
                    else {
                        // var _selectedTask = $filter('filter')(response.output, { id: vm.stateParams.id });
                        // if (_selectedTask.length > 0)
                        //   vm.tasks.splice(vm.tasks.indexOf(_selectedTask[0]), 1)
                        vm.prossesing = false;
                        vm.findIsInProgress = false;
                        vm.loadMoreIsInProgress = false;
                        //requestForm.createdDate = new Date();
                        closeSidebar();
                        if (append)
                            var a = response.output;
                        else
                            var a = response.output

                        vm.totalTasks = response.totalCount;
                    }
                    console.log(response.output);

                    var TaskExcelArray = [];
                    console.log(response.output)
                    $.each(response.output, function (i, item) {
                        var _executorUser = "Qeyd olunmayib";
                        var createdUser = "Qeyd olunmayib";
                        var status = "Qeyd olunmayib";
                        var tarix = TaskCustomDateParse(item.createdDate);
                        var Departament = "Qeyd Olunmayib";
                        var NowDateTime = Date.now()
                        if (item.executorUser != null) {
                            _executorUser = item.executorUser.firstName + " " + item.executorUser.lastName
                        }
                        if (item.generatedUser != null) {
                            createdUser = item.generatedUser.firstName + " " + item.generatedUser.lastName
                        }
                        if (item.taskStatus != null) {
                            status = item.taskStatus.name;
                            if (item.taskStatus.createdDate != null) {
                                tarix = TaskCustomDateParse(item.taskStatus.createdDate);
                            }
                        }
                        if (item.department != null) {
                            Departament = item.department.name
                        }
                        var excelData =
                        {
                            Müraciət: item.customerRequestId,
                            Tapşırıq: item.id,
                            Başlıq: item.description,
                            Qeyd: item.note,
                            Tapşırığı_açan: createdUser,
                            Açılma_tarixi: TaskCustomDateParse(item.createdDate),
                            İcracı: _executorUser,
                            İcraci_Bölmə: Departament,
                            Status: status,
                            Status_tarixi: TaskCustomDateParse(item.endDate),
                            //İcra_Müddəti: TaskPeriodOfPerformance(item.createdDate, NowDateTime) + "-saat"

                        }
                        TaskExcelArray.push(excelData);
                    });


                    // lf.setItem("tasks", vm.tasks.slice(0, vm.pageSize));

                    var ws = XLSX.utils.json_to_sheet(TaskExcelArray);
                    /* add to workbook */
                    var wb = XLSX.utils.book_new();
                    XLSX.utils.book_append_sheet(wb, ws, "Sheet1");
                    /* write workbook and force a download */
                    XLSX.writeFile(wb, "export.xlsx");
                },

                // ERROR
                function (response) {
                    vm.prossesing = false
                    vm.findIsInProgress = false;
                    vm.loadMoreIsInProgress = false;

                    console.error(response.errorList)
                }
            );

        }




        function TaskCustomDateParse(date) {
            if (date != null) {
                var indexT = date.indexOf("T");
                if (indexT > 0) {
                    date = date.replace("T", " ");
                    date = date.substring(0, 16)
                }
            }
            return date;
        }

        function TaskPeriodOfPerformance(createDate, endDate) {
            if (createDate != null && endDate != null) {
                createDate = new Date(createDate).getTime();
                endDate = new Date(endDate).getTime();
                var milliseconds = endDate - createDate;
                var hours = Math.floor(milliseconds / (60 * 60 * 1000));

                if (hours > 0) {
                    return hours;
                }
                else {
                    return "İcraya Baslanilmayib";
                }
            }
            else {
                return "Baslangic Veya son Tarix Qeyd Olunmayib";
            }
        }
    }
})();
