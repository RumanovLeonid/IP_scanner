using System.Net.NetworkInformation;

namespace IP_scanner
{
    public class ToPing
    {
        private readonly Ping ping = new Ping();
        private PingReply pingReply;
        public IPStatus Ping(string IPAdress)
        {
            pingReply = ping.Send(IPAdress, 5);
            return pingReply.Status;
        }
    }
}
