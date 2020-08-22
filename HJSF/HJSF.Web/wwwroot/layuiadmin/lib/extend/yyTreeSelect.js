layui.define(['table', 'jquery', 'form', 'yyTree', 'com'], function (exports) {
    "use strict";

    var MOD_NAME = 'yyTreeSelect',
        $ = layui.jquery,
        table = layui.table,
        tree = layui.yyTree,
        com = layui.com,
        form = layui.form;
    var yyTreeSelect = function () {
        this.v = '1.1.0';
    };

    /**
    * 初始化表格选择器
    */
    yyTreeSelect.prototype.render = function (opt) {
        var elem = $(opt.elem);
        opt.treeUrl = opt.treeUrl || ''; //树形数据源链接
        opt.treeText = opt.treeText || '根节点'; //树形根节点名称
        opt.treeKey = opt.treeKey || 'categoryId'; //节点表格查询参数名称
        opt.treeList = opt.treeList || [];
        opt.showCheckbox = opt.showCheckbox || false;
        opt.treeId = opt.treeId || "yyTree_" + new Date().getTime();
        elem.off('click').on('click', function (e) {
            e.stopPropagation();

            if ($('div.yyTreeSelect').length >= 1) {
                return false;
            }

            var t = elem.offset().top + elem.outerHeight() + "px";
            var l = elem.offset().left + "px";
            var tableName = "yyTreeSelect_table_" + new Date().getTime();
            var treeName = "yyTreeSelect_tree_" + new Date().getTime();

            var tableBox = '<div class="yyTreeSelect layui-anim layui-anim-upbit" style="left:' + l + ';top:' + t + ';border: 1px solid #d2d2d2;background-color: #fff;box-shadow: 0 2px 4px rgba(0,0,0,.12);padding:0px;position: absolute;z-index:66666666;margin: 5px 0;border-radius: 2px;min-width:230px;">';
            tableBox += '    <div style="height: 250px; overflow: auto;" id="' + treeName + '"></div>';
            if (opt.showCheckbox) {
                tableBox += '<div class="tableSelectBar" style="height: 36px;padding-right: 10px;padding-top: 5px;text-align: right;border-top: 1px solid #ddd;">';
                tableBox += '   <button style="float:right;" class="layui-btn layui-btn-sm yyTreeSelect_btn_select">选择<span></span></button>';
                tableBox += '</div>';
            }
            tableBox += '</div>';
            tableBox = $(tableBox);
            $('body').append(tableBox);

            //数据缓存
            var checkedData = [];


            //树形
            function renderTree() {

                tree.render({
                    elem: '#' + treeName
                    , id: opt.treeId
                    , data: opt.treeList
                    , showCheckbox: opt.showCheckbox
                    , onlyIconControl: true
                    , showLine: true  //是否开启连接线
                    , accordion: true
                    , click: function (obj) {
                        if (opt.showCheckbox) {
                            return false;
                        }
                        else {
                            opt.done(obj);
                            yyTreeSelect.hide();
                            return false;
                        }
                    }
                });

                $('.yyTreeSelect_btn_select').click(function () {
                    //获得选中的节点
                    var checkData = tree.getChecked(opt.treeId);

                    var data = [];

                    function loopData(arr) {
                        let d = [];
                        if (arr && arr.length > 0) {
                            layui.each(arr, function (index, item) {
                                d.push({ id: item.id, label: item.label });
                                d = d.concat(loopData(item.children));
                            });
                        }
                        return d;
                    }

                    layui.each(checkData, function (index, item) {
                        data.push({ id: item.id, label: item.label });
                        data = data.concat(loopData(item.children));
                    });
                    opt.done(data);

                    yyTreeSelect.hide();
                    return false;
                });
            };

            //加载树形数据
            if (opt.treeList.length > 0) {
                renderTree();
            }
            else {
                com.post({
                    url: opt.treeUrl,
                    error: function (res) {
                        renderTree();
                    },
                    done: function (res) {
                        opt.treeList = res.data;
                        renderTree();
                    }
                });
            }



            //FIX位置
            var overHeight = (elem.offset().top + elem.outerHeight() + tableBox.outerHeight() - $(window).scrollTop()) > $(window).height();
            var overWidth = (elem.offset().left + tableBox.outerWidth()) > $(window).width();
            overHeight && tableBox.css({ 'top': 'auto', 'bottom': '0px' });
            overWidth && tableBox.css({ 'left': 'auto', 'right': '5px' })

            //点击其他区域关闭
            $(document).mouseup(function (e) {
                //var userSet_con = $('' + opt.elem + ',.yyTreeSelect');
                var userSet_con = $('.yyTreeSelect');
                if (!userSet_con.is(e.target) && userSet_con.has(e.target).length === 0) {
                    tableBox.remove();
                    delete table.cache[tableName];
                    checkedData = [];
                }
            });
        })
    }


    /**
    * 隐藏选择器
    */
    yyTreeSelect.prototype.hide = function (opt) {
        $('.yyTreeSelect').remove();
    }

    //自动完成渲染
    var yyTreeSelect = new yyTreeSelect();

    //FIX 滚动时错位
    if (window.top == window.self) {
        $(window).scroll(function () {
            yyTreeSelect.hide();
        });
    }

    exports(MOD_NAME, yyTreeSelect);
})