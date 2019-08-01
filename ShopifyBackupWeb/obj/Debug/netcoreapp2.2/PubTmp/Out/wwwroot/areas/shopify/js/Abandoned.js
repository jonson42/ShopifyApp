var app = angular.module("crewcloud", ["ui.bootstrap"]);
app.service("$ajax", function ($http) {
    this.url = "/api/shopify/";
    this.getShopify = function (_listSeq) {
        var temp = this.url + "getShopify";
        return $http.get(temp);
    };
    this.getAbandoned = function () {
        var temp = this.url + "getAbandoned";
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
    $scope.SendEmail = function () {
        $ajax.sendEMail().then(function response(success) {
            console.log("Nghiem dep trai qua" + success);
        }, function error(error) {
            console.log("Roi roi do");
        });
    };
    $scope.listAbandoned = [];
    $scope.List = function () {
        $ajax.getAbandoned().then(function response(success) {
            debugger;
            $scope.listAbandoned = success.data;
            
        }, function error(error) {
            console.log("Error get abandoned !");
        });
    };
    $scope.List();
});