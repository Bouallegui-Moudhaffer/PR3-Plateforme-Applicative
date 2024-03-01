using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;

namespace PA.Desktop.Services
{
    public class SignalRService
    {
        private HubConnection _connection;

        public SignalRService(string machineId)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl($"https://localhost:7029/machinesHub?posteId={machineId}")
                .WithAutomaticReconnect()
                .Build();

            _connection.On<string>("ReceiveCommand", (command) =>
            {
                switch (command.ToLower())
                {
                    case "shutdown":
                        ShutdownMachine();
                        break;
                    case "restart":
                        RestartMachine();
                        break;
                        // Handle other commands as needed
                }
            });
        }

        public async Task StartAsync()
        {
            await _connection.StartAsync();
        }

        public async Task StopAsync()
        {
            await _connection.StopAsync();
        }
        void ShutdownMachine()
        {
            Process.Start("shutdown", "/s /t 0");
            MessageBox.Show("Shutdown command received");
        }

        void RestartMachine()
        {
            Process.Start("shutdown", "/r /t 0");
        }
    }
}
