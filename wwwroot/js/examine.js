function create_() {
    $.post('/Examine/Create_', { p_id: document.getElementById("patient_id").value, title: document.getElementById("title").value, detail: document.getElementById("detail").value  }, function (result) {
        if (result == 1) {
            alert("�����ɹ�");
            //$('#').remove();
            window.location.href = "../Examine/Index_doctor";
        }
        
        else if(result==0) {
            //alert("�������֤����������");
           // window.location.href = "../Examine/Create";
            $('.popover-dismiss').popover({
                trigger: 'focus'
            })
            alert("�������֤����������");
           window.location.href = "../Examine/Create";
        }
    })
}

function write_() {
    $.post('/Examine/Write_', { r_id: document.getElementById("report_id").value,p_id: document.getElementById("patient_id").value, title: document.getElementById("title").value, detail: document.getElementById("detail").value }, function (result) {
        if (result == 1) {
            alert("�����ɹ�");
            window.location.href = "../Examine/Write_index";
        }

    })
}

