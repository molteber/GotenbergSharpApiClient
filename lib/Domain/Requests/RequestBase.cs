﻿
namespace Gotenberg.Sharp.API.Client.Domain.Requests
{
    public class RequestBase
    {
        public RequestConfig Config { get; set; }

        public AssetRequest Assets { get; set; }
    }

}