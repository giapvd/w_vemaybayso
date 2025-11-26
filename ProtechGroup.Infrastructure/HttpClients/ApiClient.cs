using ProtechGroup.Domain;
using ProtechGroup.Infrastructure.Setting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ProtechGroup.Infrastructure.HttpClients
{
    public class ApiClient
    {
        public static async Task<string> PostMethodHttpClientAddHeader(string url, string bodypost,
                                                               Dictionary<string, string> headers)
        {
            using (var client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30) })
            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                // Gắn headers
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        if (header.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
                        {
                            // Nếu là Content-Type thì set cho Content
                            if (request.Content != null)
                            {
                                request.Content.Headers.ContentType =
                                    new System.Net.Http.Headers.MediaTypeHeaderValue(header.Value);
                            }
                        }
                        else
                        {
                            // Còn lại thì add vào request headers
                            request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(bodypost))
                {
                    request.Content = new StringContent(bodypost, Encoding.UTF8);
                    request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                }
                try
                {
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi gọi API: {ex.Message}", ex);
                }
            }
        }
       
        public static async Task<string> GetMethodHttpClientAddHeader(string urlGet,
                                                               Dictionary<string, string> headers)
        {
            using (var client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30) })
            using (var request = new HttpRequestMessage(HttpMethod.Get, urlGet))
            {
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
