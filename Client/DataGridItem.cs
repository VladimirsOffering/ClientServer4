using System;

namespace Client
{
    internal class DataGridItem
    {
        public string IP { get; set; }
        public string PORT { get; set; }

        public DataGridItem(string ip, string port)
        {
            this.IP = ip;
            this.PORT = port;
        }
        public DataGridItem()
        {
        }
    }
}
