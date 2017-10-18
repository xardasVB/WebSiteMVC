﻿(function () {
    $(function () {
        var AccountManager = {};
        AccountManager.AjaxLogin = function () {
            var form = $("form_login");
            $.ajax({
                url: "/Account/LoginPopup",
                type: "POST",
                data: form.serialize(),
                //dataType: "json",
                //contentType: "application/json; charset=utf8",
                success: function (data) {
                    alert(data.result);
                }
            });
        }
        var aMgr = AccountManager;
        $("#btnLoginPopup").on("click", function () {
            aMgr.AjaxLogin();
        });
    });
})();