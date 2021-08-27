using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Kirov.Aliyun.OSS.Configs;
using Kirov.Aliyun.OSS.Objects;

namespace Kirov.Aliyun.OSS
{
    public class OSSClient
    {
        private readonly IObjectClient objectClient;
        public OSSClient(AliyunOSSConfig config, HttpClient httpClient)
        {
            objectClient = new DefaultObjectClient(config, httpClient);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="bucketName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public Task PutObject(Stream stream, string bucketName, string path)
        {
            return objectClient.Put(stream, bucketName, path);
        }
    }
}