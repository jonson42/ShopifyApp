var app = angular.module("crewcloud", ["ui.bootstrap"]);
app.service("$ajax", function ($http) {
    this.url = "/api/shopify/";
    this.register = function (user) {
        var temp = this.url + "register";
        return $http.post(temp, user);
    };
   
});
app.factory("$share", function ($rootScope) {
    return {
        callReadInfo: function (no) { $rootScope.$broadcast("list:readinfo", no); },
        onReadInfo: function ($scope, handler) { $scope.$on("list:readinfo", function (event, no) { handler(no); }); }
    };
});

app.controller("mainCtr", function ($scope, $ajax) {
    $scope.Register = function () {
        var user = {
            User: $scope.User,
            Password: $scope.Password,
            Shop: $scope.Shop,
            AppId: $scope.AppId,
            AppPass: $scope.AppPass
        }
        $ajax.register(user).then(function response(success) {
            location.href="/Login";
        }, function error(error) {
            console.log("Roi roi do");
        });
    };
});