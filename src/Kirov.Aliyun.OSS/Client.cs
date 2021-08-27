using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Aliyun.OSS;
using Kirov.Aliyun.OSS.Configs;
using MimeTypes;
using Newtonsoft.Json;

namespace Kirov.Aliyun.OSS
{
    public abstract class Client
    {
        protected readonly HttpClient httpClient;
        protected readonly AliyunOSSConfig config;

        protected Client(AliyunOSSConfig config, HttpClient httpClient)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(AliyunOSSConfig));
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(HttpClient));
            if (!config.RegionEndpoint.StartsWith("http://") && !config.RegionEndpoint.StartsWith("https://"))
            {
                throw new ArgumentException("Must start with \"http://\" or \"https://\"", nameof(AliyunOSSConfig.RegionEndpoint));
            }
        }

        protected async Task<HttpResponseMessage> SendAsync(string bucketName, string path, HttpMethod method, HttpContent content)
        {
            using (var httpRequestMessage = new HttpRequestMessage(method, GeneratePresignedUri(method, bucketName, path)))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue(MimeTypeMap.GetMimeType(path));
                httpRequestMessage.Content = content;
                return await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
            }
        }

        private string GeneratePresignedUri(HttpMethod method, string bucketName, string path)
        {
            var ossClient = new OssClient(config.RegionEndpoint, config.AccessKey, config.AssessSecret);
            return ossClient.GeneratePresignedUri(new GeneratePresignedUriRequest(bucketName, path)
            {
                // Process = process,
                // Expiration = expiration,
                ContentType = MimeTypeMap.GetMimeType(path),
                Method = ConvertHttpMethod(method)
            }).ToString();
        }

        private SignHttpMethod ConvertHttpMethod(HttpMethod method)
        {
            if (method == HttpMethod.Get)
            {
                return SignHttpMethod.Get;
            }
            else if (method == HttpMethod.Post)
            {
                return SignHttpMethod.Post;
            }
            else if (method == HttpMethod.Put)
            {
                return SignHttpMethod.Put;
            }
            else if (method == HttpMethod.Delete)
            {
                return SignHttpMethod.Delete;
            }
            else
            {
                return SignHttpMethod.Head;
            }
        }
    }
}