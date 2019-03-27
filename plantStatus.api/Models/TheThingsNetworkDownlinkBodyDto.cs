using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace plantStatus.api.Models {
    public class TheThingsNetworkDownlinkBodyDto
    {
        public string dev_id;
        public int port;
        public bool confirmed;
        public string payload_raw;
    }
}
