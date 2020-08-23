function addRadios() {
    var parameters = {
        content: $('#radioContent').val(),
        op1: $('#option1').val(),
        op2: $('#option2').val(),
        op3: $('#option3').val(),
        op4: $('#option4').val(),
    };
    $.post('/Questionnaire/AddRadios', parameters, function (result) {
        if (result) {
            $(location).attr('href', '/questionnaire/read')
        }
        else {
            $('#radioErrorMessage').html('您没有设置题目，题目至少应有一个选项')
        }
    })
}

function addFill() {
    var parameters = {
        q_content: $('#fillContent').val(),
    };
    $.post('/Questionnaire/AddFill', parameters, function (result) {
        if (result) {
            $(location).attr('href', '/questionnaire/read')
        }
        else {
            $('#fillErrorMessage').html('您没有设置题目')
        }
    }
}

function deleteQuestion() {
    var parameters = {
        delete_id: $('#deleteID')
    }

    $.post('/Questionnaire/DeleteQuestion', parameters, function (result) {
        if (result) {
            $(location).attr('href', '/questionnaire/read')
        }
        else {
            $('#managerErrorMessage').html('请输入正确的题号')
        }
    })
}
