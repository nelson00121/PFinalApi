using Minio;
using Minio.DataModel.Args;

namespace Bucket;

public interface IMinioService
{
    Task<string> UploadImageAsync(Stream imageStream, string fileName, string contentType = "image/jpeg");
    Task<bool> DeleteImageAsync(string fileName);
    Task<string> GetImageUrlAsync(string fileName);
    string GetBucketAddress();
}
