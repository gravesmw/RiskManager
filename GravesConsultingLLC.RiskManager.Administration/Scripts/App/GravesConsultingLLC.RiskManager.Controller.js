'use strict';
var app = angular.module('GravesConsultingLLC.RiskManager.Controller', [
        
]);

app.controller('ContainerViewController', ['$scope', 'containerViewFactory', 'prompt', function ($scope, containerViewFactory, prompt) {

    var vm = this;
    vm.containerViews = [];
    vm.containers = [];
    vm.selectedContainerView = {};
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
        vm.selectedContainerView = containerView;

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

    vm.createContainerView = function () {
        prompt({
            title: 'Add New Container to View',
            input: true,
            label: 'New Container Name',
            value: ''
        }).then(function (name) {

            var newContainer = {
                Name: name,
                ViewID: vm.selectedContainerView.ViewID,
                ParentContainerViewID: vm.selectedContainer.ContainerViewID
            };

            containerViewFactory.createContainer(newContainer.ViewID, newContainer)
                .success(function (containerView) {
                    $scope.containerTree.currentNode.Children.push(containerView);
                })
                .error(function (error) {
                    console.log(error.message);
                });

        });


    }

    vm.deleteContainerView = function () {

        var viewID = vm.selectedContainerView.ViewID;
        var containerViewID = vm.selectedContainer.ContainerViewID;

        containerViewFactory.deleteContainer(viewID, containerViewID)
            .success(function () {
                if (vm.containers.selected == "selected") {
                    vm.containers.length = 0;
                }
                else {
                    for (var i = 0; i < vm.containers.length; i++) {
                        if (vm.containers[i].selected == "selected") {
                            vm.containers.length = 0;
                        }
                        else {
                            removeNode(vm.containers, vm.containers[i], i);
                        }
                    }
                }
            })
            .error(function (error) {
                console.log(error.message);
            });

        function removeNode(parent, node, index) {
            if (node.selected == "selected") {
                parent.Children.splice(index, 1);
            }
            else {
                if (node.Children.length > 0) {
                    for (var i = 0; i < node.Children.length; i++) {
                        removeNode(node, node.Children[i], i);
                    }
                }
            }
        }
    }

}]);

app.controller('DefectGroupController', ['$scope', function ($scope) {

}]);

app.controller('ObjectTypeController', ['$scope', function ($scope) {

}]);