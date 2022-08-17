using System;
using System.IO;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Hosting;

namespace Host.Classes
{
    public sealed class FileService : IFileService
    {
        #region Init

        private readonly IWebHostEnvironment webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment) => this.webHostEnvironment = webHostEnvironment;
        #endregion

        public string Upload(FileUploadDto fileUploadDto)
        {
            if (fileUploadDto.File is null) return string.Empty;

            var directoryPath = $@"{webHostEnvironment.WebRootPath}/UploadedFiles/{fileUploadDto.Path}";

            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

            var fileName = $"{DateTime.Now.ToFileTime()}{Path.GetExtension(fileUploadDto.File.FileName)}";
            var filePath = $@"{directoryPath}/{fileName}";
            using var output = File.Create(filePath);
            fileUploadDto.File.CopyTo(output);

            return $@"{fileUploadDto.Path}/{fileName}";
        }
    }
}
