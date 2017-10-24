(function () {

    $(function () {
        var AccountManager = {};
        AccountManager.AjaxLogin = function () {
            var form = $("#form_login");
            $.ajax({
                url: "/Account/LoginPopup",
                type: "POST",
                data: form.serialize(),
                //dataType: "json",
                //contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.result == 1)
                        location.reload();
                    else
                        $('.background').css('display', 'none');
                }
            });
        }

        AccountManager.AjaxRegister = function () {
            var form = $("#form_register");
            $.ajax({
                url: "/Account/RegisterPopup",
                type: "POST",
                data: form.serialize(),
                //dataType: "json",
                //contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.result == 1)
                        location.reload();
                    else
                        $('.background').css('display', 'none');
                }
            });
        }

        var aMgr = AccountManager;

        $("#btnLoginPopup").on("click", function () {
            $('.background').css('display', 'block');
            var res = aMgr.AjaxLogin();
        });

        $("#btnRegisterPopup").on("click", function () {
            $('.background').css('display', 'block');
            var res = aMgr.AjaxRegister();
        });
    });
})();