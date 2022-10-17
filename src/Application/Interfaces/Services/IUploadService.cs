using SSDB.Application.Requests;

namespace SSDB.Application.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}