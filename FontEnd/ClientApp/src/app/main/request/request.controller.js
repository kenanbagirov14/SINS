(function () {
    'use strict';

    angular
        .module('app.request')
        .controller('RequestController', RequestController);

    /** @ngInject */
    function RequestController($scope,
        $mdSidenav,
        $mdMenu,
        // Requests,
        // RequestTypes,
        // DepartmentList,
        // UserDepartments,
        // RegionList,
        // SourceTypes,
        // RequestStatusList,
        // TaskStatusList,
        // InjuryTypes,
        // UserList,
        msUtils,
        $mdDialog,
        $document,
        $stateParams,
        $state,
        $rootScope,
        $q,
        $timeout,
        $filter,
        $interval,
        $window,
        msHub,
        msApi,
        msNavigationService,
        authService,
        MainService,
        ACLService,
        NotificationService,
        lf
    ) {


        var vm = this,
            UserDepartments;
        $timeout.cancel($window.__env.timeout);
        vm.hub = msHub.hub();
        vm.requests = [];//Requests;// []; //todo rewrite to array
        //debugger;

        if (!ACLService.check(['routeRequest'], authService)) {
            //$state.go('app.pages_error_unauthorize');
            location.assign('/pages/error/unauthorize')
            return;
        }
        lf.getItem("DepartmentList", function (err, data) {
            if (data) fullFillDepartmentList(data)
            else {
                msApi.request('Departments@query',
                    function (response) {
                        fullFillDepartmentList(response.output);
                        lf.setItem("DepartmentList", response.output)
                    }
                )
            }
        }),
            lf.getItem("UserDepartments", function (err, data) {
                if (data) UserDepartments = data
                else {
                    UserDepartments = [];
                    msApi.request('UserDepartments@save', {},
                        function (response) {
                            UserDepartments = response.output;
                            lf.setItem("UserDepartments", UserDepartments)
                        }
                    )
                }
            }),
            lf.getItem("RegionList", function (err, data) {
                if (data) fullFillRegionList(data)
                else {
                    msApi.request('Regions@query',
                        function (response) {
                            fullFillRegionList(response.output)
                            lf.setItem("RegionList", response.output)
                        }
                    )
                }
            }),
            vm.sourceTypes = [null, null, null, null, null, null];
        lf.getItem("SourceTypess", function (err, data) {
            if (data) vm.sourceTypes = data;
            else {
                msApi.request('sourceTypes@query',
                    function (response) {
                        response.output.map(function (e) {
                            vm.sourceTypes[e.id] = e;
                        });
                        console.log(vm.sourceTypes)
                        lf.setItem("SourceTypes", vm.sourceTypes)
                    }
                )
            }
        }),
            lf.getItem("RequestStatusList", function (err, data) {
                if (data) fullFillRequestStatusList(data)
                else {

                    msApi.request('RequestStatusList@query',
                        function (response) {
                            fullFillRequestStatusList(response.output)
                            lf.setItem("RequestStatusList", response.output)
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
            }),
            lf.getItem("InjuryTypes", function (err, data) {
                if (data) fullFillInjuryTypes(data)
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
                            fullFillUserList(response.output)
                            lf.setItem("UserList", response.output)
                        }
                    )
                }
            })

        //#region variables list
        vm.hasAccess = hasAccess;
        vm.authData = authService.authentication;
        vm.filterKey = ""
        vm.sourceTypeName = $state.current.name;
        vm.stateParams = $stateParams
        msNavigationService.clearNavigation();
        // Data 

        vm.requestTypes = [];//RequestTypes;
        vm.sourceType = {};
        vm.requestStatusList = {};
        vm.taskStatusList = {};
        vm.userList = [];
        vm.injuryTypesList = [];
        //vm.requestStatusList = RequestStatusList;
        //vm.requests.concat(vm.requests);
        vm.filterIds = null;
        vm.listType = "Bütün sorğular";
        vm.listOrder = 'name';
        vm.listOrderAsc = false;
        vm.selectedRequests = [];
        vm.selectedTabName = "Müraciət növü";
        // vm.newGroupName = '';
        vm.card = [];
        vm.isDetailsOpened = true;
        vm.statusFilter = null;

        vm.findIsInProgress = false;
        // Methods
        vm.filterChange = filterChange;
        vm.openRequestDialog = openRequestDialog;
        // vm.openInternalRequestDialog = openInternalRequestDialog;
        vm.openTaskDialog = openTaskDialog;
        vm.openEmailDialog = openEmailDialog;
        vm.openLawProcessDialog = openLawProcessDialog;
        vm.openRequestInfoDialog = openRequestInfoDialog;
        vm.openRequestReplyDialog = openRequestReplyDialog;
        // vm.deleteContactConfirm = deleteContactConfirm;
        // vm.deleteContact = deleteContact;
        //  vm.deleteSelectedContacts = deleteSelectedContacts;
        vm.toggleSelectRequest = toggleSelectRequest;
        //   vm.deselectContacts = deselectContacts;
        vm.selectAllContacts = selectAllContacts;
        //  vm.deleteContact = deleteContact;
        //  vm.addNewGroup = addNewGroup;
        //  vm.deleteGroup = deleteGroup;
        vm.toggleSidenav = toggleSidenav;
        vm.toggleInArray = msUtils.toggleInArray;

        vm.exists = msUtils.exists;
        vm.showCard = showCard;
        vm.assignUser = assignUser;
        vm.updateRating = updateRating;
        vm.deleteRequest = deleteRequest
        vm.selectChilds = selectChilds
        vm.findSettings = findSettings;
        vm.saveFilter = saveFilter;
        vm.delFilter = delFilter;
        vm.userRequestSettings = {}
        // vm.setRequestStatus = setRequestStatus;
        vm.thisTabSelected = thisTabSelected;
        vm.exportToExcel = exportToExcel;



        vm.excelExport = excelExport;
        vm.excelExport1 = excelExport1;
        vm.loadMore = loadMore;
        vm.closeMenu = closeMenu;
        vm.filterData = {};
        vm.pagination = { pageNumber: 1, pageSize: 20 };
        vm.totalRequests = 0;
        //vm.statusFilterChange = statusFilterChange;
        vm.findIndex = findIndex;
        vm.filterService = filterService
        vm.clearFilter = clearFilter
        vm.isDepartmentChildShown = [];
        vm.isRequestTypeChildShown = [];
        var autoComplete = {};

        var departmentList = {}
        vm.filterData = {
            id: vm.stateParams.id,
            startDateFrom: new Date(vm.stateParams.start),
            startDateTo: new Date(vm.stateParams.end),
            requestStatusId: vm.stateParams.requeststatus ? vm.stateParams.requeststatus.split(",") : undefined,
            department: vm.stateParams.department ? vm.stateParams.department.split(",") : undefined,
            requestTypeId: vm.stateParams.requesttype ? vm.stateParams.requesttype.split(",") : undefined
        }

        switch (vm.sourceTypeName) {
            case 'app.request': vm.sourceTypeId = 1; break;
            case 'app.law': vm.sourceTypeId = 3; break;
            case 'app.clerical': vm.sourceTypeId = 4; break;
            default: vm.sourceTypeId = 2; break;
        }
        lf.getItem("requests_" + vm.sourceTypeId, function (err, data) {
            var sourceType = { id: vm.sourceTypeId, name: "" }
            if (data) {
                vm.requests = data
                vm.findIsInProgress = false
                $window.__env.timeout = $timeout(findRequest, 15 * 1000, 0, true, vm.filterData, true, true)
            } else {
                vm.findIsInProgress = true;
                filterChange(sourceType.id, sourceType.name, "sourceTypeId");
            }

        })

        if (vm.sourceTypeName == 'app.request') {
            $rootScope.pageTitle = "Daxili Müraciətlər";
            vm.sourceType.title = "daxili müraciət"
            vm.selectedTab = 1;
            vm.filterKey = 'departmentId';
        }
        else if (vm.sourceTypeName == 'app.law') {
            $rootScope.pageTitle = "Məhkəmə işləri";
            vm.sourceType.title = "məhkəmə işi"
            vm.selectedTab = 3;
            vm.filterKey = 'regionId';
        }
        else if (vm.sourceTypeName == 'app.clerical') {
            $rootScope.pageTitle = "Kargüzarlıq işləri";
            vm.sourceType.title = "kargüzarlıq işi"
            vm.selectedTab = 4;
            vm.filterKey = 'departmentId';
        }
        else {
            $rootScope.pageTitle = "Abonent Müraciətlər";
            vm.sourceType.title = "abonent müraciəti"
            vm.selectedTab = 2;

            vm.filterKey = 'regionId';
        }

        vm.showDepartmentChild = showDepartmentChild;
        vm.showRequestTypeChild = showRequestTypeChild;
        $rootScope.activePage = vm.sourceTypeName;

        thisTabSelected(null, { id: vm.sourceTypeId, name: "" });

        //#endregion

        function fullFillTaskStatusList(data) {
            data.forEach(function (element) {
                // console.log(element);
                vm.taskStatusList[element.id] = element.name;
            }, this);
        }
        function fullFillRequestStatusList(data) {
            vm.statusTypes = data;
            data.forEach(function (element) {
                vm.requestStatusList[element.id] = element.name;
            }, this);
        }

        function fullFillRegionList(data) {
            if (vm.sourceTypeName == 'app.callcenter') {
                vm.sourceList = { 2: { list: data, from: "regionId" } };
                fullFillAutoComplete(data)
            }

        }
        function fullFillDepartmentList(data) {
            if (vm.sourceTypeName != 'app.callcenter') {
                vm.sourceList = {
                    1: { list: data, from: "departmentId" },
                    3: { list: data, from: "departmentId" },
                    4: { list: data, from: "departmentId" }
                };
                fullFillAutoComplete(data)
            }

        }
        function fullFillAutoComplete(data) {
            autoComplete.departmentList = MainService.init(departmentList, 'departments', data);
            vm.departmentListSearch = autoComplete.departmentList;
        }

        function fullFillUserList(data) {
            var userList = {}
            data.forEach(function (user) {
                vm.userList[user.id] = user.userName;
            }, this);
            autoComplete.userList = MainService.init(userList, 'users', data);
            userList = {}
        }

        function fullFillInjuryTypes(data) {
            vm.injuryTypes = data;
            data.forEach(function (injury) {
                vm.injuryTypesList[injury.id] = injury.name;
            }, this);
        }

        function hasAccess(role) {
            return ACLService.check([role], authService)
        }


        function findIndex(element, index, array, id) {
            // console.log(element)
            // console.log(id)
            return element.id == id;
        }



        function showDepartmentChild(event, id) {
            vm.isDepartmentChildShown[id] = !(vm.isDepartmentChildShown[id] | false);
            return false;
        }

        function selectChilds(depId, deps) {
            if (vm.filter.departmentIds[depId]) {
                angular.forEach(deps, function (element) {
                    vm.filter.departmentIds[element.id] = true;
                });
            } else {
                angular.forEach(deps, function (element) {
                    vm.filter.departmentIds[element.id] = false;
                });
            }
        }
        function showRequestTypeChild(event, id) {
            vm.isRequestTypeChildShown[id] = !(vm.isRequestTypeChildShown[id] | false);
            return false;
        }

        /**
         * Change Contacts List Filter
         * @param type
         */

        vm.goToTask = function (taskId) {
            $state.go('app.task', { id: taskId });
        }

        /**
         * thisTabSelected
         * @param {event object} ev 
         * @param {object} sourceType 
         */
        function thisTabSelected(ev, sourceType) {
            vm.findIsInProgress = true;
            vm.requests = []; //todo uncomment
            findRequestType({ sourceTypeId: sourceType.id })


            vm.selectedTab = sourceType.id;
            vm.selectedTabName = sourceType.name;

            findSettings({ userId: vm.authData.userId, type: 2 })
        }




        //excellllll

        //excelll end



        function filterService(filter, parse) {
            if (parse != undefined) filter = angular.fromJson(filter);
            //console.log(filter);
            filter.requestStatusId = [];
            angular.forEach(filter.requestStatusIds, function (value, key) {
                if (value) this.push(Number(key));
            }, filter.requestStatusId);
            // console.log(filter);
            if (filter.requestStatusId.length < 1) delete filter.requestStatusId;
            //delete filter.requestStatusIds;

            filter.customerRequestTypeId = [];
            angular.forEach(filter.requestTypeIds, function (value, key) {
                if (value) this.push(Number(key));
            }, filter.customerRequestTypeId);
            // console.log(filter);
            if (filter.customerRequestTypeId.length < 1) delete filter.customerRequestTypeId;
            // delete filter.requestTypeIds;


            filter[vm.filterKey] = [];
            angular.forEach(filter.departmentIds, function (value, key) {
                if (value) this.push(Number(key));
            }, filter[vm.filterKey]);
            // console.log(filter);
            if (filter[vm.filterKey].length < 1) delete filter[vm.filterKey];
            // delete filter.requestTypeIds;


            //if (filter.startDateFrom) vm.filterData.startDateFrom = moment(filter.startDateFrom).format();
            // if (filter.startDateTo) vm.filterData.startDateTo = moment(filter.startDateTo).startOf('day').add(23, 'hours').add(59, 'minutes').utcOffset(4).toDate();
            if (filter.startDateTo) vm.filterData.startDateTo = filter.startDateTo
            localStorage.setItem("vmFilter", angular.toJson(vm.filterData));
            vm.filterData = filter;

            if (filter.startDateFrom) vm.filterData.startDateFrom = moment(filter.startDateFrom).format();
            if (filter.startDateTo) vm.filterData.startDateTo = new Date(moment(filter.startDateTo).toDate().getTime() + 1 * 24 * 59 * 60 * 1000);

            $state.go(vm.sourceTypeName, {
                requesttype: filter.customerRequestTypeId ? filter.customerRequestTypeId.join(",") : undefined,
                requeststatus: filter.requestStatusId ? filter.requestStatusId.join(",") : undefined,
                department: filter.departmentId ? filter.departmentId.join(",") : undefined,
                start: filter.startDateFrom ? $filter("date")(filter.startDateFrom, "yyyy-MM-dd") : undefined,
                end: filter.startDateTo ? $filter("date")(filter.startDateTo, "yyyy-MM-dd") : undefined
            }, { notify: false });

            findRequest(vm.filterData, false);
            // vm.loadMoreIsInProgress = true;
            // vm.pagination.pageNumber = 1;
            // findTask(vm.filterData);

        }

        function clearFilter() {
            vm.filterData = {};
            vm.filter = {};
            vm.pagination = {
                pageNumber: 1,
                pageSize: 20
            };
            $state.go(vm.sourceTypeName, {
                id: undefined,
                requesttype: undefined,
                requeststatus: undefined,
                department: undefined,
                start: undefined,
                end: undefined
            }, { notify: false });

            findRequest(vm.filterData, false);
            //    delete vm.filter.findByTitle 
            //    delete vm.filter.findByExecutor
            //    delete vm.filter.findById
        }

        function findRequestType(data) {
            lf.getItem("RequestTypes_" + data.sourceTypeId, function (err, result) {
                if (result) vm.requestTypes = result
                else msApi.request('requestTypes@save', data,
                    function (response) {
                        vm.requestTypes = response.output;
                        lf.setItem("RequestTypes_" + data.sourceTypeId, vm.requestTypes)
                    }
                )
            })

        }
        /**
         * filterChange
         * @param {*} objectIndex 
         * @param {*} objectReadableName 
         */
        function filterChange(objectIndex, objectReadableName, objectName) {
            vm.pagination = { pageNumber: 1, pageSize: 20 };
            //vm.requests = [];
            console.log("start");
            vm.filterData = vm.sourceType;
            if (objectIndex === -1) {
                findRequest({});
            }
            else {
                vm.listType = objectReadableName; //todo show all filter's name
                vm.filterData[objectName] = objectIndex;
                findRequest(vm.filterData);
                // vm.filterIds = typeIndex.id;
            }
            vm.selectedRequests = [];
        }
        /**
         * statusFilterChange
         * @param {*} requestTypeIndex 
         * @param {*} requestTypeName 
         */
        // function statusFilterChange(statusIndex, statusName) {
        //     vm.listType = statusName
        //     vm.statusFilter = statusIndex;
        //     // vm.filterIds = typeIndex.id;  
        //     vm.selectedRequests = [];

        // }
        /**
         * add new user to task
         * @param ev
         * @param task
         */
        function assignUser(ev, task) {
            $mdDialog.show({
                controller: 'AssignDialogController',
                controllerAs: 'vm',
                templateUrl: 'app/main/request/dialogs/user/assign-dialog.html',
                parent: angular.element($document.find('#request')),
                targetEvent: ev,
                clickOutsideToClose: true,
                locals: {
                    Task: task,
                    UserList: vm.userList,
                    event: ev
                }
            });
        }


        // console.log( vm.departmentListSearch);
        // ******************************
        // Internal methods
        // ******************************

        /**
         * Open new request dialog
         *
         * @param ev
         * @param contact
         */
        function openRequestDialog(ev, sourceType, caller, thisRequest) {
            console.log(sourceType);

            vm.selectedTab = sourceType.id;
            if (!caller) caller = { number: null }
            if (!thisRequest) thisRequest = {
                'startDate': new Date(),
                'startDateTimeStamp': new Date().getTime(),
                'sourceTypeId': sourceType.id,
                'customerNumber': parseInt(caller.number)
            };
            $mdDialog.show({
                controller: 'RequestDialogController',
                controllerAs: 'vm',
                templateUrl: 'app/main/request/dialogs/request/request-dialog.html',
                //  parent: angular.element($document.find('#request-container')),
                targetEvent: ev,
                clickOutsideToClose: true,
                locals: {
                    SourceList: vm.sourceList[sourceType.id],
                    RequestTypes: vm.requestTypes,
                    Requests: vm.requests,
                    SourceType: sourceType,
                    ThisRequest: thisRequest,
                    Caller: caller,
                    AutoComplete: autoComplete,
                    OpenTaskDialog: openTaskDialog,
                    UserDepartments: UserDepartments,
                    AuthData: vm.authData,
                    event: ev
                }
            });
        }

        function openEmailDialog(ev, thisRequest) {

            if (!thisRequest) thisRequest = {
                'startDate': new Date(),
                'startDateTimeStamp': new Date().getTime(),
                'sourceTypeId': vm.sourceType.id
                //'customerNumber': parseInt(caller.number)
            };
            $mdDialog.show({
                controller: 'RequestEmailController',
                controllerAs: 'vm',
                templateUrl: 'app/main/request/dialogs/request-email/request-email.html',
                //  parent: angular.element($document.find('#request-container')),
                targetEvent: ev,
                clickOutsideToClose: true,
                locals: {
                    ThisRequest: thisRequest,
                    event: ev
                }
            });
        }
        function openLawProcessDialog(ev, thisRequest) {
            console.log(ev);
            $mdDialog.show({
                controller: 'RequestLawProcessDialogController',
                controllerAs: 'vm',
                templateUrl: 'app/main/request/dialogs/request-law-process/request-law-process.html',
                targetEvent: ev,
                clickOutsideToClose: true,
                locals: {
                    SourceList: vm.sourceList[vm.selectedTab],
                    RequestTypes: vm.requestTypes,
                    Requests: vm.requests,
                    //SourceType: sourceType,
                    RequestStatusList: vm.requestStatusList,
                    ThisRequest: thisRequest,
                    AuthData: vm.authData,
                    //    NotificationService:NotificationService,
                    event: ev
                }
            });
        }



        if (!vm.hub.methods.hasOwnProperty('remoteopenrequestdialog')) {
            vm.hub.on('remoteopenrequestdialog', function (response) {
                console.log(response)
                openRequestDialog(null, { id: response.Local, name: "Local" }, { number: response.CallerNumber });
                //console.log(response);
            });
        }

        if (!vm.hub.methods.hasOwnProperty('remoterequestupdate')) {
            vm.hub.on('remoterequestupdate', function (response) {
                try {
                    // console.log(response)
                    $rootScope.$evalAsync(function () {
                        var oldRequest = $filter('filter')(vm.requests, { id: response.newData.id })[0];
                        console.log(oldRequest);
                        vm.requests[vm.requests.indexOf(oldRequest)] = response.newData;
                    })
                    NotificationService.show(response.newData.id + " nömrəli müraciət yeniləndi", 'success')
                    //openRequestDialog(null, { id: response.Local, name: "Local" }, { number: response.CallerNumber });
                    //console.log(response);
                } catch (err) {
                    console.log(response)
                    console.log(err)
                }
            });
        }
        if (!vm.hub.methods.hasOwnProperty('remoterequestadd')) {
            vm.hub.on('remoterequestadd', function (response) {
                try {
                    console.log('remoterequestadd', response);
                    // var _requestExist = $filter('filter')(vm.requests, { id: response.newData.id });
                    // if (_requestExist.length < 1) {
                    if (response.newData.createdUserId != vm.authData.userId) {
                        if (response.newData.sourceTypeId == vm.sourceType.id) {
                            $rootScope.$evalAsync(function () {
                                vm.requests.unshift(response.newData);
                            })
                        }

                        NotificationService.show(response.newData.id + " nömrəli " + vm.sourceType.title + " əlavə edildi", 'success')
                    }
                }
                catch (err) {
                    console.log(response)
                    console.log(err)
                }
                //openRequestDialog(null, { id: response.Local, name: "Local" }, { number: response.CallerNumber });
                //console.log(response);
            });
        }


        /**
         * Open new request dialog
         *
         * @param ev
         * @param contact
         */
        function openTaskDialog(ev, thisRequest) {
            var a;
            console.log("=======================================================================")
            console.log(thisRequest);
            console.log("=======================================================================")
            vm.card = [];
            thisRequest ? vm.card[thisRequest.id] = true : false;
           
            $mdDialog.show({
                controller: 'RequestTaskDialogController',
                controllerAs: 'vm',
                templateUrl: 'app/main/request/dialogs/task/task-dialog.html',
                //  parent: angular.element($document.find('#task-container')),
                targetEvent: ev,
                clickOutsideToClose: true,
                locals: {
                    SourceList: vm.sourceList[vm.sourceTypeId],
                    ThisRequest: thisRequest,
                    RequestTypes: vm.requestTypes,
                    InjuryTypes: vm.injuryTypes,
                    ThisTask: {},
                    AutoComplete: autoComplete,
                    UserDepartments: UserDepartments,
                    Projects: null,
                    OpenProjectDialog: openProjectDialog,
                    event: ev
                }
              
            });
            //console.log("Show Diolog=======================================================================")
            //console.log(locals);
            //console.log("Show Diolog=======================================================================")

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

        function openRequestInfoDialog(ev, thisRequest) {
            // vm.selectedTab = sourceType.id;
            var caller = { number: null }
            if (!thisRequest) thisRequest = {
                'startDate': new Date(),
                'startDateTimeStamp': new Date().getTime(),
                'sourceTypeId': vm.selectedTab,
                'customerNumber': parseInt(caller.number)
            };
            $mdDialog.show({
                controller: 'RequestInfoDialogController',
                controllerAs: 'vm',
                templateUrl: 'app/main/request/dialogs/request-info/info-dialog.html',
                //  parent: angular.element($document.find('#request-container')),
                targetEvent: ev,
                clickOutsideToClose: true,
                locals: {
                    SourceList: vm.sourceList[vm.selectedTab],
                    RequestTypes: vm.requestTypes,
                    Requests: vm.requests,
                    //SourceType: sourceType,
                    RequestStatusList: vm.requestStatusList,
                    ThisRequest: thisRequest,
                    Caller: caller,
                    AuthData: vm.authData,
                    //    NotificationService:NotificationService,
                    event: ev
                }
            });
        }

        function openRequestReplyDialog(ev, thisRequest) {
            // vm.selectedTab = sourceType.id;
            var caller = { number: null }
            if (!thisRequest) thisRequest = {
                'startDate': new Date(),
                'startDateTimeStamp': new Date().getTime(),
                'sourceTypeId': vm.selectedTab,
                'customerNumber': parseInt(caller.number)
            };
            $mdDialog.show({
                controller: 'RequestReplyDialogController',
                controllerAs: 'vm',
                templateUrl: 'app/main/request/dialogs/request-reply/request-reply.html',
                //  parent: angular.element($document.find('#request-container')),
                targetEvent: ev,
                clickOutsideToClose: true,
                locals: {
                    SourceList: vm.sourceList[vm.selectedTab],
                    RequestTypes: vm.requestTypes,
                    Requests: vm.requests,
                    //SourceType: sourceType,
                    RequestStatusList: vm.requestStatusList,
                    ThisRequest: thisRequest,
                    Caller: caller,
                    AuthData: vm.authData,
                    //    NotificationService:NotificationService,
                    event: ev
                }
            });
        }

        function deleteRequest(ev, request) {
            var confirm = $mdDialog.confirm()
                .title('Bu müraciəti silmək istədiyinizdən əminsiniz?')
                .htmlContent('<b>#' + request.id + " Mövzu: " + request.text + '<br/> ' + request.description + '</b>' + ' silinəcək.')
                .ariaLabel('delete request')
                .targetEvent(ev)
                .ok('Bəli')
                .cancel('Xeyr');

            $mdDialog.show(confirm).then(function () {
                msApi.request('delRequest@save', { id: request.id },
                    // SUCCESS
                    function (response) {
                        vm.requests.splice(vm.requests.indexOf(request), 1);
                    },
                    // ERROR
                    function (response) {
                        console.error(response.output)
                    }
                );
            }, function () {
            });
        }

        // /**
        //  * Delete Contact Confirm Dialog
        //  */
        // function deleteContactConfirm(contact, ev) {
        //     var confirm = $mdDialog.confirm()
        //         .title('Are you sure want to delete the contact?')
        //         .htmlContent('<b>' + contact.name + ' ' + contact.lastName + '</b>' + ' will be deleted.')
        //         .ariaLabel('delete contact')
        //         .targetEvent(ev)
        //         .ok('OK')
        //         .cancel('CANCEL');

        //     $mdDialog.show(confirm).then(function () {

        //         deleteContact(contact);
        //         vm.selectedRequests = [];

        //     }, function () {

        //     });
        // }

        // /**
        //  * Delete Contact
        //  */
        // function deleteContact(request) {
        //     vm.requests.splice(vm.requests.indexOf(request), 1);
        // }

        // /**
        //  * Delete Selected Contacts
        //  */
        // function deleteSelectedContacts(ev) {
        //     var confirm = $mdDialog.confirm()
        //         .title('Are you sure want to delete the selected contacts?')
        //         .htmlContent('<b>' + vm.selectedRequests.length + ' selected</b>' + ' will be deleted.')
        //         .ariaLabel('delete contacts')
        //         .targetEvent(ev)
        //         .ok('OK')
        //         .cancel('CANCEL');

        //     $mdDialog.show(confirm).then(function () {

        //         vm.selectedRequests.forEach(function (contact) {
        //             deleteContact(contact);
        //         });

        //         vm.selectedRequests = [];

        //     });

        // }

        /**
         * Toggle selected status of the contact
         *
         * @param contact
         * @param event
         */
        function toggleSelectRequest(request, event) {

            if (event) {
                event.stopPropagation();
            }

            if (vm.selectedRequests.indexOf(request) > -1) {
                vm.selectedRequests.splice(vm.selectedRequests.indexOf(request), 1);
            }
            else {
                vm.selectedRequests.push(request);
            }
        }

        /**
         * Deselect contacts
         */
        function deselectContacts() {
            vm.selectedRequests = [];
        }

        /**
         * Sselect all contacts
         */
        function selectAllContacts() {
            vm.selectedRequests = $scope.filteredRequests;
        }

        /**
         *
         */
        function addNewGroup() {
            if (vm.newGroupName === '') {
                return;
            }

            var newGroup = {
                'id': msUtils.guidGenerator(),
                'name': vm.newGroupName,
                'contactIds': []
            };

            vm.user.groups.push(newGroup);
            vm.newGroupName = '';
        }

        /**
         * Delete Group
         */
        function deleteGroup(ev) {
            var group = vm.listType;

            var confirm = $mdDialog.confirm()
                .title('Are you sure want to delete the group?')
                .htmlContent('<b>' + group.name + '</b>' + ' will be deleted.')
                .ariaLabel('delete group')
                .targetEvent(ev)
                .ok('OK')
                .cancel('CANCEL');

            $mdDialog.show(confirm).then(function () {

                //vm.user.groups.splice(vm.user.groups.indexOf(group), 1);

                // filterChange('all');
            });

        }

        /**
         * Toggle sidenav
         *
         * @param sidenavId
         */
        function toggleSidenav(sidenavId) {
            $mdSidenav(sidenavId).toggle();
        }

        function showCard(cardId) {
            var thisStatus = vm.card[cardId];
            vm.card = [];
            vm.card[cardId] = !thisStatus;
        }

        function loadMore() {
            vm.loadMoreIsInProgress = true;
            vm.pagination.pageNumber++;
            findRequest(vm.filterData, true);
            //console.log('test')
        }

        /**
        * Close dialog
        */
        function closeDialog() {
            $mdDialog.hide();
        }
        function closeMenu() {
            $mdMenu.hide();
        }

        $window.__env.intervals.forEach(function (element) {
            $interval.cancel(element);
        });
        $window.__env.intervals.push($interval(findRequest, 299 * 1000, 0, true, vm.filterData, true, true));


        /**
         * findRequest
         * @param {*json} data 
         */
        function findRequest(data, append, withInterval) {
          //  console.log('timeouthappened')
            if (withInterval == undefined) withInterval = false;
            //  var offlineDepartments = angular.fromJson(localStorage.offlineDepartments || "{}");
            if (!withInterval) {
                data = angular.merge({}, data, vm.pagination);
            } else {
                data = angular.merge({}, vm.filterData, { pageNumber: 1, pageSize: vm.pagination.pageNumber * vm.pagination.pageSize });
            }
            data.sourceTypeId = vm.sourceTypeId;
            vm.findIsInProgress = !append;
            // console.log(data);

            msApi.request('findRequests@save', data,
                // SUCCESS
                function (response) {
                    if (withInterval) {
                        vm.requests = response.output;
                    } else {
                        // console.log(response.length)
                        //requestForm.createdDate = new Date();
                        vm.findIsInProgress = false;
                        vm.loadMoreIsInProgress = false;
                        if (append)
                            vm.requests = vm.requests.concat(response.output);
                        else
                            vm.requests = response.output; //todo uncomment
                        //console.log(response.data)
                        vm.totalRequests = response.totalCount;
                        if (response.output.length < 10)
                            vm.endOfResponse = true;
                        else
                            vm.endOfResponse = false;

                        if (vm.stateParams.id) vm.openRequestInfoDialog({}, $filter('filter')(vm.requests, { id: vm.stateParams.id })[0])
                    }
                    console.log(response.output);
                    lf.setItem("requests_" + vm.sourceTypeId, vm.requests.slice(0, vm.pageSize))
                },
                // ERROR
                function (response) {
                    console.error(response.output)
                }
            );
        }


        /**
         * findSettings
         * @param {*json} data 
         */
        function findSettings(data) {
            lf.getItem("userRequestSettings", function (err, result) {
                if (result) vm.userRequestSettings = result
                else msApi.request('findSettings@save', data,
                    // SUCCESS
                    function (response) {
                        vm.userRequestSettings = response.output
                        lf.setItem("userRequestSettings", vm.userRequestSettings)
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
                msApi.request('addSettings@save', { name: result, type: 2, settings: angular.toJson(filter) },
                    // SUCCESS
                    function (response) {
                        console.log(response)
                        vm.userRequestSettings.push({ name: result, settings: angular.toJson(filter), id: response.output.id });
                        localStorage.offlineUserRequestSettings = angular.toJson({ output: vm.userRequestSettings });
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

        function exportToExcel(ev, requests) {

            var editedReq = [];
            //mapop

            $.each(requests, function (i, item) {

                var newReq = {
                    id: item.id,
                    Yaradılma_tarixi: item.createdDate.substring(0, 10),
                    Deepartment: item.department['name'],
                    Müraciət_növü: item.customerRequestType['name'],
                    Sifarişçi: item.customerName,
                    Nömrə: item.customerNumber,
                    Qəbul_Edən: item.createdUser['firstName'] + " " + item.createdUser['lastName'],
                    Başlama_tarixi: item.startDate.substring(0, 10),
                    Status: item.requestStatus['name']
                };
                editedReq.push(newReq);
            });
            //end mapp
           // console.log(requests, XLSX);
            var ws = XLSX.utils.json_to_sheet(editedReq);
            /* add to workbook */
            var wb = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(wb, ws, "Presidents");
            /* write workbook and force a download */
            XLSX.writeFile(wb, "export.xlsx");
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
                        vm.userRequestSettings.splice(vm.userRequestSettings.indexOf(set), 1);
                        localStorage.offlineUserRequestSettings = angular.toJson({ output: vm.userRequestSettings });
                    },
                    // ERROR
                    function (response) {
                        console.error(response.output)
                    }
                );
            }, function () {
            });
        }

        function updateRating(ratingId, value, requestId) {
            if (angular.isNumber(ratingId)) {
                msApi.request('updateRating@save', { id: ratingId, requestPoint: value },
                    // SUCCESS
                    function (response) {
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
                msApi.request('addRating@save', { requestId: requestId, requestPoint: value },
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

        //////////////////////////////////////////

        function excelExport1(filter, parse) {
            if (parse != undefined) filter = angular.fromJson(filter);
            //console.log(filter);
            filter.requestStatusId = [];
            angular.forEach(filter.requestStatusIds, function (value, key) {
                if (value) this.push(Number(key));
            }, filter.requestStatusId);
            // console.log(filter);
            if (filter.requestStatusId.length < 1) delete filter.requestStatusId;
            //delete filter.requestStatusIds;

            filter.customerRequestTypeId = [];
            angular.forEach(filter.requestTypeIds, function (value, key) {
                if (value) this.push(Number(key));
            }, filter.customerRequestTypeId);
            // console.log(filter);
            if (filter.customerRequestTypeId.length < 1) delete filter.customerRequestTypeId;
            // delete filter.requestTypeIds;


            filter[vm.filterKey] = [];
            angular.forEach(filter.departmentIds, function (value, key) {
                if (value) this.push(Number(key));
            }, filter[vm.filterKey]);
            // console.log(filter);
            if (filter[vm.filterKey].length < 1) delete filter[vm.filterKey];
            // delete filter.requestTypeIds;


            //if (filter.startDateFrom) vm.filterData.startDateFrom = moment(filter.startDateFrom).format();
            // if (filter.startDateTo) vm.filterData.startDateTo = moment(filter.startDateTo).startOf('day').add(23, 'hours').add(59, 'minutes').utcOffset(4).toDate();
            if (filter.startDateTo) vm.filterData.startDateTo = filter.startDateTo
            localStorage.setItem("vmFilter", angular.toJson(vm.filterData));
            vm.filterData = filter;

            if (filter.startDateFrom) vm.filterData.startDateFrom = moment(filter.startDateFrom).format();
            if (filter.startDateTo) vm.filterData.startDateTo = new Date(moment(filter.startDateTo).toDate().getTime() + 1 * 24 * 59 * 60 * 1000);

            $state.go(vm.sourceTypeName, {
                requesttype: filter.customerRequestTypeId ? filter.customerRequestTypeId.join(",") : undefined,
                requeststatus: filter.requestStatusId ? filter.requestStatusId.join(",") : undefined,
                department: filter.departmentId ? filter.departmentId.join(",") : undefined,
                start: filter.startDateFrom ? $filter("date")(filter.startDateFrom, "yyyy-MM-dd") : undefined,
                end: filter.startDateTo ? $filter("date")(filter.startDateTo, "yyyy-MM-dd") : undefined
            }, { notify: false });

            excelExport(vm.filterData, false);
            // vm.loadMoreIsInProgress = true;
            // vm.pagination.pageNumber = 1;
            // findTask(vm.filterData);

        }



        //////////////////////

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
            data.sourceTypeId = vm.sourceTypeId;
            vm.findIsInProgress = !append;

            msApi.request('findRequests@save', data,
                // SUCCESS
                function (response) {
                    if (withInterval) {
                        var a = response.output;
                    } else {

                        // requestForm.createdDate = new Date();
                        vm.findIsInProgress = false;
                        vm.loadMoreIsInProgress = false;
                        if (append)
                            var a = response.output;
                        else
                            var a = response.output; //todo uncomment

                        vm.totalRequests = response.totalCount;

                        if (response.output.length < 10)
                            vm.endOfResponse = true;
                        else
                            vm.endOfResponse = false;

                        //if (vm.stateParams.id) vm.openRequestInfoDialog({}, $filter('filter')(vm.requests, { id: vm.stateParams.id })[0])
                    }
                    // lf.setItem("requests_" + vm.sourceTypeId, vm.requests.slice(0, vm.pageSize))
                    var excelDataArray = [];
                    $.each(response.output, function (i, item) {
                        var region = "Qeyd olunmayib";
                        var MuracietNovu = "Qeyd olunmayib";
                        var createdUser = "Qeyd olunmayib";
                        var status = "Qeyd olunmayib";
                        var tarix = CustomDateParse(item.createdDate);
                        var sonTapsiriqTarixi = CustomDateParse(item.createdDate);

                        if (item.customerRequestType != null) {
                            MuracietNovu = item.customerRequestType.name
                        }
                        if (item.region != null) {
                            region = item.region.name;
                        }
                        if (item.createdUser != null) {
                            createdUser = item.createdUser.firstName + " " + item.createdUser.lastName
                        }
                        if (item.requestStatus != null) {
                            status = item.requestStatus.name;

                            if (item.requestStatus.createdDate != null) {
                                tarix = CustomDateParse(item.requestStatus.createdDate);

                            }
                        }
                        if (item.mainTask.length > 0) {
                            var lastTaskIndex = item.mainTask.length-1
                            sonTapsiriqTarixi = item.mainTask[lastTaskIndex].endDate
                        }
                        var excelData =
                        {
                            ID: item.id,
                            Açıldığı_tarix: CustomDateParse(item.createdDate),
                            Bölmə: region,
                            Müraciət_növü: MuracietNovu,
                            Sifarişçi: item.customerName,
                            Nömrə: item.contactNumber,
                            Müşdəri_Nömrəsi: item.customerNumber,
                            Başlama_tarixi: CustomDateParse(item.startDate),
                            Qəbul_edən: createdUser,
                            Status: status,
                            Son_Status_Tarixi: tarix,
                            //Son_Tapsiriqin_icra_Tarixi: CustomDateParse(sonTapsiriqTarixi),
                            //İcra_Müddəti: RequestPeriodOfPerformance(item.createdDate, sonTapsiriqTarixi)
                        }

                        if (item.department != null && item.sourceTypeId ==1 ) {

                            excelData.Bölmə = item.department.name;
                        }

                        excelDataArray.push(excelData);
                    });

                    console.log(response.output);
                    //console.log(excelDataArray);

                    var ws = XLSX.utils.json_to_sheet(excelDataArray);
                    /* add to workbook */
                    var wb = XLSX.utils.book_new();
                    XLSX.utils.book_append_sheet(wb, ws, "Sheet1");
                    /* write workbook and force a download */
                    XLSX.writeFile(wb, "export.xlsx");
                },
                // ERROR
                function (response) {
                    console.error(response.output)
                }
            );
            // console.log(requests, XLSX);
        }
        function CustomDateParse(date) {
            if (date != null) {
                var indexT = date.indexOf("T");
                if (indexT > 0) {
                    date = date.replace("T", " ");
                    date = date.substring(0, 16)
                }
            }
            return date;
        }

        function RequestPeriodOfPerformance(createDate, endDate) {
            if (createDate != null && endDate != null) {

                createDate = new Date(createDate).getTime();
                endDate = new Date(endDate).getTime();
                var milliseconds = endDate - createDate;
                var hours = Math.floor(milliseconds / (60 * 60 * 1000));
                //console.log(createDate);
                //console.log(endDate);
                if (hours > 0) {
                    return hours + "-saat";
                }
                else if (hours < 0) {
                    return (hours*-1) + "-saat";
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

