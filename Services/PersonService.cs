using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetConf2018.Data;
using DotNetConf2018.Entities;
using DotNetConf2018.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;

namespace DotNetConf2018.Services
{
    public class PersonService : IPerson
    {
        private readonly string _blobConn;
        private readonly DotNetConfContext _context;

        public PersonService(IConfiguration configuration, DotNetConfContext context)
        {
            _blobConn = configuration.GetConnectionString("BLOB_CONN");
            _context = context;
        }

        public async Task<string> UploadPicture(IFormFile file)
        {
            var storageAccount = CloudStorageAccount.Parse(_blobConn);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("images");
            await container.CreateIfNotExistsAsync();
            var blockBlob = container.GetBlockBlobReference(file.FileName);

            using (var fileStream = file.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(fileStream);
            }

            return blockBlob.Uri.ToString().Replace("/images/", "/images-sm/");
        }

        public async Task Save(string name, string email, string pictureUrl)
        {
            var person = new Person { Name = name, Email = email, PictureUrl = pictureUrl };            
            await _context.Persons.AddAsync(person);
            _context.SaveChanges();
        }

        public async Task<List<Person>> List()
        {
            return await _context.Persons.OrderBy(p => p.Name).ToListAsync();
        }
    }
}