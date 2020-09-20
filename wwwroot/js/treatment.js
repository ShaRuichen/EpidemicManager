
function click12() {
    $.post('/Treatment/Click2', {  pat_id: document.getElementById("patient_id").value, med: document.getElementById("medicine").value, detail: document.getElementById("detail").value }, function (result) {
        if (result == 1) { alert("插入完成"); window.location.href = "/home"; }
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
        if (result == 1) { alert("这个病人不存在") }
        if (result == 2) {
            var a = document.getElementById("plan_id2").value;
            window.location.href = "../Treatment/Check?patient="+a;
        }
    })
}

function modifi2() {
    var board = document.getElementById("staff");
    var store = new Array(2);
    for (var i = 0; i < 4; i++) {
        var e = document.createElement("textarea");
        var h = document.createElement("br");
        e.type = "text"
        e.setAttribute("id", "input_" + i);//设置name属性
        store[i] = e;
        board.appendChild(e);//将e追加到board最后一个节点后面
        board.appendChild(h); 
    }
    var mmm = document.createElement("input");
    mmm.type = "button";
    mmm.setAttribute("value", "确定");
    mmm.setAttribute("onclick", "insert()");
    mmm.setAttribute("id", "cc");
    mmm.setAttribute("class", "btn btn-primary");
    board.appendChild(mmm);
    store[3].setAttribute("class", "bigput");
    store[3].setAttribute("style", "width: 200px; height: 200px;" );
    store[1].setAttribute("class", "smallput");
    store[0].setAttribute("class", "textareaab");
    store[0].setAttribute("rows", 1);
    store[2].setAttribute("rows", 1);
    store[0].setAttribute("cols", 7);
    store[2].setAttribute("cols", 7);
    store[0].setAttribute("readonly", "readonly");
    store[2].setAttribute("readonly", "readonly");
    store[2].setAttribute("class", "textareaab");
    $.post('/Treatment/Give', { plan_id2: document.getElementById("sss").value }, function (result) {
        document.getElementById("input_1").value = result[2];  
        document.getElementById("input_3").value = result[3];  
        document.getElementById("input_0").value = "用药：";  
        document.getElementById("input_2").value = "备注：";  
        //store[0].setAttribute("value", result[2]);
        //store[1].setAttribute("value", result[3]);
    })
}

function insert() {
    $.post('/Treatment/Update',
        { plan_id: document.getElementById("sss").value,  med: document.getElementById("input_0").value, det: document.getElementById("input_1").value }, function (result) {
            alert("修改成功");
            window.location.href = "../Treatment/Check?patient=null";
    })
    
}

function CheckLogin1() {
    $.post('/Treatment/CheckLogin', function (result) {
        if (result == 0) { alert("请您先登录"); window.location.href = "../Login?path=../Treatment/Check?patient=null"; }
        if (result == 1) { alert("当前不是医生请登陆"); window.location.href = "../Login?path=../Treatment/Check?patient=null";}
    })
}
function CheckLogin2() {
    $.post('/Treatment/CheckLogin', function (result) {
        if (result == 0) { alert("请您先登录"); window.location.href = "../Login?path=../Treatment/Insert"; }
        if (result == 1) { alert("当前不是医生请登陆"); window.location.href = "../Login?path=../Treatment/Insert"; }
    })
}
function Moo(h) {
    var a = document.getElementById(h.id).value;
    window.location.href = "../Treatment/Findout?plan_id2=" + a;
}
function Moo1(h) {
    var a = document.getElementById(h.id).value;
    window.location.href = "../Treatment/Findout2?plan_id2=" + a;
}
