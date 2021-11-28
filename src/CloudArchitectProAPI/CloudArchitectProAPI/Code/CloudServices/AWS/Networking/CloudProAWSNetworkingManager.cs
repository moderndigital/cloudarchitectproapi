using Amazon;
using Amazon.Runtime;
using System.Text.Json;

namespace CloudArchitectProAPI.Code.CloudServices.AWS.Networking
{
    public class CloudProAWSNetworkingManager
    {
        public CloudProAWSCloudManagerByRegion  Parent;
        public RegionEndpoint                   AWSRegion;
        public AWSCredentials                   AWSCredentials;
        public CloudProAWSVPCManager            VPCManager;

        public CloudProAWSNetworkingManager(CloudProAWSCloudManagerByRegion parent)
        {
            Initialize(parent);
        }

        public JsonDocument GetEverything()
        {
            return null;
        }

        protected void Initialize(CloudProAWSCloudManagerByRegion parent)
        {
            Parent = parent;
            VPCManager = new CloudProAWSVPCManager(this);
        }
    }
}
