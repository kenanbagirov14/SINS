(function () {
    'use strict';
    angular
        .module('app.request',
            [
                // 3rd Party Dependencies
                'xeditable',
                'ngMaterialDatePicker',
                'angular-rating-icons'
                //'localStorageService'
                // 'infinite-scroll'
            ]
        )
        .config(config);

    // function sharedResolve(
    //     offlineDepartments,
    //     offlineUserDepartments,
    //     offlineRegions,
    //     offlineSourceTypes,
    //     offlineRequestStatusList,
    //     offlineTaskStatusList,
    //     offlineInjuryTypes,
    //     offlineGetUser
    // ) {
    //     return {
    //         DepartmentList: function (msApi) {
    //             if (offlineDepartments.output) {
    //                 return offlineDepartments;
    //             }
    //             else {
    //                 return msApi.resolve('Departments@query') || [];
    //             }

    //         },
    //         UserDepartments: function (msApi) {
    //             if (offlineUserDepartments.output) {
    //                 return offlineUserDepartments;
    //             }
    //             else {
    //                 return msApi.resolve('UserDepartments@save');
    //             }
    //         },
    //         RegionList: function (msApi) {
    //             if (offlineRegions.output) {
    //                 return offlineRegions;
    //             }
    //             else {
    //                 return msApi.resolve('Regions@query');
    //             }
    //         },
    //         SourceTypes: function (msApi) {
    //             if (offlineSourceTypes.output) {
    //                 return offlineSourceTypes;
    //             }
    //             else {
    //                 return msApi.resolve('sourceTypes@query');
    //             }
    //         },
    //         RequestStatusList: function (msApi) {
    //             if (offlineRequestStatusList.output) {
    //                 return offlineRequestStatusList;
    //             }
    //             else {
    //                 return msApi.resolve('RequestStatusList@query');
    //             }
    //         },
    //         TaskStatusList: function (msApi) {
    //             if (offlineTaskStatusList.output) {
    //                 return offlineTaskStatusList;
    //             }
    //             else {
    //                 return msApi.resolve('TaskStatusList@query');
    //             }
    //         },
    //         InjuryTypes: function (msApi) {
    //             if (offlineInjuryTypes.output) {
    //                 return offlineInjuryTypes;
    //             }
    //             else {
    //                 return msApi.resolve('injuryTypes@query');
    //             }
    //         },
    //         UserList: function (msApi) {
    //             if (offlineGetUser.output) {
    //                 return offlineGetUser;
    //             }
    //             else {
    //                 return msApi.resolve('getUser@query');
    //             }
    //         }
    //     }
    // }
    /** @ngInject */
    function config( $stateProvider,$translatePartialLoaderProvider) {

        // console.log(offlineDepartments);

        $stateProvider.state('app.request', {
            url: '/request/?id&department&requesttype&requeststatus&start&end',
            views: {
                'content@app': {
                    templateUrl: 'app/main/request/request.html',
                    controller: 'RequestController as vm'
                }
            }
            
        });


        $stateProvider.state('app.callcenter', {
            url: '/callcenter/?id&department&requesttype&requeststatus&start&end',
            views: {
                'content@app': {
                    templateUrl: 'app/main/request/callcenter.html',
                    controller: 'RequestController as vm'
                }
            }            
        });


        $stateProvider.state('app.law', {
            url: '/law/?id&department&requesttype&requeststatus&start&end',
            views: {
                'content@app': {
                    templateUrl: 'app/main/request/law.html',
                    controller: 'RequestController as vm'
                }
            }            
        });

        $stateProvider.state('app.clerical', {
            url: '/clerical/?id&department&requesttype&requeststatus&start&end',
            views: {
                'content@app': {
                    templateUrl: 'app/main/request/clerical.html',
                    controller: 'RequestController as vm'
                }
            }            
        });



     


        // Translation
        $translatePartialLoaderProvider.addPart('app/main/request');
        // Api

        //  msApiProvider.register('requests', ['CustomerRequest/getallasync']);



    }

})();

