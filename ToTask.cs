using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace IP_scanner
{
    class ToTask
    {
        private List<string> IP_List = new List<string>();
        private string IPAdress;
        private Task[] tasks = new Task[4];

        public List<string> Scan()
        {
            tasks[0] = Task.Run(ScanIPRange_1);
            tasks[1] = Task.Run(ScanIPRange_2);
            tasks[2] = Task.Run(ScanIPRange_3);
            tasks[3] = Task.Run(ScanIPRange_4);

            Task.WaitAll(tasks);

            return IP_List;
        }
        public List<string> GetIP_List()
        {
            return IP_List;
        }

        private void ScanIPRange_1()
        {
            ToPing ping = new ToPing();
            ScanRange(ping, 1, 63);
        }
        private void ScanIPRange_2()
        {
            ToPing ping = new ToPing();
            ScanRange(ping, 64, 127);
        }
        private void ScanIPRange_3()
        {
            ToPing ping = new ToPing();
            ScanRange(ping, 128, 191);
        }
        private void ScanIPRange_4()
        {
            ToPing ping = new ToPing();
            ScanRange(ping, 191, 254);
        }
        private void ScanRange(ToPing ping, int begin, int end)
        {
            for (int i = begin; i <= end; i++)
            {
                IPAdress = "192.168.1." + i;
                if (ping.Ping(IPAdress) == IPStatus.Success)
                {
                    IP_List.Add(IPAdress);
                }
            }
        }
    }
}
