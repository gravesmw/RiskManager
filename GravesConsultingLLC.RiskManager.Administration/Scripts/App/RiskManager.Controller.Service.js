'use strict';
var app = angular.module('RiskManager.Controller.Service', [
    
]);

//Service that manages container view server side action
app.factory("containerViewFactory", ['$http', function ($http) {

    var urlBase = '/ContainerView';
    var containerViewFactory = {};

    containerViewFactory.getContainerViews = function () {
        return $http.get(urlBase);
    };

    containerViewFactory.getContainerViewHierarchy = function (viewID) {
        return $http.get(urlBase + '/' + viewID);
    };

    containerViewFactory.createContainer = function (viewID, newContainer) {
        return $http.post(urlBase + '/' + viewID, newContainer);
    };

    containerViewFactory.deleteContainer = function (viewID, containerViewID) {
        return $http.delete(urlBase + '/' + viewID + '/container/' + containerViewID);
    };

    return containerViewFactory;
}]);

//Service containg shared functions
app.factory("commonFuncFactory", [ function() {
    
    var commonFuncFactory = {};

    commonFuncFactory.removeNodeFromTree = function (parent, node, index) {
        if (node.selected == "selected") {
            parent.Children.splice(index, 1);
        }
        else {
            if (node.Children.length > 0) {
                for (var i = 0; i < node.Children.length; i++) {
                    commonFuncFactory.removeNodeFromTree(node, node.Children[i], i);
                }
            }
        }
    }
    
    return commonFuncFactory;
}]);