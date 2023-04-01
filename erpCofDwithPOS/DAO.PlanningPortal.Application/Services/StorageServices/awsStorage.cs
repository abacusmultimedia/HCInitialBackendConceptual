using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Http;

namespace UploadToAWS
{
    static class UploadObject
    {
        private static IAmazonS3 _s3Client;

        private const string BucketName = "profdocs";

        private const string ObjectName1 = "new-test-file.txt";

        // updated it to take any object from desktop, just adjust the file name above
        private static readonly string PathToDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        //static async Task Main()
        //{
        //    // 1. this is necessary for the endpoint
        //    var config = new AmazonS3Config { ServiceURL = "https://s3.wasabisys.com" };

        //    // this will allow you to call whatever profile you have
        //    var credentials = new StoredProfileAWSCredentials("muhammadshakirpulhr@gmail.com");

        //    // create s3 connection with credential files and config.
        //    _s3Client = new AmazonS3Client(credentials, config);

        //    // The method expects the full path, including the file name.
        //    var path = $"{PathToDesktop}/{ObjectName1}";

        //    await UploadObjectFromFileAsync(_s3Client, BucketName, ObjectName1, path);
        //}


        public static async Task<string> read(string fileName , string   _bucketName)
        {   
            var s3 = new AmazonS3Client("AKIATZS5PLRAAQYVCPU7", "giByPDDCV3PV18ksbqhL70JEFqaaK+XrCs8rk9SG");

            // set parameters
            string bucketName = _bucketName;
            string key = fileName;

            // create a get object request
            var getObjectRequest = new GetObjectRequest() { BucketName = bucketName, Key = key };

            // call a get operation
            var mydata = await s3.GetObjectAsync(getObjectRequest);



            // Create a new instance of memorystream
            var memoryStream = new MemoryStream();

            // Use the .CopyTo() method and write current filestream to memory stream
            mydata.ResponseStream.CopyTo(memoryStream);

            // Convert Stream To Array
            byte[] byteArray = memoryStream.ToArray();
            string base64String = Convert.ToBase64String(byteArray, 0, byteArray.Length);

            return base64String;


        }




        public static async Task UploadObjectFromFileAsync(
        string _bucketName,
        string objectName,
        IFormFile file)
        {
            try
            {
                var client = new AmazonS3Client("AKIATZS5PLRAAQYVCPU7", "giByPDDCV3PV18ksbqhL70JEFqaaK+XrCs8rk9SG");
                // check if BUcket exists 
                var bucketExist = await AmazonS3Util.DoesS3BucketExistAsync(client, _bucketName);
                if (!bucketExist)
                {
                    // Create a bucket 

                    var bucketRequest = new PutBucketRequest()
                    {
                        BucketName = _bucketName,
                        UseClientRegion = true
                    };
                    await client.PutBucketAsync(bucketRequest);
                    // 

                }
                var objectRequest = new PutObjectRequest()
                {
                    BucketName = _bucketName,
                    Key = objectName,
                    InputStream = file.OpenReadStream(),

                };
                var resp = await client.PutObjectAsync(objectRequest);

            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }



    }
}