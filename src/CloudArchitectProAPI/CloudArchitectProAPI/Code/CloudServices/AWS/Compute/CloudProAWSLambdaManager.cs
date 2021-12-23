using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace CloudArchitectProAPI.Code.CloudServices.AWS.Compute
{
    public class CloudProAWSLambdaManager
    {
        public CloudProAWSComputeManager                            Parent;
        public Amazon.Lambda.AmazonLambdaClient                     LambdaClient;
        protected int                                               _resourceCount;
        protected List<Amazon.Lambda.Model.FunctionConfiguration>   _instances;
        public bool                                                 Initialized;

        public CloudProAWSLambdaManager(CloudProAWSComputeManager parent)
        {
            Initialize(parent);
        }

        public string GetAllLambdaInstanceReports()
        {
            var retVal = new StringBuilder();

            foreach (var instance in _instances)
            {
                var instanceReport = "Lambda Function : " + instance.FunctionName + "\n";
                retVal.Append(instanceReport);
            }

            return retVal.ToString();
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
                if (RegionNameIsOk(Parent.AWSRegion.SystemName))
                {
                    var response = LambdaClient.ListFunctions();
                    foreach (var functionConfiguration in response.Functions)
                    {
                        _instances.Add(functionConfiguration);
                    }
                }
            }
        }

        /// <summary>
        /// Hardcodes certain region names to exclude.
        /// </summary>
        /// <param name="regionSystemName">AWS system name for the Region</param>
        /// <returns>false if regionSystemName is an excluded region</returns>
        protected bool RegionNameIsOk(string regionSystemName)
        {
            var retVal = true;

            if (string.Equals(regionSystemName, Amazon.RegionEndpoint.APEast1.SystemName, StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(regionSystemName, Amazon.RegionEndpoint.USGovCloudEast1.SystemName, StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(regionSystemName, Amazon.RegionEndpoint.USGovCloudWest1.SystemName, StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(regionSystemName, Amazon.RegionEndpoint.USIsobEast1.SystemName, StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(regionSystemName, Amazon.RegionEndpoint.USIsoEast1.SystemName, StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(regionSystemName, Amazon.RegionEndpoint.USIsoWest1.SystemName, StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(regionSystemName, Amazon.RegionEndpoint.AFSouth1.SystemName, StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(regionSystemName, Amazon.RegionEndpoint.APSoutheast3.SystemName, StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(regionSystemName, Amazon.RegionEndpoint.EUSouth1.SystemName, StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(regionSystemName, Amazon.RegionEndpoint.MESouth1.SystemName, StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(regionSystemName, Amazon.RegionEndpoint.CNNorth1.SystemName, StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(regionSystemName, Amazon.RegionEndpoint.CNNorthWest1.SystemName, StringComparison.InvariantCultureIgnoreCase))
            {
                retVal = false;
            }

            return retVal;
        }
    }
}