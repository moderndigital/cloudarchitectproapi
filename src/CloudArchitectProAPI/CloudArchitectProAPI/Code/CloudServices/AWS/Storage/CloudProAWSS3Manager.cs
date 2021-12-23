using Amazon.S3;
using System.Text.Json;

namespace CloudArchitectProAPI.Code.CloudServices.AWS.Storage
{
    public class CloudProAWSS3Manager
    {
        public CloudProAWSStorageManager Parent;
        protected AmazonS3Client         S3Client;        

        public CloudProAWSS3Manager(CloudProAWSStorageManager parent)
        {
            Initialize(parent);
        }

        public string GetAllS3Buckets()
        {
            return null;
        }

        protected void Initialize(CloudProAWSStorageManager parent)
        {
            Parent = parent;
            S3Client = new AmazonS3Client(parent.AWSCredentials, parent.AWSRegion);
        }
    }
}
