using System;
using System.Collections.Generic;
using RestSharp;

namespace Jumpcloud_APITest
{
    internal static class Api
    {
        private static IRestResponse SendRequest(Method type, string url, Dictionary<string, string>? headers, string? body = null)
        {
            Console.WriteLine($"Sending {type} Request '{url}'\n\tHeader: {headers}\n\tBody: {body}");

            var client = new RestClient(url);
            var request = new RestRequest(type);
            if (headers != null) request.AddHeaders(headers);
            if (body != null) request.AddJsonBody(body);
            var response = client.Execute(request);

            Console.WriteLine($"{type} request completed with StatusCode: {response.StatusCode}");
            return response;
        }

        internal static IRestResponse Get(string url, Dictionary<string, string>? headers = null, string? body = null) => SendRequest(Method.GET, url, headers, body);
        internal static IRestResponse Post(string url, Dictionary<string, string>? headers = null, string? body = null) => SendRequest(Method.POST, url, headers, body);
        internal static IRestResponse Put(string url, Dictionary<string, string>? headers = null, string? body = null) => SendRequest(Method.PUT, url, headers, body);
        internal static IRestResponse Delete(string url, Dictionary<string, string>? headers = null, string? body = null) => SendRequest(Method.DELETE, url, headers, body);
    }
}
