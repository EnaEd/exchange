using System.IO;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services.Interfaces
{
    public interface IGoogleStorageService
    {
        Task<string> UploadFileAsync(string bucketName, string fileName, string contentType, MemoryStream content);
    }
}
