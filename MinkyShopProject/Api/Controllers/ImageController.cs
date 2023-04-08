using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MinkyShopProject.Admin.Shared;
using System.Text;

namespace MinkyShopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private static string ApiKey = "AIzaSyBXtrCKY7bnByz6gOVzZrum_5FZJqYG__k";
        private static string Bucket = "minkyshop-bed92.appspot.com";
        private static string AuthEmail = "haidang15122002@gmail.com";
        private static string AuthPassword = "badao1234";

        [HttpPost]
        public async Task UploadImage(string image)
        {
            var stream = System.IO.File.Open(image, FileMode.Open);

            // of course you can login using other method, not just email+password
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            // you can use CancellationTokenSource to cancel the upload midway
            var cancellation = new CancellationTokenSource();

            var imageUrl = image.Split(@"\");

            var task = new FirebaseStorage(
                Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                })
                .Child("images")
                .Child(imageUrl[imageUrl.Length - 1])
                .PutAsync(stream, cancellation.Token);

            // cancel the upload
            // cancellation.Cancel();

            try
            {
                // error during upload will be thrown when you await the task
                Console.WriteLine("Download link:\n" + await task);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception was thrown: {0}", ex);
            }
            stream.Close();
        }

        private readonly IWebHostEnvironment env;
        private readonly ILogger<ImageController> logger;

        //public ImageController(IWebHostEnvironment env,
        //    ILogger<ImageController> logger)
        //{
        //    this.env = env;
        //    this.logger = logger;
        //}

        //[HttpPost]
        //public async Task<ActionResult<IList<UploadResult>>> PostFile(
        //    [FromForm] IEnumerable<IFormFile> files)
        //{
        //    var maxAllowedFiles = 3;
        //    long maxFileSize = 1024 * 15;
        //    var filesProcessed = 0;
        //    var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
        //    List<UploadResult> uploadResults = new();

        //    foreach (var file in files)
        //    {
        //        var uploadResult = new UploadResult();
        //        string trustedFileNameForFileStorage;
        //        var untrustedFileName = file.FileName;
        //        uploadResult.FileName = untrustedFileName;
        //        var trustedFileNameForDisplay =
        //            WebUtility.HtmlEncode(untrustedFileName);

        //        if (filesProcessed < maxAllowedFiles)
        //        {
        //            if (file.Length == 0)
        //            {
        //                logger.LogInformation("{FileName} length is 0 (Err: 1)",
        //                    trustedFileNameForDisplay);
        //                uploadResult.ErrorCode = 1;
        //            }
        //            else if (file.Length > maxFileSize)
        //            {
        //                logger.LogInformation("{FileName} of {Length} bytes is " +
        //                    "larger than the limit of {Limit} bytes (Err: 2)",
        //                    trustedFileNameForDisplay, file.Length, maxFileSize);
        //                uploadResult.ErrorCode = 2;
        //            }
        //            else
        //            {
        //                try
        //                {
        //                    trustedFileNameForFileStorage = Path.GetRandomFileName();
        //                    var path = Path.Combine(env.ContentRootPath,
        //                        env.EnvironmentName, "unsafe_uploads",
        //                        trustedFileNameForFileStorage);

        //                    await using FileStream fs = new(path, FileMode.Create);
        //                    await file.CopyToAsync(fs);

        //                    logger.LogInformation("{FileName} saved at {Path}",
        //                        trustedFileNameForDisplay, path);
        //                    uploadResult.Uploaded = true;
        //                    uploadResult.StoredFileName = trustedFileNameForFileStorage;
        //                }
        //                catch (IOException ex)
        //                {
        //                    logger.LogError("{FileName} error on upload (Err: 3): {Message}",
        //                        trustedFileNameForDisplay, ex.Message);
        //                    uploadResult.ErrorCode = 3;
        //                }
        //            }

        //            filesProcessed++;
        //        }
        //        else
        //        {
        //            logger.LogInformation("{FileName} not uploaded because the " +
        //                "request exceeded the allowed {Count} of files (Err: 4)",
        //                trustedFileNameForDisplay, maxAllowedFiles);
        //            uploadResult.ErrorCode = 4;
        //        }

        //        uploadResults.Add(uploadResult);
        //    }

        //    return new CreatedResult(resourcePath, uploadResults);
        //}
    }
}
