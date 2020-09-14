@model Mamodel
@{
    ViewBag.Ref = "travel";
    ViewBag.Title = "添加预设地址";
}

    <div class="container">
        <div class="text-center">
            <img src="~/res/contains/007.png" width="194" height="238" />
            <h2>预设出行地址</h2>
            <p class="lead">填写下表预设出行地址</p>
        </div>
    </div>

<div>
    <form method="post" class="container" action="Setpresite">
        <div class="form-group row">
            <label for="realpresite" class="col-sm-2 col-form-label">预设旅行地点:</label>
            <div class="col-sm-10">
                <input name="realpresite" type="text" class="form-control input-group-lg" required placeholder="预设旅行地点">
            </div>
        </div>
        <div class="form-group form-check">
            <input type="checkbox" class="form-check-input" required id="exampleCheck1">
            <label class="form-check-label" for="exampleCheck1">我已确认无误</label>
        </div>
        <div class="text-center">
            <input type="submit" class="btn btn-primary btn-block" href="/" onclick="comfirm()" value="添加" />
            <a class="btn btn-secondary btn-lg" href="/">返回</a>
        </div>
    </form>
</div>
