"use strict";
exports.__esModule = true;
var jquery_1 = require("jquery");
require("./summernote-en-US");
require("../summernote");
var dom_1 = require("./core/dom");
var range_1 = require("./core/range");
var Editor_1 = require("./module/Editor");
var Clipboard_1 = require("./module/Clipboard");
var Dropzone_1 = require("./module/Dropzone");
var Codeview_1 = require("./module/Codeview");
var Statusbar_1 = require("./module/Statusbar");
var Fullscreen_1 = require("./module/Fullscreen");
var Handle_1 = require("./module/Handle");
var AutoLink_1 = require("./module/AutoLink");
var AutoSync_1 = require("./module/AutoSync");
var AutoReplace_1 = require("./module/AutoReplace");
var Placeholder_1 = require("./module/Placeholder");
var Buttons_1 = require("./module/Buttons");
var Toolbar_1 = require("./module/Toolbar");
var LinkDialog_1 = require("./module/LinkDialog");
var LinkPopover_1 = require("./module/LinkPopover");
var ImageDialog_1 = require("./module/ImageDialog");
var ImagePopover_1 = require("./module/ImagePopover");
var TablePopover_1 = require("./module/TablePopover");
var VideoDialog_1 = require("./module/VideoDialog");
var HelpDialog_1 = require("./module/HelpDialog");
var AirPopover_1 = require("./module/AirPopover");
var HintPopover_1 = require("./module/HintPopover");
jquery_1["default"].summernote = jquery_1["default"].extend(jquery_1["default"].summernote, {
    version: '@@VERSION@@',
    plugins: {},
    dom: dom_1["default"],
    range: range_1["default"],
    options: {
        langInfo: jquery_1["default"].summernote.lang['en-US'],
        modules: {
            'editor': Editor_1["default"],
            'clipboard': Clipboard_1["default"],
            'dropzone': Dropzone_1["default"],
            'codeview': Codeview_1["default"],
            'statusbar': Statusbar_1["default"],
            'fullscreen': Fullscreen_1["default"],
            'handle': Handle_1["default"],
            // FIXME: HintPopover must be front of autolink
            //  - Script error about range when Enter key is pressed on hint popover
            'hintPopover': HintPopover_1["default"],
            'autoLink': AutoLink_1["default"],
            'autoSync': AutoSync_1["default"],
            'autoReplace': AutoReplace_1["default"],
            'placeholder': Placeholder_1["default"],
            'buttons': Buttons_1["default"],
            'toolbar': Toolbar_1["default"],
            'linkDialog': LinkDialog_1["default"],
            'linkPopover': LinkPopover_1["default"],
            'imageDialog': ImageDialog_1["default"],
            'imagePopover': ImagePopover_1["default"],
            'tablePopover': TablePopover_1["default"],
            'videoDialog': VideoDialog_1["default"],
            'helpDialog': HelpDialog_1["default"],
            'airPopover': AirPopover_1["default"]
        },
        buttons: {},
        lang: 'en-US',
        followingToolbar: false,
        otherStaticBar: '',
        // toolbar
        toolbar: [
            ['style', ['style']],
            ['font', ['bold', 'underline', 'clear']],
            ['fontname', ['fontname']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['table', ['table']],
            ['insert', ['link', 'picture', 'video']],
            ['view', ['fullscreen', 'codeview', 'help']],
        ],
        // popover
        popatmouse: true,
        popover: {
            image: [
                ['resize', ['resizeFull', 'resizeHalf', 'resizeQuarter', 'resizeNone']],
                ['float', ['floatLeft', 'floatRight', 'floatNone']],
                ['remove', ['removeMedia']],
            ],
            link: [
                ['link', ['linkDialogShow', 'unlink']],
            ],
            table: [
                ['add', ['addRowDown', 'addRowUp', 'addColLeft', 'addColRight']],
                ['delete', ['deleteRow', 'deleteCol', 'deleteTable']],
            ],
            air: [
                ['color', ['color']],
                ['font', ['bold', 'underline', 'clear']],
                ['para', ['ul', 'paragraph']],
                ['table', ['table']],
                ['insert', ['link', 'picture']],
            ]
        },
        // air mode: inline editor
        airMode: false,
        width: null,
        height: null,
        linkTargetBlank: true,
        focus: false,
        tabSize: 4,
        styleWithSpan: true,
        shortcuts: true,
        textareaAutoSync: true,
        hintDirection: 'bottom',
        tooltip: 'auto',
        container: 'body',
        maxTextLength: 0,
        blockquoteBreakingLevel: 2,
        spellCheck: true,
        styleTags: ['p', 'blockquote', 'pre', 'h1', 'h2', 'h3', 'h4', 'h5', 'h6'],
        fontNames: [
            'Arial', 'Arial Black', 'Comic Sans MS', 'Courier New',
            'Helvetica Neue', 'Helvetica', 'Impact', 'Lucida Grande',
            'Tahoma', 'Times New Roman', 'Verdana',
        ],
        fontNamesIgnoreCheck: [],
        fontSizes: ['8', '9', '10', '11', '12', '14', '18', '24', '36'],
        // pallete colors(n x n)
        colors: [
            ['#000000', '#424242', '#636363', '#9C9C94', '#CEC6CE', '#EFEFEF', '#F7F7F7', '#FFFFFF'],
            ['#FF0000', '#FF9C00', '#FFFF00', '#00FF00', '#00FFFF', '#0000FF', '#9C00FF', '#FF00FF'],
            ['#F7C6CE', '#FFE7CE', '#FFEFC6', '#D6EFD6', '#CEDEE7', '#CEE7F7', '#D6D6E7', '#E7D6DE'],
            ['#E79C9C', '#FFC69C', '#FFE79C', '#B5D6A5', '#A5C6CE', '#9CC6EF', '#B5A5D6', '#D6A5BD'],
            ['#E76363', '#F7AD6B', '#FFD663', '#94BD7B', '#73A5AD', '#6BADDE', '#8C7BC6', '#C67BA5'],
            ['#CE0000', '#E79439', '#EFC631', '#6BA54A', '#4A7B8C', '#3984C6', '#634AA5', '#A54A7B'],
            ['#9C0000', '#B56308', '#BD9400', '#397B21', '#104A5A', '#085294', '#311873', '#731842'],
            ['#630000', '#7B3900', '#846300', '#295218', '#083139', '#003163', '#21104A', '#4A1031'],
        ],
        // http://chir.ag/projects/name-that-color/
        colorsName: [
            ['Black', 'Tundora', 'Dove Gray', 'Star Dust', 'Pale Slate', 'Gallery', 'Alabaster', 'White'],
            ['Red', 'Orange Peel', 'Yellow', 'Green', 'Cyan', 'Blue', 'Electric Violet', 'Magenta'],
            ['Azalea', 'Karry', 'Egg White', 'Zanah', 'Botticelli', 'Tropical Blue', 'Mischka', 'Twilight'],
            ['Tonys Pink', 'Peach Orange', 'Cream Brulee', 'Sprout', 'Casper', 'Perano', 'Cold Purple', 'Careys Pink'],
            ['Mandy', 'Rajah', 'Dandelion', 'Olivine', 'Gulf Stream', 'Viking', 'Blue Marguerite', 'Puce'],
            ['Guardsman Red', 'Fire Bush', 'Golden Dream', 'Chelsea Cucumber', 'Smalt Blue', 'Boston Blue', 'Butterfly Bush', 'Cadillac'],
            ['Sangria', 'Mai Tai', 'Buddha Gold', 'Forest Green', 'Eden', 'Venice Blue', 'Meteorite', 'Claret'],
            ['Rosewood', 'Cinnamon', 'Olive', 'Parsley', 'Tiber', 'Midnight Blue', 'Valentino', 'Loulou'],
        ],
        colorButton: {
            foreColor: '#000000',
            backColor: '#FFFF00'
        },
        lineHeights: ['1.0', '1.2', '1.4', '1.5', '1.6', '1.8', '2.0', '3.0'],
        tableClassName: 'table table-bordered',
        insertTableMaxSize: {
            col: 10,
            row: 10
        },
        dialogsInBody: false,
        dialogsFade: false,
        maximumImageFileSize: null,
        callbacks: {
            onBeforeCommand: null,
            onBlur: null,
            onBlurCodeview: null,
            onChange: null,
            onChangeCodeview: null,
            onDialogShown: null,
            onEnter: null,
            onFocus: null,
            onImageLinkInsert: null,
            onImageUpload: null,
            onImageUploadError: null,
            onInit: null,
            onKeydown: null,
            onKeyup: null,
            onMousedown: null,
            onMouseup: null,
            onPaste: null,
            onScroll: null
        },
        codemirror: {
            mode: 'text/html',
            htmlMode: true,
            lineNumbers: true
        },
        codeviewFilter: false,
        codeviewFilterRegex: /<\/*(?:applet|b(?:ase|gsound|link)|embed|frame(?:set)?|ilayer|l(?:ayer|ink)|meta|object|s(?:cript|tyle)|t(?:itle|extarea)|xml)[^>]*?>/gi,
        codeviewIframeFilter: true,
        codeviewIframeWhitelistSrc: [],
        codeviewIframeWhitelistSrcBase: [
            'www.youtube.com',
            'www.youtube-nocookie.com',
            'www.facebook.com',
            'vine.co',
            'instagram.com',
            'player.vimeo.com',
            'www.dailymotion.com',
            'player.youku.com',
            'v.qq.com',
        ],
        keyMap: {
            pc: {
                'ENTER': 'insertParagraph',
                'CTRL+Z': 'undo',
                'CTRL+Y': 'redo',
                'TAB': 'tab',
                'SHIFT+TAB': 'untab',
                'CTRL+B': 'bold',
                'CTRL+I': 'italic',
                'CTRL+U': 'underline',
                'CTRL+SHIFT+S': 'strikethrough',
                'CTRL+BACKSLASH': 'removeFormat',
                'CTRL+SHIFT+L': 'justifyLeft',
                'CTRL+SHIFT+E': 'justifyCenter',
                'CTRL+SHIFT+R': 'justifyRight',
                'CTRL+SHIFT+J': 'justifyFull',
                'CTRL+SHIFT+NUM7': 'insertUnorderedList',
                'CTRL+SHIFT+NUM8': 'insertOrderedList',
                'CTRL+LEFTBRACKET': 'outdent',
                'CTRL+RIGHTBRACKET': 'indent',
                'CTRL+NUM0': 'formatPara',
                'CTRL+NUM1': 'formatH1',
                'CTRL+NUM2': 'formatH2',
                'CTRL+NUM3': 'formatH3',
                'CTRL+NUM4': 'formatH4',
                'CTRL+NUM5': 'formatH5',
                'CTRL+NUM6': 'formatH6',
                'CTRL+ENTER': 'insertHorizontalRule',
                'CTRL+K': 'linkDialog.show'
            },
            mac: {
                'ENTER': 'insertParagraph',
                'CMD+Z': 'undo',
                'CMD+SHIFT+Z': 'redo',
                'TAB': 'tab',
                'SHIFT+TAB': 'untab',
                'CMD+B': 'bold',
                'CMD+I': 'italic',
                'CMD+U': 'underline',
                'CMD+SHIFT+S': 'strikethrough',
                'CMD+BACKSLASH': 'removeFormat',
                'CMD+SHIFT+L': 'justifyLeft',
                'CMD+SHIFT+E': 'justifyCenter',
                'CMD+SHIFT+R': 'justifyRight',
                'CMD+SHIFT+J': 'justifyFull',
                'CMD+SHIFT+NUM7': 'insertUnorderedList',
                'CMD+SHIFT+NUM8': 'insertOrderedList',
                'CMD+LEFTBRACKET': 'outdent',
                'CMD+RIGHTBRACKET': 'indent',
                'CMD+NUM0': 'formatPara',
                'CMD+NUM1': 'formatH1',
                'CMD+NUM2': 'formatH2',
                'CMD+NUM3': 'formatH3',
                'CMD+NUM4': 'formatH4',
                'CMD+NUM5': 'formatH5',
                'CMD+NUM6': 'formatH6',
                'CMD+ENTER': 'insertHorizontalRule',
                'CMD+K': 'linkDialog.show'
            }
        },
        icons: {
            'align': 'note-icon-align',
            'alignCenter': 'note-icon-align-center',
            'alignJustify': 'note-icon-align-justify',
            'alignLeft': 'note-icon-align-left',
            'alignRight': 'note-icon-align-right',
            'rowBelow': 'note-icon-row-below',
            'colBefore': 'note-icon-col-before',
            'colAfter': 'note-icon-col-after',
            'rowAbove': 'note-icon-row-above',
            'rowRemove': 'note-icon-row-remove',
            'colRemove': 'note-icon-col-remove',
            'indent': 'note-icon-align-indent',
            'outdent': 'note-icon-align-outdent',
            'arrowsAlt': 'note-icon-arrows-alt',
            'bold': 'note-icon-bold',
            'caret': 'note-icon-caret',
            'circle': 'note-icon-circle',
            'close': 'note-icon-close',
            'code': 'note-icon-code',
            'eraser': 'note-icon-eraser',
            'floatLeft': 'note-icon-float-left',
            'floatRight': 'note-icon-float-right',
            'font': 'note-icon-font',
            'frame': 'note-icon-frame',
            'italic': 'note-icon-italic',
            'link': 'note-icon-link',
            'unlink': 'note-icon-chain-broken',
            'magic': 'note-icon-magic',
            'menuCheck': 'note-icon-menu-check',
            'minus': 'note-icon-minus',
            'orderedlist': 'note-icon-orderedlist',
            'pencil': 'note-icon-pencil',
            'picture': 'note-icon-picture',
            'question': 'note-icon-question',
            'redo': 'note-icon-redo',
            'rollback': 'note-icon-rollback',
            'square': 'note-icon-square',
            'strikethrough': 'note-icon-strikethrough',
            'subscript': 'note-icon-subscript',
            'superscript': 'note-icon-superscript',
            'table': 'note-icon-table',
            'textHeight': 'note-icon-text-height',
            'trash': 'note-icon-trash',
            'underline': 'note-icon-underline',
            'undo': 'note-icon-undo',
            'unorderedlist': 'note-icon-unorderedlist',
            'video': 'note-icon-video'
        }
    }
});
//# sourceMappingURL=settings.js.map