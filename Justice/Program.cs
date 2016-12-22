using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Justice
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener socket = new TcpListener(IPAddress.Any, 443);
            socket.Start();
            Console.WriteLine("Server Started. " + DateTime.Now);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            X509Certificate2 cert = new X509Certificate2("c:/users/andrew/certs/local.pfx", "a");
            Console.WriteLine(cert.SubjectName);
            while (true)
            {
                TcpClient client = socket.AcceptTcpClient();
                Proccess(client, cert);
            }
        }

        static void Proccess(TcpClient client, X509Certificate2 cert)
        {
            SslStream ssl = new SslStream(client.GetStream(), false);
     
 
                ssl.AuthenticateAsServer(cert, false, SslProtocols.Tls12, true);

                string message = ReadMessage(ssl);
                byte[] messageBytes = Encoding.UTF8.GetBytes(BuildString("<h1>Testing</h1>"));
                ssl.Write(messageBytes);
                Console.WriteLine("Received : {0}", message);
                ssl.Close();
                client.Close();

 
        }

        static string BuildString(string data)
        {
            byte[] leng = new byte[1024];
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("HTTP/1.1 200 OK");
            sb.AppendLine("Access-Control-Allow-Origin: *");
            sb.AppendLine("Access-Control-Allow-Headers: Origin, X-Requested-with, Content-Type, Accept");
            sb.AppendLine("Content-Type: application/javascript");
            sb.AppendLine("Transfer-Encoding: chunked");
            int length = Encoding.UTF8.GetBytes(data).Length + 2 ;
            sb.AppendLine("Content-Length: " + length);
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine(data);
            return sb.ToString();
        }
        static string ReadMessage(SslStream str)
        {
            byte[] buffer = new byte[2048];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                bytes = str.Read(buffer, 0, buffer.Length);
                Decoder decoder = Encoding.UTF8.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                decoder.GetChars(buffer, 0, bytes, chars, 0);
                messageData.Append(chars);
                if (messageData.ToString().IndexOf("<EOF>") != -1)
                {
                    break;
                }
            } while (bytes != 0);
            return messageData.ToString();
        }

        public static string RemoveServerHeader(string data)
        {
            int i = 0;
            string[] split = data.Split('\n');
            string datad = "";
            while (i < 8)
            {
                split[i] = "";
                i++;

            }
            foreach (string a in split)
            {
                datad = datad + a;
            }
            return datad;
        }

    }

}
