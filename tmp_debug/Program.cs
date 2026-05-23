using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SecureMedicalRecordSystem.Infrastructure.Data;
using SecureMedicalRecordSystem.Core.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Amazon.S3;
using Amazon.Runtime;
using Amazon.S3.Model;

var prodDbConn = "Server=db47282.public.databaseasp.net,1433;Database=db47282;User Id=db47282;Password=kamal1234;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=true;";

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(prodDbConn));
    })
    .Build();

using var scope = host.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

var recordId = Guid.Parse("75cab835-88c8-4410-934d-54a80185775c");
var record = await context.MedicalRecords
    .FirstOrDefaultAsync(r => r.Id == recordId);

if (record == null)
{
    Console.WriteLine("Record not found.");
    return;
}

Console.WriteLine("=== Medical Record found in Production DB ===");
Console.WriteLine($"Id: {record.Id}");
Console.WriteLine($"S3ObjectKey: {record.S3ObjectKey}");

// Check Tigris connection and download
Console.WriteLine("\n=== Connecting to Tigris S3 and downloading ===");
var accessKey = "tid_SWimFPPYVZtwhFdmLCaGsSijrYVpBORbbjyQxKtmVBmbPXyPRl";
var secretKey = "tsec_3YwCN75Swyj-XhjB_29uYS_6+lCtgrPAacNAUiTVdxqfTZm6vk16ditAAmBvBzrVERKB2r";
var serviceUrl = "https://t3.storage.dev";
var bucketName = "medical-records-encrypted";

var credentials = new BasicAWSCredentials(accessKey, secretKey);
var config = new AmazonS3Config
{
    ServiceURL = serviceUrl,
    ForcePathStyle = true,
    AuthenticationRegion = "us-east-1"
};

using var s3Client = new AmazonS3Client(credentials, config);

try
{
    Console.WriteLine($"Attempting S3 GetObjectAsync for Key: '{record.S3ObjectKey}' from Bucket: '{bucketName}'");
    var response = await s3Client.GetObjectAsync(new GetObjectRequest
    {
        BucketName = bucketName,
        Key = record.S3ObjectKey
    });

    Console.WriteLine($"SUCCESS! HttpStatusCode: {response.HttpStatusCode}, ContentLength: {response.ContentLength}");
    using (var reader = new StreamReader(response.ResponseStream))
    {
        var firstBytes = new char[20];
        int read = await reader.ReadBlockAsync(firstBytes, 0, 20);
        Console.WriteLine($"First {read} chars: {new string(firstBytes, 0, read)}");
    }
}
catch (AmazonS3Exception ex)
{
    Console.WriteLine($"AmazonS3Exception: {ex.Message}");
    Console.WriteLine($"ErrorCode: {ex.ErrorCode}");
    Console.WriteLine($"StatusCode: {ex.StatusCode}");
}
catch (Exception ex)
{
    Console.WriteLine($"General Exception: {ex.Message}");
    if (ex.InnerException != null)
    {
        Console.WriteLine($"Inner: {ex.InnerException.Message}");
    }
}



