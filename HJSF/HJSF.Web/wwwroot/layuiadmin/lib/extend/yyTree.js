/**
 
 @Name：layui.tree 树
 @Author：star1029
 @License：MIT

 */

layui.define('form', function (exports) {
    "use strict";

    var $ = layui.$
        , form = layui.form
        , layer = layui.layer

        //模块名
        , MOD_NAME = 'yyTree'

        //外部接口
        , tree = {
            config: {}
            , index: layui[MOD_NAME] ? (layui[MOD_NAME].index + 10000) : 0

            //设置全局项
            , set: function (options) {
                var that = this;
                that.config = $.extend({}, that.config, options);
                return that;
            }

            //事件监听
            , on: function (events, callback) {
                return layui.onevent.call(this, MOD_NAME, events, callback);
            }
        }

        //操作当前实例
        , thisModule = function () {
            var that = this
                , options = that.config
                , id = options.id || that.index;

            thisModule.that[id] = that; //记录当前实例对象
            thisModule.config[id] = options; //记录当前实例配置项

            return {
                config: options
                //重置实例
                , reload: function (options) {
                    that.reload.call(that, options);
                }
                , getChecked: function () {
                    return that.getChecked.call(that);
                }
                , setChecked: function (id) {//设置值
                    return that.setChecked.call(that, id);
                }
            }
        }

        //获取当前实例配置项
        , getThisModuleConfig = function (id) {
            var config = thisModule.config[id];
            if (!config) hint.error('The ID option was not found in the ' + MOD_NAME + ' instance');
            return config || null;
        }

        //字符常量
        , SHOW = 'layui-show', HIDE = 'layui-hide', NONE = 'layui-none', DISABLED = 'layui-disabled'

        , ELEM_VIEW = 'layui-tree', ELEM_SET = 'layui-tree-set', ICON_CLICK = 'layui-tree-iconClick'
        , ICON_ADD = 'layui-icon-addition', ICON_SUB = 'layui-icon-subtraction', ELEM_ENTRY = 'layui-tree-entry', ELEM_MAIN = 'layui-tree-main', ELEM_TEXT = 'layui-tree-txt', ELEM_PACK = 'layui-tree-pack', ELEM_SPREAD = 'layui-tree-spread'
        , ELEM_LINE_SHORT = 'layui-tree-setLineShort', ELEM_SHOW = 'layui-tree-showLine', ELEM_EXTEND = 'layui-tree-lineExtend'

        //构造器
        , Class = function (options) {
            var that = this;
            that.index = ++tree.index;
            that.config = $.extend({}, that.config, tree.config, options);
            that.render();
        };

    //默认配置
    Class.prototype.config = {
        data: []  //数据

        , showCheckbox: false  //是否显示复选框
        , showLine: true  //是否开启连接线
        , accordion: false  //是否开启手风琴模式
        , onlyIconControl: false  //是否仅允许节点左侧图标控制展开收缩
        , isJump: false  //是否允许点击节点时弹出新窗口跳转
        , edit: false  //是否开启节点的操作图标
        , isCheckParent:true  //是否同步选中父节点
        , text: {
            defaultNodeName: '未命名' //节点默认名称
            , none: '无数据'  //数据为空时的文本提示
        }
    };

    //重载实例
    Class.prototype.reload = function (options) {
        var that = this;

        layui.each(options, function (key, item) {
            if (item.constructor === Array) delete that.config[key];
        });

        that.config = $.extend(true, {}, that.config, options);
        that.render();
    };

    //主体渲染
    Class.prototype.render = function () {
        var that = this
            , options = that.config;

        that.checkids = [];

        var temp = $('<div class="layui-tree' + (options.showCheckbox ? " layui-form" : "") + (options.showLine ? " layui-tree-line" : "") + '" lay-filter="LAY-tree-' + that.index + '"></div>');
        that.tree(temp);

        var othis = options.elem = $(options.elem);
        if (!othis[0]) return;

        //索引
        that.key = options.id || that.index;

        //插入组件结构
        that.elem = temp;
        that.elemNone = $('<div class="layui-tree-emptyText">' + options.text.none + '</div>');
        othis.html(that.elem);

        if (that.elem.find('.layui-tree-set').length == 0) {
            return that.elem.append(that.elemNone);
        };

        //复选框渲染
        if (options.showCheckbox) {
            that.renderForm('checkbox');
        };

        that.elem.find('.layui-tree-set').each(function () {
            var othis = $(this);
            //最外层
            if (!othis.parent('.layui-tree-pack')[0]) {
                othis.addClass('layui-tree-setHide');
            };

            //没有下一个节点 上一层父级有延伸线
            if (!othis.next()[0] && othis.parents('.layui-tree-pack').eq(1).hasClass('layui-tree-lineExtend')) {
                othis.addClass(ELEM_LINE_SHORT);
            };

            //没有下一个节点 外层最后一个
            if (!othis.next()[0] && !othis.parents('.layui-tree-set').eq(0).next()[0]) {
                othis.addClass(ELEM_LINE_SHORT);
            };
        });

        that.events();
    };

    //渲染表单
    Class.prototype.renderForm = function (type) {
        form.render(type, 'LAY-tree-' + this.index);
    };

    //节点解析
    Class.prototype.tree = function (elem, children) {
        var that = this
            , options = that.config
            , data = children || options.data;

        //遍历数据
        layui.each(data, function (index, item) {
            var hasChild = item.children && item.children.length > 0
                , packDiv = $('<div class="layui-tree-pack" ' + (item.spread ? 'style="display: block;"' : '') + '"></div>')
                , entryDiv = $(['<div data-id="' + (item.id || item.value) + '" class="layui-tree-set' + (item.spread ? " layui-tree-spread" : "") + (item.checked ? " layui-tree-checkedFirst" : "") + '">'
                    , '<div class="layui-tree-entry">'
                    , '<div class="layui-tree-main">'
                    //箭头
                    , function () {

                        //if (options.showLine) {
                        //    if (hasChild) {
                        //        return '<span class="layui-tree-iconClick layui-tree-icon"><i class="layui-icon ' + (item.spread ? "layui-icon-subtraction" : "layui-icon-addition") + '"></i></span>';
                        //    } else {
                        //        //return '<span class="layui-tree-iconClick"><i class="layui-icon layui-icon-file"></i></span>';
                        //        return '<span class="layui-tree-iconClick"></span>';
                        //    };
                        //} else {
                        //    return '<span class="layui-tree-iconClick"><i class="layui-tree-iconArrow ' + (hasChild ? "" : HIDE) + '"></i></span>';
                        //};

                        if (options.showLine) {
                            if (hasChild) {
                                return '<span class="layui-tree-iconClick layui-tree-icon"><i class="layui-icon ' + (item.spread ? "layui-icon-subtraction" : "layui-icon-addition") + '"></i></span>';
                            } else {
                                return '<span class="layui-tree-iconClick"><i class="yy-icon icon-wenjian"></i></span>';
                            };
                        } else {
                            return '<span class="layui-tree-iconClick"><i class="layui-tree-iconArrow ' + (hasChild ? "" : HIDE) + '"></i></span>';
                        };



                    }()

                    //复选框
                    , function () {
                        return options.showCheckbox ? '<input type="checkbox" name="' + (item.field || ('layuiTreeCheck_' + (item.id || item.value))) + '" same="layuiTreeCheck" lay-skin="primary" ' + (item.disabled ? "disabled" : "") + ' value="' + (item.id || item.value)  + '">' : '';
                    }()

                    //节点
                    , function () {
                        if (options.isJump && item.href) {
                            return '<a href="' + item.href + '" target="_blank" class="' + ELEM_TEXT + '">' + (item.title || item.label || item.text || options.text.defaultNodeName) + '</a>';
                        } else {
                            return '<span class="' + ELEM_TEXT + (item.disabled ? ' ' + DISABLED : '') + '">' + (item.title || item.label || item.text|| options.text.defaultNodeName) + '</span>';
                        }
                    }()
                    , '</div>'

                    //节点操作图标
                    , function () {
                        if (!options.edit) return '';

                        var editIcon = {
                            add: '<i class="layui-icon layui-icon-add-1" data-type="add" title="新增"></i>'
                            , update: '<i class="layui-icon layui-icon-edit" data-type="update" title="修改"></i>'
                            , del: '<i class="layui-icon layui-icon-delete" data-type="del" title="删除"></i>'
                        }, arr = ['<div class="layui-btn-group layui-tree-btnGroup">'];

                        if (options.edit === true) {
                            options.edit = ['update', 'del']
                        }

                        if (typeof options.edit === 'object') {
                            layui.each(options.edit, function (i, val) {
                                arr.push(editIcon[val] || '')
                            });
                            return arr.join('') + '</div>';
                        }
                    }()
                    , '</div></div>'].join(''));

            //如果有子节点，则递归继续生成树
            if (hasChild) {
                entryDiv.append(packDiv);
                that.tree(packDiv, item.children);
            };

            elem.append(entryDiv);

            //若有前置节点，前置节点加连接线
            if (entryDiv.prev('.' + ELEM_SET)[0]) {
                entryDiv.prev().children('.layui-tree-pack').addClass('layui-tree-showLine');
            };

            //若无子节点，则父节点加延伸线
            if (!hasChild) {
                entryDiv.parent('.layui-tree-pack').addClass('layui-tree-lineExtend');
            };

            //展开节点操作
            that.spread(entryDiv, item);

            //选择框
            if (options.showCheckbox) {
                item.checked && that.checkids.push((item.id || item.value));
                that.checkClick(entryDiv, item);
            }

            //操作节点
            options.edit && that.operate(entryDiv, item);

        });
    };

    //展开节点
    Class.prototype.spread = function (elem, item) {
        var that = this
            , options = that.config
            , entry = elem.children('.' + ELEM_ENTRY)
            , elemMain = entry.children('.' + ELEM_MAIN)
            , elemIcon = entry.find('.' + ICON_CLICK)
            , elemText = entry.find('.' + ELEM_TEXT)
            , touchOpen = options.onlyIconControl ? elemIcon : elemMain //判断展开通过节点还是箭头图标
            , state = '';

        //展开收缩
        touchOpen.on('click', function (e) {
            var packCont = elem.children('.' + ELEM_PACK)
                , iconClick = touchOpen.children('.layui-icon')[0] ? touchOpen.children('.layui-icon') : touchOpen.find('.layui-tree-icon').children('.layui-icon');

            //若没有子节点
            if (!packCont[0]) {
                state = 'normal';
            } else {
                if (elem.hasClass(ELEM_SPREAD)) {
                    elem.removeClass(ELEM_SPREAD);
                    packCont.slideUp(200);
                    iconClick.removeClass(ICON_SUB).addClass(ICON_ADD);
                } else {
                    elem.addClass(ELEM_SPREAD);
                    packCont.slideDown(200);
                    iconClick.addClass(ICON_SUB).removeClass(ICON_ADD);

                    //是否手风琴
                    if (options.accordion) {
                        var sibls = elem.siblings('.' + ELEM_SET);
                        sibls.removeClass(ELEM_SPREAD);
                        sibls.children('.' + ELEM_PACK).slideUp(200);
                        sibls.find('.layui-tree-icon').children('.layui-icon').removeClass(ICON_SUB).addClass(ICON_ADD);
                    };
                };
            };
        });

        //点击回调
        elemText.on('click', function () {
            var othis = $(this);

            //判断是否禁用状态
            if (othis.hasClass(DISABLED)) return;

            //判断展开收缩状态
            if (elem.hasClass(ELEM_SPREAD)) {
                state = options.onlyIconControl ? 'open' : 'close';
            } else {
                state = options.onlyIconControl ? 'close' : 'open';
            }

            //点击产生的回调
            options.click && options.click({
                elem: elem
                , state: state
                , data: item
            });
        });
    };

    //计算复选框选中状态
    Class.prototype.setCheckbox = function (elem, item, elemCheckbox) {
        var that = this
            , options = that.config
            , checked = elemCheckbox.prop('checked');

        if (elemCheckbox.prop('disabled')) return;

        //同步子节点选中状态
        if (typeof item.children === 'object' || elem.find('.' + ELEM_PACK)[0]) {
            var childs = elem.find('.' + ELEM_PACK).find('input[same="layuiTreeCheck"]');
            childs.each(function () {
                if (this.disabled) return; //不可点击则跳过
                this.checked = checked;
            });
        };

        //同步父节点选中状态
        var setParentsChecked = function (thisNodeElem) {
            //若无父节点，则终止递归
            if (!thisNodeElem.parents('.' + ELEM_SET)[0]) return;

            var state
                , parentPack = thisNodeElem.parent('.' + ELEM_PACK)
                , parentNodeElem = parentPack.parent()
                , parentCheckbox = parentPack.prev().find('input[same="layuiTreeCheck"]');

            //如果子节点有任意一条选中，则父节点为选中状态
            if (options.isCheckParent) {
                if (checked) {
                    parentCheckbox.prop('checked', checked);
                } else { //如果当前节点取消选中，则根据计算“兄弟和子孙”节点选中状态，来同步父节点选中状态
                    parentPack.find('input[same="layuiTreeCheck"]').each(function () {
                        if (this.checked) {
                            state = true;
                        }
                    });

                    //如果兄弟子孙节点全部未选中，则父节点也应为非选中状态
                    state || parentCheckbox.prop('checked', false);
                }
            }
            //向父节点递归
            setParentsChecked(parentNodeElem);
        };

        setParentsChecked(elem);

        that.renderForm('checkbox');
    };

    //复选框选择
    Class.prototype.checkClick = function (elem, item) {
        var that = this
            , options = that.config
            , entry = elem.children('.' + ELEM_ENTRY)
            , elemMain = entry.children('.' + ELEM_MAIN);



        //点击复选框
        elemMain.on('click', 'input[same="layuiTreeCheck"]+', function (e) {
            layui.stope(e); //阻止点击节点事件

            var elemCheckbox = $(this).prev()
                , checked = elemCheckbox.prop('checked');

            if (elemCheckbox.prop('disabled')) return;

            that.setCheckbox(elem, item, elemCheckbox);

            //复选框点击产生的回调
            options.oncheck && options.oncheck({
                elem: elem
                , checked: checked
                , data: item
            });
        });
    };

    //节点操作
    Class.prototype.operate = function (elem, item) {
        var that = this
            , options = that.config
            , entry = elem.children('.' + ELEM_ENTRY)
            , elemMain = entry.children('.' + ELEM_MAIN);

        entry.children('.layui-tree-btnGroup').on('click', '.layui-icon', function (e) {
            layui.stope(e);  //阻止节点操作

            var type = $(this).data("type")
                , packCont = elem.children('.' + ELEM_PACK)
                , returnObj = {
                    data: item
                    , type: type
                    , elem: elem
                };
            options.operate && options.operate(returnObj);
        });
    };

    //部分事件
    Class.prototype.events = function () {
        var that = this
            , options = that.config
            , checkWarp = that.elem.find('.layui-tree-checkedFirst');

        //初始选中
        that.setChecked(that.checkids);

        //搜索
        that.elem.find('.layui-tree-search').on('keyup', function () {
            var input = $(this)
                , val = input.val()
                , pack = input.nextAll()
                , arr = [];

            //遍历所有的值
            pack.find('.' + ELEM_TEXT).each(function () {
                var entry = $(this).parents('.' + ELEM_ENTRY);
                //若值匹配，加一个类以作标识
                if ($(this).html().indexOf(val) != -1) {
                    arr.push($(this).parent());

                    var select = function (div) {
                        div.addClass('layui-tree-searchShow');
                        //向上父节点渲染
                        if (div.parent('.' + ELEM_PACK)[0]) {
                            select(div.parent('.' + ELEM_PACK).parent('.' + ELEM_SET));
                        };
                    };
                    select(entry.parent('.' + ELEM_SET));
                };
            });

            //根据标志剔除
            pack.find('.' + ELEM_ENTRY).each(function () {
                var parent = $(this).parent('.' + ELEM_SET);
                if (!parent.hasClass('layui-tree-searchShow')) {
                    parent.addClass(HIDE);
                };
            });
            if (pack.find('.layui-tree-searchShow').length == 0) {
                that.elem.append(that.elemNone);
            };

            //节点过滤的回调
            options.onsearch && options.onsearch({
                elem: arr
            });
        });

        //还原搜索初始状态
        that.elem.find('.layui-tree-search').on('keydown', function () {
            $(this).nextAll().find('.' + ELEM_ENTRY).each(function () {
                var parent = $(this).parent('.' + ELEM_SET);
                parent.removeClass('layui-tree-searchShow ' + HIDE);
            });
            if ($('.layui-tree-emptyText')[0]) $('.layui-tree-emptyText').remove();
        });
    };

    //得到选中节点
    Class.prototype.getChecked = function () {
        var that = this
            , options = that.config
            , checkId = []
            , checkData = [];

        //遍历节点找到选中索引
        that.elem.find('.layui-form-checked').each(function () {
            checkId.push($(this).prev()[0].value);
        
        });

        //遍历节点
        var eachNodes = function (data, checkNode) {
          
            layui.each(data, function (index, item) {
                layui.each(checkId, function (index2, item2) {
                    if ((item.id || item.value) == item2) {
                        var cloneItem = $.extend({}, item);
                        delete cloneItem.children;
                       
                        checkNode.push(cloneItem);
                       
                        if (item.children) {
                            cloneItem.children = [];
                            eachNodes(item.children, cloneItem.children);
                        }
                        return true
                    }
                });
            });
        };

        eachNodes($.extend({}, options.data), checkData);
       
        return checkData;
    };

    //设置选中节点
    Class.prototype.setChecked = function (checkedId) {
        var that = this
            , options = that.config;

        //初始选中
        that.elem.find('.' + ELEM_SET).each(function (i, item) {
            var thisId = $(this).data('id')
                , input = $(item).children('.' + ELEM_ENTRY).find('input[same="layuiTreeCheck"]')
                , reInput = input.next();

            //若返回数字
            if (typeof checkedId === 'number') {
                if (thisId == checkedId) {
                    if (!input[0].checked) {
                        reInput.click();
                    };
                    return false;
                };
            }
            //若返回数组
            else if (typeof checkedId === 'object') {
                layui.each(checkedId, function (index, value) {
                    if (value == thisId && !input[0].checked) {
                        reInput.click();
                        return true;
                    }
                });
            };
        });
    };

    //记录所有实例
    thisModule.that = {}; //记录所有实例对象
    thisModule.config = {}; //记录所有实例配置项

    //重载实例
    tree.reload = function (id, options) {
        var that = thisModule.that[id];
        that.reload(options);

        return thisModule.call(that);
    };

    //获得选中的节点数据
    tree.getChecked = function (id) {
        var that = thisModule.that[id];
        return that.getChecked();
    };

    //设置选中节点
    tree.setChecked = function (id, checkedId) {
        var that = thisModule.that[id];
        return that.setChecked(checkedId);
    };

    //核心入口
    tree.render = function (options) {
        var inst = new Class(options);
        return thisModule.call(inst);
    };

    exports(MOD_NAME, tree);
})