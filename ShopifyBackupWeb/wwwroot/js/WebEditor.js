(function (window)
{
    WebEditor = function (id)
    {
        var imageUploadHandlerUrl;

        var myObj = this, oEditors = [], _guid_ = "";

        this.id = id;

        this.init = function ()
        {
            nhn.husky.EZCreator.createInIFrame({
                oAppRef: oEditors,
                elPlaceHolder: this.id,
                sSkinURI: "/SE2.8.2.O12056/SmartEditor2Skin.html?20141030",

                htParams: {
                    bUseToolbar: true
                },

                fOnAppLoad: function () {

                    oEditors[0].setDefaultFont("돋움", 9);
                    oEditors[0].exec("SET_WYSIWYG_STYLE", [{ "fontSize": "9pt", "fontFamily": "돋움"}]);
                },

                fOnBeforeUnload: function () { },
                fCreator: "createSEditor2"
            });
        };

        this.setSize = function (width, height) {
            oEditors[0].exec("MSG_EDITING_AREA_RESIZE_STARTED", []);
            oEditors[0].exec("RESIZE_EDITING_AREA_BY", [width, height]);
            oEditors[0].exec("MSG_EDITING_AREA_RESIZE_ENDED", []);
        };

        this.focus = function () {
            oEditors[0].exec("FOCUS", []);
        };

        this.getContent = function () {
            return oEditors[0].getIR();
        };

        this.setContent = function (text) {
            oEditors[0].setRawHTMLContents(text);
        };

        this.setContent2 = function (text) {
            $("#webeditor").html(text);
        };

        this.updateContentField = function () {
            oEditors[0].exec("UPDATE_CONTENTS_FIELD", []);
        };

        this.loadCompleted = function () {
            if (oEditors.length == 1)
                return true;
            else
                return false;
        };

        window.__webEditor_InsertImage__ = function (url, guid) {
            var html = [];
            html.push("<img class=\"_bz_webeditor_addimage_tag_\" src=\"");
            html.push(url);
            html.push("\" />");
            html = html.join("");

            oEditors[0].exec("PASTE_HTML", [html]);
        };

        window.__webEditor_InsertHrTag__ = function () {
            var html = [];
            html.push("<hr style=\"page-break-after:always;\" />");

            oEditors[0].exec("PASTE_HTML", [html]);
        };
    };

})(window);