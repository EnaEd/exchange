using Exchange.Mobile.Core.Constants;
using Exchange.Mobile.Core.Services.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services
{
    public class GoogleStorageService : IGoogleStorageService
    {
        public async Task<string> UploadFileAsync(string bucketName, string fileName, string contentType, MemoryStream content)
        {
            try
            {
                var credentials = GoogleCredential.FromJson("Exchange-0c08ff4fae13");
                var _storage = StorageClient.Create();
                var obj = await _storage.UploadObjectAsync(bucketName, fileName, contentType, content);
                return $"{Constant.GoogleConstant.GOOGLE_STORAGE_BUCKET_PATH}{fileName}";
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(message: $"{ex.Message}");
                throw;
            }
        }
    }
}
