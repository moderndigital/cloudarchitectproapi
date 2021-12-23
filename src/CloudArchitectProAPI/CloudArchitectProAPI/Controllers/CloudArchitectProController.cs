using CloudArchitectProAPI.CodeCloudClients;
using System.Web.Http;

namespace CloudArchitectProAPI.Controllers
{
    public class CloudArchitectProController : ApiController
    {
        // GET api/cloudarchitectpro
        public string Get()
        {
            var awsCloudClient = new AWSCloudClient();
            return awsCloudClient.GetEverythingNeededForCloudProScene();
        }
    }
}