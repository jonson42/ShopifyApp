var app = angular.module("crewcloud", ["ui.bootstrap"]);
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
    $scope.orderId = parseInt($queryStr.search().id);
    $scope.GetDetails = function () {
        $ajax.getDetails($scope.orderId).then(function response(success) {
            $scope.itemDetails = success.data;
        }, function error(error) {
            console.log("Roi roi do");
        });
    };
    $scope.GetDetails();
    $scope.tracking = {
        Number: "",
        Carrier: "",
        Url:""
    }
    $scope.MarkFulfiel = function () {
        if ($scope.tracking.Number === "") {
            $scope.flagTracking = true;
        } else {
            $scope.tracking.Number = "";
            $scope.tracking.Carrier = "";
            $scope.tracking.Url = "";
            var dataEmail = {
                name: $scope.itemDetails.name,
                email: $scope.itemDetails.email,
                listItem: $scope.itemDetails.line_items,
                Order: $scope.itemDetails,
                TrackingUrl: $scope.tracking.Url + "/" + $scope.tracking.Number
            };
            debugger;
            $ajax.sendEmailFullField(dataEmail).then(function response(success) {
                location.href = "/";
            }, function error(error) {
                console.log("Roi roi do");
            });
            //location.href = "/";
            $scope.flagMark = true;
        }
    }
});