﻿var app = angular.module("crewcloud", ["ui.bootstrap"]);
app.service("$ajax", function ($http) {
    this.url = "/api/shopify/";
    this.getShopify = function (_listSeq) {
        var temp = this.url + "getShopify";
        return $http.get(temp);
    };
    this.sendEMail = function () {
        var temp = this.url + "sendEmail";
        
        return $http.get(temp);
    };
    this.exportExcel = function () {
        var temp = this.url + "exportExcel";

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
    $scope.listOrder = [];
    $scope.GetShopifyData = function () {
        $ajax.getShopify().then(function response(success) {
            $scope.listOrder = success.data;
        }, function error(error) {
            console.log("Index.js link function GetShopifyData !" + error.ToString());
        });
    };
    $scope.GetShopifyData();
    $scope.showDetails = function (id) {
        debugger;
        window.location="/Home/Details?id=" + id;
    };
    $scope.ExportExcel = function () {
        $ajax.exportExcel().then(function response(success) {
            console.log("Export excel successfully !");
        }, function error(error) {
            console.log("Export error !");
        });
    };

});