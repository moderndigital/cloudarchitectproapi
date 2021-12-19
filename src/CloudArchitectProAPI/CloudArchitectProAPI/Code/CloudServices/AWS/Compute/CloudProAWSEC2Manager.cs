using Amazon.EC2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public JsonDocument GetAllEC2Instances()
        {
            return JsonDocument.Parse(ResourceCount.ToString());
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
                // copied from (my) CloudCrud...EC2...ReadList()

                try
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
                catch (Exception e)
                {
                    if (e is Amazon.EC2.AmazonEC2Exception)
                    {
                        // eat the exception
                        // Debug.WriteLine(exception.ToString());
                        Debug.WriteLine("EC2 Exception for this region :" + Parent.AWSRegion.SystemName);
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