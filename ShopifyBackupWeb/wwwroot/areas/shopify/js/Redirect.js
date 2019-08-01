var app = angular.module("crewcloud", ["ui.bootstrap"]);
app.service("$ajax", function ($http) {
    this.url = "/api/shopify/";

    this.updateHost = function (listHost) {
        var temp = this.url + "updateHost";
        return $http.post(temp, listHost);
    };
    this.getDefaultHost = function () {
        var temp = this.url + "getDefaultHost";
        return $http.get(temp);
    };
    this.getCollectionDefault = function () {
        var temp = this.url + "getCollectionDefault";
        return $http.get(temp);
    };
    this.updateCollection = function (listCollection) {
        var temp = this.url + "updateCollection";
        return $http.post(temp, listCollection);
    };
});
app.factory("$share", function ($rootScope) {
    return {
        callReadInfo: function (no) { $rootScope.$broadcast("list:readinfo", no); },
        onReadInfo: function ($scope, handler) { $scope.$on("list:readinfo", function (event, no) { handler(no); }); }
    };
});

app.controller("mainCtr", function ($scope, $ajax) {
    $scope.HostList = [];
    $scope.CollectionList = [];
    
    $scope.AddHost = function () {
        var item = {
            name: ""
        }
        $scope.HostList.push(item);
    };
    $scope.RemoveLink = function (index, indexHost) {
        $scope.HostList.splice(index, 1);
    };

    $scope.GetHostDefault = function () {
        $ajax.getDefaultHost().then(function response(success) {
            $scope.HostList = success.data;
        }, function error(error) {
            console.log("Export error !" );
        });
    };
    $scope.GetHostDefault();
    
    
    $scope.SaveHost = function () {
        $ajax.updateHost($scope.HostList).then(function response(success) {
            alert("Save host successfully !");
        }, function error(error) {
                alert("Save host error !");
        });

    };

    $scope.GetCollectionDefault = function () {
        $ajax.getCollectionDefault().then(function response(success) {
            if (success.data != "") {
                $scope.CollectionList = success.data;
            }
        }, function error(error) {
            console.log("Export error !");
        });
    };
    $scope.GetCollectionDefault();
    $scope.AddCollection = function () {
        var item = {
            name: ""
        }
        $scope.CollectionList.push(item);
    };
    $scope.SaveCollection = function () {
        $ajax.updateCollection($scope.CollectionList).then(function response(success) {
            alert("Save collection successfully !");
        }, function error(error) {
            alert("Save host error !");
        });
    };
    
    $scope.RemoveColection = function (index, indexHost) {
        $scope.CollectionList.splice(index, 1);
    };
});