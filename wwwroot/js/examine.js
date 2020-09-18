function create_() {
    $.post('/Examine/Create_', { p_id: document.getElementById("patient_id").value, title: document.getElementById("title").value, detail: document.getElementById("detail").value  }, function (result) {
        if (result == 1) {
            alert("创建成功");
            //$('#').remove();
            window.location.href = "../Examine/Index_doctor";
        }
        
        else if(result==0) {
            //alert("病人身份证号输入有误");
           // window.location.href = "../Examine/Create";
            $('.popover-dismiss').popover({
                trigger: 'focus'
            })
            alert("病人身份证号输入有误");
           window.location.href = "../Examine/Create";
        }
    })
}

function write_() {
    $.post('/Examine/Write_', { r_id: document.getElementById("report_id").value,p_id: document.getElementById("patient_id").value, title: document.getElementById("title").value, detail: document.getElementById("detail").value }, function (result) {
        if (result == 1) {
            alert("创建成功");
            window.location.href = "../Examine/Write_index";
        }

    })
}

