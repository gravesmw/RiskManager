'use strict';
var app = angular.module('RiskManager.Controller', []);

app.controller('ContainerViewController', ['$scope', 'viewFactory', 'containerViewFactory', 'containerFactory', 'ModalService', 'commonFuncFactory',
    function ($scope, viewFactory, containerViewFactory, containerFactory, ModalService, commonFuncFactory) {

    getContainerViews();
    
    //Load select element with container views
    function getContainerViews() {
        viewFactory.getViews()
            .success(function (views) {
                $scope.containerViews = views;
            })
            .error(function (error) {
                console.log(error.message);
            });
    }

    //Create new view button action
    $scope.createView = function () {
        ModalService.showModal({
            templateUrl: "/Templates/AddNodeModal.html",
            controller: "addNode",
            inputs: {
                title: "Create Container View",
                description: false
            }
        }).then(function (modal) {
            modal.element.modal();
            modal.close.then(function (result) {

                var newView = {
                    name: result.name
                };

                viewFactory.createView(newView)
                    .success(function (view) {
                        $scope.containerViews.push(view);
                    })
                .error(function (error) {
                    console.log(error.message);
                });
            })
        });
    }

    //Get container view hierarchy for tree based on selected view from select element
    $scope.getContainerViewHierarchy = function () {

        //Clear selected node (if any) when switching views
        if ($scope.viewContainers != undefined) {
            commonFuncFactory.clearSelectedNode(
                $scope.containerTree.currentNode,
                $scope.mode
            );
        }

        containerViewFactory.getContainerViewHierarchy($scope.selectedView.viewID)
            .success(function (viewContainers) {
                if (viewContainers[0] != null){
                    $scope.viewContainers = viewContainers;
                } else {
                    $scope.viewContainers = [];
                }
            })
            .error(function (error) {
                console.log(error.message);
            });
    }

    //Create new container button action
    $scope.createContainerView = function () {
        ModalService.showModal({
            templateUrl: "/Templates/AddNodeModal.html",
            controller: "addNode",
            inputs: {
                title: "Create Container",
                description: false
            }
        }).then(function (modal) {
            modal.element.modal();
            modal.close.then(function (result) {
                createContainer(result.name);
            })
        });
    }
    
    //Add existing container button action
    $scope.addExistingContainerView = function(){
        
        containerFactory.getPossibleContainers($scope.selectedView.viewID)
            .success(function (containers) {
                ModalService.showModal({
                    templateUrl: "/Templates/AddExistingNodeModal.html",
                    controller: "addExisting",
                    inputs: {
                        title: "Select Existing Container",
                        nodes: containers
                    }
                }).then(function (modal) {
                    modal.element.modal();
                    modal.close.then(function (result) {
                        createContainer(result.name);
                    })
                })
            })
            .error(function (error) {
                console.log(error.message);
            });
      
    }

    function createContainer(name) {

        //Container views can have only one root
        if ($scope.viewContainers.length == 1 && ($scope.containerTree.currentNode == undefined || $scope.containerTree.currentNode.selected == undefined)) {
            alert('Please select a parent node');
            return;
        }

        //Create new container instance based on selected values
        var parentViewID = null;
        if ($scope.containerTree.currentNode && $scope.containerTree.currentNode.selected == 'selected') {
            parentViewID = $scope.containerTree.currentNode.nodeID
        }

        var newContainer = {
            name: name,
            viewID: $scope.selectedView.viewID,
            parentID: parentViewID
        };

        containerViewFactory.createContainer(newContainer.viewID, newContainer)
            .success(function (containerView) {
                //Create children array if it does not exist. This indicates the root
                //node is being created
                debugger;
                if (containerView.children == undefined) {
                    var children = [];
                    containerView["children"] = children;
                }
                //if not currentNode it is root
                if ($scope.containerTree.currentNode) {
                    $scope.containerTree.currentNode.children.push(containerView);
                }
                else {
                    //Verify model array exists before push
                    if ($scope.viewContainers == undefined) {
                        $scope.viewContainers = [];
                    }
                    $scope.viewContainers.push(containerView);
                }
            })
            .error(function (error) {
                console.log(error.message);
            });

        commonFuncFactory.clearSelectedNode(
            $scope.containerTree.currentNode,
            $scope.mode
        );
    }
    
    //Move container button action
    $scope.moveContainerView = function () {

        //Verify source node is selected
        if (!commonFuncFactory.verifySelectedNode($scope.containerTree.currentNode)) {
            return;
        }

        if ($scope.containerTree.currentNode.parentID == null) {
            alert('Cannot move root node');
            return;
        }

        var possibleParents =
             commonFuncFactory.getPossibleParents($scope.viewContainers, $scope.containerTree.currentNode.parentID);
      
        ModalService.showModal({
            templateUrl: "/Templates/MoveNodeModal.html",
            controller: "moveNode",
            inputs: {
                title: "Change Container Parent",
                possibleParents: possibleParents
            }
        }).then(function (modal) {
            modal.element.modal();
            modal.close.then(function (result) {

                //Create instance of container view to post to server with 
                //new parent
                var updatedContaner = {
                    nodeID: $scope.containerTree.currentNode.nodeID,
                    name: $scope.containerTree.currentNode.name,
                    viewID: $scope.selectedView.viewID,
                    parentID: result.nodeID
                }

                containerViewFactory.updateContainer(updatedContaner.viewID, updatedContaner)
                    .success(function () {
                        //Remove node from source parent in tree
                        commonFuncFactory.removeNodeFromTree(
                            $scope.viewContainers,
                            updatedContaner.nodeID);


                        //Add children to container view instance if any
                        if ($scope.containerTree.currentNode.children == undefined || $scope.containerTree.currentNode.children.length > 0) {
                            var children = [];
                            updatedContaner["children"] = $scope.containerTree.currentNode.children;
                        }

                       
                        //Add node to target parent in tree
                        commonFuncFactory.addNodeToTree(
                            $scope.viewContainers,
                            updatedContaner);
                    })
                    .error(function (error) {
                        console.log(error.message);
                    });
            });
        });

        commonFuncFactory.clearSelectedNode(
            $scope.containerTree.currentNode,
            $scope.mode
        );

    }

    //delete container button action
    $scope.deleteContainerView = function () {
        
        //Get selected values
        var viewID = $scope.selectedView.viewID;
        var containerViewID = $scope.containerTree.currentNode.nodeID;

        containerViewFactory.deleteContainer(viewID, containerViewID)
            .success(function () {
                //Remove node from tree
                commonFuncFactory.removeNodeFromTree(
                    $scope.viewContainers,
                    containerViewID
                );
            })
            .error(function (error) {
                console.log(error.message);
            });

        commonFuncFactory.clearSelectedNode(
            $scope.containerTree.currentNode,
            $scope.mode
        );
    }
    
}]);

//Modal controllers
app.controller('moveNode', ['$scope', '$element', 'title', 'possibleParents', 'close', 
  function ($scope, $element, title, possibleParents, close) {
    $scope.title = title;
    $scope.parents = possibleParents;
        
    $scope.close = function() {
        close({
            nodeID: $scope.selectedParent.nodeID
        }, 500); 
    };
  }]);

app.controller('addNode', ['$scope', '$element', 'title', 'description', 'close',
  function ($scope, $element, title, description, close) {
      $scope.title = title;
      $scope.name = null;
      $scope.description = null;
      $scope.showDescription = description;

      $scope.close = function () {
          close({
              name: $scope.name,
              description: $scope.description
          }, 500);
      };
  }]);

app.controller('addExisting', ['$scope', '$element', 'title', 'nodes', 'close',
  function ($scope, $element, title, nodes, close) {

      $scope.title = title;
      $scope.nodes = nodes;

      $scope.close = function () {
          close({
              name: $scope.selectedNode.name
          }, 500);
      };
  }]);


app.controller('DefectGroupController', ['$scope', 'defectGroupFactory', 'ModalService', 'commonFuncFactory',
    function ($scope, defectGroupFactory, ModalService, commonFuncFactory) {

    getGroups();

    function getGroups() {
        defectGroupFactory.getGroups()
            .success(function (groups) {
                $scope.viewGroups = groups;
            })
            .error(function (error) {
                console.log(error.message);
            });
    }

    $scope.createGroup = function () {
        ModalService.showModal({
            templateUrl: "/Templates/AddNodeModal.html",
            controller: "addNode",
            inputs: {
                title: "Create Defect Group",
                description: true
            }
        }).then(function (modal) {
            modal.element.modal();
            modal.close.then(function (result) {

                var parentGroupID = null;
                if ($scope.groupTree.currentNode && $scope.groupTree.currentNode.selected == 'selected') {
                    parentGroupID = $scope.groupTree.currentNode.nodeID
                }

                var newGroup = {
                    name: result.name,
                    description: result.description,
                    parentID : parentGroupID
                };

                defectGroupFactory.createGroup(newGroup)
                    .success(function (group) {
                        delete group['description']
                        if ($scope.groupTree.currentNode) {
                            $scope.groupTree.currentNode.children.push(group);
                        }
                        else {
                            //Verify model array exists before push
                            if ($scope.viewGroups == undefined) {
                                $scope.viewGroups = [];
                            }
                            $scope.viewGroups.push(group);
                        }
                    })
                .error(function (error) {
                    console.log(error.message);
                });
            })
        });
    }

    $scope.moveGroup = function () {

        //Verify source node is selected
        if (!commonFuncFactory.verifySelectedNode($scope.groupTree.currentNode)) {
            return;
        }

        var possibleParents =
             commonFuncFactory.getPossibleParents($scope.viewGroups, $scope.groupTree.currentNode.parentID);

        ModalService.showModal({
            templateUrl: "/Templates/MoveNodeModal.html",
            controller: "moveNode",
            inputs: {
                title: "Change Group Parent",
                possibleParents: possibleParents
            }
        }).then(function (modal) {
            modal.element.modal();
            modal.close.then(function (result) {

                //Create instance of group to post to server with 
                //new parent
                var updatedGroup = {
                    nodeID: $scope.groupTree.currentNode.nodeID,
                    name: $scope.groupTree.currentNode.name,
                    parentID: result.nodeID
                }

                defectGroupFactory.moveGroup(updatedGroup)
                    .success(function () {

                        //Remove node from source parent in tree
                        commonFuncFactory.removeNodeFromTree(
                            $scope.viewGroups,
                            updatedGroup.nodeID);

                        //Add children to container view instance if any
                        if ($scope.groupTree.currentNode.children == undefined || $scope.groupTree.currentNode.children.length > 0) {
                            var children = [];
                            updatedGroup["children"] = $scope.groupTree.currentNode.children;
                        }

                        //Add node to target parent in tree
                        commonFuncFactory.addNodeToTree(
                            $scope.viewGroups,
                            updatedGroup);
                    })
                    .error(function (error) {
                        console.log(error.message);
                    });
            });
        });

        commonFuncFactory.clearSelectedNode(
            $scope.groupTree.currentNode,
            $scope.mode
        );
    }

    $scope.deleteGroup = function () {
        //Get selected values
        var defectGroupID = $scope.groupTree.currentNode.nodeID;

        defectGroupFactory.deleteGroup(defectGroupID)
            .success(function () {
                //Remove node from tree
                commonFuncFactory.removeNodeFromTree(
                    $scope.viewGroups,
                    defectGroupID
                );
            })
            .error(function (error) {
                console.log(error.message);
            });

        commonFuncFactory.clearSelectedNode(
            $scope.groupTree.currentNode,
            $scope.mode
        );
    }
}]);

app.controller('ObjectTypeController', ['$scope', function ($scope) {

}]);