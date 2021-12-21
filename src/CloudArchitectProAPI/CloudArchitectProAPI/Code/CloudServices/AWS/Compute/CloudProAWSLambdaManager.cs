using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;

namespace CloudArchitectProAPI.Code.CloudServices.AWS.Compute
{
    public class CloudProAWSLambdaManager
    {
        public CloudProAWSComputeManager Parent;
        public Amazon.Lambda.AmazonLambdaClient LambdaClient;
        protected int _resourceCount;
        protected List<Amazon.Lambda.Model.FunctionConfiguration> _instances;
        public bool Initialized;

        public CloudProAWSLambdaManager(CloudProAWSComputeManager parent)
        {
            Initialize(parent);
        }

        public JsonDocument GetAllLambdaInstances()
        {
            return JsonDocument.Parse(ResourceCount.ToString());
        }

        public int ResourceCount
        {
            get
            {

                if (!Initialized)
                {
                    return 0;
                }

                return _instances.Count;
            }
        }

        public List<Amazon.Lambda.Model.FunctionConfiguration> Instances
        {
            get { return _instances; }
        }

        protected void Initialize(CloudProAWSComputeManager parent)
        {
            Initialized = false;

            _instances = new List<Amazon.Lambda.Model.FunctionConfiguration>();
            Parent = parent;
            LambdaClient = new Amazon.Lambda.AmazonLambdaClient(parent.AWSCredentials, parent.AWSRegion);
            GetAllLambdaInstancesInternal();

            Initialized = true;
        }

        protected void GetAllLambdaInstancesInternal()
        {
            if (LambdaClient != null)
            {
                // copied from (my) CloudCrud...Lambda...ReadList()

                try
                {
                    var response = LambdaClient.ListFunctions();
                    foreach (var functionConfiguration in response.Functions)
                    {
                        _instances.Add(functionConfiguration);
                    }
                }
                catch (Exception e)
                {
                    if (e is Amazon.Lambda.AmazonLambdaException)
                    {
                        // eat the exception
                        // Debug.WriteLine(exception.ToString());
                        Debug.WriteLine("Lambda Exception for this region :" + Parent.AWSRegion.SystemName);
                    }
                    else
                    {
                        // eat the exception
                        Debug.WriteLine("Exception for this region :" + Parent.AWSRegion.SystemName + " was \n" + e.ToString());
                    }
                }
            }
        }
    }
}