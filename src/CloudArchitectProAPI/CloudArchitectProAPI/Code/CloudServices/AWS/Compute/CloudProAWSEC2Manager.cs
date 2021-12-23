using Amazon.EC2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace CloudArchitectProAPI.Code.CloudServices.AWS.Compute
{
    public class CloudProAWSEC2Manager
    {
        public CloudProAWSComputeManager            Parent;
        public Amazon.EC2.AmazonEC2Client           EC2Client;
        protected int                               _resourceCount;
        protected List<Amazon.EC2.Model.Instance>   _instances;
        public bool                                 Initialized;

        public CloudProAWSEC2Manager(CloudProAWSComputeManager parent)
        {
            Initialize(parent);
        }

        public string GetAllEC2InstanceReports()
        {
            var retVal = new StringBuilder();

            foreach(var instance in _instances)
            {
                var instanceReport = "EC2 Instance : " + instance.InstanceId + "\n";
                retVal.Append(instanceReport);
            }

            return retVal.ToString();
        }

        public int ResourceCount
        {
            get {

                if (!Initialized)
                {
                    return 0;
                }

                return _instances.Count;
            }
        }

        public List<Amazon.EC2.Model.Instance> Instances
        {
            get { return _instances; }
        }

        protected void Initialize(CloudProAWSComputeManager parent)
        {
            Initialized = false;

            _instances = new List<Instance>();
            Parent = parent;
            EC2Client = new Amazon.EC2.AmazonEC2Client(parent.AWSCredentials, parent.AWSRegion);
            GetAllEC2InstancesInternal();
            
            Initialized = true;
        }

        protected void GetAllEC2InstancesInternal()
        {
            if (EC2Client != null)
            {
                if (CheckIfRegionIsOk())
                {
                    var response = EC2Client.DescribeInstances();

                    foreach (var ec2reservation in response.Reservations)
                    {
                        foreach (var instance in ec2reservation.Instances)
                        {
                            _instances.Add(instance);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if AWS Region is OK
        /// </summary>
        /// <returns>false if region is not ok</returns>
        protected bool CheckIfRegionIsOk()
        {
            var retVal = false;

            if (RegionNameIsOk(Parent.AWSRegion.SystemName))
            {
                try
                {
                    // do a test API call.  This API call fails for regions that are not enabled.
                    var regionsResponse = EC2Client.DescribeRegions();
                    retVal = true;
                }
                catch
                {
                    // eat the exception
                }
            }

            return retVal;
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
                string.Equals(regionSystemName, Amazon.RegionEndpoint.USIsoWest1.SystemName, StringComparison.InvariantCultureIgnoreCase))
            {
                retVal = false;
            }

            return retVal;
        }
    }
}