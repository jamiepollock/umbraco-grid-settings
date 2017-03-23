(function () {
    'use strict';

    var controller = function ($scope) {
        $scope.options = [
            { value: 'left', title: 'Left' },
            { value: 'right', title: 'Right' },
            { value: 'center', title: 'Center' },
            { value: 'justify', title: 'Justify' }
        ];
    };

    angular.module("umbraco").controller("Our.Umbraco.GridSettings.TextAlignController", controller);
})();