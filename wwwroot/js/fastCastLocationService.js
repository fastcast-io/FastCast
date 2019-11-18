// Location Service
var mainForm = document.getElementById("joinSession");
var longitude = null, latitude = null;

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            console.log("Getting location")
            document.getElementById("longitude").value = position.coords.longitude;
            document.getElementById("latitude").value = position.coords.latitude;
        }, function (error) {
            console.log("There was some issue with getting your location. Please reload")
        });

    } else {
        alert("Geolocation is not supported by this browser. Please use a browser supporting browser");
    }
}

function locationWasInjected() {
    return $("#longitude").val() != "" && $("#longitude").val() != ""
}

function accessLocation() {
    return new Promise(function (resolve, reject) {
        (function waitForLocationUpdate(times) {
            if (locationWasInjected()) {
                return resolve()
            }

            if (times == 5) return reject()

            getLocation();
            setTimeout(waitForLocationUpdate, 1000, times++);
        })(1);
    })
}




function formSubmit(e) {
    if (!locationWasInjected()) {
        e.preventDefault();
    }

    Promise.resolve(accessLocation()).then(function success() {
        $("#joinSession").unbind().submit();
    }).catch(function (err) {
        console.log("There was an error");
    })
}

function startProcessingLocation() {
    console.log("Window loaded");
    $('#joinSession').submit(formSubmit)
}


window.onload = startProcessingLocation