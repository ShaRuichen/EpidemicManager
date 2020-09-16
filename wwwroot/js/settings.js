function Delete_people()
{
    var message = confirm("确认删除个人信息吗？");
    if (message) {
        window.location.href = "/Settings/Deletepeople"
    }
}

function Delete_doctor()
{
    var message = confirm("确认删除个人信息吗？");
    if (message) {
        window.location.href = "/Settings/Deletedoctor";
    }
    else {
        
    }
}
function Delete_manager()
{
    var message = confirm("确认删除个人信息吗？");
    if (message) {
        window.location.href = "/Settings/Deletemanager";
    }
    else {
        
    }
}
function Delete_patient()
{
    var message = confirm("确认删除个人信息吗？");
    if (message) {
        window.location.href = "/Settings/Deletepatient";
    }
    else {
        
    }
}