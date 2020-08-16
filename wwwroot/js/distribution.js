function click1(str) {
    $.post('/Distribution/Click', { name: str, number: 100 }, function (result) {
        $('#button').html(result.name + result.num)
    })
}

function click2(str1,str2) {
    $.post('/Distribution/ClickToDistributeMoney', { hospname: str2, donid: str1 }, function (result) {
        
        $('#' + str1).remove();
    })
}


function click3(str1, str2) {
    $.post('/Distribution/ClickToDistributeMaterial', { hospname: str2, donid: str1 }, function (result) {
       
        $('#' + str1).remove();
    })
}
