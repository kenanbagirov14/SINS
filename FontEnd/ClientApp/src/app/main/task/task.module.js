(function () {
    'use strict';

    angular
        .module('app.task',
            [
                // 3rd Party Dependencies
                //'xeditable'
                'angularInlineEdit',
                'naif.base64',
                'angular-rating-icons',
                'monospaced.elastic',
                'sins'
            ])
        .config(config)
        .filter('unsafe', function ($sce) { return $sce.trustAsHtml; })
        .filter('toDate', function () {
            return function (input) {
                return new Date();
            }
        })
        .filter('nullOrEmpty', function () {
            return function (input, text) {
                if (!input || input == "" || input == null || input == undefined)
                    return text || "......";
                else
                    return input
            }
        })
        .directive('chooseFile', function () {
            return {
                link: function (scope, elem, attrs) {
                    var button = elem.find('button');
                    var input = angular.element(elem[0].querySelector('input#fileInput'));
                    button.bind('click', function () {
                        input[0].click();
                    });
                    input.bind('change', function (e) {
                        scope.$apply(function () {
                            // var files = e.target.files;
                            // if (files[0]) {
                            //     scope.fileName = files[0].name;
                            // } else {
                            //     scope.fileName = null;
                            // }
                        });
                    });
                }
            };
        });
    /** @ngInject */
    function config($stateProvider) {
     // console.log(HubProvider)

        $stateProvider.state('app.task', {
            url: '/task/?id&title&executor&taskstatus&project&department&start&end',
            // params: {
            //     id:null,
            //     title: null,
            //     executor: null,
            //     taskStatusId: null,
            //     projectId: null,
            //     departmentId: null
            // },
            views: {
                'content@app': {
                    templateUrl: 'app/main/task/task.html',
                    controller: 'TaskController as vm'
                }
            },
            resolve: {
                // Tasks: function (msApi) {
                //     return msApi.resolve('findTask@save',{pageNumber:1,pageSize:10});
                // },
                // InjuryTypes: function (msApi) {
                //     var offlineInjuryTypes = angular.fromJson(localStorage.offlineInjuryTypes || "{}"); //added
                //     if (offlineInjuryTypes.output) {
                //         return offlineInjuryTypes;
                //     }
                //     else {
                //         return msApi.resolve('injuryTypes@query');
                //     }
                // },
                // UserList: function (msApi) {
                //     var offlineGetUser = angular.fromJson(localStorage.offlineGetUser || "{}");//added
                //     if (offlineGetUser.output) {
                //         return offlineGetUser;
                //     }
                //     else {
                //         return msApi.resolve('getUser@query');
                //     }
                // },
                // Departments: function (msApi) {
                //     var offlineDepartments = angular.fromJson(localStorage.offlineDepartments || "{}"); //added
                //     if (offlineDepartments.output) {
                //         return offlineDepartments;
                //     }
                //     else {
                //         return msApi.resolve('Departments@query');
                //     }
                // },
                // UserDepartments: function (msApi) {
                //     var offlineUserDepartments = angular.fromJson(localStorage.offlineUserDepartments || "{}"); //added
                //     if (offlineUserDepartments.output) {
                //         return offlineUserDepartments;
                //     }
                //     else {
                //         return msApi.resolve('UserDepartments@save');
                //     }
                // },
                // Regions: function (msApi) {
                //     var offlineRegions = angular.fromJson(localStorage.offlineRegions || "{}"); //added
                //     if (offlineRegions.output) {
                //         return offlineRegions;
                //     }
                //     else {
                //         return msApi.resolve('Regions@query');
                //     }
                // },
                // Projects: function (msApi) {
                //     var offlineProjects = angular.fromJson(localStorage.offlineProjects || "{}"); //added
                //     if (offlineProjects.output) {
                //         return offlineProjects;
                //     }
                //     else {
                //         return msApi.resolve('Projects@query');
                //     }
                // },
                // SourceTypes: function (msApi) {
                //     var offlineSourceTypes = angular.fromJson(localStorage.offlineSourceTypes || "{}");//added
                //     if (offlineSourceTypes.output) {
                //         return offlineSourceTypes;
                //     }
                //     else {
                //         return msApi.resolve('sourceTypes@query');
                //     }
                // },
                // TaskStatusList: function (msApi) {
                //     var offlineTaskStatusList = angular.fromJson(localStorage.offlineTaskStatusList || "{}"); //added
                //     if (offlineTaskStatusList.output) {
                //         return offlineTaskStatusList;
                //     }
                //     else {
                //         return msApi.resolve('TaskStatusList@query');
                //     }
                // },
            }
        });

        // Translation
        //$translatePartialLoaderProvider.addPart('app/main/task');
        // Api


    }

})();