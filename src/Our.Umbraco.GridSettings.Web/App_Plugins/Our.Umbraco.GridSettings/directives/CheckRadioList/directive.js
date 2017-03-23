(function () {
    'use strict';

    function CheckRadioListDirective(assetsService) {
        function link(scope, el, attr, ctrl) {
            assetsService.loadCss("/App_Plugins/Our.Umbraco.GridSettings/directives/CheckRadioList/view.css");

            scope.setValue = function (value) {
                scope.model = value;
            }

            scope.isValue = function (value) {
                return value === scope.model;
            }
        }

        var directive = {
            restrict: 'E',
            replace: true,
            templateUrl: '/App_Plugins/Our.Umbraco.GridSettings/directives/CheckRadioList/view.html',
            scope: {
                options: '=',
                model: '='
            },
            link: link
        };

        return directive;
    }

    angular.module('umbraco.directives').directive('gridsettingsCheckRadioList', CheckRadioListDirective);
})();