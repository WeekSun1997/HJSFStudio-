/***
 * 无限极联动选择组件封装模块
 */
layui.define(['jquery', 'form'], function (exports) {
    var $ = layui.$, form = layui.form;
    // 监听选中事件
    form.on('select', function (data) {
        var id = data.value;
        var obj = data.elem.name;
        var _this = $("select[name='" + obj + "']");
        var child = _this.attr("child");
        var childUrl = $("select[name='" + obj + "']").attr("childDataPath");
        if ($("select[name='" + child + "']").length > 0) {
            CascadeSelect.GetListByParent(child, id, childUrl, true);
        }
    });

    var CascadeSelect = {
        init: function (initObj, isReset) {
            var url = $("select[name='" + initObj + "']").attr("url");
            // ajax请求
            $.ajax({
                type: "POST",
                url: url,
                dataType: "json",
                headers: {
                    Authorization: layui.data(layui.setter.tableName)[layui.setter.request.tokenName]|| ''
                },
                //data: { id: id },
                //async: false,
                success: function (data) {
                    $.each(data, function (i, d) {
                        $("select[name='" + initObj + "']").append(
                            "<option value='" + d.value + "'>" + d.text + "</option>");
                    });
                    form.render();
                    var selectValue = $("select[name='" + initObj + "']").attr("selectValue");

                    var child = $("select[name='" + initObj + "']").attr("child");
                    $("select[name='" + initObj + "']").val(selectValue);
                    var childUrl = $("select[name='" + initObj + "']").attr("childDataPath");
                    if (childUrl) {
                        console.info(child)
                        CascadeSelect.GetListByParent(child, selectValue, childUrl, isReset);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    layer.msg('系统异常导致操作失败, 请联系管理员。', { icon: 5, shift: 6 });
                },
                statusCode: {
                    404: function () {
                        layer.msg('未找到指定请求，请检查访问路径！', { icon: 5, shift: 6 });
                    },
                    500: function () {
                        layer.msg('系统错误，请联系管理员。', { icon: 5, shift: 6 });
                    }
                },
                complete: function (XMLHttpRequest, textStatus) {
                }
            });

        },
        GetListByParent: function (obj, id, childUrl, isReset) {
       
            // 先清空下拉框
            var _this = $("select[name='" + obj + "']");
            _this.empty();
            _this.append("<option value=''>" + _this.attr("promtion") + "</option>");
            var child = _this.attr("child");
            var _thischild = $("select[name='" + child + "']");
            var selectValue = "";
            if (id !== "") {
                $.ajax({
                    type: "POST",
                    url: childUrl+'/'+id,
                    dataType: "json",
                    //data: { id: id },
                    //async: false,
                    headers: {
                        Authorization: layui.data(layui.setter.tableName)[layui.setter.request.tokenName] || ''
                    },
                    success: function (data) {
                        
                        if (data.length > 0) {
                            $.each(data, function (i, d) {
                                $("select[name='" + obj + "']").append("<option value='" + d.value + "'>" + d.text + "</option>");
                            });
                            if (!isReset) {
                                selectValue = _this.attr("selectValue");
                                $("select[name='" + obj + "']").val(selectValue);
                            }
                            if (_thischild.length > 0) {
                                childUrl = _this.attr("childDataPath");
                                CascadeSelect.GetListByParent(child, selectValue, childUrl, isReset);
                            }
                        }
                        form.render();
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        layer.msg('系统异常导致操作失败, 请联系管理员。', { icon: 5, shift: 6 });
                    },
                    statusCode: {
                        404: function () {
                            layer.msg('未找到指定请求，请检查访问路径！', { icon: 5, shift: 6 });
                        },
                        500: function () {
                            layer.msg('系统错误，请联系管理员。', { icon: 5, shift: 6 });
                        }
                    },
                    complete: function (XMLHttpRequest, textStatus) {
                    }
                });

            } else {
                while (_thischild.length > 0) {
                    _thischild.empty();
                    _thischild.append("<option value=''>" + _thischild.attr("promtion") + "</option>");
                    _thischild = $("select[name='" + _thischild.attr("child") + "']");
                }
                form.render();
            }
        }
    };

    exports('cascadeSelect', CascadeSelect);
});