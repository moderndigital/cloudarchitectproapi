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
        protected int                           _resourceCount; 

        public CloudProAWSComputeManager(CloudProAWSCloudManagerByRegion parent)
        {
            Initialize(parent);
        }

        public JsonDocument GetEverything()
        {
            return EC2Manager.GetAllEC2Instances();
        }

        public int ResourceCount
        {
            get { return _resourceCount; }
        }

        protected void Initialize(CloudProAWSCloudManagerByRegion parent)
        {
            _resourceCount = 0;
            Parent = parent;
            AWSRegion = parent.AWSRegion;
            AWSCredentials = parent.AWSCredentials;
            EC2Manager = new CloudProAWSEC2Manager(this);
        }
    }
}
