using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace Jumpcloud_APITest
{
    internal static class Api
    {
        private static IRestResponse SendRequest(Method type, string url, Dictionary<string, string>? headers, string? body = null, bool bodyAsJson = true)
        {
            Console.WriteLine($"Sending {type} Request '{url}'\n\tHeader: {headers}\n\tBody: {body}");

            var client = new RestClient(url);
            var request = new RestRequest(type);
            if (headers != null) request.AddHeaders(headers);
            if (body != null)
                if (bodyAsJson) request.AddJsonBody(body);
                //else request.AddBody(body, "text/plain"); // plain text body
                else request.AddParameter("text/plain", body, ParameterType.RequestBody);

            var response = client.Execute(request);

            Console.WriteLine($"{type} request completed with StatusCode: {response.StatusCode}");
            return response;
        }
        internal static IRestResponse Get(string url, Dictionary<string, string>? headers = null, string? body = null) => SendRequest(Method.GET, url, headers, body);
        internal static IRestResponse Post(string url, Dictionary<string, string>? headers = null, string? body = null, bool bodyAsJson = true) => SendRequest(Method.POST, url, headers, body, bodyAsJson);
        internal static IRestResponse Put(string url, Dictionary<string, string>? headers = null, string? body = null) => SendRequest(Method.PUT, url, headers, body);
        internal static IRestResponse Delete(string url, Dictionary<string, string>? headers = null, string? body = null) => SendRequest(Method.DELETE, url, headers, body);

        private static void SendRequestAsync(Method type, string url, Dictionary<string, string>? headers, string? body = null)
        {
            Console.WriteLine($"Sending {type} Request '{url}'\n\tHeader: {headers}\n\tBody: {body}");

            var client = new RestClient(url);
            var request = new RestRequest(type);
            if (headers != null) request.AddHeaders(headers);
            if (body != null) request.AddJsonBody(body);
            client.ExecuteAsync(request);
        }
        internal static void GetAsync(string url, Dictionary<string, string>? headers = null, string? body = null) => SendRequestAsync(Method.GET, url, headers, body);
        internal static void PostAsync(string url, Dictionary<string, string>? headers = null, string? body = null) => SendRequestAsync(Method.POST, url, headers, body);
        internal static void PutAsync(string url, Dictionary<string, string>? headers = null, string? body = null) => SendRequestAsync(Method.PUT, url, headers, body);
        internal static void DeleteAsync(string url, Dictionary<string, string>? headers = null, string? body = null) => SendRequestAsync(Method.DELETE, url, headers, body);
    }
}
