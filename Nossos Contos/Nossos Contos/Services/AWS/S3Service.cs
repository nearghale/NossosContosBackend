using Amazon.S3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Services.AWS
{
    public class S3Service
    {

		private AmazonS3Client _client;
		private string _bucketName;

		public S3Service(Model.Configurations.AWS.Credentials credential, Model.Configurations.AWS.S3Configuration configuration)
		{
			_client = new AmazonS3Client(credential.AccessKeyId, credential.SecretAccessKey, Amazon.RegionEndpoint.GetBySystemName(configuration.Region));
			_bucketName = configuration.BucketName;
		}

		public void Upload(Stream stream, string key, S3CannedACL permission, string bucketName = "")
		{
			var request = new Amazon.S3.Model.PutObjectRequest()
			{
				BucketName = String.IsNullOrEmpty(bucketName) ? _bucketName : bucketName,
				CannedACL = permission,
				InputStream = stream,
				Key = key
			};
			var response = _client.PutObjectAsync(request);
			response.Wait();

			if (!response.IsCompletedSuccessfully)
				throw response.Exception;
		}

		public Stream Read(string key, string bucketName = "")
		{
			var request = new Amazon.S3.Model.GetObjectRequest()
			{
				BucketName = String.IsNullOrEmpty(bucketName) ? _bucketName : bucketName,
				Key = key,
				//ResponseExpires = 
				//Timeout = 999999999,
				//ReadWriteTimeout = 999999999
			};

			//using (GetObjectResponse response = client.GetObject(request))
			//{
			var response = _client.GetObjectAsync(request);
			response.Wait();

			if (!response.IsCompletedSuccessfully)
				throw response.Exception;

			string title = response.Result.Metadata["x-amz-meta-title"];
			//Console.WriteLine("The object's title is {0}", title);
			return response.Result.ResponseStream;
			//string dest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), keyName);
			//if (!File.Exists(dest))
			//{
			//response.WriteResponseStreamToFile(dest);
			//}
			//}
		}


	}
}
