namespace Common.Application.Contracts
{
    public interface IFileService
    {
        string Upload(FileUploadDto fileUploadDto);
    }
}
