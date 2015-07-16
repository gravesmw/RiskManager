'use strict';
var app = angular.module('RiskManager.Controller.Service', [
    
]);

//Service that manages view server side action
app.factory("viewFactory", ['$http', function ($http) {

    var urlBase = '/View';
    var viewFactory = {};

    viewFactory.getViews = function () {
        return $http.get(urlBase);
    };

    viewFactory.createView = function (newView) {
        return $http.post(urlBase, newView);
    };

    return viewFactory;
}]);

//Service that manages container view server side action
app.factory("containerViewFactory", ['$http', function ($http) {

    var urlBase = '/ContainerView';
    var containerViewFactory = {};

    containerViewFactory.getContainerViewHierarchy = function (viewID) {
        return $http.get(urlBase + '/' + viewID);
    };

    containerViewFactory.createContainer = function (viewID, newContainer) {
        return $http.post(urlBase + '/' + viewID, newContainer);
    };

    containerViewFactory.updateContainer = function (viewID, updatedContainer) {
        return $http.put(urlBase + '/' + viewID, updatedContainer);
    };

    containerViewFactory.deleteContainer = function (viewID, containerViewID) {
        return $http.delete(urlBase + '/' + viewID + '/container/' + containerViewID);
    };

    return containerViewFactory;
}]);

//Service that manages container view server side action
app.factory("containerFactory", ['$http', function ($http) {

    var urlBase = '/Container';
    var containerFactory = {};

    containerFactory.getPossibleContainers = function (viewID) {
        return $http.get(urlBase + '/' + viewID);
    };

    return containerFactory;
}]);


//Service containng shared functions
app.factory("commonFuncFactory", [ function() {
    
    var commonFuncFactory = {};

    commonFuncFactory.removeNodeFromTree = function (tree, nodeID) {
        for (var i = 0; i < tree.length; i++) {
            if (tree[i].nodeID == nodeID) {
                tree.splice(i, 1);
                break;
            }
            if (tree[i].children) {
                commonFuncFactory.removeNodeFromTree(tree[i].children, nodeID);
            }
        }
    }

    commonFuncFactory.addNodeToTree = function (tree, node) {
        for (var i = 0; i < tree.length; i++) {
            if (tree[i].nodeID == node.parentID) {
                tree[i].children.push(node);
                break;
            }
            if (tree[i].children) {
                commonFuncFactory.addNodeToTree(tree[i].children, node);
            }
        }
    }
    
    commonFuncFactory.getPossibleParents = function (tree, currentParentID) {

        var possibleParents = [];
        for (var i = 0; i < tree.length; i++) {
            getParents(currentParentID, tree[i], possibleParents);
        }

        possibleParents.sort(
            function (a, b) { return b.name < a.name }
        );

        return possibleParents;
    }

    function getParents(currentParentID, treeNode, possibleParents){
        if (treeNode.nodeID != currentParentID && treeNode.selected != 'selected') {
            possibleParents.push(treeNode);
        }

        if (treeNode.children) {
            for (var i = 0; i < treeNode.children.length; i++) {
                getParents(currentParentID, treeNode.children[i], possibleParents);
            }
        }
    }

    commonFuncFactory.verifySelectedNode = function(node){
        if (node == undefined || node.selected == undefined){
            alert("Please select node from tree");
            return false;
        }
        return true;
    }

    commonFuncFactory.clearSelectedNode = function (selectedNode, mode) {
        if (selectedNode) {
            selectedNode.selected = undefined;
            selectedNode = undefined;
            mode = undefined;
        }
    }
    
    return commonFuncFactory;
}]);