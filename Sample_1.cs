using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

class Sample_1
{
    public class PingResult
    {
        public long RoundtripTime { get; private set; }
        public IPStatus Status { get; private set; }

        public PingResult(long roundtripTime, IPStatus status)
        {
            this.RoundtripTime = roundtripTime;
            this.Status = status;
        }

    }

    static async Task<PingResult> Ping(string host, int timeout = 2000)
    {
        var ping = new Ping();
        var result = await ping.SendPingAsync(host, timeout);
        return new PingResult(result.RoundtripTime, result.Status);
    }


    static async void demo_Sample_1()
    {
        var hosts = Enumerable.Repeat("www.google.com", 100).ToArray();
    var tasks = hosts.Select(x => Ping(x)).ToArray();
    var results = await Task.WhenAll(tasks);
    foreach (var result in results)
        Console.WriteLine($"{result.Status} - {result.RoundtripTime}");
      }
}