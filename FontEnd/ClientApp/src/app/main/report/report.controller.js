(function () {
    'use strict';

    angular
        .module('app.report')
        .controller('ReportController', ReportController);

    /** @ngInject */
    function ReportController($filter, $rootScope, msApi, msNavigationService, ACLService, authService) {
        //debugger;
        if (!ACLService.check(['routeReport'], authService)) {
            //$state.go('app.pages_error_unauthorize');
            location.assign('/pages/error/unauthorize')
            return;
        }
        var vm = this;
        vm.selectedTab = 0;
        $rootScope.pageTitle = "Hesabatlar";
        $rootScope.activePage = "report";
        //vm.selectedChart = 'requestByDepartment'; // set defualt chart
        vm.findIsInProgress = false;
        vm.thisReportData = { data: [{ name: null, value: 0 }], totalCount: 0 };
        vm.showChart = showChart;
        vm.excelExport = excelExport;
        vm.chartData = {
            labels: [],
            data: []
        }
        msNavigationService.clearNavigation();
        // msNavigationService.saveItem('reportType', {
        //     title: 'Hesabat forması',
        //     group: true,
        //     weight: 1

        // });
        // msNavigationService.saveItem('reportType.request', {
        //     weight: 2,
        //     // expand: true,
        //     title: 'Sorğular',
        //     icon: 'icon-comment-check-outline'
        // });

        // msNavigationService.saveItem('reportType.request.byDepartment', {
        //     title: "Şöbələr üzrə",
        //     weight: 1,
        //     event: function () {
        //         vm.showChart('requestByDepartment');
        //     },
        //     icon: 'icon-package'
        // });

        // msNavigationService.saveItem('reportType.request.byCategory', {
        //     title: "Kateqoriyalar üzrə",
        //     weight: 2,
        //     event: function () {
        //         vm.showChart('requestByRequestType');
        //     },
        //     icon: 'icon-chart-pie'
        // });

        // msNavigationService.saveItem('reportType.request.byStatus', {
        //     title: "İcra vəziyyəti üzrə",
        //     weight: 3,
        //     event: function () {
        //         vm.showChart('requestByRequestStatus');
        //     },
        //     icon: 'icon-check-all'
        // });

        // msNavigationService.saveItem('reportType.request.byRegion', {
        //     weight: 3,
        //     // group: true,
        //     title: 'TKQ-lər üzrə',
        //     event: function () {
        //         vm.showChart('requestByRegion');
        //     },
        //     icon: 'icon-source-fork'

        // });
        // msNavigationService.saveItem('reportType.task', {
        //     weight: 3,
        //     expand: false,
        //     title: 'Tapşırıqlar',
        //     icon: 'icon-check-circle'

        // });
        // msNavigationService.saveItem('reportType.task.byExecutor', {
        //     weight: 3,
        //     // group: true,
        //     title: 'İcraçılar üzrə',
        //     event: function () {
        //         vm.showChart('taskByExecutorUser');
        //     },
        //     icon: 'icon-check'

        // });



        // msNavigationService.saveItem('reportType.task.byDepartment', {
        //     weight: 3,
        //     // group: true,
        //     title: 'Şöbələr üzrə',
        //     event: function () {
        //         vm.showChart('taskByDepartment');
        //     },
        //     icon: 'icon-puzzle'
        // });

        vm.dtOptions = {
            dom: '<"top"f>rt<"bottom"<"left"<"length"l>><"right"<"info"i><"pagination"p>>>',
            pagingType: 'simple',
            autoWidth: false,
            responsive: true,
            searching: false,
            //paging: false,showChart
            lengthChange: false,
            iDisplayLength: 30,
            info: false

        };
        /**
        * thisTabSelected
        * @param {event object} ev 
        * @param {object} sourceType         */

        vm.showChart('requestByDepartment'); // fills start chart 


        function thisTabSelected(ev, sourceType) {
            vm.selectedTab = sourceType.id;

        }

        function showChart(type) {
            findReport(type, { createdDateFrom: vm.startDate, createdDateTo: vm.endDate });
        }

        function findReport(type, data) {
            //debugger
            vm.findIsInProgress = true;
            vm.thisReportData = [];
            vm.chartData = {
                labels: [],
                data: []
            }
            msApi.request(type + '@save', data,
                // SUCCESS
                function (response) {
                    //requestForm.createdDate = new Date();
                    vm.findIsInProgress = false;
                    vm.selectedChart = type;
                    vm.thisReportData = response;
                    $filter("orderBy")(vm.thisReportData.data, "-value");
                    angular.forEach(vm.thisReportData.data, function (element) {
                        vm.chartData.labels.push(element.name);
                        vm.chartData.data.push(element.value);
                    });
                    // console.log(vm.thisReportData);
                },
                // ERROR
                function (response) {
                    //console.error(response)
                }
            );
        }


        function excelExport(data) {

            console.log(vm.thisReportData);
            console.log(vm.thisReportData.data);
            console.log(vm.chartData);

            var array = vm.thisReportData.data;
            var excelArray = [];
            for (var i = 0; i < array.length; i++) {
                var name = "Təyin Edilməyib";
                var value = 0;
                var completedValue = 0;
                var rating = 0;
                var inExecution = 0;

                if (array[i].name != null) {
                    name = array[i].name;
                }
                if (array[i].value != null) {
                    value = array[i].value;
                }

                if (data == "byDepartment") {
                    var ToExcelObject =
                    {
                        Müraciət_Növü: name,
                        Say: value
                    }
                }
               else if (data == "byRequestType") {
                    var ToExcelObject =
                    {
                        Şöbə: name,
                        Say: value
                    }
                }
                else if (data == "byRequestStatus") {
                    var ToExcelObject =
                    {
                        İcra_vəziyyəti: name,
                        Say: value
                    }
                }
                else {
                    if (array[i].completedValue != null) {
                        completedValue = array[i].completedValue;
                    }
                    if (array[i].rating != null) {
                        rating = array[i].rating;
                    }
                    if (array[i].value != null && array[i].completedValue != null) {
                        inExecution = array[i].value - array[i].completedValue;
                    }
                    var ToExcelObject =
                    {
                        Icraçı: name,
                        Tapşırıq_sayı: value,
                        İcra_olunan: completedValue,
                        İcrada: inExecution,
                        Qiymət:rating
                    }
                }
                excelArray.push(ToExcelObject);
            }
            var ws = XLSX.utils.json_to_sheet(excelArray);
            /* add to workbook */
            var wb = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(wb, ws, "Sheet1");
            /* write workbook and force a download */
            XLSX.writeFile(wb, "export.xlsx");
        }
    }
})(); 
