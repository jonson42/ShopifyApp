// 각 페이지 스크립트의 상단에 복사해 넣어서 사용하세요.
var app = angular.module("crewcloud", []).run(function ($rootScope) {
    // AngularJS 컨트롤러 전역에서 사용하는 변수 선언
}).config(function ($httpProvider) {
    // 비동기 통신시 로딩 처리
    $httpProvider.interceptors.push('ajaxInterceptor');
});