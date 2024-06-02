using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WirelessPointerDriver;

public class Server(IPAddress address, ushort port) {

    public TcpListener Tcp {get; private set;} = null!; 

    public void StartServer() {
        Tcp = new TcpListener(address, port);
        Tcp.Start();
        MouseController.SetDisplayResolution();
        Console.WriteLine("Server Started");
        while (true) {
            Socket socket = Tcp.AcceptSocket();
            ListenSocket(socket);
        }
    }

    private void ListenSocket(Socket socket) {
        while (socket.Connected) {
            byte[] bytes = new byte[1024];
            socket.Receive(bytes);
            string response = Encoding.UTF8.GetString(bytes);
            CommandSystem.InterpretCommand(response);
        }
    }

}