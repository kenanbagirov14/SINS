(function () {
    'use strict';

    angular
        .module('app.pages.error.unauthorize', [])
        .config(config)        

    /** @ngInject */
    function config($stateProvider, $translatePartialLoaderProvider, msNavigationServiceProvider, msApiProvider) {
        // State
        $stateProvider.state('app.pages_error_unauthorize', {
            url: '/pages/error/unauthorize',
            views: {
                'main@': {
                    templateUrl: 'app/core/layouts/content-only.html',
                    controller: 'MainController as vm'
                },
                'content@app.pages_error_unauthorize': {
                    templateUrl: 'app/main/pages/error/unauthorize.html',
                    controller: 'UnauthorizeController as vm'
                }
            },

            bodyClass: 'unauthorize'
        });



    }

})();