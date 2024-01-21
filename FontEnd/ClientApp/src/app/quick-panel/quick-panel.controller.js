(function () {
    'use strict';

    angular
        .module('app.quick-panel')
        .controller('QuickPanelController', QuickPanelController);

    /** @ngInject */
    function QuickPanelController($rootScope, msApi) {
        var vm = this;
        
        //vm.news = JSON.parse(localStorageService.get("new"));
        //console.log(vm.news);
        //console.dir(vm.ls.get("new"));
        // Data
        vm.date = new Date();
        vm.settings = {
            notify: true,
            cloud: false,
            retro: true
        };


    }

})();