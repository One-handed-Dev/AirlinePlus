$(document).ready(function () {
    $("#roomSelect").change(function () {
        var targetUrl = '/api/ReserveHotel/GetAvailableRoomCount?roomId=' + $("#roomSelect  option:selected").val();
        $.ajax({
            url: targetUrl,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            contentType: false,
            processData: false,
            type: 'GET',
            error: function (xhr, status) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
                alert(status.code);
            },
            success: function (resp) {
                if (resp != null) {
                    var str = "";
                    var json = JSON.parse(resp);
                    str += '<option selected="selected" value="0">انتخاب کنید...</option>';
                    for (var i = 1; i <= json; i++) {
                        var subJson = i;
                        str += '<option value="' + subJson + '">' + subJson + '</option>'
                    }
                    $("#roomCountSelect").html(str);
                }
            }
        });
    });
})