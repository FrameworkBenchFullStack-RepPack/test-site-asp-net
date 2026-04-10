const connection = new signalR.HubConnectionBuilder()
    .withUrl("/LiveHub")
    .configureLogging("none")
    .build();

connection.on("ReceiveNumbers", function (data) {
    document.getElementById("sig-1").innerText = data.number1;
    document.getElementById("sig-2").innerText = data.number2;
    document.getElementById("sig-3").innerText = data.number3;
});

function startSignalR() {
    if (connection.state === signalR.HubConnectionState.Disconnected) {
        connection.start().catch(err => console.error(err.toString()));
    }
}

startSignalR();
