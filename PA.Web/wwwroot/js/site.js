let connection = null;

async function startSignalRConnection() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7029/machinesHub")
        .withAutomaticReconnect()
        .build();

    try {
        await connection.start();
        console.log("SignalR connection established.");
    } catch (err) {
        console.error("SignalR Connection Error: ", err);
    }
}

async function sendCommandToMachine(machineId, command) {
    if (!connection) {
        await startSignalRConnection();
    }
    await connection.invoke("SendCommandToMachine", machineId, command);
}

startSignalRConnection();
