using System.IO;
using System.Threading.Tasks;

namespace Kirov.Aliyun.OSS.Objects
{
    public interface IObjectClient
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="bucketName"></param>
        /// <param name="path"></param>
        Task Put(Stream stream, string bucketName, string path);

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="bucketName"></param>
        /// <param name="path"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        Task Put(Stream stream, string bucketName, string path, int bufferSize);
    }
}