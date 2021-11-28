using CloudArchitectProAPI.Code.CloudServices.AWS;
using System.Text.Json;

namespace CloudArchitectProAPI.CodeCloudClients
{
    public class AWSCloudClient
    {
        protected CloudProAWSCloudManager cloudMgr;

        public AWSCloudClient()
        {
            Init();
        }

        public JsonDocument GetEverythingNeededForCloudProScene()
        {
            return cloudMgr.GetEverything();
        }

        protected void Init()
        {
            cloudMgr = new CloudProAWSCloudManager();
        }
    }
}
