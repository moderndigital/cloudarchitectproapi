using CloudArchitectProAPI.CodeCloudClients;
using System.Text.Json;
using System.Web.Http;

namespace CloudArchitectProAPI.Controllers
{
    public class CloudArchitectProController : ApiController
    {
        // GET api/cloudarchitectpro
        public JsonDocument Get()
        {
            var awsCloudClient = new AWSCloudClient();
            return awsCloudClient.GetEverythingNeededForCloudProScene();
        }
    }
}