using Amazon;
using Amazon.Runtime;
using System.Text;
using System.Text.Json;

namespace CloudArchitectProAPI.Code.CloudServices.AWS.Compute
{
    public class CloudProAWSComputeManager
    {
        public CloudProAWSCloudManagerByRegion  Parent;
        public CloudProAWSEC2Manager            EC2Manager;
        public CloudProAWSLambdaManager         LambdaManager;
        public RegionEndpoint                   AWSRegion;
        public AWSCredentials                   AWSCredentials;
        protected int                           _resourceCount; 

        public CloudProAWSComputeManager(CloudProAWSCloudManagerByRegion parent)
        {
            Initialize(parent);
        }

        public string GetEverything()
        {
            var retVal = new StringBuilder();

            var ec2Count = EC2Manager.ResourceCount;
            var lambdaCount = LambdaManager.ResourceCount;

            if ((ec2Count > 0) || (lambdaCount > 0))
            {
                var computerManagerHeader = "\n--- Computing Resources for " + Parent.AWSRegion.DisplayName + " ---\n";
                retVal.Append(computerManagerHeader);

                retVal.Append(EC2Manager.GetAllEC2InstanceReports());
                retVal.Append(LambdaManager.GetAllLambdaInstanceReports());

                var computerManagerFooter = "\n--- End Computing Resources for " + Parent.AWSRegion.DisplayName + " ---\n";
                retVal.Append(computerManagerFooter);
            }

            return retVal.ToString();
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
            LambdaManager = new CloudProAWSLambdaManager(this);
        }
    }
}
