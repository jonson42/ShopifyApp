var app = angular.module("crewcloud", ["ui.bootstrap"]);
app.service("$ajax", function ($http) {
    this.url = "/api/shopify/";
    this.getShopify = function (_listSeq) {
        var temp = this.url + "getShopify";
        return $http.get(temp);
    };
    this.url = "/api/shopify/";
    this.getDetails = function (orderId) {
        var temp = this.url + "getDetails?idProduct=" + orderId;
        return $http.get(temp, orderId);
    };
    this.sendEmailRefunds = function (dataEmail) {
        var temp = this.url + "sendEmailRefunds";
        return $http.post(temp, dataEmail);
    };
    
});
app.factory("$share", function ($rootScope) {
    return {
        callReadInfo: function (no) { $rootScope.$broadcast("list:readinfo", no); },
        onReadInfo: function ($scope, handler) { $scope.$on("list:readinfo", function (event, no) { handler(no); }); }
    };
});

app.controller("mainCtr", function ($scope, $ajax, $queryStr) {
    $scope.orderId = parseInt($queryStr.search().id);
    $scope.moneyRefund = "0";
    $scope.limitMoney = 0;
    $scope.GetDetails = function () {
        $ajax.getDetails($scope.orderId).then(function response(success) {
            $scope.itemDetails = success.data;
            if (success.data.shipping_lines.length == 0) {
                $scope.flagRefunds = true;
            } else {
                $scope.limitMoney = success.data.shipping_lines[0].discounted_price;
            }
        }, function error(error) {
            console.log("Roi roi do");
        });
    };
    $scope.GetDetails();
    $scope.ChangeMoney = function (value) {
        if ($scope.limitMoney < parseInt(value.moneyRefund)) {
            value.moneyRefund = 0;
            $scope.moneyRefund = 0;
        } else {
            $scope.moneyRefund = value.moneyRefund;
        }

    };
    $scope.SendRefunds = function () {
        var dataEmail = {
            MoneyRefunds: $scope.moneyRefund,
            Order: $scope.itemDetails, 
            Email: $scope.itemDetails.email
        };

        $ajax.sendEmailRefunds(dataEmail).then(function response(success) {
            location.href = "/";
        }, function error(error) {
            console.log("Roi roi do");
        });
    }
});