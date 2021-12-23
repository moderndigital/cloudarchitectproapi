using Amazon;
using Amazon.Runtime;
using CloudArchitectProAPI.Code.CloudServices.AWS.Compute;
using CloudArchitectProAPI.Code.CloudServices.AWS.Networking;
using CloudArchitectProAPI.Code.CloudServices.AWS.Storage;
using System.Text.Json;

namespace CloudArchitectProAPI.Code.CloudServices.AWS
{
    public class CloudProAWSCloudManagerByRegion
    {
        public bool                                 IsInitialized;
        public CloudProAWSCloudManager              Parent;
        public RegionEndpoint                       AWSRegion;
        public AWSCredentials                       AWSCredentials;
        public CloudProAWSComputeManager            ComputeManager;
        public CloudProAWSNetworkingManager         NetworkingManager;
        public CloudProAWSStorageManager            StorageManager;

        public CloudProAWSCloudManagerByRegion(CloudProAWSCloudManager parent, 
                                               RegionEndpoint region, 
                                               AWSCredentials awsCredentials)
        {
            IsInitialized = false;
            Initialize(parent, region, awsCredentials);
        }

        protected void Initialize(CloudProAWSCloudManager parent, 
                                  RegionEndpoint region, 
                                  AWSCredentials awsCredentials)
        {
            Parent = parent;
            AWSRegion = region;
            AWSCredentials = awsCredentials;
            ComputeManager = new CloudProAWSComputeManager(this);
            NetworkingManager = new CloudProAWSNetworkingManager(this);
            StorageManager = new CloudProAWSStorageManager(this);
            IsInitialized = true;
        }

        public string GetEverything()
        {
            if (!IsInitialized)
            {
                return null;
            }

            var computeResourcesJson = ComputeManager.GetEverything();
            var networkingResourcesJson = NetworkingManager.GetEverything();
            var storageResourcesJson = StorageManager.GetEverything();

            return computeResourcesJson;
        }
    }
}
