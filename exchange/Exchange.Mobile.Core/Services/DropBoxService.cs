using Dropbox.Api;
using Dropbox.Api.Files;
using Exchange.Mobile.Core.Constants;
using Exchange.Mobile.Core.Services.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services
{
    public class DropBoxService : IDropBoxService
    {
        public async Task<Stream> DownLoadFileAsync(string path)
        {
            var stream = new MemoryStream();
            using (var dropBox = new DropboxClient(Constant.DropBoxConstant.ACCESS_TOKEN))
            using (var response = await dropBox.Files.DownloadAsync(path))
            {
                var t = await response.GetContentAsStreamAsync();
                await t.CopyToAsync(stream);
            }
            return stream;
        }

        public async Task<string> UploadFile(string folder, string fileName, byte[] content)
        {
            FileMetadata updated = default;
            using (var dropBox = new DropboxClient(Constant.DropBoxConstant.ACCESS_TOKEN))
            using (var stream = new MemoryStream(content))
            {
                updated = await dropBox.Files.UploadAsync($"/{folder}/{fileName}", WriteMode.Overwrite.Instance, body: stream);
            }
            return $"{Constant.DropBoxConstant.BASE_PATH}{updated.PathDisplay}";
        }
    }
}
