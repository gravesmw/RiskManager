'use strict';
var app = angular.module('GravesConsultingLLC.RiskManager', [
    'ngRoute',
    'ui.bootstrap',
    'cgPrompt',
    'angularTreeview',
    'angular-loading-bar', 
    'GravesConsultingLLC.RiskManager.Controller',
    'GravesConsultingLLC.RiskManager.Controller.Service'
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