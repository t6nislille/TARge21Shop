﻿using Microsoft.EntityFrameworkCore;
using TARge21Shop.Core.Domain;
using TARge21Shop.Core.Dto;
using TARge21Shop.Core.ServiceInterface;
using TARge21Shop.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TARge21Shop.ApplicationServices.Services
{
    public class FilesServices : IFilesServices
    {
        private readonly TARge21ShopContext _context;
        private readonly IHostingEnvironment _webHost;


        public FilesServices
            (
                TARge21ShopContext context,
                IHostingEnvironment webHost
            )
        {
            _context = context;
            _webHost = webHost;
        }

        public void UploadFilesToDatabase(SpaceshipDto dto, Spaceship domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                foreach (var photo in dto.Files)
                {
                    using (var target = new MemoryStream())
                    {
                        FileToDatabase files = new FileToDatabase()
                        {
                            Id = Guid.NewGuid(),
                            ImageTitle = photo.FileName,
                            SpaceshipId = domain.Id,
                        };

                        photo.CopyTo(target);
                        files.ImageData = target.ToArray();

                        _context.FileToDatabases.Add(files);
                    }
                }
            }
        }

        public async Task<FileToDatabase> RemoveImage(FileToDatabaseDto dto)
        {
            var image = await _context.FileToDatabases
                .Where(x => x.Id == dto.Id)
                .FirstOrDefaultAsync();

            _context.FileToDatabases.Remove(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public async Task<List<FileToDatabase>> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos)
        {
            foreach (var dto in dtos)
            {
                var image = await _context.FileToDatabases
                    .Where(x => x.Id == dto.Id)
                    .FirstOrDefaultAsync();

                _context.FileToDatabases.Remove(image);
                await _context.SaveChangesAsync();
            }

            return null;
        }

        public void FilesToApi(RealEstateDto dto, RealEstate realEstate)
        {
            //string uniqueFilename = null;

            if (dto.Files != null && dto.Files.Count >0)
            {
                if (!Directory.Exists(_webHost.WebRootPath + "\\multipleFileUpload\\"))
                {
                    Directory.CreateDirectory(_webHost.WebRootPath + "\\multipleFileUpload\\");
                }

                foreach (var image in dto.Files)
                {
                    string uploadsFolder = Path.Combine(_webHost.WebRootPath, "multipleFileUpload");
                    string uniqueFilename = Guid.NewGuid().ToString() + "_" + image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFilename);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);

                        FileToApi path = new FileToApi
                        {
                            Id = Guid.NewGuid(),
                            ExistingFilePath = filePath,
                            RealEstateId = realEstate.Id,
                        };

                        _context.FileToApis.AddAsync(path);
                    }
                }
            }
        }
    }
        

}