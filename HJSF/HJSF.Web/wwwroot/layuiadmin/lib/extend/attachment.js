/**

 @Name：附件上传
 @Author：上海友赢信息科技有限公司

 */
layui.define(['table', 'util', 'form', 'element', 'upload', 'layer'], function (exports) {
    "use strict";
    var $ = layui.$
        , util = layui.util
        , form = layui.form
        , setter = layui.setter
        , element = layui.element
        , upload = layui.upload
        , layer = layui.layer
        , table = layui.table;


    var attachment = {
        elem: ''
        , elemTable: 'YY-table-attachment'
        , elemDetailTable: 'YY-table-attachment-detail'
        , elemButton: 'YY-button-attachment'
        , elemProgressBox: 'YY-box-progress-attachment'
        , elemProgress: 'YY-progress-attachment'
        , imageExt: ['.jpg', '.png', '.gif', '.bmp', '.jpeg']
        , attachmentList: []
        , render: function (opt) {
            /**
             * opt.elem           doc元素名称
             * opt.attachmentList 附件列表
             * opt.title          标题
             * opt.type           detail 浏览  edit 修改
             * */
            var that = this;
            //that.elem = '#' + opt.elem;
            that.elem = opt.elem;
            that.attachmentList = opt.attachmentList;

            let attachHtml = '';
            attachHtml += '<div class="layui-row" style="margin-bottom:20px; ">';
            attachHtml += '<fieldset class="layui-elem-field layui-field-title">';
            attachHtml += '    <legend>附件信息</legend>';
            attachHtml += '</fieldset>';

            if (opt.type === 'edit') {
                attachHtml += '    <div class="layui-form-item">';
                attachHtml += '        <button type="button" class="layui-btn" id="' + that.elemButton + '">选择文件</button>';
                attachHtml += '    </div>';
            }
            attachHtml += '    <div class="layui-form-item">';
            attachHtml += '        <table id="' + that.elemTable + '" lay-filter="' + that.elemTable + '"></table>';
            attachHtml += '    </div>';

            attachHtml += '    <div class="layui-form-item" id="' + that.elemProgressBox + '" style="display:none">';
            attachHtml += '        <div class="layui-progress" lay-showPercent="yes" lay-filter="' + that.elemProgress + '">';
            attachHtml += '            <div class="layui-progress-bar layui-bg-red" lay-percent="0%"></div>';
            attachHtml += '        </div>';
            attachHtml += '    </div>';

            attachHtml += '</div>';

            $(that.elem).html(attachHtml);

            if (opt.type === 'edit') {
                that.renderEdit();

                upload.render({
                    elem: '#' + that.elemButton
                    , url: '/v1/upload/UpLoadAttachment'
                    , auto: true
                    , accept: 'file'
                    , exts: 'jpg|png|gif|bmp|jpeg|doc|docx|xls|xlsx|ppt|pptx|pdf'
                    , headers: { "Authorization": layui.data(setter.tableName)[setter.request.tokenName] || '' }
                    , multiple: true
                    //, data: {
                    //    folder: opt.folder,
                    //    no: function () {
                    //        return $('.attachmentNo').text();
                    //    }
                    //}
                    , allDone: function (obj) { //当文件全部被提交后，才触发
                        //console.log(obj.total); //得到总文件数
                        //console.log(obj.successful); //请求成功的文件数
                        //console.log(obj.aborted); //请求失败的文件数

                        $('#' + that.elemProgressBox).hide();
                        element.progress(that.elemProgress, '0%');

                        //全部上传完毕更新一次列表
                        that.renderEdit();
                    }
                    , before: function (obj) { //obj参数包含的信息，跟 choose回调完全一致，可参见上文。
                        //layer.load(); //上传loading
                        $('#' + that.elemProgressBox).show();
                    }
                    , done: function (res, index, upload) { //每个文件提交一次触发一次。详见“请求成功的回调”
                        if (res.code === 0) {
                            that.attachmentList.push(res.data);
                        }
                        else {
                            layer.msg('文件' + index + '上传失败：' + res.msg, { icon: 2, time: 3000 });
                        }
                    }
                    , progress: function (n, elem) {
                        var percent = n + '%';
                        element.progress(that.elemProgress, percent);
                    }
                });

            }
            else {
                that.renderDetail();
            }

            table.on('tool(' + that.elemTable + ')', function (obj) {
                var layEvent = obj.event;
                //console.log(obj);
                var tr = obj.tr;
                if (layEvent === 'del') { //删除
                    that.attachmentList = table.cache[that.elemTable];
                    that.attachmentList.splice(obj.tr.data('index'), 1);

                    table.reload(that.elemTable, {
                        data: that.attachmentList
                    });

                }
                else if (layEvent === 'preview') { //图片预览

                    var url = $(this).data('href');
                    if (url && url.length > 0) {
                        layer.photos({
                            photos: {
                                "title": "查看图片" //相册标题
                                , "data": [{
                                    "src": url //原图地址
                                }]
                            }
                            , shade: 0.5
                            , closeBtn: 1
                            , anim: 5
                        });
                    }



                }
            });

            //监听单元格编辑
            table.on('edit(' + that.elemTable + ')', function (obj) {

                //编辑完成后获取一次值
                that.attachmentList = table.cache[that.elemTable];

            });


            $(document).on("mousewheel DOMMouseScroll", ".layui-layer-phimg img", function (e) {
                var delta = (e.originalEvent.wheelDelta && (e.originalEvent.wheelDelta > 0 ? 1 : -1)) || // chrome & ie
                    (e.originalEvent.detail && (e.originalEvent.detail > 0 ? -1 : 1)); // firefox
                var imagep = $(".layui-layer-phimg").parent().parent();
                var image = $(".layui-layer-phimg").parent();
                var h = image.height();
                var w = image.width();
                if (delta > 0) {
                    //if (h < (window.innerHeight)) {
                    h = h * 1.05;
                    w = w * 1.05;
                    //}
                } else if (delta < 0) {
                    if (h > 100) {
                        h = h * 0.95;
                        w = w * 0.95;
                    }
                }
                imagep.css("top", (window.innerHeight - h) / 2);
                imagep.css("left", (window.innerWidth - w) / 2);
                image.height(h);
                image.width(w);
                imagep.height(h);
                imagep.width(w);
            });

        }
        , renderEdit: function () {
            var that = this;

            table.render({
                elem: '#' + that.elemTable
                , page: {
                    limit: 10,
                }
                , size: 'sm'
                , data: that.attachmentList || []
                , cols: [[
                    {
                        field: 'sourceName', title: '素材名称', minWidth: 100, templet: function (d) {
                            if (that.imageExt.indexOf(d.ext.toLowerCase()) > -1) {
                                return '<a href="javascript:void(0)" data-href="' + setter.fileDomain + d.savePath + '" lay-event="preview">' + d.sourceName + '</a>';
                            }
                            return '<a href="' + setter.fileDomain + d.savePath + '" target="_blank">' + d.sourceName + '</a>';
                        }
                    }
                    , { field: 'description', title: '描述 (单击可编辑)', edit: 'text', minWidth: 100 }
                    , { field: 'createDate', title: '添加时间', width: 170 }
                    , { field: 'createUserName', title: '创建者', width: 100 }
                    , {
                        title: '', align: 'center', width: 40, templet: function () {
                            return '<i class="layui-icon layui-icon-delete" style="cursor: pointer;" lay-event="del"></i>';
                        }
                    }
                ]]
                , text: { none: '请上传附件，支持多选' }
            });
        }
        , renderDetail: function () {
            var that = this;

            table.render({
                elem: '#' + that.elemTable
                , page: {
                    limit: 10,
                }
                , size: 'sm'
                , data: that.attachmentList
                , cols: [[
                    {
                        field: 'sourceName', title: '文件名(JPG|PDF|DOC|DOCX)', minWidth: 100, templet: function (d) {
                            if (that.imageExt.indexOf(d.ext.toLowerCase()) > -1) {
                                return '<a href="javascript:void(0)" data-href="' + setter.fileDomain + d.savePath + '" lay-event="preview">' + d.sourceName + '</a>';
                            }
                            return '<a href="' + setter.fileDomain + d.savePath + '" target="_blank">' + d.sourceName + '</a>';
                        }
                    }
                    , { field: 'description', title: '描述', minWidth: 100 }
                    , { field: 'createDate', title: '添加时间', width: 170 }
                    , { field: 'createUserName', title: '创建者', width: 100 }
                ]]
                , text: { none: '未上传附件' }
            });

        }
    };


    exports('attachment', attachment);
});