function getLocation() {
    var geolocation = new BMap.Geolocation();
    var gc = new BMap.Geocoder();
    geolocation.getCurrentPosition(function (r) {
        console.log(r);
        if (this.getStatus() == BMAP_STATUS_SUCCESS) {
            var pt = r.point;
            gc.getLocation(pt, function (rs) {
                var addComp = rs.addressComponents;
                var province = addComp.province;
                var city = addComp.city;
                var area = addComp.district;
                document.getElementById('site1').value = province + ":" + city + ":" + area;
                $("#site").text(province + ":" + city + ":" + area);
                $("#button").text(province + ":" + city + ":" + area);
            });
        }
        else {
            alert("定位失败");
        }
    }, { enableHighAccuracy: true });
}
