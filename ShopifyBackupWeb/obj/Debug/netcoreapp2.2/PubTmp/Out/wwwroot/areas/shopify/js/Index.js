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
        if ($scope.search.value!== "") {
            var check = [];
            if ($scope.search.value.search("->") === -1) {
                for (var i = 0; i < $scope.listOrder.length; i++) {
                    var address = $scope.listOrder[i].shippingAddress === null || $scope.listOrder[i].shippingAddress.address1 === null ? "" : $scope.listOrder[i].shippingAddress.address1;
                    var address2 = $scope.listOrder[i].shippingAddress === null || $scope.listOrder[i].shippingAddress.address2 === null ? "" : $scope.listOrder[i].shippingAddress.address2;
                    var city = $scope.listOrder[i].shippingAddress === null || $scope.listOrder[i].shippingAddress.city === null ? "" : $scope.listOrder[i].shippingAddress.city;
                    var email = $scope.listOrder[i].email;
                    var order = $scope.listOrder[i].order.toLowerCase();
                    var customer = $scope.listOrder[i].customer.toLowerCase();
                    email = email.toLowerCase();
                    var textSearch = $scope.search.value.toLowerCase();
                    address = address.toLowerCase();
                    address2 = address2.toLowerCase();
                    city = city.toLowerCase();
                    if (order.includes(textSearch) || customer.includes(textSearch)
                        || email.includes(textSearch)
                        || address.includes(textSearch) || address2.includes(textSearch)
                        || city.includes(textSearch)) {
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
                    if (parseInt(item.stt) >= parseInt(startIndex) && parseInt(item.stt) <= parseInt(endIndex)) {
                        check.push(item);
                    }
                });

            }
            $scope.listOrder = check;
        } else {
            $scope.GetShopifyData();
        }
        
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