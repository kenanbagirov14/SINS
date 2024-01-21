(function () {
    'use strict';

    angular
        .module('app.quick-panel')
        .controller('NewTabController', NewTabController);

    /** @ngInject */
    function NewTabController(msApi, $timeout, $rootScope, $state, msHub) {
        var vm = this;
        vm.hub = msHub.hub();
        vm.actionQuickData = actionQuickData;
        vm.removeQuickData = removeQuickData;
        vm.quickData = [];
        vm.quickDataLoad = [];
        vm.findIsInProgress = false;
        vm.findNotification = findNotification;
        $rootScope.findNotification = vm.findNotification;
        vm.findNotification({ status: true }, true, false);
        function actionQuickData(data) {
            data.status = false;
            msApi.request('updateNotification@save', { id: [data.id], status: false },
                // Success
                function (response) {
                    // $state.go('app.' + data.notification.notificationType.name, { id: data.notification.relationalId });
                },
                function (err) {
                    console.log(err);
                }
            );
        }

        function findNotification(data, showCount, isAllNotificationLoaded) {
            if (!showCount && !isAllNotificationLoaded) vm.findIsInProgress = true;
            if (isAllNotificationLoaded) return;
            msApi.request('findNotification@save', data,
                // Success
                function (response) {
                    vm.findIsInProgress = false;
                    if (showCount)
                        vm.quickData = response.output;
                    else {
                        vm.quickDataLoad = response.output;
                    }
                    if (showCount)
                        $rootScope.notificationCount = response.totalCount;
                },
                function (err) {
                    vm.findIsInProgress = false;
                    console.log(err);
                }
            );
        }
        function removeQuickData(data) {
            msApi.request('removeNotification@save', { id: [data.id] },
                // Success
                function (response) {
                    if (data.status)
                        vm.quickData.splice(vm.quickData.indexOf(data), 1);
                    else
                        vm.quickDataLoad.splice(vm.quickDataLoad.indexOf(data), 1);

                },
                function (err) {
                    console.log(err);
                }
            );
        }
        if (!vm.hub.methods.hasOwnProperty('remotenotificationadd')) {
            vm.hub.on('remotenotificationadd', function (response) {
                console.log(response)
                $rootScope.$evalAsync(function () {
                    vm.quickData.unshift(response.newData);
                });
            });
        }
    }

})();