using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services.Interfaces
{
    public interface IDropBoxService
    {
        Task<string> UploadFile(string folder, string fileName, byte[] content);
        Task<byte[]> DownLoadFileAsync(string path);
    }
}
