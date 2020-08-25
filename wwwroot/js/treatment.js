function click2() {
    $.post('/Treatment/Click2', {  pat_id: document.getElementById("patient_id").value, date: document.getElementById("date").value,time: document.getElementById("time").value, med: document.getElementById("medicine").value, detail: document.getElementById("detail").value }, function (result) {
        if (result == 1) { alert("插入完成"); }
        else if (result == 0) {
            alert("您未登录或身份受限");
            window.location.href="../Login/Index";
        }
        else if (result == 2) {
            alert("这名病人不存在请核实");
        }
    })
}

function login() {
    window.location.href = "../Login/Index";
}

function modifi() {
    $.post('/Treatment/Find', { plan_id2: document.getElementById("plan_id2").value }, function (result) {
        if (result == 1) { alert("这个plan不存在") }
        if (result == 2) {
            var board = document.getElementById("staff");
            var store = new Array(2);
            for (var i = 0; i < 4; i++) {
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
            mmm.setAttribute("id","cc");
            board.appendChild(mmm);
            store[0].setAttribute("type", "date");
            store[1].setAttribute("type", "time");
            $.post('/Treatment/Give', { plan_id2: document.getElementById("plan_id2").value }, function (result) {
                store[0].setAttribute("value", result[0].substr(0, 10));
                store[1].setAttribute("value", result[1]);
                store[2].setAttribute("value", result[2]);
                store[3].setAttribute("value", result[3]);
            })
        }
        //$('#button').html(result.plan_id + result.detail)
    })
}

function insert() {
    $.post('/Treatment/Update',
        { plan_id: document.getElementById("plan_id2").value, date: document.getElementById("input_0").value, time: document.getElementById("input_1").value, med: document.getElementById("input_2").value, det: document.getElementById("input_3").value }, function (result) {
            alert("修改成功");
            window.location.href = "../Treatment/Check";
    })
    
}

function CheckLogin1() {
    $.post('/Treatment/CheckLogin', function (result) {
        if (result == 0) { alert("请您先登录"); window.location.href = "../Login?path=../Treatment/Check"; }
        if (result == 1) { alert("当前不是医生请登陆"); window.location.href = "../Login?path=../Treatment/Check";}
    })
}
function CheckLogin2() {
    $.post('/Treatment/CheckLogin', function (result) {
        if (result == 0) { alert("请您先登录"); window.location.href = "../Login?path=../Treatment/Insert"; }
        if (result == 1) { alert("当前不是医生请登陆"); window.location.href = "../Login?path=../Treatment/Insert"; }
    })
}
