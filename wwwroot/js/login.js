function login() {
    var id = $('input[name="id"]')
    var password = $('input[name="password"]')
    var parameters = {
        id: id.val(),
        password: password.val(),
        kind: $('input[name="kind"]:checked').val(),
        rememberMe: $("input[type='radio']").attr('checked')
    }

    $.post('/login/login', parameters, function (result) {
        var isSucceeded = result.isSucceeded
        if (isSucceeded) {
            var path = result.path
            $(location).attr('href', path)
        }
        else {
            if (result.isIdValid) {
                id.removeClass('is-invalid')
                id.addClass('is-valid')
            }
            else {
                id.removeClass('is-valid')
                id.addClass('is-invalid')
            }
            if (result.isPasswordValid) {
                password.removeClass('is-invalid')
                password.addClass('is-valid')
            }
            else {
                password.removeClass('is-valid')
                password.addClass('is-invalid')
            }
        }
    })
    return false
}

function changeId() {
    var id = $('#userId').val()
}

function registerPeople() {
    var parameters = {
        id: $('#id').val(),
        password: $('#password').val(),
        name: $('#name').val(),
        sex: $('input[name="sex"]:checked').val(),
        tel: $('#tel').val(),
        address: $('#address').val()
    };
    $.post('/Login/RegisterPeople', parameters, function (result) {
        if (result) {
            $(location).attr('href', '/Home')
        }
        else {
            $('#id').addClass('is-invalid')
        }
    })
    parameters = {
        id: parameters.id,
        password: parameters.password,
        kind: "people",
    }
    $.post('/login/login', parameters, function (result) { })
    return false
}

function enterId(value) {
    $('#id').removeClass('is-invalid')
}

function enterPassword(value) {
    var valid = $('#validPassword')
    if (valid.val() == "") return
    var password = $('#password')
    if (valid.val() != value) {
        valid.removeClass('is-valid')
        valid.addClass('is-invalid')
    }
    else {
        valid.removeClass('is-invalid')
        valid.addClass('is-valid')
    }
}

function checkPassword(value) {
    var password = $('#password')
    var valid = $('#validPassword')
    if (password.val() != value) {
        valid.removeClass('is-valid')
        valid.addClass('is-invalid')
    }
    else {
        valid.removeClass('is-invalid')
        valid.addClass('is-valid')
    }
}

function registerPatient() {
    if ($('#patientPassword').val() != $('#patientConfirmPassword').val()) {
        $('#patientErrorMessage').html('两次密码不一致。')
        return
    }

    var parameters = {
        id: $('#patientId').val(),
        password: $('#patientPassword').val(),
        name: $('#patientName').val(),
        sex: $('input[name="patientSex"]:checked').val(),
        hospital: $('#patientHospital').val(),
    };
    $.post('/Login/RegisterPatient', parameters, function (result) {
        if (result) {
            $(location).attr('href', '/Home')
        }
        else if (result == 'False') {
            $('#patientErrorMessage').html('该身份证号已注册。')
        }
        else {
            $('#patientErrorMessage').html('指定的医院不存在。')
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

