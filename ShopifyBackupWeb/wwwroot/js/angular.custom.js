// textbox 에서 엔터 입력시 호출될 메서드 설정
app.directive('enter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.enter);
                });
                event.preventDefault();
            }
        });
    };
});

// img tag src 파일 경로 없을 경우 기본 이미지 설정
app.directive('errSrc', function () {
    return {
        link: function (scope, element, attrs) {
            element.bind('error', function () {
                if (attrs.src !== attrs.errSrc) {
                    attrs.$set('src', attrs.errSrc);
                }
            });
        }
    };
});

// textbox에서 숫자만 입력해야할 경우 설정
app.directive('numberOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            function fromUser(text) {
                if (text) {
                    var transformedInput = String(text).replace(/[^0-9]/g, '');
                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return undefined;
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});

// textarea 에서 한글 입력 바인딩 문제 발생시 설정 (textarea에서 자동 설정됨)
app.directive('textarea', ['$parse', function ($parse) {
    return {
        priority: 2,
        restrict: 'E',
        compile: function (element) {
            element.on('compositionstart', function (e) {
                e.stopImmediatePropagation();
            });
        }
    };
}]);

// textbox 한글 입력 바인딩 문제 발생시 설정
app.directive('krInput', ['$parse', function ($parse) {
    return {
        priority: 2,
        restrict: 'A',
        compile: function (element) {
            element.on('compositionstart', function (e) {
                e.stopImmediatePropagation();
            });
        }
    };
}]);

// textbox에 자동으로 포커스 입력시 설정
app.directive('focusOn', function ($timeout) {
    return {
        restrict: 'A',
        link: function ($scope, element, $attr) {
            $scope.$watch($attr.focusOn, function (_focusVal) {
                $timeout(function () {
                    _focusVal ? element[0].focus() :
                        element[0].blur();
                });
            });
        }
    };
});

// text에 숫자에 3자리마다 콤마를 넣고 싶을 때 사용
app.filter('comma', function () {
    return function (x) {
        return (x !== undefined && x !== null) ? parseInt(x).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") : "";
    };
});

// text에 숫자에 3자리마다 콤마를 넣고 싶을 때 사용
app.filter('utc', function () {
    return function (x) {
        return (x !== undefined && x !== null) ? eval(x.replace(/\/Date\((.*?)\)\//gi, "new Date($1)")) : new Date();
    };
});

// html 문자열에서 태그 속성 제거시키지 않을 때 사용
app.filter('render', function () { return function (value) { return value + ''; }; });
app.filter('trustAsHtml', function ($sce) { return $sce.trustAsHtml; });

// ajax 비동기 통신 시 전역 메시지 출력 (차후 레이어 팝업은 변경 필요)app.config(['$httpProvider', function ($httpProvider) {
var loadingInterval = null;
var loadingFlag = false;
app.factory('ajaxInterceptor', function ($q) {
    var interceptor = {
        'request': function (config) {
            if (loadingInterval == null) {
                loadingInterval = setInterval(function () {
                    if (loadingInterval != null && loadingFlag == false) {
                        clearInterval(loadingInterval);
                        loadingInterval = null;
                        loading.hidePleaseWait();
                    } else {
                        loading.showPleaseWait();
                    }
                }, 1000);
            }
            loadingFlag = true;
            return config; // or $q.when(config);
        },
        'response': function (response) {
            loadingFlag = false;
            // successful response
            return response; // or $q.when(config);
        },
        'requestError': function (rejection) {
            loadingFlag = false;
            // an error happened on the request
            // if we can recover from the error
            // we can return a new request
            // or promise
            return response; // or new promise
            // Otherwise, we can reject the next
            // by returning a rejection
            // return $q.reject(rejection);
        },
        'responseError': function (rejection) {
            loadingFlag = false;
            // an error happened on the request
            // if we can recover from the error
            // we can return a new response
            // or promise
            return rejection; // or new promise
            // Otherwise, we can reject the next
            // by returning a rejection
            // return $q.reject(rejection);
        }
    };
    return interceptor;
});

// 쿼리스트링 가져올때 사용
app.factory("$queryStr", function ($window, $location) {
    function search() {
        var left = $window.location.search
            .split(/[&||?]/)
            .filter(function (x) { return x.indexOf("=") > -1; })
            .map(function (x) { return x.split(/=/); })
            .map(function (x) {
                x[1] = x[1].replace(/\+/g, " ");
                return x;
            })
            .reduce(function (acc, current) {
                acc[current[0]] = current[1];
                return acc;
            }, {});

        var right = $location.search() || {};

        var leftAndRight = Object.keys(right)
            .reduce(function (acc, current) {
                acc[current] = right[current];
                return acc;
            }, left);

        return leftAndRight;
    }

    return { search: search };
});