(function () {
    'use strict';

    angular
        .module('app.core')
        .directive('msCard', msCardDirective);

    /** @ngInject */
    function msCardDirective() {

        return {
            restrict: 'E',
            scope: {
                templatePath: '=template',
                cardData: '=ngModel',
                vm: '=viewModel'
            },
            template: '<div class="ms-card-content-wrapper" ng-include="templatePath" onload="cardTemplateLoaded()"></div>',
            compile: function (tElement) {

                // Add class
                tElement.addClass('ms-card');

                return function postLink(scope, iElement) {
                    // Methods
                    scope.cardTemplateLoaded = cardTemplateLoaded;
                    // console.log(scope.tasks)
                    //////////

                    /**
                     * Emit cardTemplateLoaded event
                     */
                    function cardTemplateLoaded() {
                        scope.$emit('msCard::cardTemplateLoaded', iElement);
                    }
                };
            }
        };
    }
})();