(function () {
    'use strict';

    var controller = function ($scope) {
        $scope.colors = [];

        for (var prevalueIndex = 0; prevalueIndex < $scope.model.prevalues.length; prevalueIndex++) {
            var prevalue = $scope.model.prevalues[prevalueIndex];

            if (typeof (prevalue) === 'string') {
                $scope.colors.push({
                    'label': prevalue,
                    'value': prevalue
                });
            } else if (prevalue.label && prevalue.value) {
                $scope.colors.push(prevalue);
            }
        }

        $scope.setValue = function (value) {
            $scope.model.value = value;
        }

        $scope.isValue = function (value) {
            return value === $scope.model.value;
        }
    };

    angular.module("umbraco").controller("Our.Umbraco.GridSettings.ColorPickerController", controller);
})();