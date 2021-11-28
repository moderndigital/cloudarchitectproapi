using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CloudArchitectProAPI.Code.CloudServices.AWS
{
    public class CloudProAWSCloudManager
    {
        public bool IsInitialized;
        protected Dictionary<RegionEndpoint, CloudProAWSCloudManagerByRegion> AWSCloudManagersByRegion;

        public CloudProAWSCloudManager()
        {
            IsInitialized = false;
            AWSCloudManagersByRegion = null;
            Initialize();
        }

        public JsonDocument GetEverything()
        {
            if (!IsInitialized)
            {
                return null;
            }

            var sb = new StringBuilder();

            var keys = AWSCloudManagersByRegion.Keys;
                
            foreach(var region in keys)
            {
                var cloudMgrForRegion = AWSCloudManagersByRegion[region];
                    
                if (cloudMgrForRegion != null)
                {
                    var dataForRegion = cloudMgrForRegion.GetEverything();
                    sb.Append(dataForRegion);
                }
            }

            return JsonDocument.Parse(sb.ToString());
        }

        protected void Initialize()
        {
            InitializeCloudManagersByRegion();
            IsInitialized = true;
        }

        protected void InitializeCloudManagersByRegion()
        {
            AWSCloudManagersByRegion = new Dictionary<RegionEndpoint, CloudProAWSCloudManagerByRegion>();

            var accessKey = Environment.GetEnvironmentVariable("CloudArchitectProAccessKey");
            var secretKey = Environment.GetEnvironmentVariable("CloudArchitectProSecretKey");

            if (!string.IsNullOrEmpty(accessKey) && !string.IsNullOrEmpty(secretKey))
            {
                var options = new CredentialProfileOptions
                {
                    AccessKey = accessKey,
                    SecretKey = secretKey
                };

                var regions = RegionEndpoint.EnumerableAllRegions;

                foreach (var region in regions)
                {
                    var profile = new CredentialProfile("basic_profile", options)
                    {
                        Region = region
                    };

                    var netSDKFile = new NetSDKCredentialsFile();
                    netSDKFile.RegisterProfile(profile);

                    var chain = new CredentialProfileStoreChain();

                    if (chain.TryGetAWSCredentials("basic_profile", out AWSCredentials awsCredentials))
                    {
                        var awsCloudRegionalMgr = new CloudProAWSCloudManagerByRegion(this, region, awsCredentials);
                        AWSCloudManagersByRegion.Add(region, awsCloudRegionalMgr);
                    }
                }
            }
        }
    }
}
