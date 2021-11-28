using System.Text.Json;

namespace CloudArchitectProAPI.Code.CloudServices.AWS.Compute
{
    public class CloudProAWSEC2Manager
    {
        public CloudProAWSComputeManager    Parent;
        public Amazon.EC2.AmazonEC2Client   EC2Client;

        public CloudProAWSEC2Manager(CloudProAWSComputeManager parent)
        {
            Initialize(parent);
        }

        public JsonDocument GetAllEC2Instances()
        {
            return null;
        }

        protected void Initialize(CloudProAWSComputeManager parent)
        {
            Parent = parent;
            EC2Client = new Amazon.EC2.AmazonEC2Client(parent.AWSCredentials, parent.AWSRegion);
        }
    }
}
