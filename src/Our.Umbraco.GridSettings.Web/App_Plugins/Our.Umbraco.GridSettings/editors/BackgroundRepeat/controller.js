(function () {
    'use strict';

    var controller = function ($scope) {
        $scope.options = [
            { value: 'no-repeat', title: 'No repeat' },
            { value: 'repeat', title: 'Repeat horizontally & vertically' },
            { value: 'repeat-x', title: 'Repeat horizontally only' },
            { value: 'repeat-y', title: 'Repeat vertically only' }
        ];
    };

    angular.module("umbraco").controller("Our.Umbraco.GridSettings.BackgroundRepeatController", controller);
})();