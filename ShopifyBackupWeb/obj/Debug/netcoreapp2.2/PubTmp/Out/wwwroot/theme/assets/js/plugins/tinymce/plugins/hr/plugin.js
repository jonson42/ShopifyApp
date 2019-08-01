(function () {
var hr = (function () {
  'use strict';

  var global = tinymce.util.Tools.resolve('tinymce.PluginManager');

  var register = function (editor) {
    editor.addCommand('InsertHorizontalRule', function () {
      editor.execCommand('mceInsertContent', false, '<hr />');
    });
  };
  var $_fi9bdqcnjkc9bd20 = { register: register };

  var register$1 = function (editor) {
    editor.addButton('hr', {
      icon: 'hr',
      tooltip: 'Horizontal line',
      cmd: 'InsertHorizontalRule'
    });
    editor.addMenuItem('hr', {
      icon: 'hr',
      text: 'Horizontal line',
      cmd: 'InsertHorizontalRule',
      context: 'insert'
    });
  };
  var $_8218g6cojkc9bd22 = { register: register$1 };

  global.add('hr', function (editor) {
    $_fi9bdqcnjkc9bd20.register(editor);
    $_8218g6cojkc9bd22.register(editor);
  });
  function Plugin () {
  }

  return Plugin;

}());
})();
