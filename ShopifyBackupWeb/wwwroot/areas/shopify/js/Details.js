﻿var app = angular.module("crewcloud", ["ui.bootstrap"]);
app.service("$ajax", function ($http) {
    this.url = "/api/shopify/";
    this.getDetails = function (orderId) {
        var temp = this.url + "getDetails?idProduct=" + orderId;
        return $http.get(temp, orderId);
    };
    
    this.sendEmailFullField = function (dataEmail) {
        var temp = this.url + "sendEmailFullField";
        return $http.post(temp, dataEmail);
    };
});
app.factory("$share", function ($rootScope) {
    return {
        callReadInfo: function (no) { $rootScope.$broadcast("list:readinfo", no); },
        onReadInfo: function ($scope, handler) { $scope.$on("list:readinfo", function (event, no) { handler(no); }); }
    };
});

app.controller("detailsCtr", function ($scope, $ajax, $queryStr) {
    $scope.itemDetails;
    $scope.trackingNumber = "";
    $scope.trackingCarrier = "";
    $scope.trackingUrl = "";
    $scope.flagMark = false;
    var orderId = parseInt($queryStr.search().id);
    $scope.GetDetails = function () {
        $ajax.getDetails(orderId).then(function response(success) {
            $scope.itemDetails = success.data;
        }, function error(error) {
            console.log("Roi roi do");
        });
    };
    $scope.GetDetails();

    $scope.MarkFulfiel = function () {
        if ($scope.trackingNumber === "") {
            $scope.flagTracking = true;
        } else {
            $scope.trackingNumber = "";
            $scope.trackingCarrier = "";
            $scope.trackingUrl = "";
            var dataEmail = {
                name: $scope.itemDetails.name,
                email: $scope.itemDetails.email,
                listItem: $scope.itemDetails.line_items
            }
            $ajax.sendEmailFullField(dataEmail).then(function response(success) {
                debugger;
            }, function error(error) {
                console.log("Roi roi do");
            });
            //location.href = "/";
            $scope.flagMark = true;

        }
        
    }
});