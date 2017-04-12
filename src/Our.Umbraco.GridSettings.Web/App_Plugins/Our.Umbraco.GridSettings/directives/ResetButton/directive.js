(function () {
    'use strict';

    function ResetButtonDirective() {
        function link(scope, el, attr, ctrl) {
            if (attr.defaultValue) {
                scope.defaultValue = attr.defaultValue;
            } else {
                scope.defaultValue = '';
            }
            scope.labelKey = attr.labelKey;

            scope.resetValue = function () {
                scope.model = scope.defaultValue;
            }
            scope.isVisible = function () {
                var hasValue = typeof (scope.model) !== 'undefined';
                var valueEqualsDefaultValue = scope.model === scope.defaultValue;

                return hasValue && (valueEqualsDefaultValue === false);
            }
        }

        var directive = {
            restrict: 'E',
            replace: true,
            templateUrl: '/App_Plugins/Our.Umbraco.GridSettings/directives/ResetButton/view.html',
            scope: {
                labelKey: '&',
                model: '=',
                defaultValue: '&?'
            },
            link: link
        };

        return directive;
    }

    angular.module('umbraco.directives').directive('gridsettingsResetButton', ResetButtonDirective);
})();