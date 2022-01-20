using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumpcloud_APITest
{
    internal class Stats
    {
        [JsonProperty("TotalRequests")]
        public int TotalRequests { get; set; }

        [JsonProperty("AverageTime")]
        public int AverageTime { get; set; }
    }
}
