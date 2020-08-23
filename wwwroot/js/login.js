function login() {
    var id = $('#userId').val()
    var password = $('#userPassword').val()
    var kind = $('input[name="userKind"]:checked').val()

    $.post('/login/Login', { id: id, password: password, kind: kind }, function (result) {
        var isSucceeded = result.isSucceeded
        if (isSucceeded) {
            var path = result.path
            $(location).attr('href', path)
        }
        else {
            var message = result.message
            $('#errorMessage').html(message)
        }
    })
}

function changeId() {
    var id = $('#userId').val()
}

$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    e.target // newly activated tab
    e.relatedTarget // previous active tab
})

function registerPeople() {
    if ($('#peoplePassword').val() != $('#peopleConfirmPassword').val()) {
        $('#peopleErrorMessage').html('两次密码不一致。')
        return
    }
    var parameters = {
        id: $('#peopleId').val(),
        password: $('#peoplePassword').val(),
        name: $('#peopleName').val(),
        sex: $('input[name="peopleSex"]:checked').val(),
        tel: $('#peopleTel').val(),
        address: $('#peopleAddress').val()
    };
    $.post('/Login/RegisterPeople', parameters, function (result) {
        if (result) {
            $(location).attr('href', '/Home')
        }
        else {
            $('#peopleErrorMessage').html('该身份证号已注册。')
        }
    })
}

function registerDoctor() {
    if ($('#doctorPassword').val() != $('#doctorConfirmPassword').val()) {
        $('#doctorErrorMessage').html('两次密码不一致。')
        return
    }
    var parameters = {
        id: $('#doctorId').val(),
        password: $('#doctorPassword').val(),
        name: $('#doctorName').val(),
        hospital: $('#hospital').val()
    };
    $.post('/Login/RegisterDoctor', parameters, function (result) {
        if (result == 'True') {
            $(location).attr('href', '/Home')
        }
        else if (result == 'False') {
            $('#doctorErrorMessage').html('该身份证号已注册。')
        }
        else {
            $('#doctorErrorMessage').html('指定的医院不存在。')
        }
    })
}

function registerManager() {
    if ($('#managerPassword').val() != $('#managerConfirmPassword').val()) {
        $('#managerErrorMessage').html('两次密码不一致。')
        return
    }
    var parameters = {
        id: $('#managerId').val(),
        password: $('#managerPassword').val(),
        name: $('#managerName').val(),
        sex: $('input[name="managerSex"]:checked').val(),
        tel: $('#managerTel').val(),
        address: $('#managerAddress').val(),
        unit: $('#unit').val()
    };
    $.post('/Login/RegisterManager', parameters, function (result) {
        if (result) {
            $(location).attr('href', '/Home')
        }
        else {
            $('#managerErrorMessage').html('该身份证号已注册。')
        }
    })
}

