// Location Feature
var mainForm = document.getElementById("joinSession");
var longitude = null, latitude = null;

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            longitude = position.coords.longitude;
            latitude = position.coords.latitude;
        }, function (error) {
            console.log("There was some issue with getting your location. Please reload")
        });

    } else {
        alert("Geolocation is not supported by this browser. Please use a browser supporting browser");
        // TODO: Implement reloading to piss them
    }
}

getLocation();
var times = 0;
function formSubmit() {
    getLocation();
    document.getElementById("latitude").value = latitude;
    document.getElementById("longitude").value = longitude;

    if ((latitude != undefined && latitude != null) && (longitude != undefined && longitude != null)) {
        console.log(`Successfully retrieved user information. {longitude:${longitude}, latitude:${latitude}`)
        document.getElementById("latitude").value = latitude;
        document.getElementById("longitude").value = longitude;
        mainForm.submit();
    }
    else if (times < 20) {
        console.log(`Trying location. ${times}`)
        times += 1;
        formSubmit();
    }
    else {
        alert("Geolocation is not supported by this browser. Please use a browser supporting browser");
    }
}

document.addEventListener("keyup", function submit(e) {
    if (e.keyCode === 13) {
        // If user taps enter, submit!
        document.getElementById("join-button").click();
    }
})