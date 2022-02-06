﻿using System;
using System.Net;
using System.Net.Http;

using Gotenberg.Sharp.API.Client.Domain.Requests;
using Gotenberg.Sharp.API.Client.Extensions;

using Newtonsoft.Json;

// ReSharper disable All CA1032
// ReSharper disable All CA1822 
// ReSharper disable All MemberCanBePrivate.Global
namespace Gotenberg.Sharp.API.Client.Infrastructure
{
    /// <inheritdoc />
    public sealed class GotenbergApiException : Exception
    {
        readonly IApiRequest _request;
        readonly HttpResponseMessage _response;

        public GotenbergApiException(string message, IApiRequest request, HttpResponseMessage response)
            : base(message)
        {
            this._request = request;
            this._response = response;
            this.StatusCode = _response.StatusCode;
            this.RequestUri = _response.RequestMessage.RequestUri;
            this.ReasonPhrase = _response.ReasonPhrase;
        }

        public HttpStatusCode StatusCode { get; }

        public Uri RequestUri { get; }

        public string ReasonPhrase { get; }

        public static GotenbergApiException Create(IApiRequest request, HttpResponseMessage response)
        {
            var message = response.Content.ReadAsStringAsync().Result;
            return new GotenbergApiException(message, request, response);
        }

        public string ToVerboseJson(
            bool indentJson = false, 
            bool includeRequestContent = true, 
            bool includeGotenbergResponse = true)
        {
            using (_response)
            {
                return JsonConvert.SerializeObject(new
                {
                    GotenbergMessage = Message,
                    GotenbergResponseReceived = includeGotenbergResponse ? _response : null,
                    ClientRequestSent = _request,
                    ClientRequestFormContent = includeRequestContent ? _request.IfNullEmptyContent().ToDumpFriendlyFormat(false) : null,
                }, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = indentJson ? Formatting.Indented : Formatting.None
                });
            }
        }
    }
}