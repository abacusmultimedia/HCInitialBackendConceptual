
using System;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Model;

namespace AWSWasabi
{
    /**
    * Example for Wasabi S3 read object.
    */
    public static class Program
    {
        public static async void Main(string[] args)
        {
            // 1. this is necessary for the endpoint
            var s3Config = new AmazonS3Config() { ServiceURL = "https://s3.wasabisys.com" };

            // To use access key and secret key directly, just add in the access and secret keys directly to the function call:
            // create iam connection with credential files and config
            // var s3 = new AmazonS3Client("<access-key>", "secret-key", s3Config);

            // To use AWS credentials from the AWS .credentials file.
            var chain = new CredentialProfileStoreChain();
            if (chain.TryGetProfile("wasabi", out var basicProfile))
            {
                // create iam connection with credential files and config
                var s3 = new AmazonS3Client(basicProfile.Options.AccessKey, basicProfile.Options.SecretKey, s3Config);

                // set parameters
                const string bucketName = "test-c-sharp-bucket-rv";
                const string key = "Test.txt";

                // create a get object request
                var getObjectRequest = new GetObjectRequest() { BucketName = bucketName, Key = key };

                // call a get operation
                var mydata = await s3.GetObjectAsync(getObjectRequest);
            }
            else
            {
                Console.Write("Error Invalid credentials");
            }
        }
    }
}