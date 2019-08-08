var app = angular.module("crewcloud", ["ui.bootstrap"]);
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
    this.exportExcel = function (listProduct) {
        var temp = this.url + "exportExcel";

        return $http.post(temp, listProduct);
    };
});
app.factory("$share", function ($rootScope) {
    return {
        callReadInfo: function (no) { $rootScope.$broadcast("list:readinfo", no); },
        onReadInfo: function ($scope, handler) { $scope.$on("list:readinfo", function (event, no) { handler(no); }); }
    };
});

app.controller("mainCtr", function ($scope, $ajax) {
    $scope.totalItems = 0;
    $scope.viewCount = 10;
    $scope.currentPageIndex = 1;
    $scope.numPages = 5;
    $scope.pageChanged = function () {

    };
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
            $scope.totalItems = success.data.length;
        }, function error(error) {
            console.log("Index.js link function GetShopifyData !" + error.ToString());
        });
    };
    $scope.SearchText = function () {
        var check = [];
        if ($scope.search.value == -1) {
            for (var i = 0; i < $scope.listOrder.length; i++) {
                if ($scope.listOrder[i].order.includes($scope.search.value) || $scope.listOrder[i].customer.includes($scope.search.value)
                    || $scope.listOrder[i].email.includes($scope.search.value)
                    || $scope.listOrder[i].shippingAddress.address1.includes($scope.search.value)) {
                    check.push($scope.listOrder[i]);
                }
                angular.forEach($scope.listOrder.line_items, function (item) {
                    if (item.title.includes($scope.search.value)) {
                        check.push($scope.listOrder[i]);
                    }
                });
            }
        } else {
            var startIndex = $scope.search.value.split("->")[0];
            var endIndex = $scope.search.value.split("->")[1];
            angular.forEach($scope.listOrder, function (item) {
                if (item.stt >= startIndex && item.stt <= endIndex) {
                    check.push(item);
                }
            });
            
        }
        $scope.listOrder = check;
    };
    $scope.GetShopifyData();
    $scope.showDetails = function (id) {
        debugger;
        window.location="/Home/Details?id=" + id;
    };
    $scope.CheckAll = function () {
        angular.forEach($scope.listOrder, function (item) {
            if (!$scope.selectedAll.value) {
                item.checkvalue = true;
            } else {
                item.checkvalue = false;
            }
        })
    };
    $scope.ExportExcel = function () {
        $scope.listProduct = [];
        angular.forEach($scope.listOrder, function (item) {
            angular.forEach(item.listProduct, function (itemSub) {
                if (item.checkvalue) {
                    $scope.listProduct.push(itemSub);
                }
            });
        });
        debugger;
        $ajax.exportExcel($scope.listProduct).then(function response(success) {
            window.open("/Data_Init/Out/ExportOut.xlsx","_blank");
        }, function error(error) {
            console.log("Export error !");
        });
    };

});