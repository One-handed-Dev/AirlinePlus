using Microsoft.AspNetCore.Http;

namespace Common.Application.Contracts
{
    public sealed record FileUploadDto(IFormFile File, string Path);
}
