﻿using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System.Net;

public class ImageApi
{

    private readonly IAmazonS3 _s3client;
    private readonly IAmazonRekognition _rekognitionClient;

    //Constructor
    public ImageApi(IAmazonS3 s3client, IAmazonRekognition rekognitionClient)
    {
        _s3client = s3client;
        _rekognitionClient = rekognitionClient;
    }    

    //Function to convert base64 to MemoryStream
    public MemoryStream ConvertBase64ToMemoryStream(string base64)
    {
        byte[] bytes = Convert.FromBase64String(base64);
        return new MemoryStream(bytes);
    }

    //Function to upload image base64 to S3 bucket with given bucket name and key and returns http status code
    public async Task<HttpStatusCode> UploadImage(string base64, string bucketName, string key)
    {
        MemoryStream bytes = ConvertBase64ToMemoryStream(base64);
        PutObjectRequest request = new PutObjectRequest()
        {
            BucketName = bucketName,
            Key = key,
            InputStream = bytes
        };
        PutObjectResponse response = await _s3client.PutObjectAsync(request);
        return response.HttpStatusCode;
    }

    //Function to moderate image bytes with AWS Rekognition
    public async Task<List<Label>> DetectLabels(MemoryStream bytes)
    {
        return new List<Label>();
    }
}
