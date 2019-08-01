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
});
app.factory("$share", function ($rootScope) {
    return {
        callReadInfo: function (no) { $rootScope.$broadcast("list:readinfo", no); },
        onReadInfo: function ($scope, handler) { $scope.$on("list:readinfo", function (event, no) { handler(no); }); }
    };
});

app.controller("mainCtr", function ($scope, $ajax) {
    $scope.HostList = [];
    
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
});