using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using plantStatus.api.Models;
using RestSharp;

namespace plantStatus.api.Services {
    public class TheThingsNetworkDownlinkService
    {
        public void QueueDownlink(string downlinkUri, TheThingsNetworkDownlinkBodyDto downlinkBody)
        {
            Debug.WriteLine($"Sending to URI {downlinkUri}");
            var client = new RestClient(downlinkUri);

            var request = new RestRequest();

            request.AddJsonBody(downlinkBody);

            var response = client.Post(request);
        }
    }
}
