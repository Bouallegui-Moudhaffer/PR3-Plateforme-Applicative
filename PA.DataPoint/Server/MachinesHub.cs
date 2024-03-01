using Microsoft.AspNetCore.SignalR;

namespace PA.DataPoint.Server
{
    public class MachinesHub : Hub
    {
        private static readonly Dictionary<string, string> machineConnections = new Dictionary<string, string>();

        public override Task OnConnectedAsync()
        {
            var machineId = Context.GetHttpContext().Request.Query["posteId"];
            if (!string.IsNullOrEmpty(machineId))
            {
                machineConnections[machineId] = Context.ConnectionId;
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var item = machineConnections.FirstOrDefault(kvp => kvp.Value == Context.ConnectionId);
            if (!string.IsNullOrEmpty(item.Key))
            {
                machineConnections.Remove(item.Key);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendCommandToMachine(string machineId, string command)
        {
            if (machineConnections.TryGetValue(machineId, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceiveCommand", command);
            }
            else
            {
                Console.WriteLine($"MachineId {machineId} not connected or not found.");
            }
        }
    }
}
