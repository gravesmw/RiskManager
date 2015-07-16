'use strict';
var app = angular.module('RiskManager', [
    'ngRoute',
    'angularModalService',
    'angularTreeview',
    'angular-loading-bar', 
    'RiskManager.Controller',
    'RiskManager.Controller.Service'
]);

app.config(['$routeProvider', function ($routeProvider) {

    $routeProvider.when('/ContainerView', {
        controller: 'ContainerViewController',
        controllerAs: 'vmContainerViewController',
        templateUrl: '/Templates/ContainerView.html'
    }).when('/DefectGroup', {
        controller: 'DefectGroupController',
        controllerAs: 'vmDefectGroupController',
        templateUrl: '/Templates/DefectGroup.html'
    }).when('/ObjectType', {
        controller: 'ObjectTypeController',
        controllerAs: 'vmObjectTypeController',
        templateUrl: '/Templates/ObjectType.html'
    })
    .otherwise({ redirectTo: '/' });

}]);