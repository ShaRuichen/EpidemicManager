function cha_people()
{
    var parameters = {
        name: $('input[name="name"]').val(),
        address: $('input[name="address"]').val(),
        tel: $('input[name="tel"]').val(),
        sex: $("input[name='sex']:checked").val(),
        oldpassword: $('input[name="oldpassword"]').val(),
        password: $('#people_pwd').val(),
    }
    $.post('/Settings/Changepeople', parameters, function (result) {
        if (result) {
            window.location.href = "/Home/Index"
        }
        else {
            $('#people_error').html('密码错误！重新检查密码')
        }
    })
    return false
}

function Delete_people()
{
    window.location.href = "/Settings/Deletepeople"
}

function delpeople()
{
    var parameters = {
        password: $('#people_pwd').val(),
    }
    $.post('/Settings/Deletepeople', parameters, function (result)
    {
        if (result) {
            window.location.href ="/shared/logout"
        }
        else {
            $('#people_error').html('密码错误！重新检查密码')
        }
    })
    return false
}

function cha_manager()
{
    var parameters = {
        name: $('input[name="name"]').val(),
        sex: $("input[name='sex']:checked").val(),
        work_unit: $('input[name="work_unit"]').val(),
        tel: $('input[name="tel"]').val(),
        oldpassword: $('input[name="oldpassword"]').val(),
        password: $('#people_pwd').val(),
    }
    $.post('/Settings/Manager', parameters, function (result) {
        if (result) {
            window.location.href = "/Home/Index"
        }
        else {
            $('#people_error').html('密码错误！重新检查密码')
        }
    })
    return false
}

function cha_doctor() {
    var parameters = {
        name: $('input[name="name"]').val(),
        hos_name: $('input[name="hos_name"]').val(),
        oldpassword: $('input[name="oldpassword"]').val(),
        password: $('#people_pwd').val(),
    }
    $.post('/Settings/Doctor', parameters, function (result) {
        if (result) {
            window.location.href = "/Home/Index"
        }
        else {
            $('#people_error').html('密码错误！重新检查密码')
        }
    })
    return false
}

function del_manager()
{
    var parameters = {
        password: $('#people_pwd').val(),
    }
    $.post('/Settings/Deletemanager', parameters, function (result) {
        if (result) {
            window.location.href = "/shared/logout"
        }
        else {
            $('#people_error').html('密码错误！重新检查密码')
        }
    })
    return false
}

function del_doctor()
{
    var parameters = {
        password: $('#people_pwd').val(),
    }
    $.post('/Settings/Deletedoctor', parameters, function (result) {
        if (result) {
            window.location.href = "/shared/logout"
        }
        else {
            $('#people_error').html('密码错误！重新检查密码')
        }
    })
    return false
}

function Delete_doctor()
{
        window.location.href = "/Settings/Deletedoctor";
}
function Delete_manager()
{
        window.location.href = "/Settings/Deletemanager";
}
function Delete_patient()
{
        window.location.href = "/Settings/Deletepatient";
}