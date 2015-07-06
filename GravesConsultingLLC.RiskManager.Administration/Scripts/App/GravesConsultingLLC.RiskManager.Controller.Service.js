'use strict';
var app = angular.module('GravesConsultingLLC.RiskManager.Controller.Service', [
    
]);

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