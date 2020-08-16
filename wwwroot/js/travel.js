function click1(str) {
    $.post('/Template/Click', { name: str, number: 100 }, function (result) {
        $('#button').html(result.name + result.num)
    })
}
