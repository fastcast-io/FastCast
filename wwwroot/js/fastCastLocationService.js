// Location Feature
var mainForm = document.getElementById("joinSession");
var longitude = null, latitude = null;
//var submitted = false;

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
        // TODO: Implement reloading to piss them
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
                console.log("DELAY")
                getLocation();
                setTimeout(waitForLocationUpdate, 1000, times++);
        })(1)
    })
}




function formSubmit(e) {
    //console.log("WAS CALLED")
    //getLocation();
    if (!locationWasInjected()) {
        console.log("WAS CALLED")
        e.preventDefault();
    }

    Promise.resolve(accessLocation()).then(function success() {
        $("#joinSession").unbind().submit();
        
        //e.submit()
        //this.submit();
        //console.log({e})
        //e.preventDefault();
        //return false
        //formSubmit();
        //accessLocation().then(function () {
        //})
    })
    //    .catch(function () {
    //    alert("Geolocation is not supported by this browser.");
    //    e.preventDefault();
    //    return false
    //})
    /*
    if ($("#latitude").length && $("#longitude").length && $("#latitude").val() != "" && $("#longitude").val() == "") {
        console.log(`${$("#latitude").val()} and ${$("#latitude").val()}`)
        //console.log(`${}`)
        //$.ajax({
        //    url: '/Index',
        //    type: 'POST',
        //    contentType: 'application/json; charSet=utf-8',
        //    headers: {
        //        RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        //    },
        //    data: JSON.stringify({
        //        AuthCode: $('#AuthCode').val(),
        //        Latitude: $('#Latitude').val(),
        //        Longitude: $('#Longitude').val()
        //    })
        //}).done(function (result) {
        //    console.log("Successfully submitted")
        //}).catch(function (err) {
        //    console.log(`Error submitting ${err}. Please refresh.`)
        //})
    } else {
        e.preventDefault();
        console.log(`${$('#longitude').val()}`)
        console.log("ELSE")
        //getLocation();
    }*/
}

function startProcessingLocation() {
    console.log("Window loaded");
    $('#joinSession').submit(formSubmit)
    //getLocation();
    //$(document).on("submit", function t() {
    //    console.log(`Getting location before submit`)
    //    getLocation();
    //})
}

document.addEventListener("keyup", function submit(e) {
    if (e.keyCode === 13) {
        // If user taps enter, submit!
        document.getElementById("join-button").click();
    }
})


window.onload = startProcessingLocation