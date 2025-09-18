using Minio;
using Minio.DataModel.Args;
namespace Bucket;    
public class MinioService : IMinioService
{
    private readonly IMinioClient _minioClient;
    private const string BucketName = "imagenes";
    private readonly string _endpoint;

    public MinioService(IMinioClient minioClient, string server, int puerto, bool ssl)
    {
        _minioClient = minioClient;
        string certificado = ssl ? "https" : "http";
        _endpoint = $"{certificado}://{server}:{puerto}";
    }

    public async Task<string> UploadImageAsync(Stream imageStream, string fileName, string contentType = "image/jpeg")
    {
        await EnsureBucketExistsAsync();

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(BucketName)
            .WithObject(fileName)
            .WithStreamData(imageStream)
            .WithObjectSize(imageStream.Length)
            .WithContentType(contentType);

        await _minioClient.PutObjectAsync(putObjectArgs);
        
        return $"{BucketName}/{fileName}";
    }

    public async Task<bool> DeleteImageAsync(string fileName)
    {
        try
        {
            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(BucketName)
                .WithObject(fileName);

            await _minioClient.RemoveObjectAsync(removeObjectArgs);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<string> GetImageUrlAsync(string fileName)
    {
        var presignedGetObjectArgs = new PresignedGetObjectArgs()
            .WithBucket(BucketName)
            .WithObject(fileName)
            .WithExpiry(60 * 60 * 24);

        return await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);
    }

    public string GetBucketAddress()
    {
        return $"{_endpoint}/";
    }

    private async Task EnsureBucketExistsAsync()
    {
        var bucketExistsArgs = new BucketExistsArgs()
            .WithBucket(BucketName);

        bool found = await _minioClient.BucketExistsAsync(bucketExistsArgs);
        
        if (!found)
        {
            var makeBucketArgs = new MakeBucketArgs()
                .WithBucket(BucketName);

            await _minioClient.MakeBucketAsync(makeBucketArgs);
        }
    }
}

