function click2() {
    $.post('/Treatment/Click2', {  doc_id: document.getElementById("doctor_id").value, pat_id: document.getElementById("patient_id").value, date: document.getElementById("date").value,time: document.getElementById("time").value, med: document.getElementById("medicine").value, detail: document.getElementById("detail").value }, function (result) {
        alert("插入完成");
    })
}

function modifi() {
    $.post('/Treatment/Find', { plan_id2: document.getElementById("plan_id2").value }, function (result) {
        if (result == 1) { alert("这个plan不存在") }
        if (result == 2) {
            var board = document.getElementById("staff");
            var store = new Array(2);
            for (var i = 0; i < 2; i++) {
                var e = document.createElement("input");
                e.type = "text"
                e.setAttribute("id", "input_" + i);//设置name属性
                store[i] = e;
                board.appendChild(e);//将e追加到board最后一个节点后面
            }
            var mmm = document.createElement("input");
            mmm.type = "button";
            mmm.setAttribute("value", "确定");
            mmm.setAttribute("onclick", "insert()");
            board.appendChild(mmm);
            $.post('/Treatment/Give', { plan_id2: document.getElementById("plan_id2").value }, function (result) {
                store[0].setAttribute("value", result[0]);
                store[1].setAttribute("value", result[1]);
            })
        }
        //$('#button').html(result.plan_id + result.detail)
    })
}

function insert() {
    $.post('/Treatment/Update',
        { plan_id: document.getElementById("plan_id2").value, med: document.getElementById("input_0").value, det: document.getElementById("input_1").value }, function (result) {
        if (result == 1) alert("修改成功");
    })
}
