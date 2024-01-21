(function () {
    'use strict';

    angular
        .module('app.core')
        .filter('toTrusted', toTrustedFilter)
        .filter('htmlToPlaintext', htmlToPlainTextFilter)
        .filter('nospace', nospaceFilter)
        .filter('humanizeDoc', humanizeDocFilter)
        .filter('intersect', intersectFilter)
        .filter('includes', includesFilter);

    /** @ngInject */
    function toTrustedFilter($sce) {
        return function (value) {
            return $sce.trustAsHtml(value);
        };
    }

    function intersectFilter() {
        return function (arr1, arr2) {
            return arr1.filter(function (n) {
                return arr2.indexOf(n) != -1
            });
        };
    };
    function includesFilter() {
        return function (arr, part) {
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].indexOf(part)) {
                    return i;
                }
            }
            return -1

        };
    };
    /** @ngInject */
    function htmlToPlainTextFilter() {
        return function (text) {
            //  text =
            return String(text).replace(/\r\n/g, '').replace(/<[^>\r\n]+>/gm, '').replace(/(v).+}/gm, '').replace(/(\<!--.)+{+.+}/gm, '').replace(/\&quot;/g, "'");
        };
    }

    /** @ngInject */
    function nospaceFilter() {
        return function (value) {
            return (!value) ? '' : value.replace(/ /g, '');
        };
    }

    /** @ngInject */
    function humanizeDocFilter() {
        return function (doc) {
            if (!doc) {
                return;
            }
            if (doc.type === 'directive') {
                return doc.name.replace(/([A-Z])/g, function ($1) {
                    return '-' + $1.toLowerCase();
                });
            }
            return doc.label || doc.name;
        };
    }

})();