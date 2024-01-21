(function ()
{
    'use strict';

    angular
        .module('sins')
        .controller('IndexController', IndexController);

    /** @ngInject */
    function IndexController(sinsTheming)
    {
        var vm = this;

        // Data
        vm.themes = sinsTheming.themes;

        //////////
    }
})();