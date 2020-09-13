﻿function login() {
    var parameters = {
        id: $('input[name="id"]').val(),
        password: $('input[name="password"]').val(),
        kind: $('input[name="kind"]:checked').val()
    }

    $.post('/login/login', parameters, function (result) {
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
    return false
}

function changeId() {
    var id = $('#userId').val()
}

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
    return false
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

