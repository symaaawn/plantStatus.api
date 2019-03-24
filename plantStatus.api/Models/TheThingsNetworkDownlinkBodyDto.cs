using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace plantStatus.api.Models {
    public class TheThingsNetworkDownlinkBodyDto
    {
        public string app_id;
        public string dev_id;
        public string hardware_serial;
        public string port;
        public string counter;
        public string payload_raw;
        public object payload_fields;
        public object metadata;
        public string downlink_url;
    }
}
