/*!
 * Cropper v3.0.0
 */

layui.define(['jquery', 'layer', 'cropper', 'setter'], function (exports) {
    var $ = layui.jquery
        , cropper = layui.cropper
        , setter = layui.setter
        , layer = layui.layer;
    var html = ""+//<link rel=\"stylesheet\" href=\"/Content/layuiadmin/lib/extend/cropper/cropper.css\">\n" +
        "<div class=\"layui-fluid showImgEdit\">\n" +
        "    <div class=\"layui-form-item\">\n" +
        "        <div class=\"layui-input-inline layui-btn-container\" style=\"width: auto; color:#fff;\">\n" +
        "            <label for=\"cropper_avatarImgUpload\" class=\"layui-btn\" style=\"color:#fff\">\n" +
        "                <i class=\"layui-icon\">&#xe67c;</i>选择图片\n" +
        "            </label>\n" +
        "            <input class=\"layui-upload-file\" id=\"cropper_avatarImgUpload\" type=\"file\" value=\"选择图片\" name=\"file\">\n" +
        "        </div>\n" +
        "        <div class=\"layui-form-mid layui-word-aux\">支持图片格式：jpg|jpeg|png|bmp|svg</div>\n" +
        "    </div>\n" +
        "    <div class=\"layui-row layui-col-space15\">\n" +
        "        <div class=\"layui-col-xs9\">\n" +
        "            <div class=\"readyimg\" style=\"height:300px;background-color: rgb(247, 247, 247);\">\n" +
        "                <img src=\"\" >\n" +
        "            </div>\n" +
        "        </div>\n" +
        "        <div class=\"layui-col-xs3\">\n" +
        "            <div class=\"img-preview\" style=\"width:100px;height:100px;overflow:hidden\">\n" +
        "            </div>\n" +
        "        </div>\n" +
        "    </div>\n" +
        "    <div class=\"layui-row layui-col-space15\">\n" +
        "        <div class=\"layui-col-xs9\">\n" +
        "            <div class=\"layui-row\">\n" +
        "                <div class=\"layui-col-xs12\">\n" +
        "                    <button type=\"button\" class=\"layui-btn\" cropper-event=\"rotate\" data-option=\"-15\" title=\"左转\"> <span class=\"croppericonfont croppericon-zuoxuanzhuan\"></span></button>\n" +
        "                    <button type=\"button\" class=\"layui-btn\" cropper-event=\"rotate\" data-option=\"15\" title=\"右转\"> <span class=\"croppericonfont croppericon-youxuanzhuan\"></span></button>\n" +
        //"                </div>\n" +
        //"                <div class=\"layui-col-xs5\" style=\"text-align: right;\">\n" +
        "                    <button type=\"button\" class=\"layui-btn\" cropper-event=\"setDragMode\" title=\"移动\"><span class=\"croppericonfont croppericon-yidong\"></span></button>\n" +
        "                    <button type=\"button\" class=\"layui-btn\" cropper-event=\"zoomLarge\" title=\"放大图片\"><span class=\"croppericonfont croppericon-fangda\"></span></button>\n" +
        "                    <button type=\"button\" class=\"layui-btn\" cropper-event=\"zoomSmall\" title=\"缩小图片\"><span class=\"croppericonfont croppericon-yuanjiaojuxing\"></span></button>\n" +
        "                    <button type=\"button\" class=\"layui-btn\" cropper-event=\"reset\" title=\"重置图片\"><i class=\"layui-icon layui-icon-refresh\"></i></button>\n" +
        "                </div>\n" +
        "            </div>\n" +
        "        </div>\n" +
        "        <div class=\"layui-col-xs3\">\n" +
        "            <button class=\"layui-btn layui-btn-fluid\" cropper-event=\"confirmSave\" type=\"button\"> 保存图片</button>\n" +
        "        </div>\n" +
        "    </div>\n" +
        "\n" +
        "</div>";
    var obj = {
        render: function (e) {
            //$('body').append(html);
            var self = this,
                elem = e.elem,
                saveW = e.saveW,
                saveH = e.saveH,
                mark = e.mark,
                area = e.area || ['700px', '500px'],
                url = e.url,
                elemId = 'edit-image-box',
                done = e.done;

            $(elem).on('click', function () {
                layer.open({
                    type: 1
                    //, content: content
                    , id: elemId
                    , title:'图片上传'
                    , area: area
                    , resize: false
                    , success: function (layero, index) {
                        let item = $("#" + this.id);
                        item.html(html);

                        var content = item.find('.showImgEdit')
                            , image = item.find(".showImgEdit .readyimg img")
                            , preview = '.showImgEdit .img-preview'
                            , file = item.find(".showImgEdit input[name='file']")
                            , options = { aspectRatio: mark, preview: preview, viewMode: 1 };


                        image.cropper(options);

                        item.find(".layui-btn").on('click', function () {
                            var othis = $(this);
                            var event = othis.attr("cropper-event");
                            //监听确认保存图像
                            if (event === 'confirmSave') {
                                var src = image.attr('src');
                                if (!src || src.length <= 0) {
                                    layer.msg('请先选择要上传的图片', { icon: 2, time: 2000 });
                                    return;
                                }
                                image.cropper("getCroppedCanvas", {
                                    width: saveW,
                                    height: saveH
                                }).toBlob(function (blob) {
                                    var formData = new FormData();

                                    formData.append('file', blob, 'head.png');
                                    //formData.append('Content-Type', 'image/png');
                                    othis.attr('disabled', true);
                                    othis.text('上传中....');
                                    othis.addClass('layui-disabled');
                                    $.ajax({
                                        method: "post",
                                        url: url, //用于文件上传的服务器端请求地址
                                        data: formData,
                                        headers: { token: layui.data(setter.tableName)[setter.request.tokenName] || ''},
                                        processData: false,
                                        contentType: false,
                                        success: function (result) {
                                            if (result.code == 0) {
                                                layer.msg(result.msg, { icon: 1, time: 2000 });
                                                layer.close(index);
                                                return done(result.data);
                                            } else {
                                                othis.attr('disabled', false);
                                                othis.text('保存图片');
                                                othis.removeClass('layui-disabled');

                                                layer.alert(result.msg, { icon: 2 });
                                            }

                                        },
                                        error: function () {
                                            othis.attr('disabled', false);
                                            othis.text('保存图片');
                                            othis.removeClass('layui-disabled');
                                            layer.msg('图片上传失败，请重试', { icon: 2,time:2000 });
                                        }
                                    });

                                });
                                //监听旋转
                            } else if (event === 'rotate') {
                                var src = image.attr('src');
                                if (!src || src.length <= 0)
                                    return;

                                var option = $(this).attr('data-option');
                                image.cropper('rotate', option);
                                //重设图片
                            } else if (event === 'reset') {
                                var src = image.attr('src');
                                if (!src || src.length <= 0)
                                    return;

                                image.cropper('reset');
                            }
                            else if (event === 'zoomLarge') {
                                var src = image.attr('src');
                                if (!src || src.length <= 0)
                                    return;

                                image.cropper('zoom', 0.1);
                            }
                            else if (event === 'zoomSmall') {
                                var src = image.attr('src');
                                if (!src || src.length <= 0)
                                    return;

                                image.cropper('zoom', -0.1);
                            }
                            else if (event === 'setDragMode') {
                                var src = image.attr('src');
                                if (!src || src.length <= 0)
                                    return;

                                image.cropper('setDragMode', "move");
                            }

                            //文件选择
                            file.change(function () {
                                var r = new FileReader();
                                var f = this.files[0];
                                r.readAsDataURL(f);
                                r.onload = function (e) {
                                    image.cropper('destroy').attr('src', this.result).cropper(options);
                                };
                            });
                        });

                    }
                    , cancel: function (index) {
                        layer.close(index);
                        $('#' + elemId).find(".showImgEdit .readyimg img").cropper('destroy');
                    }
                });
            });
        }

    };
    exports('croppers', obj);
});