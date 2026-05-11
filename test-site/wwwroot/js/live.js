import "microsoft/signalR";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/LiveHub")
    .configureLogging("none")
    .build();

connection.on("ReceiveNumbers", function (data) {
    const element = document.querySelector("#live-data");
    if(!element) return;
    element.getElementById("sig-1").innerText = data.number1;
    element.getElementById("sig-2").innerText = data.number2;
    element.getElementById("sig-3").innerText = data.number3;
});

function startSignalR() {
    if (connection.state === signalR.HubConnectionState.Disconnected) {
        connection.start().catch(err => console.error(err.toString()));
    }
}

startSignalR();
