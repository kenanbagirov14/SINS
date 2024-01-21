(function ()
{
    'use strict';

    angular
        .module('app.core')
        .provider('sinsConfig', sinsConfigProvider);

    /** @ngInject */
    function sinsConfigProvider()
    {
        // Default configuration
        var sinsConfiguration = {
            'disableCustomScrollbars'        : false,
            'disableMdInkRippleOnMobile'     : true,
            'disableCustomScrollbarsOnMobile': true
        };

        // Methods
        this.config = config;

        //////////

        /**
         * Extend default configuration with the given one
         *
         * @param configuration
         */
        function config(configuration)
        {
            sinsConfiguration = angular.extend({}, sinsConfiguration, configuration);
        }

        /**
         * Service
         */
        this.$get = function ()
        {
            var service = {
                getConfig: getConfig,
                setConfig: setConfig
            };

            return service;

            //////////

            /**
             * Returns a config value
             */
            function getConfig(configName)
            {
                if ( angular.isUndefined(sinsConfiguration[configName]) )
                {
                    return false;
                }

                return sinsConfiguration[configName];
            }

            /**
             * Creates or updates config object
             *
             * @param configName
             * @param configValue
             */
            function setConfig(configName, configValue)
            {
                sinsConfiguration[configName] = configValue;
            }
        };
    }

})();