layui.define(['jquery', 'layer', 'upload', 'croppers', 'setter'], function (exports) {
    var $ = layui.$
        , upload = layui.upload
        , setter = layui.setter
        , croppers = layui.croppers
        , layer = layui.layer;



    var obj = {
        render: function (e) {
            var othis = this
                //上传完成处理事件
                , done = e.done
                //, value = e.value
                //图片表单名称
                , name = e.name
                , cropper = e.cropper || false
                , multiple = false
                , inputValue = e.inputValue || ''
                , icon = e.icon || 'layui-icon-add-1'
                //元素
                , elem = e.elem;

            delete e.elem;
            delete e.name;
            //delete e.value;
            delete e.icon;
            delete e.done;
            delete e.cropper;

            var html = '<div class="uploadimage-item" style="width: 92px; height: 92px; display: table-cell;vertical-align: middle; border:0px solid #ddd; position:relative; text-align:right;">' +
                '<div class="uploadimage-bar" style="display: none;font-size:17px; color:#fff; position:absolute; top:0px; background-color:#fff; height:20px; width:90px; padding-right:2px; background-color:rgba(0, 0, 0, 0.5); text-align:right;">' +
                '        <i class="layui-icon layui-icon-upload uploadimage-up" style="cursor:pointer; padding-top:2px;" title="重新上传"></i>' +
                '        <i class="layui-icon layui-icon-delete uploadimage-del" style="cursor:pointer" title="删除图片"></i>' +
                '    </div>' +
                '<span class="layui-icon ' + icon + ' uploadimage-up" style="line-height:90px; font-size:60px; cursor:pointer;" title="点击上传图片"></span>' +
                '<img class="layui-upload-img" style="cursor:pointer; max-height:92px; max-width:92px;"> ' +
                //'<input type="hidden" value="" name="' + name + '" /> ' +
                '</div > ';

            var htmlElem = $(html)
                , bar = htmlElem.find(".uploadimage-bar")
                , btnUp = htmlElem.find(".uploadimage-up")
                , btnDefaultUp = htmlElem.find("span")
                , btnDel = htmlElem.find(".uploadimage-del")
                , image = htmlElem.find("img")
                //, input = htmlElem.find("input")
                , input = $('input[name="' + name + '"]')
                , value = inputValue || input.val();

            if (value && value.length > 0) {
                input.val(value);
                btnDefaultUp.hide();
                image.attr('src', (fileDomain || '') + value);
                image.show();
            }
            else {
                image.hide();
                btnDefaultUp.show();
            }

            //image.mouseenter(function () {
            //    bar.show();
            //});

            //image.mouseleave(function () {
            //    bar.hide();
            //});
            htmlElem.mouseenter(function () {
                if (value && value.length > 0) {
                    bar.show();
                }
            });

            htmlElem.mouseleave(function () {

                bar.hide();
            });

            bar.mouseenter(function () {
                if (value && value.length > 0) {
                    bar.show();
                }
            });

            //bar.mouseleave(function () {
            //    bar.hide();
            //});

            image.click(function () {
                var src = $(this).attr("src");
                layer.photos({
                    photos: {
                        "title": "查看图片"
                        , "data": [{
                            "src": src
                        }]
                    }
                    , shade: 0.01
                    , closeBtn: 1
                    , anim: 5
                });
            });

            btnDel.click(function () {
                layer.confirm('确定要删除当前图片吗', { icon: 3, title: '提示' }, function (index) {
                    value = '';
                    input.val('');
                    image.attr('src', '');
                    image.hide();
                    btnDefaultUp.show();
                    layer.close(index);
                });
            });

            elem.html('');
            htmlElem.appendTo(elem);

            if (cropper) {

                var opts = $.extend({
                    elem: btnUp
                    , done: function (url) {
                        value = url;
                        input.val(url);
                        btnDefaultUp.hide();
                        image.attr('src', fileDomain + url);
                        image.show();
                        typeof done === 'function' && success(url);
                    }
                }, e);

                croppers.render(opts);

            }
            else {
                var options = $.extend({
                    elem: btnUp
                    , accept: 'images'
                    , exts: 'jpg|png|gif|bmp|jpeg'
                    , auto: true
                    , headers: { token: layui.data(setter.tableName)[setter.request.tokenName] || '' }
                    , done: function (res, index, upload) {
                        if (res.code == 0) {
                            value = res.data;
                            input.val(res.data);
                            btnDefaultUp.hide();
                            image.attr('src', fileDomain + res.data);
                            image.show();

                            typeof done === 'function' && success(result);
                        }
                        else {
                            layer.msg(res.msg, { icon: 2, time: 2000 });
                        }

                    }
                    //, error: function (index, upload) {
                    //    //当上传失败时，你可以生成一个“重新上传”的按钮，点击该按钮时，执行 upload() 方法即可实现重新上传
                    //}
                }, e);
                upload.render(options);
            }


        }
    };


    exports('uploadImage', obj);
});