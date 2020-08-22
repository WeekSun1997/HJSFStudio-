layui.define(['jquery', 'form', 'table', 'layer', 'setter', 'view', 'admin'], function (exports) {
    "use strict";

    var $ = layui.$
        , layer = layui.layer
        , setter = layui.setter
        , view = layui.view
        , admin = layui.admin
        , form = layui.form
        , table = layui.table

        //外部接口
        , com = {
            //基础参数
            config: {}
            //全局设置
            , set: function (options) {
                var that = this;
                $.extend(true, that.config, options);
                return that;
            }
            //核心入口
            , render: function (options) {
                var c = new Class(options);
                c.init();
                return c;
            }
            , getPopupArea: function (area) {
                return admin.screen() < 2 ? ['94%', '96%'] : area || ['800px', '600px'];
            }
            //获取Token字符串
            , getToken: function () {
                var token = layui.data(setter.tableName)[setter.request.tokenName] || '';
                return token || '';
            }
            //添加请求token
            , addToken: function (data) {
                var tk = {};
                tk[setter.request.tokenName] = this.getToken();
                return $.extend(data || {}, tk);
            }
            //视图路由
            , viewrouter: function (href) {
                if (/^\//.test(href)) return href;
                return setter.views + href + setter.engine;
            }
            //API路由
            , apirouter: function (href) {
                if (/^\//.test(href)) return href;
                return '/' + setter.apiVersion + '/' + href;
            }
            //API请求
            , post: function (opt) {

                var that = this,
                    success = opt.success,
                    error = opt.error,
                    done = opt.done;

                delete opt.success;
                delete opt.error;
                delete opt.done;

                opt.url = that.apirouter(opt.url);

                var loadIndex = layer.load(2);

                admin.req($.extend({
                    type: 'post',
                    //contentType: "application/json",
                    //dataType: 'json',
                    done: function (result) {
                        typeof done === 'function' && done(result);
                    },
                    success: function (result) {
                        layer.close(loadIndex);
                        typeof success === 'function' && success(result);
                    },
                    error: function (e, code) {
                        layer.close(loadIndex);
                        typeof error === 'function' && error(e);
                    }
                }, opt));

            }
            //弹窗
            , popup: function (opt) {

                var defaultOption = $.extend({
                    id: opt.id || '',
                    title: '窗口',
                    type: 1,
                    skin: 'layui-layer-rim',
                    anim: 0,
                    resize: false,
                    maxmin: true,
                    shade: 0.1,
                    area: this.getPopupArea()
                }, opt);

                return layer.open(defaultOption);
            }
            //文件下载
            , download: function (d, url) {
                url = this.apirouter(url);
                d = this.addToken(d);
                var dlform = document.createElement('form');
                dlform.style = "display:none;";
                dlform.method = 'post';
                dlform.action = url;
                dlform.target = 'download';
                for (var key in d) {
                    var hdnInput = document.createElement('input');
                    hdnInput.type = 'hidden';
                    hdnInput.name = key;
                    hdnInput.value = d[key];
                    dlform.appendChild(hdnInput);
                }
                document.body.appendChild(dlform);
                dlform.submit();
                document.body.removeChild(dlform);
                return false;
            }
            //成功提示信息
            , success: function (msg, fun) {
                layer.msg(msg, { icon: 1, time: 2000, offset: '15px' }, function () {
                    typeof fun === 'function' && fun();
                });
            }
            //错误提示信息
            , error: function (msg, fun) {
                layer.msg(msg, { icon: 2, time: 3000, offset: '15px' }, function () {
                    typeof fun === 'function' && fun();
                });
            }


            // 选择表单
            , select: function (opt) {
                var that = this,
                    param = $.extend({
                        //元素
                        elem: '',
                        //数据请求Url
                        url: '',
                        //请求方式
                        type: 'post',
                        //提示文本
                        tip: '',
                        //默认值
                        dv: [],
                        //数据
                        data: [],
                        //文本字段名称
                        fieldText: 'text',
                        //值字段名称
                        fieldValue: 'value',
                        //查询条件
                        where: {},
                        //表单类型：select,checkbox,radio
                        inputType: 'select',
                        //表单名称
                        inputName: '',
                        //filter名称
                        filterName: ''
                    }, opt);

                // 获取数据
                if (param.data && param.data.length > 0) {
                    that.initselect(param);
                } else {
                    if (param.url) {
                        param.url = com.apirouter(param.url);
                        $.ajax({
                            url: param.url,
                            type: param.type,
                            data: param.where || {},
                            //dataType: 'json',
                            headers: {
                                "Authorization": com.getToken()
                            },
                            success: function (res) {
                                if (res.code != 0) {
                                    layer.msg(res.msg, { icon: 2, time: 3000 });
                                }
                                else {
                                    param.data = res.data;
                                    that.initselect(param);
                                }
                            },
                            error: function (e) {
                                layer.msg(param.tip + '数据加载失败', { icon: 2, time: 3000 });
                            }
                        });

                    }

                }
            }
            // 渲染表单元素
            , initselect: function (param) {
                $(param.elem).each(function (index, el) {
                    $(el).empty();
                    var html = '';
                    if (param.inputType === 'select') {
                        if (param.tip) {
                            html += '<option value="">' + param.tip + '</option>';
                        }
                        layui.each(param.data, function (index, item) {
                            html += '<option value="' + item[param.fieldValue] + '" ' + (param.dv.indexOf(item[param.fieldValue]) > -1 ? 'selected' : '') + '>' + item[param.fieldText] + '</option>';
                        });
                    }
                    else if (param.inputType === 'checkbox') {
                        layui.each(param.data, function (index, item) {
                            html += '<input type="checkbox" name="' + param.inputName + '" value="' + item[param.fieldValue] + '" title="' + item[param.fieldText] + '" lay-skin="primary" ' + (param.dv.indexOf(item[param.fieldValue]) > -1 ? ' checked' : '') + (param.filterName ? (' lay-filter="' + param.filterName + '"') : '') + '>';
                        });
                    }
                    else if (param.inputType === 'radio') {
                        layui.each(param.data, function (index, item) {
                            html += '<input type="radio" name="' + param.inputName + '" value="' + item[param.fieldValue] + '" title="' + item[param.fieldText] + '" ' + (param.dv.indexOf(item[param.fieldValue]) > -1 ? ' checked' : '') + (param.filterName ? (' lay-filter="' + param.filterName + '"') : '') + '>';
                        });
                    }

                    $(el).append(html);
                });
                form.render();
            }
            //获取时间日期部分
            , getDate: function (d) {
                return d ? d.split(' ')[0] : '';
            }
            // 数字转中文大小
            , bindNumberToChinese: function (opt) {
                /*
                 * opt.numberInput //数字表单元素  #id .id
                 * opt.chineseInput //中文表单元素 #id .id                 * 
                 */
                var elem = $(opt.chineseInput);
                $(opt.numberInput).blur(function () {
                    var n = $(this).val();
                    if (!n) {
                        elem.val('');
                        return false;
                    }
                    if (!/^(0|[1-9]\d*)(\.\d+)?$/.test(n)) {
                        elem.val('');
                        return false;
                    }
                    var unit = "千百拾亿千百拾万千百拾元角分", str = "";
                    n += "00";
                    var p = n.indexOf('.');
                    if (p >= 0)
                        n = n.substring(0, p) + n.substr(p + 1, 2);
                    unit = unit.substr(unit.length - n.length);
                    for (var i = 0; i < n.length; i++)
                        str += '零壹贰叁肆伍陆柒捌玖'.charAt(n.charAt(i)) + unit.charAt(i);
                    str = str.replace(/零(千|百|拾|角)/g, "零")
                        .replace(/(零)+/g, "零")
                        .replace(/零(万|亿|元)/g, "$1")
                        .replace(/(亿)万|壹(拾)/g, "$1$2")
                        .replace(/^元零?|零分/g, "")
                        .replace(/元$/g, "元整");

                    elem.val(str);
                    return false;
                })
            }
            , numberToChinese: function (n) {
                if (!n) {
                    return '';
                }
                if (!/^(0|[1-9]\d*)(\.\d+)?$/.test(n)) {
                    return '';
                }
                var unit = "千百拾亿千百拾万千百拾元角分", str = "";
                n += "00";
                var p = n.indexOf('.');
                if (p >= 0)
                    n = n.substring(0, p) + n.substr(p + 1, 2);
                unit = unit.substr(unit.length - n.length);
                for (var i = 0; i < n.length; i++)
                    str += '零壹贰叁肆伍陆柒捌玖'.charAt(n.charAt(i)) + unit.charAt(i);
                str = str.replace(/零(千|百|拾|角)/g, "零")
                    .replace(/(零)+/g, "零")
                    .replace(/零(万|亿|元)/g, "$1")
                    .replace(/(亿)万|壹(拾)/g, "$1$2")
                    .replace(/^元零?|零分/g, "")
                    .replace(/元$/g, "元整");

                return str;

            }
            //禁用表单元素
            , disabledForm: function (popup) {
                if (popup.defaultVal.isCanNotEdit == true) {
                    $('#' + popup.formId + ' input').attr('disabled', true);
                    $('#' + popup.formId + ' textarea').attr('disabled', true);
                    $('#' + popup.formId + ' select').attr('disabled', true);
                    $('#' + popup.formId + ' reado').attr('disabled', true);
                    $('#' + popup.formId + ' .edit-button-line').hide();
                }
            }
        }
        //构造器
        , Class = function (options) {
            //var that = this;
            let _config = {
                //参数链接
                paramUrl: '',
                //缓存参数列表
                param: {},
                //主键名称
                primaryKey: 'id',
                //是否自动初始化
                autoInit: true,
                //自动加载按钮
                initButton: true,
                //表格内图片预览参数名称  
                photoPreviewName: '',
                //表单视图展示方式：popup/view     
                formType: 'popup',
                //栏目事件标识名称，用于获取当前模块操作按钮    
                channelEvent: '',
                //栏目名称  
                channelName: '',
                //div主框架名称，通常用于获取其内部元素进行操作   
                elemFulid: '',
                //表格元素名称
                elemTable: '',
                //表格内状态开关名称   
                elemTableSwitch: '',
                //表单名称       
                elemForm: '',
                //操作按钮   
                elemBtnToolBar: '',
                //搜索按钮名称   
                elemBtnSearch: '',
                //提交按钮名称  
                elemBtnSubmit: '',
                //添加初始化信息  
                addEntity: { status: 1 },
                //弹窗参数
                popup: {},
                //行显示按钮列表
                toolButtons: ['detail', 'edit', 'remove'],
                //当前栏目所有可以执行按钮
                allButtons: [],
                //附加事件
                events: {},
                //表格初始化信息
                tableInfo: {
                    cols: [[
                        { type: 'checkbox', fixed: 'left' },
                        { field: 'Id', title: '编号', width: 80, align: 'center' }
                    ]]
                },

                //是否远程加载编辑数据
                editIsLoad: false,
                //是否远程加载详情数据
                detailIsLoad: false,

                //表格重载
                reloadTable: function (opt) {
                    table.reload(this.elemTable, opt || {});
                },
                //获取表格选择行数据
                getCheckData: function () {
                    var checkStatus = table.checkStatus(this.elemTable);
                    return checkStatus.data;
                },
                //获取编辑页面默认Html
                editBodyHtml: function (opt) {
                    var html = '';
                    html += '<form class="layui-form edit-form" id="' + opt.formId + '" lay-filter="' + opt.formId + '">';
                    html += '<div class="layui-row" id="' + opt.bodyId + '"></div>';
                    html += '   <div class="layui-form-item edit-button-line">';
                    html += '        <button type="button" class="layui-btn" lay-submit lay-filter="' + opt.submitButtonId + '" id="' + opt.submitButtonId + '" style="width:180px;">提交保存</button>';
                    html += '   </div>';
                    html += '</form>';
                    return html;
                },
                //详情页面默认Html
                detailBodyHtml: function (opt) {
                    var html = '';
                    html += '<form class="layui-form edit-form" id="' + opt.formId + '" lay-filter="' + opt.formId + '">';
                    html += '<div class="layui-row" id="' + opt.bodyId + '"></div>';
                    html += '</form>';
                    return html;
                },
                //列表视图加载成功执行事件
                listSuccess: function () { },
                //添加编辑等视图初始化完成执行事件
                viewSuccess: function (popupconfig) {

                    //form.render(null, this.config.elemForm);
                },
                //查看详情视图初始化完成执行事件
                detailSuccess: function (popupconfig) {

                },
                //添加编辑提交前执行事件，返回null或者false将终止请求
                postBegin: function (popupconfig, d) {
                    d.status = d.status || 0;
                    return d;
                },
                //添加编辑提交后执行事件
                postDone: function (popupconfig, result) {
                    var that = this;
                    that.reloadTable({}); //重载表格
                    that.success(result.msg);
                    if (popupconfig) {
                        layer.close(popupconfig.index); //执行关闭 
                    }
                },

                //成功提示信息
                success: function (msg, fun) {
                    com.success(msg, fun);
                },
                //错误提示信息
                error: function (msg, fun) {
                    com.error(msg, fun);
                }

            };
            this.config = $.extend({}, _config, options);
        };


    //表单验证
    form.verify({
        //非空，用于下拉获取光标
        notempty: function (value, item) {
            if (!value) {
                let el = $(item);
                let text = el.attr('lay-reqtext');
                el.next().find('input').focus();
                return text || '必填项不能为空';
            }
        },
        //正数
        positivenumber: function (value, item) {
            if (value && value.length > 0) {
                var reg = /^\d+(?=\.{0,1}\d+$|$)/;
                if (!reg.test(value)) {
                    $(item).focus();
                    return '请输入正确的正整数';
                }
            }
        }
        //正数
        , qq: function (value, item) {
            if (value && value.length > 0) {
                var reg = /^[1-9]\d{4,12}$/;
                if (!reg.test(value)) {
                    $(item).focus();
                    return '请输入正确的QQ号';
                }
            }
        }
        // 小数(金额)：
        , Amount: function (value, item) {
            if (value && value.length > 0) {
                var reg = /^[1-9][0-9]*([.][0-9]{1,2})?$/;
                if (!reg.test(value)) {
                    $(item).focus();
                    return '请输入有效的数字';
                }
            }

        }
        , email: function (value, item) {
            if (value && value.length > 0) {
                if (/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/.test(value) == false) {
                    $(item).focus();
                    return "请填写正确的邮箱地址";
                }
            }
        }
        , tel: function (value, item) {
            if (value && value.length > 0) {
                if (/(^(\d{3,4}-)?\d{7,8})$|(13[0-9]{9})/.test(value) == false) {
                    $(item).focus();
                    return "请填写正确的电话号码";
                }
            }
        }
        , phone: function (value, item) {
            if (value && value.length > 0) {
                if (/^1\d{10}$/.test(value) == false) {
                    $(item).focus();
                    return "请填写正确的手机号码";
                }
            }
        }
        , url: function (value, item) {
            if (value && value.length > 0) {
                if (/(^#)|(^http(s*):\/\/[^\s]+\.[^\s]+)/.test(value) == false) {
                    $(item).focus();
                    return "请填写正确的链接地址";
                }
            }
        }
        , number: function (value, item) {
            if (value && value.length > 0) {
                if (!value || isNaN(value)) {
                    $(item).focus();
                    return "只能填写数字";
                }
            }
        }
    });


    //初始化
    Class.prototype.init = function () {
        var that = this;

        //主区域
        that.config.elemFulid = that.config.elemFulid || 'YY-fulid' + '-' + that.config.channelEvent;
        //表格
        that.config.elemTable = that.config.elemTable || 'YY-table' + '-' + that.config.channelEvent;
        //状态开关
        that.config.elemTableSwitch = that.config.elemTableSwitch || 'YY-table-Status' + '-' + that.config.channelEvent;
        //搜索按钮
        that.config.elemBtnSearch = that.config.elemBtnSearch || 'YY-btn-search' + '-' + that.config.channelEvent;
        //操作按钮
        that.config.elemBtnToolBar = that.config.elemBtnToolBar || 'YY-toolbar' + '-' + that.config.channelEvent;

        //加载参数
        if (that.config.paramUrl) {
            com.post({
                url: that.config.paramUrl,
                //done: function (res) {
                //    that.initAll();
                //},
                success: function (res) {
                    that.config.param = $.extend({}, that.config.param, res.data || {});
                    that.initAll();
                }
            });
        }
        else {
            that.initAll();
        }

        form.render();
    }

    //初始化所有
    Class.prototype.initAll = function () {

        //this.initList()  //列表视图
        //    .initTable() //表格
        //    .initSearch() //搜索按钮
        //    .initStatusSwitch() //状态开关
        //    .initButton() //操作按钮
        //    .initTool();  //工具条



        var that = this;
        if (that.config.initButton) {
            com.post({
                url: setter.buttonGetUrl,
                data: { "eventName": that.config.channelEvent },
                done: function (res) {
                    //that.config.allButtons = that.config.allButtons || [];
                    layui.each(res.data, function (index, item) {
                        that.config.allButtons.push(item);
                    });

                    if (that.config.autoInit) {
                        that.initList()  //列表视图
                            .initTable() //表格
                            .initSearch() //搜索按钮
                            .initStatusSwitch() //状态开关
                            .initButton() //操作按钮
                            .initTool();  //工具条
                    };
                }
            });
        }
        else {
            if (that.config.autoInit) {
                that.initList()  //列表视图
                    .initTable() //表格
                    .initSearch() //搜索按钮
                    .initStatusSwitch() //状态开关    
                    .initButton() //操作按钮            
                    .initTool();  //工具条
            }
        }

    }

    //获取弹窗参数
    Class.prototype.popupConfig = function (opt) {
        var that = this, dynamicId = new Date().getTime();
        opt = opt || {};
        //重新组装参数
        var popupDefaultConfig = {
            //是否编辑，编辑将自动添加form，button元素
            isEdit: true,
            //动态id
            dynamicId: dynamicId,
            //弹窗id
            popupId: 'YY-popup-' + dynamicId,
            //窗体名称
            popupTitle: '窗口',
            //表单id
            formId: 'YY-from-' + dynamicId,
            //表单内容id
            bodyId: 'YY-body-' + dynamicId,
            //提交按钮id
            submitButtonId: 'YY-btn-submit-' + dynamicId,
            //关闭按钮id
            closeButtonId: 'YY-btn-close-' + dynamicId,
            //默认值
            defaultVal: {},
            //表单提交前事件
            postbegin: function (d) { },
            ////数据提交成功事件
            //postdone: function (result) { that.config.postDone(result); },
            ////视图加载成功事件
            //viewdone: function () { },
            ////数据保存URL
            //saveUrl: "",
            ////视图URL
            //viewUrl: "",
            //弹窗html
            //html: ''
            ////弹窗元素
            //layero: null,
            ////弹窗索引
            //index:0
        }

        return $.extend({}, popupDefaultConfig, opt);

    }

    //添加，编辑弹窗
    Class.prototype.popup = function (opt) {

        var that = this;
        //弹窗
        com.popup($.extend({
            id: opt.popupId,
            title: opt.popupTitle,
            content: opt.html,
            success: function (layero, index) {
                //赋值
                opt.layero = layero;
                opt.index = index;
                if (opt.viewUrl) {
                    //加载视图
                    view(opt.bodyId)
                        //视图渲染
                        .render(opt.viewUrl, opt.defaultVal)
                        //视图加载完成事件
                        .done(function () {
                            //视图成功事件
                            typeof opt.viewdone === 'function' && opt.viewdone(opt);
                            if (opt.isEdit) {
                                //监听提交
                                form.on('submit(' + opt.submitButtonId + ')', function (data) {
                                    //获取提交的字段
                                    var field = opt.postbegin(data.field);
                                    if (field === false || field === null) {
                                        return false;
                                    }
                                    var btn = $(data.elem);
                                    btn.attr("disabled", true);
                                    btn.addClass('layui-btn-disabled');
                                    btn.html('数据处理中...');
                                    com.post({
                                        url: opt.saveUrl,
                                        data: field,
                                        done: function (result) {
                                            if (typeof opt.postdone === 'function') {
                                                opt.postdone(result);
                                            }
                                        },
                                        success: function (res) {
                                            if (res.code !== 0) {
                                                btn.attr("disabled", false);
                                                btn.removeClass('layui-btn-disabled');
                                                btn.html('提交保存');
                                            }
                                            else {
                                                btn.html('提交成功');
                                            }
                                        },
                                        error: function (e) {
                                            btn.attr("disabled", false);
                                            btn.removeClass('layui-btn-disabled');
                                            btn.html('提交保存');
                                        }
                                    });
                                    return false;
                                });
                            }

                            $('#' + opt.closeButtonId).click(function () {
                                layer.close(index);
                            });

                            form.render(null, opt.formId);

                        });
                }
                else {
                    typeof opt.viewdone === 'function' && opt.viewdone(opt);
                    form.render(null, opt.formId);
                }
            }
        }, that.config.popup));

    }

    //初始化列表视图
    Class.prototype.initList = function () {
        this.config.listSuccess();
        return this;
    };

    //设置表格
    Class.prototype.initTable = function () {

        var that = this;

        var url = $('#' + that.config.elemTable).data('href');
        url = com.apirouter(url);
        var o = $.extend({
            elem: '#' + that.config.elemTable,
            page: {
                limit: 30
            },
            cols: [[]],
            url: url,
            //toolbar: '<div>'
            //    + '    <div class="layui-btn-container" id="' + that.config.elemBtnToolBar + '"> '
            //    //+ '         <button type="button" class="layui-btn layui-btn-sm" data-href="' + that.config.channelEvent + '/add" data-viewurl="' + that.config.channelEvent + '/edit" lay-event="add"><i class="layui-icon layui-icon-add-1"></i>添加</button> '
            //    //+ '         <button type="button" class="layui-btn layui-btn-sm" data-href="' + that.config.channelEvent + '/edit" data-viewurl="' + that.config.channelEvent + '/edit" lay-event="edit"><i class="layui-icon layui-icon-edit"></i>编辑</button> '
            //    //+ '         <button type="button" class="layui-btn layui-btn-sm" data-href="' + that.config.channelEvent + '/remove" data-viewurl="' + that.config.channelEvent + '/remove" lay-event="remove"><i class="layui-icon layui-icon-delete"></i>删除</button> '
            //    //+ '         <button type="button" class="layui-btn layui-btn-sm" data-href="' + that.config.channelEvent + '/enable" data-viewurl="' + that.config.channelEvent + '/enable" lay-event="enable"><i class="layui-icon layui-icon-edit"></i>启用</button> '
            //    //+ '         <button type="button" class="layui-btn layui-btn-sm" data-href="' + that.config.channelEvent + '/disable" data-viewurl="' + that.config.channelEvent + '/disable" lay-event="disable"><i class="layui-icon layui-icon-delete"></i>禁用</button> '
            //    //+ '         <button type="button" class="layui-btn layui-btn-sm" data-href="' + that.config.channelEvent + '/detail" data-viewurl="' + that.config.channelEvent + '/detail" lay-event="detail"><i class="layui-icon layui-icon-delete"></i>详情</button> '
            //    + '    </div>'
            //    + '</div>',
            toolbar: false,
            method: 'post',
            size: 'sm',
            even: true,
            loading: true,
            //height: 'full-230',
            //defaultToolbar: ['filter', 'print', {
            //    title: '刷新' //标题
            //    , layEvent: 'LAYTABLE_REFRESH' //事件名，用于 toolbar 事件中使用
            //    , icon: 'layui-icon-refresh-3' //图标类名
            //}]
        }, that.config.tableInfo);

        table.render(o);

        return that;
    };

    //重载表格
    Class.prototype.reloadTable = function (opt) {
        this.config.reloadTable(opt);
        return false;
    };

    //设置状态点击事件
    Class.prototype.initStatusSwitch = function () {
        var that = this;

        //监听开关
        form.on('switch(' + that.config.elemTableSwitch + ')', function (obj) {

            var url = $(obj.elem).data('href');
            var keys = {};
            keys[that.config.primaryKey] = this.value;
            var obj = {
                data: [keys],
                url: url,
                elem: obj.elem
            };
            if (obj.elem.checked) {
                //layer.confirm('确定修改状态为启用吗？', { icon: 3, title: '提示' }, function (index) {
                that.enable(obj);
                //});
            }
            else {
                //layer.confirm('确定修改状态为禁用吗？', { icon: 3, title: '提示' }, function (index) {
                that.disable(obj);
                //});
            }
        });
        return that;
    };

    //设置工具条
    Class.prototype.initTool = function () {
        var that = this;

        let active = {
            //刷新表格
            LAYTABLE_REFRESH: function (obj) {
                that.reloadTable();
            },
            //添加按钮
            add: function (obj) {
                that.add(obj);
                return false;
            },
            //修改
            edit: function (obj) {
                that.edit(obj);
                return false;
            },
            //删除
            remove: function (obj) {
                that.remove(obj);
                return false;
            },
            //启用
            enable: function (obj) {
                that.enable(obj);
                return false;
            },
            //禁用
            disable: function (obj) {
                that.disable(obj);
                return false;
            },
            //回收
            recovery: function (obj) {
                that.recovery(obj);
                return false;
            },
            //还原
            restore: function (obj) {
                that.restore(obj);
                return false;
            },
            //查看
            detail: function (obj) {
                that.detail(obj);
                return false;
            },
            //打印
            print: function (obj) {
                that.print(obj);
                return false;
            },
            //图片预览
            preview: function (obj) {
                if (obj.url && obj.url.length > 0) {
                    layer.photos({
                        photos: {
                            "title": "查看图片" //相册标题
                            , "data": [{
                                "src": obj.url //原图地址
                            }]
                        }
                        , shade: 0.01
                        , closeBtn: 1
                        , anim: 5
                    });
                }
            }
        };

        active = $.extend(active, that.config.events);

        //头工具栏事件
        table.on('toolbar(' + that.config.elemTable + ')', function (obj) {

            let url = $(this).data('href');
            let viewurl = $(this).data('viewurl');
            let checkStatus = table.checkStatus(obj.config.id);
            let data = checkStatus.data;
            let type = obj.event;
            var param = { data: data, url: url, viewurl: viewurl };
            active[type] ? active[type].call(this, param) : '';
        });

        //监听行工具事件
        table.on('tool(' + that.config.elemTable + ')', function (obj) {

            let url = $(this).data('href');
            let viewurl = $(this).data('viewurl');
            ///当前行的数据
            let data = [obj.data];
            let type = obj.event;
            var param = { data: data, url: url, viewurl: viewurl };
            active[type] ? active[type].call(this, param) : '';


        });

        $('#' + that.config.elemBtnToolBar + ' button').on('click', function () {
            let url = $(this).data('href');
            let viewurl = $(this).data('viewurl');
            let checkStatus = table.checkStatus(that.config.elemTable);
            let data = checkStatus.data;
            var type = $(this).data('type');
            var param = { data: data, url: url, viewurl: viewurl };
            active[type] ? active[type].call(this, param) : '';
        });
        return that;
    };

    //初始化搜索按钮
    Class.prototype.initSearch = function () {
        var that = this;
        //监听搜索
        form.on('submit(' + that.config.elemBtnSearch + ')', function (data) {
            that.reloadTable({ where: data.field });
            return false;
        });

        $('#' + that.config.elemBtnSearch).next('button[type="reset"]').click(function () {
            setTimeout(function () {
                $('#' + that.config.elemBtnSearch).click();
            }, 100);
        });
        
        return that;
    };

    //初始化操作按钮
    Class.prototype.initButton = function () {
        if (!this.config.initButton) return;
        let that = this,
            elem = $('#' + that.config.elemBtnToolBar),
            html = '';
        layui.each(that.config.allButtons, function (index, item) {
            if (item.isShow == true) {
                html += '<button type="button" class="layui-btn layui-btn-sm ' + (item.className || '') + '" data-href="' + item.channelLink + '" data-viewurl="' + (item.viewLink || item.channelLink) + '" data-type="' + item.eventName + '"><i class="layui-icon ' + item.iconName + '"></i>' + item.channelName + '</button>';
            }
        });
        elem.html(html);
        return that;
    }

    //插入表单元素
    Class.prototype.select = {
        // 渲染
        render: function (opt) {

            var that = this,
                param = $.extend({
                    //元素
                    elem: '',
                    //数据请求Url
                    url: '',
                    //请求方式
                    type: 'post',
                    //提示文本
                    tip: '',
                    //默认值
                    dv: [],
                    //数据
                    data: [],
                    //文本字段名称
                    fieldText: 'text',
                    //值字段名称
                    fieldValue: 'value',
                    //查询条件
                    where: {},
                    //表单类型：select,checkbox,radio
                    inputType: 'select',
                    //表单名称
                    inputName: ''

                }, opt);

            param.url = com.apirouter(param.url);
            // 获取数据
            if (param.data && param.data.length > 0) {
                that.init(param);
            } else {

                $.ajax({
                    url: param.url,
                    type: param.type,
                    data: param.where || {},
                    dataType: 'json',
                    headers: {
                        token: com.getToken()
                    },
                    success: function (res) {
                        if (res.code != 0) {
                            layer.alert(res.msg, { icon: 2 });
                        }
                        else {
                            param.data = res.data;
                            that.init(param);
                        }
                    },
                    error: function (e) {
                        layer.alert('数据加载失败', { icon: 2 });
                    }
                });
            }
        },
        // 渲染表格
        init: function (param) {

            $(param.elem).each(function (index, el) {
                $(el).empty();
                var html = '';
                if (param.inputType === 'select') {
                    if (param.tip) {
                        html += '<option value="">' + param.tip + '</option>';
                    }
                    layui.each(param.data, function (index, item) {
                        html += '<option value="' + item[param.fieldValue] + '" ' + (param.dv.indexOf(item[param.fieldValue]) > -1 ? 'selected' : '') + '>' + item[param.fieldText] + '</option>';
                    });
                }
                else if (param.inputType === 'checkbox') {
                    layui.each(param.data, function (index, item) {
                        html += '<input type="checkbox" name="' + param.inputName + '" value="' + item[param.fieldValue] + '" title="' + item[param.fieldText] + '" lay-skin="primary" ' + (param.dv.indexOf(item[param.fieldValue]) > -1 ? 'checked' : '') + '>';
                    });
                }
                else if (param.inputType === 'radio') {
                    layui.each(param.data, function (index, item) {
                        html += '<input type="radio" name="' + param.inputName + '" value="' + item[param.fieldValue] + '" title="' + item[param.fieldText] + '" ' + (param.dv.indexOf(item[param.fieldValue]) > -1 ? 'checked' : '') + '>';
                    });
                }

                $(el).append(html);
            });
        }
    };

    //添加按钮
    Class.prototype.add = function (obj) {
        var that = this;
        console.info(that)
        if (that.config.formType === 'popup') {

            var popupConfig = that.popupConfig({
                isEdit: true,
                viewUrl: obj.viewurl,
                saveUrl: obj.url,
                popupTitle: "添加" + that.config.channelName,
                defaultVal: that.config.addEntity,
                postbegin: function (d) {
                    return that.config.postBegin(popupConfig, d);
                },
                postdone: function (result) {
                    that.config.postDone(popupConfig, result);
                },
                viewdone: function (opt) {
                    that.config.viewSuccess(opt);
                }
            });

            //重新赋值接口设置参数
            //弹窗id
            popupConfig.popupId = that.config.elemPopup || popupConfig.popupId;
            //表单id
            popupConfig.formId = that.config.elemForm || popupConfig.formId;
            //提交按钮id
            popupConfig.submitButtonId = that.config.elemBtnSubmit || popupConfig.submitButtonId;

            popupConfig.html = that.config.editBodyHtml(popupConfig);

            that.popup(popupConfig);
        }
        else {
            window.location.hash = '/'+ obj.viewurl;
        }
        return false;
    };

    //修改
    Class.prototype.edit = function (obj) {
        var that = this;
        var dataCount = obj.data.length;
        if (dataCount < 1) {
            com.error("请选择要修改的数据");
            return false;
        }
        if (dataCount > 1) {
            com.error("只允许同时修改一条数据");
            return false;
        }

        obj.viewurl = obj.viewurl || obj.url;
        var popupUrl = obj.viewurl + "/" + obj.data[0][that.config.primaryKey];
        if (that.config.formType === 'popup') {

            if (that.config.editIsLoad) {
                com.post({
                    url: popupUrl,
                    type: 'get',
                    done: function (res) {

                        var popupConfig = that.popupConfig({
                            isEdit: true,
                            viewUrl: obj.viewurl,
                            saveUrl: obj.url,
                            popupTitle: "编辑" + that.config.channelName,
                            defaultVal: res.data,
                            postbegin: function (d) {
                                return that.config.postBegin(popupConfig, d);
                            },
                            postdone: function (result) {
                                that.config.postDone(popupConfig, result);
                            },
                            viewdone: function (opt) {
                                that.config.viewSuccess(opt);
                                com.disabledForm(popupConfig);
                            }
                        });

                        //重新赋值接口设置参数
                        //弹窗id
                        popupConfig.popupId = that.config.elemPopup || popupConfig.popupId;
                        //表单id
                        popupConfig.formId = that.config.elemForm || popupConfig.formId;
                        //提交按钮id
                        popupConfig.submitButtonId = that.config.elemBtnSubmit || popupConfig.submitButtonId;

                        popupConfig.html = that.config.editBodyHtml(popupConfig);

                        that.popup(popupConfig);

                    }
                });

            }
            else {

                var popupConfig = that.popupConfig({
                    isEdit: true,
                    viewUrl: obj.viewurl,
                    saveUrl: obj.url,
                    popupTitle: "编辑" + that.config.channelName,
                    defaultVal: obj.data[0],
                    postbegin: function (d) {
                        return that.config.postBegin(popupConfig, d);
                    },
                    postdone: function (result) {
                        that.config.postDone(popupConfig, result);
                    },
                    viewdone: function (opt) {
                        that.config.viewSuccess(opt);
                        com.disabledForm(popupConfig);
                    }
                });

                //重新赋值接口设置参数
                //弹窗id
                popupConfig.popupId = that.config.elemPopup || popupConfig.popupId;
                //表单id
                popupConfig.formId = that.config.elemForm || popupConfig.formId;
                //提交按钮id
                popupConfig.submitButtonId = that.config.elemBtnSubmit || popupConfig.submitButtonId;

                popupConfig.html = that.config.editBodyHtml(popupConfig);

                that.popup(popupConfig);
            }
        }
        else {
            window.location.hash = popupUrl;
        }

        return false;
    };

    //删除
    Class.prototype.remove = function (obj) {
        var that = this;
        var dataCount = obj.data.length;
        if (dataCount < 1) {
            that.config.error("请选择要删除的数据");
            return false;
        }
        layer.confirm('信息删除后不可恢复，是否继续删除？', { icon: 3, title: '提示' }, function (index) {
            var postdata = [];
            $.each(obj.data, function (ei, item) {
                postdata.push(item[that.config.primaryKey]);
            });

            layer.close(index);
            com.post({
                url: obj.url,
                data: { "": postdata },
                done: function (res) {
                    that.config.postDone({}, res);
                }
            });
        });
        return false;
    };

    //启用
    Class.prototype.enable = function (obj) {
        var that = this;

        var dataCount = obj.data.length;
        if (dataCount < 1) {
            that.config.error("请选择要启用的数据");
            return false;
        }

        var postdata = [];
        $.each(obj.data, function (ei, item) {
            postdata.push(item[that.config.primaryKey]);
        });

        com.post({
            url: obj.url,
            data: { "": postdata },
            done: function (res) {
                that.config.postDone({}, res);
            },
            success: function (res) {
                if (res.code !== 0 && obj.elem) {
                    obj.elem.checked = false;
                    form.render('checkbox');
                }
            },
            error: function () {
                if (obj.elem) {
                    obj.elem.checked = false;
                    form.render('checkbox');
                }
            }
        });
        return false;
    };

    //禁用
    Class.prototype.disable = function (obj) {
        var that = this;
        var dataCount = obj.data.length;
        if (dataCount < 1) {
            that.config.error("请选择要禁用的数据");
            return false;
        }

        var postdata = [];
        $.each(obj.data, function (ei, item) {
            postdata.push(item[that.config.primaryKey]);
        });

        com.post({
            url: obj.url,
            data: { "": postdata },
            done: function (res) {
                that.config.postDone({}, res);
            },
            success: function (res) {
                if (res.code !== 0 && obj.elem) {
                    obj.elem.checked = true;
                    form.render('checkbox');
                }
            },
            error: function () {
                if (obj.elem) {
                    obj.elem.checked = true;
                    form.render('checkbox');
                }
            }
        });
        return false;
    };

    //回收
    Class.prototype.recovery = function (obj) {
        var that = this;
        var dataCount = obj.data.length;
        if (dataCount < 1) {
            that.config.error("请选择要回收的数据");
            return false;
        }
        layer.confirm('数据移入回收站相关联数据也将移入回收站，是否继续？', { icon: 3, title: '提示' }, function (index) {
            var postdata = [];
            $.each(obj.data, function (ei, item) {
                postdata.push(item[that.config.primaryKey]);
            });
            layer.close(index);
            com.post({
                url: obj.url,
                data: { "": postdata },
                done: function (res) {
                    that.config.postDone({}, res);
                }
            });
        });
        return false;
    };

    //还原
    Class.prototype.restore = function (obj) {
        var that = this;
        var dataCount = obj.data.length;
        if (dataCount < 1) {
            that.config.error("请选择要还原的数据");
            return false;
        }
        layer.confirm('确定要还原选择的数据吗？', { icon: 3, title: '提示' }, function (index) {
            var postdata = [];
            $.each(obj.data, function (ei, item) {
                postdata.push(item[that.config.primaryKey]);
            });
            layer.close(index);

            com.post({
                url: obj.url,
                data: { "": postdata },
                done: function (res) {
                    that.config.postDone({}, res);
                }
            });
        });
        return false;
    };

    //查看
    Class.prototype.detail = function (obj) {
        var that = this;
        var dataCount = obj.data.length;
        if (dataCount < 1) {
            com.error("请选择要查看的数据");
            return false;
        }
        if (dataCount > 1) {
            com.error("只允许同时查看一条数据");
            return false;
        }

        obj.viewurl = obj.viewurl || obj.url;
        var popupUrl = obj.viewurl + "/" + obj.data[0][that.config.primaryKey];

        if (that.config.formType === 'popup') {

            if (that.config.detailIsLoad) {
                com.post({
                    url: popupUrl,
                    type: 'get',
                    done: function (res) {

                        res.data.isCanNotEdit = true;
                        var popupConfig = that.popupConfig({
                            isEdit: false,
                            viewUrl: obj.viewurl,
                            popupTitle: that.config.channelName + " - 查看详情",
                            defaultVal: res.data,
                            viewdone: function (opt) {
                                // that.config.detailSuccess(opt);
                                that.config.viewSuccess(opt);

                                com.disabledForm(popupConfig);
                            }
                        });

                        popupConfig.html = that.config.detailBodyHtml(popupConfig);

                        that.popup(popupConfig);

                    }
                });

            }
            else {
                var d = obj.data[0];
                d.isCanNotEdit = true;
                var popupConfig = that.popupConfig({
                    isEdit: false,
                    viewUrl: obj.viewurl,
                    popupTitle: that.config.channelName + " - 查看详情",
                    defaultVal: obj.data[0],
                    viewdone: function (opt) {
                        //that.config.detailSuccess(opt);
                        that.config.viewSuccess(opt);
                        com.disabledForm(popupConfig);
                    }
                });

                popupConfig.html = that.config.detailBodyHtml(popupConfig);

                that.popup(popupConfig);
            }
        }
        else {
            window.location.hash = popupUrl;
        }

        return false;
    };

    //打印
    Class.prototype.print = function (obj) {
        var that = this;
        var v = document.createElement("div");
        var f = ["<head>", "<style>", "body{font-size: 12px; color: #666;}", "table{width: 100%; border-collapse: collapse; border-spacing: 0;}", "th,td{line-height: 20px; padding: 9px 15px; border: 1px solid #ccc; text-align: left; font-size: 12px; color: #666;}", "a{color: #666; text-decoration:none;}", "*.layui-hide{display: none}", "</style>", "</head>"].join("");
        $(v).append($("[lay-id=\"" + that.config.elemTable + "\"] .layui-table-box").find(".layui-table-header").html());
        $(v).find("tr").after($("[lay-id=\"" + that.config.elemTable + "\"] .layui-table-body.layui-table-main table").html());
        $(v).find("th.layui-table-patch").remove();
        $(v).find(".layui-table-col-special").remove();
        var h = window.open("打印窗口", "_blank");
        h.document.write(f + $(v).prop("outerHTML"));
        h.document.close();
        h.print();
        h.close();
        return false;
    };

    //表格内缩略图模板
    Class.prototype.tplPreview = function (src) {
        if (src && src.length > 0) {
            return '<img src="' + (setter.fileDomain || '') + src + '" lay-event="preview" data-href="' + (setter.fileDomain || '') + src + '" style="width:22px;cursor: pointer;">';
        }
        else {
            return '';
        }
    };

    //行按钮模板，默认仅显示编辑、删除
    Class.prototype.tplTool = function (d, other) {
        var that = this;
        let html = '';
        $.each(that.config.allButtons, function (index, item) {
            if (that.config.toolButtons.indexOf(item.eventName) >= 0) {
                html += '<button type="button" class="layui-btn layui-btn-xs ' + (item.className || '') + '" data-href="' + item.channelLink + '" data-viewurl="' + (item.viewLink || '') + '" lay-event="' + item.eventName + '">' + item.channelName + '</button>';
            }
        });

        html += other || '';
        return html;
    };

    //状态模板
    Class.prototype.tplStatus = function (d) {
        var that = this;
        var html = '';

        if (d.status === 1) {
            html = '';
            $.each(that.config.allButtons, function (index, item) {
                if (item.eventName === 'disable') {
                    html = '<input type="checkbox" lay-skin="switch" lay-text="启用|禁用" checked lay-filter="' + that.config.elemTableSwitch + '" value="' + d.id + '" data-href="' + item.channelLink + '">';
                    return true;
                }
            });
            return html || '<span class="layui-badge layui-bg-green">' + d.statusDescription + '</span>';
        }
        else if (d.status === 0) {
            html = '';
            $.each(that.config.allButtons, function (index, item) {
                if (item.eventName === 'enable') {
                    html = '<input type="checkbox" lay-skin="switch" lay-text="启用|禁用" lay-filter="' + that.config.elemTableSwitch + '" value="' + d.id + '" data-href="' + item.channelLink + '">';
                    return true;
                }
            });
            return html || '<span class="layui-badge layui-bg-red">' + d.statusDescription + '</span>';
        }
        else {
            return '<span class="layui-badge layui-bg-blue">' + d.statusDescription + '</span>';
        }

    };


    //设置全局 table 实例的 token（这样一来，所有 table 实例均会有效）
    table.set({
        headers: {
            "Authorization": com.getToken()
        }
    });

    //暴露接口
    exports('com', com);
});