$(document).ready(function () {
    if ($('#kind').attr("kind") != "people")
    {
        alert("只有民众可以进行捐献！");
        window.location.href = "../Login/Index";
    }
}
)