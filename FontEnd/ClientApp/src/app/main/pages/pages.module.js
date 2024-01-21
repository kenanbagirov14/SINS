(function () {
    'use strict';

    angular
        .module('app.pages', [
            'app.pages.auth.login',
            'app.pages.error.unauthorize',
            'app.pages.auth.register'
        ])
        .config(config);

    /** @ngInject */
    function config(msNavigationServiceProvider) {
        // Navigation
        // msNavigationServiceProvider.saveItem('Request', {
        //     title : 'USER INTERFACE',
        //     group : true,
        //     weight: 3
        // });

        // msNavigationServiceProvider.saveItem('Home', {
        //     title: 'Home',
        //     icon: 'icon-window-restore',
        //     state: 'app.home'
        // });
    }
})();