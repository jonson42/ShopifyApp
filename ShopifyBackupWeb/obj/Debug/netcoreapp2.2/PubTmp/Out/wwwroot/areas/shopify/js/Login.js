var app = angular.module("crewcloud", ["ui.bootstrap"]);
app.service("$ajax", function ($http) {
    this.url = "/api/shopify/";

    this.login = function (listHost) {
        var temp = this.url + "Login";
        return $http.post(temp, listHost);
    };
});
app.factory("$share", function ($rootScope) {
    return {
        callReadInfo: function (no) { $rootScope.$broadcast("list:readinfo", no); },
        onReadInfo: function ($scope, handler) { $scope.$on("list:readinfo", function (event, no) { handler(no); }); }
    };
});

app.controller("mainCtr", function ($scope, $ajax) {
    $scope.Login = function () {
        var user = {
            User: $scope.User,
            Password: $scope.Password
        }
        $ajax.login(user).then(function response(success) {
           
            if (success.data === true) {
                location.href = "/Home";
            } else {
                alert("User and password incorrect !");
            }
        }, function error(error) {
                alert("Error from server ");
        });
    }
});