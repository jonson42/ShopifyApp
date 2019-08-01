// 날짜 변경
Date.prototype.addDays = function (days) {
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() + days);
    return dat;
}

// 월 변경
Date.prototype.addMonths = function (months) {
    var dat = new Date(this.valueOf());
    dat.setMonth(dat.getMonth() + months);
    return dat;
}

// 전역 레이어 메시지
function showMessage(title, message) {
    $.gritter.add({
        // (string | mandatory) the heading of the notification
        title: (title === undefined || title === null || title === "") ? "정보" : title,
        // (string | mandatory) the text inside the notification
        text: message
    });
}

// 전역 에러 메시지
function errMessage(title, message) {
    $.gritter.add({
        // (string | mandatory) the heading of the notification
        title: (title === undefined || title === null || title === "") ? "정보" : title,
        // (string | mandatory) the text inside the notification
        text: message
    });
    setTimeout(function () { $.gritter.removeAll(); }, 3000);
}

// 전역 메시지 레이어 삭제
function hideMessage() {
    $.gritter.removeAll();
}

// C# DateTime 형식으로 변환 비동기 데이터 전송시 변환 필요
function formatDate(date) {
    if (date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;

        return [year, month, day].join('-');
    } else {
        return "";
    }
}

function containsAny(str, substrings) {
    if (str == undefined) return null;
    for (var i = 0; i != substrings.length; i++) {
        var substring = substrings[i];
        if (str.indexOf(substring) != -1) {
            return substring;
        }
    }
    return null;
}

function formatDateTime(datestring) {
    //datestring format as 'yy-MM-dd hh:mm'

    try {

        //var datestring = "2010-08-09 01:02:03";
        var reggie = /(\d{2})-(\d{2})-(\d{2}) (\d{2}):(\d{2})/;
        var dateArray = reggie.exec(datestring);
        var dateObject = new Date(
            (+dateArray[1] + 2000),
            (+dateArray[2]) - 1, // Careful, month starts at 0!
            (+dateArray[3]),
            (+dateArray[4]),
            (+dateArray[5]),
            (+0)
        );
        //console.log(dateObject);
        datestring = datestring.toLowerCase();
        var date = dateObject; //new Date(datestring.substring(0, 8));
        var today = new Date();
        var yesterday = new Date(today);
        yesterday.setDate(today.getDate() - 1);

        var textToday = lgToday;
        var textYesterday = lgYesterday;
        var timeFormat = datestring.substring(8, datestring.length);

        if (today.format("yyyy-MM-dd") == date.format("yyyy-MM-dd"))
            return textToday + date.format(" HH:mm tt").toLowerCase().replace("am", lgAM).replace("pm", lgPM); 	//get multi language "Today hh:mm tt"
        else if (yesterday.format("yyyy-MM-dd") == date.format("yyyy-MM-dd"))
            return textYesterday + date.format(" HH:mm tt").toLowerCase().replace("am", lgAM).replace("pm", lgPM); //get multi language "Yesterday hh:mm tt"
        else return date.format("yyyy-MM-dd HH:mm tt").toLowerCase().replace("am", lgAM).replace("pm", lgPM);
    }
    catch (err) {
        //console.log(err + datestring); 
        if (containsAny(datestring, ["12:", "13:", "14:", "15:", "16:", "17:", "18:", "19:", "20:", "21:", "22:", "23:", "24:"]) != null)
            return datestring + " " + lgPM;
        return datestring + " " + lgAM;
    }
}

function methodParams(i) {
    var pns = new Array();
    var pvs = new Array();

    this.addParam = function (n, v) {
        pns.push(n); pvs.push(v);
    };

    this.toString = function () {
        if (pns.length == 0) return "{}";

        var r = "{";
        for (var i = 0; i < pns.length; i++) r += pns[i] + ":'" + pvs[i] + "',";
        r = r.substr(0, r.length - 1);
        return r + "}";
    };
};

var loading;
loading = loading || (function () {
    var pleaseWaitDiv = $('<div class="modal" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false"> <div class="modal-dialog" role="document"> <div class="modal-content"> <div class="modal-header"> <h3 class="modal-title" id="myModalLabel">진행중...</h3> </div><div class="modal-body"> <div class="progress"> <div class="progress-bar progress-bar-info progress-bar-striped" role="progressbar" data-transitiongoal="100" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%"> <span class="sr-only">100%</span> </div></div></div></div></div></div>');
    return {
        showPleaseWait: function () {
            pleaseWaitDiv.modal();
        },
        hidePleaseWait: function () {
            pleaseWaitDiv.modal('hide');
        }
    };
})();
