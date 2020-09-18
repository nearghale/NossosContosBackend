"use strict";
exports.__esModule = true;
var env_1 = require("../core/env");
var dom_1 = require("../core/dom");
var CodeMirror;
if (env_1["default"].hasCodeMirror) {
    CodeMirror = window.CodeMirror;
}
/**
 * @class Codeview
 */
var CodeView = /** @class */ (function () {
    function CodeView(context) {
        this.context = context;
        this.$editor = context.layoutInfo.editor;
        this.$editable = context.layoutInfo.editable;
        this.$codable = context.layoutInfo.codable;
        this.options = context.options;
    }
    CodeView.prototype.sync = function () {
        var isCodeview = this.isActivated();
        if (isCodeview && env_1["default"].hasCodeMirror) {
            this.$codable.data('cmEditor').save();
        }
    };
    /**
     * @return {Boolean}
     */
    CodeView.prototype.isActivated = function () {
        return this.$editor.hasClass('codeview');
    };
    /**
     * toggle codeview
     */
    CodeView.prototype.toggle = function () {
        if (this.isActivated()) {
            this.deactivate();
        }
        else {
            this.activate();
        }
        this.context.triggerEvent('codeview.toggled');
    };
    /**
     * purify input value
     * @param value
     * @returns {*}
     */
    CodeView.prototype.purify = function (value) {
        if (this.options.codeviewFilter) {
            // filter code view regex
            value = value.replace(this.options.codeviewFilterRegex, '');
            // allow specific iframe tag
            if (this.options.codeviewIframeFilter) {
                var whitelist_1 = this.options.codeviewIframeWhitelistSrc.concat(this.options.codeviewIframeWhitelistSrcBase);
                value = value.replace(/(<iframe.*?>.*?(?:<\/iframe>)?)/gi, function (tag) {
                    // remove if src attribute is duplicated
                    if (/<.+src(?==?('|"|\s)?)[\s\S]+src(?=('|"|\s)?)[^>]*?>/i.test(tag)) {
                        return '';
                    }
                    for (var _i = 0, whitelist_2 = whitelist_1; _i < whitelist_2.length; _i++) {
                        var src = whitelist_2[_i];
                        // pass if src is trusted
                        if ((new RegExp('src="(https?:)?\/\/' + src.replace(/[-\/\\^$*+?.()|[\]{}]/g, '\\$&') + '\/(.+)"')).test(tag)) {
                            return tag;
                        }
                    }
                    return '';
                });
            }
        }
        return value;
    };
    /**
     * activate code view
     */
    CodeView.prototype.activate = function () {
        var _this = this;
        this.$codable.val(dom_1["default"].html(this.$editable, this.options.prettifyHtml));
        this.$codable.height(this.$editable.height());
        this.context.invoke('toolbar.updateCodeview', true);
        this.$editor.addClass('codeview');
        this.$codable.focus();
        // activate CodeMirror as codable
        if (env_1["default"].hasCodeMirror) {
            var cmEditor_1 = CodeMirror.fromTextArea(this.$codable[0], this.options.codemirror);
            // CodeMirror TernServer
            if (this.options.codemirror.tern) {
                var server_1 = new CodeMirror.TernServer(this.options.codemirror.tern);
                cmEditor_1.ternServer = server_1;
                cmEditor_1.on('cursorActivity', function (cm) {
                    server_1.updateArgHints(cm);
                });
            }
            cmEditor_1.on('blur', function (event) {
                _this.context.triggerEvent('blur.codeview', cmEditor_1.getValue(), event);
            });
            cmEditor_1.on('change', function (event) {
                _this.context.triggerEvent('change.codeview', cmEditor_1.getValue(), cmEditor_1);
            });
            // CodeMirror hasn't Padding.
            cmEditor_1.setSize(null, this.$editable.outerHeight());
            this.$codable.data('cmEditor', cmEditor_1);
        }
        else {
            this.$codable.on('blur', function (event) {
                _this.context.triggerEvent('blur.codeview', _this.$codable.val(), event);
            });
            this.$codable.on('input', function (event) {
                _this.context.triggerEvent('change.codeview', _this.$codable.val(), _this.$codable);
            });
        }
    };
    /**
     * deactivate code view
     */
    CodeView.prototype.deactivate = function () {
        // deactivate CodeMirror as codable
        if (env_1["default"].hasCodeMirror) {
            var cmEditor = this.$codable.data('cmEditor');
            this.$codable.val(cmEditor.getValue());
            cmEditor.toTextArea();
        }
        var value = this.purify(dom_1["default"].value(this.$codable, this.options.prettifyHtml) || dom_1["default"].emptyPara);
        var isChange = this.$editable.html() !== value;
        this.$editable.html(value);
        this.$editable.height(this.options.height ? this.$codable.height() : 'auto');
        this.$editor.removeClass('codeview');
        if (isChange) {
            this.context.triggerEvent('change', this.$editable.html(), this.$editable);
        }
        this.$editable.focus();
        this.context.invoke('toolbar.updateCodeview', false);
    };
    CodeView.prototype.destroy = function () {
        if (this.isActivated()) {
            this.deactivate();
        }
    };
    return CodeView;
}());
exports["default"] = CodeView;
//# sourceMappingURL=Codeview.js.map