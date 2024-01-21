(function () {
    'use strict';

    /**
     * Main module of the Nis
     */
    angular
        .module('sins', [          
            'textAngular',
            'xeditable',
            'app.core',
            //'app.navigation',
            'app.toolbar',
            'app.pages',
            'app.request',
            'app.task',
            'app.report',
            'app.quick-panel',
            'app.main'
        ]);
})();
