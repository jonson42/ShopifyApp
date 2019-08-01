var app = angular.module("crewcloud", ["ui.bootstrap"]);
app.service("$ajax", function ($http) {
    this.url = "/api/shopify/";
    this.getDetails = function (orderId) {
        var temp = this.url + "getAbandonedDetails?idProduct=" + orderId;
        return $http.get(temp, orderId);
    };
});
app.factory("$share", function ($rootScope) {
    return {
        callReadInfo: function (no) { $rootScope.$broadcast("list:readinfo", no); },
        onReadInfo: function ($scope, handler) { $scope.$on("list:readinfo", function (event, no) { handler(no); }); }
    };
});

app.controller("detailsCtr", function ($scope, $ajax, $queryStr) {
    debugger;
    $scope.listItem;
    var orderId = parseInt($queryStr.search().id);
    $scope.GetDetails = function () {
        $ajax.getDetails(orderId).then(function response(success) {
            debugger;
            var listLineItem = [];
            angular.forEach(success.data.line_items, function (item) {
                item.title = item.title.replace("�", "-");
                listLineItem.push(item);
            });
            success.data.line_items = listLineItem;
            $scope.listItem = success.data;
            $scope.itemDetails = success.data;
        }, function error(error) {
            console.log("Roi roi do");
        });
    };
    $scope.GetDetails();
});