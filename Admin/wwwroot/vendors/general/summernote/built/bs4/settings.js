"use strict";
exports.__esModule = true;
var jquery_1 = require("jquery");
var ui_1 = require("./ui");
require("../base/settings.js");
jquery_1["default"].summernote = jquery_1["default"].extend(jquery_1["default"].summernote, {
    ui: ui_1["default"]
});
jquery_1["default"].summernote.options.styleTags = [
    'p',
    { title: 'Blockquote', tag: 'blockquote', className: 'blockquote', value: 'blockquote' },
    'pre', 'h1', 'h2', 'h3', 'h4', 'h5', 'h6',
];
//# sourceMappingURL=settings.js.map