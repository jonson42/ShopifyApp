(function (window) {
    WebEditor = function (id) {

        var oEditors = [];

        this.id = id;

        this.init = function () {
            tinymce.init({
                selector: '#' + id,
                plugins: ['preview contextmenu textcolor image colorpicker table code fullscreen paste link autolink lists'],
                toolbar: 'undo redo | fontsizeselect fontselect | alignleft aligncenter alignright alignjustify | bold underline italic strikethrough forecolor backcolor | numlist bullist outdent indent | table | code | link image',
                //automatic_uploads: true,images_upload_url: window._webeditor_options_.imageUploadHandlerUrl, images_reuse_filename: true,
                paste_data_images: true, default_link_target: "_blank",
                paste_postprocess: function (plugin, args)
                {
                    var allElements = args.node.getElementsByTagName("img");
                    for (i = 0; i < allElements.length; ++i) {
                        allElements[i].setAttribute('class', '_bz_webeditor_addimage_tag_');
                    }
                    if (allElements.length == 0)
                    {
                        //하이퍼링크 처리                   
                        var doc = args.node.innerHTML;
                        var regURL = new RegExp("(http|https|ftp|telnet|news|irc)://([-/.a-zA-Z0-9_~#%$?&=:200-377()]+)", "gi");
                        var regEmail = new RegExp("([xA1-xFEa-z0-9_-]+@[xA1-xFEa-z0-9-]+\.[a-z0-9-]+)", "gi");
                        args.node.innerHTML = doc.replace(regURL, "<a href='$1://$2' target='_blank'>$1://$2</a>").replace(regEmail, "<a href='mailto:$1'>$1</a>");
                    }

                },
                image_class_list: [
                    { title: 'class', value: '_bz_webeditor_addimage_tag_' }
                ]
                , images_upload_handler: function (blobInfo, success, failure) {
                    let xhr = new XMLHttpRequest();
                    xhr.open('POST', window._webeditor_options_.imageUploadHandlerUrl);
                    xhr.setRequestHeader('file-name', unescape(encodeURIComponent(blobInfo.filename()))); // manually set header

                    xhr.onload = function () {
                        if (xhr.status !== 200) {
                            failure('HTTP Error: ' + xhr.status);
                            return;
                        }


                        var aTemp = [], aSubTemp = [], htTemp = {}, aResult = []
                        var sResString = xhr.responseText;
                        try {
                            if (!sResString) {
                                return;
                            }
                            aTemp = sResString.split("&");
                            for (var i = 0; i < aTemp.length; i++) {
                                if (!!aTemp[i] && aTemp[i] != "" && aTemp[i].indexOf("=") > 0) {
                                    aSubTemp = aTemp[i].split("=");
                                    htTemp[aSubTemp[0]] = aSubTemp[1];
                                }
                            }
                        } catch (e) { }

                        aResult = htTemp;

                        success(htTemp.url);
                    };

                    let formData = new FormData();
                    formData.append('file', blobInfo.blob(), blobInfo.filename());

                    xhr.send(formData);
                }
                , font_formats: "굴림=굴림;굴림체=굴림체;궁서=궁서;궁서체=궁서체;돋움=돋움;돋움체=돋움체;바탕=바탕;바탕체=바탕체;맑은고딕='맑은고딕';나눔명조='나눔명조';Arial=Arial;Tahoma=Tahoma;Times New Roman=Times New Roman;Verdana=Verdana;Courier New=Courier New"
                , fontsize_formats: '7pt 8pt 9pt 10pt 11pt 12pt 14pt 18pt 24pt 36pt'
                , setup: function (ed) {
                    ed.on('init', function (e) {
                        this.getDoc().body.style.fontFamily = '돋움';
                        this.getDoc().body.style.fontSize = '9pt';
                        //alert("html" + $("#" + id).html());
                        //alert("text" + $("#" + id).text());
                        //alert("val" + $("#" + id).val());
                        if (this.getContent() == "") {
                            this.setContent($("#" + id).html());
                            //this.setContent($("#" + id).html().replace(/&lt;/g, '<').replace(/&gt;/g, '>').replace(/&amp;/g, '&'));
                        }
                    })
                }
            });
        };

        this.generateAutoLink = function (sAll, sBreaker, sURL, sWWWURL, sHTTPURL) {
            sBreaker = sBreaker || "";

            var sResult;
            if (sWWWURL) {
                sResult = '<a href="http://' + sWWWURL + '">' + sURL + '</a>';
            } else {
                sResult = '<a href="' + sHTTPURL + '">' + sURL + '</a>';
            }

            return sBreaker + sResult;
        }

        this.getContent = function () {
            return tinymce.editors[0].getContent();
        };

        this.setContent = function (text) {
            tinymce.editors[0].setContent(text);
        };

        this.setContent2 = function (text, id) {
            //tinymce.editors[0].setContent(text);
            //alert(text);
            $("#" + id).html(text);
            //alert($("#" + id).html())
        };
        this.updateContentField = function () {
            //추가필요
        };

        this.loadCompleted = function () {
            if (tinymce.editors[0].length == 1)
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