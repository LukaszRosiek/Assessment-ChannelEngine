using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.ClientApi
{
    public interface IClientConfig
    {
        string GetBaseApiUrl();
        string GetApiKey();

    }
}
