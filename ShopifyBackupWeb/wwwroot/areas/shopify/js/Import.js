var app = angular.module("crewcloud", ["ngFileUpload","ui.bootstrap"]);
app.service("$ajax", function ($http) {
    this.url = "/api/shopify/";
    this.getShopify = function (_listSeq) {
        var temp = this.url + "getShopify";
        return $http.get(temp);
    };

});
app.factory("$share", function ($rootScope) {
    return {
        callReadInfo: function (no) { $rootScope.$broadcast("list:readinfo", no); },
        onReadInfo: function ($scope, handler) { $scope.$on("list:readinfo", function (event, no) { handler(no); }); }
    };
});

app.controller("mainCtr", function ($scope, $ajax, Upload) {
    $scope.SendEmail = function () {
        $ajax.sendEMail().then(function response(success) {
            console.log("Nghiem dep trai qua" + success);
        }, function error(error) {
            console.log("Roi roi do");
        });
    };
    $scope.uploadFiles = function (files) {

        if (files && files.length) {
            for (var i = 0; i < files.length; i++) {
                $scope.upload(files[i]);
            }
        }
    };
function _makeGuid() {
        function k() {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
        };

        return (k() + k() + k() + k() + k() + k() + k() + k()).toLowerCase();
    };
$scope.listExcel =[];
    $scope.upload = function (file) {
        var guid = _makeGuid();
        Upload.upload({
            url: '/api/shopify/importExcel',
            data: { file: file, guid: guid }
        }).then(function (resp) {
            $scope.listExcel=resp.data;
        }, function (response) {

        });
    };
});