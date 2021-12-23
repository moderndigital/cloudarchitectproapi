using System.Text.Json;

namespace CloudArchitectProAPI.Code.CloudServices.AWS.Networking
{
    public class CloudProAWSVPCManager
    {
        public CloudProAWSNetworkingManager Parent;
        public Amazon.EC2.AmazonEC2Client   EC2Client;

        public CloudProAWSVPCManager(CloudProAWSNetworkingManager parent)
        {
            Initialize(parent);
        }

        public string GetAllVPCs()
        {
            return null;
        }

        protected void Initialize(CloudProAWSNetworkingManager parent)
        {
            Parent = parent;
            EC2Client = new Amazon.EC2.AmazonEC2Client(parent.AWSCredentials, parent.AWSRegion);
        }
    }
}
