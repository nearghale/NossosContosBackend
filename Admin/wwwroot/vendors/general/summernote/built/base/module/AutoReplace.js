"use strict";
exports.__esModule = true;
var lists_1 = require("../core/lists");
var dom_1 = require("../core/dom");
var key_1 = require("../core/key");
var AutoReplace = /** @class */ (function () {
    function AutoReplace(context) {
        var _this = this;
        this.context = context;
        this.options = context.options.replace || {};
        this.keys = [key_1["default"].code.ENTER, key_1["default"].code.SPACE, key_1["default"].code.PERIOD, key_1["default"].code.COMMA, key_1["default"].code.SEMICOLON, key_1["default"].code.SLASH];
        this.previousKeydownCode = null;
        this.events = {
            'summernote.keyup': function (we, e) {
                if (!e.isDefaultPrevented()) {
                    _this.handleKeyup(e);
                }
            },
            'summernote.keydown': function (we, e) {
                _this.handleKeydown(e);
            }
        };
    }
    AutoReplace.prototype.shouldInitialize = function () {
        return !!this.options.match;
    };
    AutoReplace.prototype.initialize = function () {
        this.lastWord = null;
    };
    AutoReplace.prototype.destroy = function () {
        this.lastWord = null;
    };
    AutoReplace.prototype.replace = function () {
        if (!this.lastWord) {
            return;
        }
        var self = this;
        var keyword = this.lastWord.toString();
        this.options.match(keyword, function (match) {
            if (match) {
                var node = '';
                if (typeof match === 'string') {
                    node = dom_1["default"].createText(match);
                }
                else if (match instanceof jQuery) {
                    node = match[0];
                }
                else if (match instanceof Node) {
                    node = match;
                }
                if (!node)
                    return;
                self.lastWord.insertNode(node);
                self.lastWord = null;
                self.context.invoke('editor.focus');
            }
        });
    };
    AutoReplace.prototype.handleKeydown = function (e) {
        // this forces it to remember the last whole word, even if multiple termination keys are pressed
        // before the previous key is let go.
        if (this.previousKeydownCode && lists_1["default"].contains(this.keys, this.previousKeydownCode)) {
            this.previousKeydownCode = e.keyCode;
            return;
        }
        if (lists_1["default"].contains(this.keys, e.keyCode)) {
            var wordRange = this.context.invoke('editor.createRange').getWordRange();
            this.lastWord = wordRange;
        }
        this.previousKeydownCode = e.keyCode;
    };
    AutoReplace.prototype.handleKeyup = function (e) {
        if (lists_1["default"].contains(this.keys, e.keyCode)) {
            this.replace();
        }
    };
    return AutoReplace;
}());
exports["default"] = AutoReplace;
//# sourceMappingURL=AutoReplace.js.map