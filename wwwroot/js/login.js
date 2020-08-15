function loginClick() {
    var id = $('#userId').val()
    var password = $('#userPassword').val()
    var kind = $('input[name="userKind"]:checked').val()

    $.post('/login/Login', { id: id, password: password, kind: kind }, function (result) {
        var isSucceeded = result.isSucceeded
        if (isSucceeded) {

        }
        else {
            var message = result.message
            $('#errorMessage').html(message)

        }
    })
}