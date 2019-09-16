var app = angular.module("crewcloud", ["ui.bootstrap"]);
app.service("$ajax", function ($http) {
    this.url = "/api/shopify/";

    this.updateHost = function (listHost) {
        var temp = this.url + "updateHost";
        return $http.post(temp, listHost);
    };
    this.getDefaultHost = function () {
        var temp = this.url + "getDefaultHost";
        return $http.get(temp);
    };
    this.updateSite = function (listHost) {
        var temp = this.url + "updateSite";
        return $http.post(temp, listHost);
    };
    this.getSiteDefault = function () {
        var temp = this.url + "getSiteDefault";
        return $http.get(temp);
    };
    this.getCollectionDefault = function () {
        var temp = this.url + "getCollectionDefault";
        return $http.get(temp);
    };
    this.updateCollection = function (listCollection) {
        var temp = this.url + "updateCollection";
        return $http.post(temp, listCollection);
    };

    this.getDefaultEmail = function () {
        var temp = this.url + "getDefaultEmail";
        return $http.get(temp);
    };
    this.updateEmail = function (itemEmail) {
        var temp = this.url + "updateEmail";
        return $http.post(temp, itemEmail);
    };

    this.getDefaultDNS = function () {
        var temp = this.url + "getDefaultDNS";
        return $http.get(temp);
    };
    this.updateDNS = function (itemEmail) {
        var temp = this.url + "updateDNS";
        return $http.post(temp, itemEmail);
    };
    this.getEmailContacts = function () {
        var temp = this.url + "getEmailContacts";
        return $http.get(temp);
    };
    this.updateEmailContacts = function (itemEmail) {
        var temp = this.url + "updateEmailContacts";
        return $http.post(temp, itemEmail);
    };
    //Shop name:
    this.getShopName = function () {
        var temp = this.url + "getShopName";
        return $http.get(temp);
    };
    this.updateShopNameDefault = function (itemEmail) {
        var temp = this.url + "updateShopNameDefault";
        return $http.post(temp, itemEmail);
    };
});
app.factory("$share", function ($rootScope) {
    return {
        callReadInfo: function (no) { $rootScope.$broadcast("list:readinfo", no); },
        onReadInfo: function ($scope, handler) { $scope.$on("list:readinfo", function (event, no) { handler(no); }); }
    };
});

app.controller("mainCtr", function ($scope, $ajax) {
    $scope.HostList = [];
    $scope.CollectionList = [];
    $scope.SiteList = [];
    $scope.AddHost = function () {
        var item = {
            name: ""
        }
        $scope.HostList.push(item);
    };
    $scope.RemoveLink = function (index, indexHost) {
        $scope.HostList.splice(index, 1);
    };

    $scope.GetHostDefault = function () {
        $ajax.getDefaultHost().then(function response(success) {
            $scope.HostList = success.data;
        }, function error(error) {
            console.log("Export error !" );
        });
    };
    $scope.GetHostDefault();
    
    
    $scope.SaveHost = function () {
        $ajax.updateHost($scope.HostList).then(function response(success) {
            alert("Save host successfully !");
        }, function error(error) {
                alert("Save host error !");
        });

    };

    $scope.GetCollectionDefault = function () {
        $ajax.getCollectionDefault().then(function response(success) {
            if (success.data !== "") {
                $scope.CollectionList = success.data;
            }
        }, function error(error) {
            console.log("Export error !");
        });
    };
    $scope.GetCollectionDefault();
    $scope.AddCollection = function () {
        var item = {
            name: ""
        }
        $scope.CollectionList.push(item);
    };
    $scope.SaveCollection = function () {
        $ajax.updateCollection($scope.CollectionList).then(function response(success) {
            alert("Save collection successfully !");
        }, function error(error) {
            alert("Save host error !");
        });
    };
    
    $scope.RemoveColection = function (index, indexHost) {
        $scope.CollectionList.splice(index, 1);
    };

    $scope.GetSiteDefault = function () {
        $ajax.getSiteDefault().then(function response(success) {
            if (success.data !== "") {
                $scope.SiteList = success.data;
            }
        }, function error(error) {
            console.log("Export error !");
        });
    };
    $scope.GetSiteDefault();
    $scope.AddSite = function () {
        var item = {
            site: "",
            appId: "",
            appPass: ""
        }
        $scope.SiteList.push(item);
    };
    $scope.SaveSite = function () {
        $ajax.updateSite($scope.SiteList).then(function response(success) {
            alert("Save collection successfully !");
        }, function error(error) {
            alert("Save host error !");
        });
    };

    $scope.RemoveSite = function (index, indexHost) {
        $scope.SiteList.splice(index, 1);
    };

    $scope.GetDefaultEmail = function () {
        $ajax.getDefaultEmail().then(function response(success) {
            if (success.data !== "") {
                $scope.email = success.data;
            }
        }, function error(error) {
            console.log("Export error !");
        });
    };
    $scope.SaveEmailDefault = function () {
        
        $ajax.updateEmail($scope.email).then(function response(success) {
            alert("Save Email successfully !");
        }, function error(error) {
            alert("Save host error !");
        });
    };
    $scope.GetDefaultEmail();
    $scope.GetDefaultDNS = function () {
        $ajax.getDefaultDNS().then(function response(success) {
            if (success.data !== "") {
                $scope.dns = success.data;
            }
        }, function error(error) {
            console.log("Export error !");
        });
    };
    $scope.GetDefaultDNS();
    $scope.SaveDNSDefault = function () {
        
        $ajax.updateDNS($scope.dns).then(function response(success) {
            alert("Save DNS successfully !");
        }, function error(error) {
            alert("Save host error !");
        });
    };

    $scope.GetEmailContacts = function () {
        $ajax.getEmailContacts().then(function response(success) {
            if (success.data !== "") {
                $scope.EmailContacts = success.data;
            }
        }, function error(error) {
            console.log("Export error !");
        });
    };
    $scope.GetEmailContacts();
    $scope.SaveEmailContactsDefault = function () {
        $ajax.updateEmailContacts($scope.EmailContacts).then(function response(success) {
            alert("Save Email Contacts successfully !");
        }, function error(error) {
            alert("Save host error !");
        });
    };
    //SaveShopNameDefault
    $scope.GetShopName = function () {
        $ajax.getShopName().then(function response(success) {
            if (success.data !== "") {
                $scope.shop = success.data;
            }
        }, function error(error) {
            console.log("Export error !");
        });
    };
    $scope.GetShopName();
    $scope.SaveShopNameDefault = function () {
        $ajax.updateShopNameDefault($scope.shop).then(function response(success) {
            alert("Save Shop name successfully !");
        }, function error(error) {
            alert("Save host error !");
        });
    };
});