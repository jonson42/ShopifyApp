(function () {
var code = (function () {
  'use strict';

  var global = tinymce.util.Tools.resolve('tinymce.PluginManager');

  var global$1 = tinymce.util.Tools.resolve('tinymce.dom.DOMUtils');

  var getMinWidth = function (editor) {
    return editor.getParam('code_dialog_width', 600);
  };
  var getMinHeight = function (editor) {
    return editor.getParam('code_dialog_height', Math.min(global$1.DOM.getViewPort().h - 200, 500));
  };
  var $_abjyyia2jkc9bchv = {
    getMinWidth: getMinWidth,
    getMinHeight: getMinHeight
  };

  var setContent = function (editor, html) {
    editor.focus();
    editor.undoManager.transact(function () {
      editor.setContent(html);
    });
    editor.selection.setCursorLocation();
    editor.nodeChanged();
  };
  var getContent = function (editor) {
    return editor.getContent({ source_view: true });
  };
  var $_d3l9y4a4jkc9bchy = {
    setContent: setContent,
    getContent: getContent
  };

  var open = function (editor) {
    var minWidth = $_abjyyia2jkc9bchv.getMinWidth(editor);
    var minHeight = $_abjyyia2jkc9bchv.getMinHeight(editor);
    var win = editor.windowManager.open({
      title: 'Source code',
      body: {
        type: 'textbox',
        name: 'code',
        multiline: true,
        minWidth: minWidth,
        minHeight: minHeight,
        spellcheck: false,
        style: 'direction: ltr; text-align: left'
      },
      onSubmit: function (e) {
        $_d3l9y4a4jkc9bchy.setContent(editor, e.data.code);
      }
    });
    win.find('#code').value($_d3l9y4a4jkc9bchy.getContent(editor));
  };
  var $_c4tgmka1jkc9bcht = { open: open };

  var register = function (editor) {
    editor.addCommand('mceCodeEditor', function () {
      $_c4tgmka1jkc9bcht.open(editor);
    });
  };
  var $_650dz7a0jkc9bchr = { register: register };

  var register$1 = function (editor) {
    editor.addButton('code', {
      icon: 'code',
      tooltip: 'Source code',
      onclick: function () {
        $_c4tgmka1jkc9bcht.open(editor);
      }
    });
    editor.addMenuItem('code', {
      icon: 'code',
      text: 'Source code',
      onclick: function () {
        $_c4tgmka1jkc9bcht.open(editor);
      }
    });
  };
  var $_5gehura5jkc9bci0 = { register: register$1 };

  global.add('code', function (editor) {
    $_650dz7a0jkc9bchr.register(editor);
    $_5gehura5jkc9bci0.register(editor);
    return {};
  });
  function Plugin () {
  }

  return Plugin;

}());
})();
