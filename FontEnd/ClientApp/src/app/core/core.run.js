(function () {
    'use strict';

    angular
        .module('app.core')
        .run(runBlock);


    /** @ngInject */
    function runBlock(msUtils, sinsGenerator, sinsConfig, authService, $animate) {
        $animate.enabled(false);
        authService.fillAuthData();
        /**
         * Generate extra classes based on registered themes so we
         * can use same colors with non-angular-material elements
         */
        sinsGenerator.generate();
        /**
         * Disable md-ink-ripple effects on mobile
         * if 'disableMdInkRippleOnMobile' config enabled
         */
        if (sinsConfig.getConfig('disableMdInkRippleOnMobile') && msUtils.isMobile()) {
            var bodyEl = angular.element('body');
            bodyEl.attr('md-no-ink', true);
        }

        /**
         * Put isMobile() to the html as a class
         */
        if (msUtils.isMobile()) {
            angular.element('html').addClass('is-mobile');
        }

        /**
         * Put browser information to the html as a class
         */
        var browserInfo = msUtils.detectBrowser();
        if (browserInfo) {
            var htmlClass = browserInfo.browser + ' ' + browserInfo.version + ' ' + browserInfo.os;
            angular.element('html').addClass(htmlClass);
        }
    }
})();