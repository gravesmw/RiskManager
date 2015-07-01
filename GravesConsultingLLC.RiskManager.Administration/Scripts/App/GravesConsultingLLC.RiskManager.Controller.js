'use strict';
var app = angular.module('GravesConsultingLLC.RiskManager.Controller', [
        
]);

app.controller('ContainerViewController', ['$scope', 'containerViewFactory', function ($scope, containerViewFactory) {

    var vm = this;
    vm.containerViews = [];
    vm.containers = [];
    vm.selectedContainer = {};

    getContainerViews();

    function getContainerViews() {
        containerViewFactory.getContainerViews()
            .success(function (views) {
                vm.containerViews = views;
            })
            .error(function (error) {
                console.log(error.message);
            });
    }

    vm.setContainers = function (containerView) {
        containerViewFactory.getContainerViewHierarchy(containerView.ViewID)
            .success(function (containerView) {
                vm.containers = containerView;
            })
            .error(function (error) {
                console.log(error.message);
            });
    }

    $scope.$watch('containerTree.currentNode', function (newObj, oldObj) {
        if ($scope.containerTree && angular.isObject($scope.containerTree.currentNode)) {
            vm.selectedContainer = $scope.containerTree.currentNode;
        }
    }, false);
}]);

app.controller('DefectGroupController', ['$scope', function ($scope) {

}]);

app.controller('ObjectTypeController', ['$scope', function ($scope) {

}]);