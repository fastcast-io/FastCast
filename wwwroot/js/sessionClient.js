﻿//"use strict"

var hasStarted = false
var mySessionCode = -1
var formWrapper = document.getElementById("form-location") //.innerHTML
var formIFrame = document.getElementById("google-form-link") //.src
var sv = document.getElementById("rl").innerText // Really bad. Should find a way to better pass data to the javascript
document.getElementById("rl").innerText = ""
formIFrame.src = ""
formIFrame.style.display = "none";
var durationSpan = document.getElementById("remaining-time")
var shortJumboTitle = document.getElementById("jumbotron-title")
//var formWrapper = document.getElementById("form-location")
var statusNoStartTd = document.getElementById("session-no-start")
var statusEndTd = document.getElementById("session-ended")
var statusStartedTd = document.getElementById("session-started")
var statusNoStartProgress = document.getElementById("session-no-start-progress")
var statusEndProgress = document.getElementById("session-ended-progress")
var statusStartedProgress = document.getElementById("session-started-progress")
var completeSurveyBtn = document.getElementById("completeSurvey")


/**
 * Complete btn is deactivated at first
 * */
completeSurveyBtn.disabled = true;

completeSurveyBtn.addEventListener("click", (e) => {
    console.log(document.getElementById("finish"));
    document.getElementById("finish").submit(); e.preventDefault();
})

var sessionConnection = new signalR.HubConnectionBuilder().withUrl("/sessionHub").build();
sessionConnection.start().then(function () {
    console.log("Connection was started for the client");

    mySessionCode = document.getElementById("mySessionCode").innerText;
    console.log(`Joining the session right away with session code: ${mySessionCode}`)
    sessionConnection.invoke("JoinSession", mySessionCode).then(function () {
        //console.log("Client has joined the session");
    }).catch(function (err) {
        return console.error(err.toString());
    });

}).catch(function (err) {
    return console.error(err.toString());
});

sessionConnection.on("UserHasJoined", function () {
        console.log("A new user has joined the session");
})

sessionConnection.on("ShowDurationLeft", function (durationLeft) {

    if (!hasStarted) {
        hasStarted = true
        // Form
        shortJumboTitle.innerText = "";
        formIFrame.src = sv;
        formIFrame.style.display = "initial";
        formWrapper.style.display = "initial";
        completeSurveyBtn.disabled = false;

        // Status
        statusNoStartTd.style.display = "none";
        statusStartedTd.style.display = "initial";

        // Progress
        statusNoStartProgress.style.display = "none";
        statusStartedProgress.style.display = "initial";
    }
    durationSpan.innerText = durationLeft.remaining;
});

sessionConnection.on("StopTimer", function () {
    if (hasStarted) {
        // Form
        shortJumboTitle.innerText = "";
        formIFrame.src = "";
        sv = "";
        formWrapper.style.display = "none";

        // Status
        statusStartedTd.style.display = "none";
        statusEndTd.style.display = "initial";

        // Progress
        statusStartedProgress.style.display = "none";
        statusEndProgress.style.display = "initial";
    }
    //console.log("yeah it's over");
    //durationSpan.innerText = 0;
    shortJumbo.innerText = "Timer stopped. Thank you for participating";
    setInterval(function () {
        console.log("Inside timer stopped")
        document.getElementById("finish").submit();
    }, 5000);
    // Submit here 
});