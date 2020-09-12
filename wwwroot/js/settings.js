function Delete_people()
{
    var message = confirm("确认删除个人信息吗？");
    if (message) {
        window.location.href = "/Settings/Deletepeople";
    }
    else {
        window.location.href = "/Settings/Changepeople";
    }
}

function Delete_doctor()
{
    var message = confirm("确认删除个人信息吗？");
    if (message) {
        window.location.href = "/Settings/Deletedoctor";
    }
    else {
        window.location.href = "/Settings/Doctor";
    }
}
function Delete_manager()
{
    var message = confirm("确认删除个人信息吗？");
    if (message) {
        window.location.href = "/Settings/Deletemanager";
    }
    else {
        window.location.href = "/Settings/Manager";
    }
}
function Delete_patient()
{
    var message = confirm("确认删除个人信息吗？");
    if (message) {
        window.location.href = "/Settings/Deletepatient";
    }
    else {
        window.location.href = "/Settings/Patient";
    }
}