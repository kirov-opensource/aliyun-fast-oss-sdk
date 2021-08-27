using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Kirov.Aliyun.OSS.Configs;

namespace Kirov.Aliyun.OSS.Objects
{
    public class DefaultObjectClient : Client, IObjectClient
    {
        public DefaultObjectClient(AliyunOSSConfig config, HttpClient httpClient) : base(config, httpClient)
        {
        }

        public async Task Put(Stream stream, string bucketName, string path)
        {
            using (var httpResponse = await base.SendAsync(bucketName, path, HttpMethod.Put, new StreamContent(stream)).ConfigureAwait(false))
            {
                if (httpResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new HttpRequestException(await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false));
                }
            }
        }

        public async Task Put(Stream stream, string bucketName, string path, int bufferSize)
        {
            using (var httpResponse = await base.SendAsync(bucketName, path, HttpMethod.Put, new StreamContent(stream, bufferSize)).ConfigureAwait(false))
            {
                if (httpResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new HttpRequestException(await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false));
                }
            }
        }

    }
}