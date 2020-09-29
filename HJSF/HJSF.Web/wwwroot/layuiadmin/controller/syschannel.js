layui.define(function (exports) {
    layui.use(['jquery', 'form', 'com', 'iconFonts', 'yyTree'], function () {
        var $ = layui.$
            , form = layui.form
            , com = layui.com
            , iconFonts = layui.iconFonts
            , tree = layui.yyTree

            , channelList =
                [{
                    checked: false,
                    children: [],
                    disabled: false,
                    spread: true,
                    id: 0,
                    label: "一级模块"
                }];
        let syschannel = null;

        //渲染左侧栏目
        function renderTree() {
            tree.render({
                elem: '#channelTree-syschannel'
                , data: channelList
                , showLine: true  //是否开启连接线
                , edit: ['add', 'update', 'del'] //操作节点的图标
                , accordion: true

                , click: function (obj) {
                    var id = obj.data.id;
                    //var labelName = obj.data.label === '一级模块'?'': obj.data.label;
                    $('#YY-childrentitle-syschannel').text(obj.data.label)
                    syschannel.reloadTable({ where: { parentId: id } })
                }
                , operate: function (obj) {
                    var type = obj.type; //得到操作类型：add、edit、del
                    var data = obj.data; //得到当前节点的数据
                    var elem = obj.elem; //得到当前节点元素
                    //Ajax 操作
                    var id = data.id; //得到节点索引
                    if (type === 'add') { //增加节点
                        syschannel.config.addEntity =
                        {
                            channelType: 0,
                            status: 1,
                            sort: 0,
                            parentId: data.id
                        };
                        syschannel.add({ url: 'syschannel/add', viewurl: 'syschannel/edit' });
                        return false;
                    } else if (type === 'update') { //修改节点
                        if (data.id === 0) return;
                        syschannel.edit({ data: [data], url: 'syschannel/edit' });
                        return false;
                    } else if (type === 'del') { //删除节点
                        if (data.id === 0) return;
                        syschannel.remove({ data: [data], url: 'syschannel/remove' });
                        return false;
                    };
                }
            });
        };

        //循环加载上级栏目
        function loopParent(el, arr, l, dv, skip) {
            let ls = '';
            for (var i = 0; i < l; i++) {
                ls += '　　';
            }

            $.each(arr, function (index, item) {
                if (!(skip > 0 && item.id === skip)) {
                    el.append('<option value="' + item.id + '" ' + (item.id == dv ? 'selected' : '') + '>' + ls + item.label + '</option>');
                    loopParent(el, item.children, l + 1, dv, skip);
                }
            });
        };

        //加载左侧栏目
        com.post({
            url: 'syschannel/MenuTreeList',
            error: function (res) {
                renderTree();
            },
            done: function (res) {
                channelList[0].children = res.data;
                renderTree();
            }
        });

        syschannel = com.render({
            channelEvent: 'syschannel',
            channelName: '系统模块',
            editIsLoad: true,
            detailIsLoad: false,
            tableInfo: {
                cols: [[
                    { type: 'checkbox', fixed: 'left', width: 60 },
                    { type: 'numbers', title: '序号', width: 80, align: 'center' },
                    { field: 'id', title: '编号', width: 80, align: 'center', hide: true, sort: true },
                    { field: 'channelName', title: '栏目名称' },
                    { field: 'parentChannelName', title: '所属模块' },
                    {
                        field: 'channelType', title: '类型', align: 'center', width: 80, templet: function (d) {
                            return '<span class="layui-badge ' + (d.channelType === 0 ? 'layui-bg-green' : 'layui-bg-cyan') + '">' + (d.channelType==0?"菜单":"按钮") + '</span>';
                        }
                    },
                    { field: 'sort', title: '排序', align: 'center', width: 60 },
                    { field: 'status', title: '状态', align: 'center', width: 90, templet: function (d) { return syschannel.tplStatus(d); } },
                    { title: '操作', align: 'center', width: 120, templet: function (d) { return syschannel.tplTool(d); } }
                ]],
                height: 'full-240'
            },
            viewSuccess: function (popup) {
                let formElem = $("#" + popup.formId);
                //图标选择器
                iconFonts.render({
                    // 选择器，推荐使用input
                    elem: '#iconName-syschannel', //选择器ID
                    // 数据类型：fontClass/layui_icon，
                    type: 'layui_icon',
                    // 是否开启搜索：true/false
                    search: true,
                    // 是否开启分页
                    page: false,
                    // 每页显示数量，默认12
                    limit: 12,
                    // 点击回调
                    click: function (data) {
                        //console.log(data);
                    }
                });

                let el = $('#parentId-syschannel');
                el.empty();

                $.each(channelList, function (index, item) {
                    let skip = popup.defaultVal.id || 0;
                    if (!(skip > 0 && item.id === skip)) {
                        el.append('<option value="' + item.id + '" ' + (item.id === popup.defaultVal.parentId ? 'selected' : '') + '>' + item.label + '</option>');
                        loopParent(el, item.children, 1, popup.defaultVal.parentId, skip);
                    }
                });

                form.on('radio(channelType-syschannel)', function (data) {
                    if (data.value == "0") {
                        $('#channel-buttons-syschannel').show();
                    }
                    else {
                        $('#channel-buttons-syschannel').hide();
                    }
                });

                form.on('select(filter-parentId-syschannel)', function (data) {
                    com.post({
                        url: 'syschannel/getsort/' + data.value,
                        done: function (res) {
                            formElem.find('input[name="sort"]').val(res.data);
                        }
                    })
                });


                if (!(popup.defaultVal.id > 0)) {
                    let pid = (popup.defaultVal && popup.defaultVal.parentId) || 0;
                    com.post({
                        url: 'syschannel/getsort/' + pid,
                        done: function (res) {
                            formElem.find('input[name="sort"]').val(res.data);
                        }
                    })
                }

            },
            postBegin: function (popup, d) {
                var newButtons = [];
                var buttonSuccess = true;
                if (d.channelType == "0") {
                    $("#" + popup.formId + " input:checkbox[name='Buttons']:checked").each(function (i) {
                        var v = $(this).val();
                        var btnInfo = {};

                        if (!d.channelLink || d.channelLink == "") {
                            com.error('模块链接不能为空');
                            buttonSuccess = false;
                            return false;
                        }

                        var basePath = d.channelLink;
                        var last = basePath.substr(basePath.length - 1, 1)
                        if (last == "/") {
                            basePath = last;
                        }

                        var pathArray = basePath.split('/');
                        var newPathArray = [];
                        for (var i = 0; i < pathArray.length - 1; i++) {
                            newPathArray.push(pathArray[i]);
                        }
                        var basePath = newPathArray.join('/') + '/';

                        switch (v) {
                            case 'add':
                                btnInfo = {
                                    channelName: "新建",
                                    //className: "yy-bg-primary",
                                    channelLink: basePath + "add",
                                    viewLink: basePath + "edit",
                                    eventName: "add",
                                    iconName: "layui-icon-add-1",
                                    isShow: true
                                };
                                break;
                            case 'edit':
                                btnInfo = {
                                    channelName: "编辑",
                                    //className: "yy-bg-success",
                                    channelLink: basePath + "edit",
                                    viewLink: basePath + "edit",
                                    eventName: "edit",
                                    iconName: "layui-icon-edit",
                                    isShow: true
                                };
                                break;
                            case 'enable':
                                btnInfo = {
                                    channelName: "启用",
                                    //className: "yy-bg-info",
                                    channelLink: basePath + "enable",
                                    eventName: "enable",
                                    iconName: "layui-icon-ok",
                                    isShow: true
                                };
                                break;
                            case 'disable':
                                btnInfo = {
                                    channelName: "禁用",
                                    //className: "yy-bg-warning",
                                    channelLink: basePath + "disable",
                                    eventName: "disable",
                                    iconName: "layui-icon-close",
                                    isShow: true
                                };

                                break;
                            case 'detail':
                                btnInfo = {
                                    channelName: "详情",
                                    //className: "layui-btn-danger",
                                    channelLink: basePath + "detail",
                                    eventName: "detail",
                                    iconName: "layui-icon-form",
                                    isShow: true
                                };
                            case 'remove':
                                btnInfo = {
                                    channelName: "删除",
                                    className: "layui-btn-danger",
                                    channelLink: basePath + "remove",
                                    eventName: "remove",
                                    iconName: "layui-icon-delete",
                                    isShow: true
                                };
                                break;
                        }

                        newButtons.push(btnInfo);
                    });
                }

                d.ButtonList = newButtons;

                if (buttonSuccess) {
                    console.log(d);
                    //return false;
                    return d;
                }

                return false;
            },
            postDone: function (popup, result) {
                layer.close(popup.index); //执行关闭
                com.success(result.msg, function () {
                    if (result.data === 0) {
                        layui.index.render();
                    }
                    else {
                        syschannel.reloadTable();
                    }
                });
            }
        });


    });
    exports('syschannel', {})
});