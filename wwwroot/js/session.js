"use strict";

var sessionConnection = new signalR.HubConnectionBuilder().withUrl("/sessionHub").build();

var sessionCode = "";
var sessionDuration = "";
var endedTimer = false;
var hasJoined = true
document.getElementById("startTimer").disabled = true;

/**
 * Once session starts
 * */
sessionConnection.start().then(function () {
    console.log("Connection was started")
    document.getElementById("startTimer").disabled = false;

    console.log("Joining the session right away")
    sessionCode = document.getElementById("sessionCode").innerText
    sessionConnection.invoke("JoinSession", sessionCode).then(function () {
        hasJoined = true
        console.log("Has joined the session");
    }).catch(function (err) {
        return console.error(err.toString());
    });

}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("startTimer").addEventListener("click", function (event) {
    sessionCode = document.getElementById("sessionCode").innerText;
    sessionDuration = parseInt(document.getElementById("sessionDuration").innerText)

    console.log({
        sessionCode,
        sessionDuration
    })

    sessionConnection.invoke("StartTimer", sessionCode, parseInt(sessionDuration)).then(function () {
        console.log(`Starting timer at ${Date.now.toString()} for ${sessionDuration} seconds`);
        endedTimer = false;
        document.getElementById("startTimer").disabled = true;
    }).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
})

sessionConnection.on("UserHasJoined", function () {
    console.log("A new user has joined the session");
})

sessionConnection.on("ShowDurationLeft", function (durationLeft) {
    if (!endedTimer) {
        if (parseFloat(durationLeft.remaining) > 0) {
            document.getElementById("remaining-time").innerText = durationLeft.remaining;
        }
        else {
            document.getElementById("remaining-time").innerText = "DONE";
            stopTimer();
            endedTimer = true
        }
    }

});

function stopTimer() {
    sessionConnection.invoke("StopTimer", sessionCode, sessionDuration).then(function () {
        console.log("Stopping timer at", Date.now.toString())
        document.getElementById("startTimer").disabled = false;
    }).catch(function (err) {
        return console.error(err.toString());
    });
}
