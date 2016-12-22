using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Controls;

namespace ICE
{
    class Water
    {
        private static X509Certificate2 _certificate;
        private static int _port;
        
        public Water(string refs, int port)
        {
            _certificate = new X509Certificate2(refs,"");
            _port = port;


        }
        public void RunServer()
        {
            TcpListener tcp = new TcpListener(IPAddress.Loopback, _port);
            MainWindow a = new MainWindow();
            a.updateLog("Tcp Listener Binded - "+IPAddress.Loopback.ToString()+":"+_port.ToString());
            tcp.Start();
            while (true)
            {
                    
            }
        }
    }
}
