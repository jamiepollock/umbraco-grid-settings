(function () {
    'use strict';

    var controller = function ($scope) {
        $scope.options = [
            { value: 'repeat', title: 'Repeat' },
            { value: 'no-repeat', title: 'No repeat' },
            { value: 'repeat-x', title: 'Repeat horizontally' },
            { value: 'repeat-y', title: 'Repeat vertically' }
        ];
    };

    angular.module("umbraco").controller("Our.Umbraco.GridSettings.BackgroundRepeatController", controller);
})();