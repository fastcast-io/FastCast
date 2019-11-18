//"use strict"

var hasStarted = false
var mySessionCode = -1
if (document.getElementById("Authenticated") && document.getElementById("Authenticated").value.toString() === "True") {
    var formWrapper = document.getElementById("form-location")
    var formIFrame = document.getElementById("google-form-link")
    var sv = "https:\//docs.google.com/forms/d/e/" + document.getElementById('SessionFormId').value + "/viewform?embedded=true&output=embed";
    document.getElementById("SessionFormId").value = ""
    formIFrame.src = ""
    formIFrame.style.display = "none";
    var durationSpan = document.getElementById("remaining-time")
    var shortJumboTitle = document.getElementById("jumbotron-title")
    var statusNoStartTd = document.getElementById("session-no-start")
    var statusEndTd = document.getElementById("session-ended")
    var statusStartedTd = document.getElementById("session-started")
    var statusNoStartProgress = document.getElementById("session-no-start-progress")
    var statusEndProgress = document.getElementById("session-ended-progress")
    var statusStartedProgress = document.getElementById("session-started-progress")
    var completeSurveyBtn = document.getElementById("completeSurvey")

    completeSurveyBtn.disabled = true;

    function redirectSuccess() {
        window.location.pathname = "/AnswerSuccess"
    }

    var sessionConnection = new signalR.HubConnectionBuilder().withUrl("/sessionHub").build();
    sessionConnection.start().then(function () {
        console.log("Connection was started for the client");

        mySessionCode = document.getElementById("mySessionCode").innerText;

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

        shortJumboTitle.innerText = "Timer stopped. Thank you for participating";
        setInterval(function () {
            console.log("Inside timer stopped")
            redirectSuccess()
        }, 5000);
        // Submit here 
    });

}
