using System;
using System.Collections.Generic;
using System.Text;

namespace ClientApiChannelEngine
{
    public interface IClientConfig
    {
        string GetBaseApiUrl();
        string GetApiKey();

    }
}
