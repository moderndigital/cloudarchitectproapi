using System.Collections.Generic;
using System.Web.Http;

namespace CloudArchitectProAPI.Controllers
{
    public class CloudArchitectProController : ApiController
    {
        // GET api/cloudarchitectpro
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}