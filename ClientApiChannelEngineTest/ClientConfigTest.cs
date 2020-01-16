using ClientApiChannelEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientApiChannelEngineTest
{
    public class ClientConfigTest : IClientConfig
    {
        public string GetApiKey()
        {
            return "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";
        }

        public string GetBaseApiUrl()
        {
            return "https://api-dev.channelengine.net";
        }
    }
}
