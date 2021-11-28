using Amazon;
using Amazon.Runtime;
using System.Text.Json;

namespace CloudArchitectProAPI.Code.CloudServices.AWS.Compute
{
    public class CloudProAWSComputeManager
    {
        public CloudProAWSCloudManagerByRegion  Parent;
        public CloudProAWSEC2Manager            EC2Manager;
        public RegionEndpoint                   AWSRegion;
        public AWSCredentials                   AWSCredentials;

        public CloudProAWSComputeManager(CloudProAWSCloudManagerByRegion parent)
        {
            Initialize(parent);
        }

        public JsonDocument GetEverything()
        {
            return EC2Manager.GetAllEC2Instances();
        }

        protected void Initialize(CloudProAWSCloudManagerByRegion parent)
        {
            Parent = parent;
            EC2Manager = new CloudProAWSEC2Manager(this);
        }
    }
}
