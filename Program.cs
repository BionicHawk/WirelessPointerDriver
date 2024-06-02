using System.Net;
using System.Net.Sockets;

namespace WirelessPointerDriver;

public class Program {

    private static IPAddress GetDefaultIP() {
        string hostname = Dns.GetHostName();
        List<IPAddress> addresses = [..Dns.GetHostAddresses(hostname)];
        List<IPAddress> validAddresses = [];

        foreach (IPAddress address in addresses)
            if (address.AddressFamily == AddressFamily.InterNetwork)
                validAddresses.Add(address);

        return validAddresses.LastOrDefault()!;
    }

    public static void Main(string[] args) {
        IPAddress address;
        ushort port = 8080;
        
        // Making some validations for the ip address and the port
        if (args.Length == 0) {
            address = GetDefaultIP();
        } else if (args.Length == 2) {
            address = IPAddress.Parse(args[0]);
            if(!ushort.TryParse(args[1], out port)) {
                Console.WriteLine("The given port is invalid");
                return;
            }
        } else if (args.Length == 3) {
            var flag = args.Last();
            if (flag.Equals("--debug")) {
                SetDebugMode();       
            }
            address = IPAddress.Parse(args[0]);
            if(!ushort.TryParse(args[1], out port)) {
                Console.WriteLine("The given port is invalid");
                return;
            }
        } else {
            Console.WriteLine("The command line arguments given are invalid");
            return;
        }
        
        Console.WriteLine($"Listening at: {address} {port}");

        // Make a tcp server
        Server server = new(address, port);
        server.StartServer(); 

    }
    
    private static void SetDebugMode() {
        MouseController.DebugMode = true;
    }

}