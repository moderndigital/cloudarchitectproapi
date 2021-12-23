using Amazon;
using Amazon.Runtime;
using System.Text.Json;

namespace CloudArchitectProAPI.Code.CloudServices.AWS.Storage
{
    public class CloudProAWSStorageManager
    {
        public CloudProAWSCloudManagerByRegion  Parent;
        public CloudProAWSS3Manager             S3Manager;
        public AWSCredentials                   AWSCredentials;
        public RegionEndpoint                   AWSRegion;

        public CloudProAWSStorageManager(CloudProAWSCloudManagerByRegion parent)
        {
            Initialize(parent);
        }

        public string GetEverything()
        {
            return S3Manager.GetAllS3Buckets();
        }

        protected void Initialize(CloudProAWSCloudManagerByRegion parent)
        {
            Parent = parent;
            AWSCredentials = parent.AWSCredentials;
            AWSRegion = parent.AWSRegion;
            S3Manager = new CloudProAWSS3Manager(this);
        }
    }
}