(function () {
    'use strict';

    angular
        .module('app.report',
        [
            // 3rd Party Dependencies
            // 'gridshore.c3js.chart',
            'datatables',
            'chart.js'
        ]
        )
        .config(config);

    /** @ngInject */
    function config($stateProvider, $translatePartialLoaderProvider, msApiProvider, msNavigationServiceProvider) {

        $stateProvider.state('app.report', {
            url: '/report',
            views: {
                'content@app': {
                    templateUrl: 'app/main/report/report.html',
                    controller: 'ReportController as vm'
                }
            },
            resolve: {
                // RequestByDepartment: function (msApi) {
                //     return msApi.resolve('requestByDepartment@query');
                // },
                // RequestTypes: function (msApi) {
                //     return msApi.resolve('requestTypes@query');
                // },
                // DepartmentList: function (msApi) {
                //     return msApi.resolve('DepartmentList@query') || [] ;
                // },
                // RegionList: function (msApi) {
                //     return msApi.resolve('RegionList@query');
                // },
                // SourceTypes: function (msApi) {
                //     return msApi.resolve('sourceTypes@query');
                // },
                // RequestStatusList: function (msApi) {
                //     return msApi.resolve('RequestStatusList@query');
                // },
                // InjuryTypes: function (msApi) {
                //     return msApi.resolve('injuryTypes@query');
                // },
                // UserList: function (msApi) {
                //     return msApi.resolve('getUser@query');
                // }
            }
        });

        // Translation
      //  $translatePartialLoaderProvider.addPart('app/main/report');
        // Api
        msApiProvider.register('requestByDepartment', ['report/requestByDepartment']);
        msApiProvider.register('requestByRequestType', ['report/requestByRequestType']);
        msApiProvider.register('requestByRequestStatus', ['report/requestByRequestStatus']);
        msApiProvider.register('taskByExecutorUser', ['report/taskByExecutorUser']);
        msApiProvider.register('taskByTaskStatus', ['report/taskByTaskStatus']);
        msApiProvider.register('requestByRegion', ['report/requestByRegion']);
        msApiProvider.register('taskByDepartment', ['report/taskByDepartment']);

    }

})();