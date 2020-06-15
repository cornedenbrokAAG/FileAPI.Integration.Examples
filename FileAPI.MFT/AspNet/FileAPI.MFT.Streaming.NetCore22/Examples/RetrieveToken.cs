﻿using FileAPI.MFT.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace FileAPI.MFT.Streaming.NetCore22.Examples
{
    public class RetrieveToken : Startup
    {
        public RetrieveToken(ITestOutputHelper output) : base(output) { }

        // This is an example of how to retrieve the authentication token from Ping.
        // This is out of the scope of the File API, but as the File API needs a token to recognize the user, this example was created.
        // To get more information about how to retrieve the token, please refer to the Ping documentation.
        // ToDo Add ping documentation.
        [Fact]
        public async Task RetrieveAnAuthenticationTokenFromPing()
        {
            Output.WriteTittle("Executing Streaming.SDK example: Retrieve an authentacion token from Ping");

            var clientId = "MyClientId";
            var clientSecret = "MyClientSecret";
            var pingBaseAddress = "https://api-test.raet.com/authentication/token"; // Change it if you want to use another environment

            // Create the request for Ping.
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
            };
            request.Headers.Add("Cache-Control", "no-cache");

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret)
            };

            request.Content = new FormUrlEncodedContent(keyValues);

            // Do the call to Ping and retrieve the token.
            using (var httpClient = new HttpClient() { BaseAddress = new Uri(pingBaseAddress) })
            {
                var response = await httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseContent)["access_token"].ToString();

                Output.WriteLine($"Token generated for clientId <{clientId}> and clientSecret <{clientSecret}>:");
                Output.WriteLine($"{token}");
            }
        }
    }
}
