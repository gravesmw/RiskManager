'use strict';
var app = angular.module('RiskManager.Controller', [
        
]);

app.controller('ContainerViewController', [
    '$scope',
    'containerViewFactory',
    'prompt',
    'commonFuncFactory',
    function ($scope, containerViewFactory, prompt, commonFuncFactory) {

    getContainerViews();
    
    //Load select element with container views
    function getContainerViews() {
        containerViewFactory.getContainerViews()
            .success(function (views) {
                $scope.containerViews = views;
            })
            .error(function (error) {
                console.log(error.message);
            });
    }

    //Get container view hierarchy for tree based on selected view from select element
    $scope.getContainerViewHierarchy = function () {
        containerViewFactory.getContainerViewHierarchy($scope.selectedView.ViewID)
            .success(function (viewContainers) {
                if (viewContainers[0] != null){
                    $scope.viewContainers = viewContainers;
                }  
            })
            .error(function (error) {
                console.log(error.message);
            });
    }

    //Create new container button action
    $scope.createContainerView = function () {
        prompt({
            title: 'Add New Container to View',
            input: true,
            label: 'New Container Name',
            value: ''
        }).then(function (name) {
            //Create new container instance based on selected values
            var ParentViewID = null;
            if ($scope.containerTree.currentNode) {
                ParentViewID =  $scope.containerTree.currentNode.ContainerViewID
            }

            var newContainer = {
                Name: name,
                ViewID: $scope.selectedView.ViewID,
                ParentContainerViewID: ParentViewID
            };

            containerViewFactory.createContainer(newContainer.ViewID, newContainer)
                .success(function (containerView) {
                    //Create children array if it does not exist. This indicates the root
                    //node is being created
                    if (containerView.Children == undefined) {
                        var Children = [];
                        containerView["Children"] = Children;
                    }
                    //if not currentNode it is root
                    if ($scope.containerTree.currentNode) {
                        $scope.containerTree.currentNode.Children.push(containerView);
                    }
                    else {
                        if ($scope.viewContainers == undefined) {
                            $scope.viewContainers = [];
                        }
                        $scope.viewContainers.push(containerView);
                    }
                })
                .error(function (error) {
                    console.log(error.message);
                });
        })
    }

    //delete container button action
    $scope.deleteContainerView = function () {
        //Get selected values
        var viewID = $scope.selectedView.ViewID;
        var containerViewID = $scope.containerTree.currentNode.ContainerViewID;

        containerViewFactory.deleteContainer(viewID, containerViewID)
            .success(function () {
                for (var i = 0; i < $scope.viewContainers.length; i++) {
                    //If root object is selected reset tree and break
                    if ($scope.viewContainers[i].selected == "selected") {
                        $scope.viewContainers.splice(i, 1);
                        $scope.containerTree.currentNode.selected = undefined;
                        $scope.containerTree.currentNode = undefined;
                        $scope.mode = undefined;
                        break;
                    }
                    else {
                        //If leaf remove from tree
                        commonFuncFactory.removeNodeFromTree($scope.viewContainers, $scope.viewContainers[i], i);
                    }
                }
            })
            .error(function (error) {
                console.log(error.message);
            });
    }
    
}]);

app.controller('DefectGroupController', ['$scope', function ($scope) {

}]);

app.controller('ObjectTypeController', ['$scope', function ($scope) {

}]);