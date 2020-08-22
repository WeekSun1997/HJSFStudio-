layui.define(['jquery', 'layer', 'form'], function (exports) {
    var $ = layui.$
        , setter = layui.setter
        , form = layui.form
        , layer = layui.layer
        , router = layui.router()
        , search = router.search;
    $('.logincaptcha').click(function () {
      
        $(".logincaptcha").attr('src', '/v1/sysuser/GetVerify?id=' + new Date().getTime());
        $('input[name="verify"]').val('');
    });


    form.on('submit(login)', function (data) {

        $(data.elem).prop("disabled", true);
        $(data.elem).addClass('layui-disabled');

        $.ajax({
            url: '/v1/SysUser/login'
            , type: 'post'
            , data: data.field
            , success: function (res) {
                if (res.code != 0) {
                    $(data.elem).prop("disabled", false);
                    $(data.elem).removeClass('layui-disabled');
                    layer.msg(res.msg, {
                        offset: '15px'
                        , icon: 2
                        , time: 3000
                    });
                }
                else {
                    //请求成功后，写入 access_token
                    layui.data('layuiAdmin', {
                        key: 'Authorization'
                        , value: res.data
                    });

                    //登入成功的提示与跳转
                    layer.msg('登入成功', {
                        offset: '15px'
                        , icon: 1
                        , time: 1000
                    }, function () {
                        //location.hash = search.redirect ? decodeURIComponent(search.redirect) : '/';
                        window.location.assign(search.redirect ? decodeURIComponent(search.redirect) : '/manage/index.html');
                    });

                }
            }
            , error: function (e) {
                $(data.elem).prop("disabled", false);
                $(data.elem).removeClass('layui-disabled');
            }
        });

        return false;
    });


    //对外暴露的接口
    exports('login', {});
});