(function () {
    'use strict';

    var controller = function ($scope, notificationsService) {
        $scope.colors = [];
        var stringPreValues = $scope.model.prevalues.filter(function (item) { return typeof (item) === 'string'; });

        if (stringPreValues.length > 0) {
            notificationsService.error("Configuration Error", "Class Color Picker (" + $scope.model.label + ") must only have key value pair prevalues.");
        } else {
            $scope.colors = $scope.model.prevalues;
        }

        $scope.setValue = function (value) {
            $scope.model.value = value;
        }

        $scope.isValue = function (value) {
            return value === $scope.model.value;
        }
    };

    angular.module("umbraco").controller("Our.Umbraco.GridSettings.ClassColorPickerController", controller);
})();