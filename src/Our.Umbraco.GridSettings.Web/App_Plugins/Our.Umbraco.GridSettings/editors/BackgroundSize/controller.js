(function () {
    'use strict';

    var controller = function ($scope) {
        $scope.options = [
            { value: 'auto', title: 'Auto', description: 'Scales the background image to contain its width and height.' },
            { value: 'cover', title: 'Cover', description: 'Scale the background image to be as large as possible so that the background area is completely covered by the background image. Some parts of the background image may not be in view within the background positioning area.' },
            { value: 'contain', title: 'Contain', description: 'Scale the image to the largest size such that both its width and its height can fit inside the content area' }
        ];
    };

    angular.module("umbraco").controller("Our.Umbraco.GridSettings.BackgroundSizeController", controller);
})();