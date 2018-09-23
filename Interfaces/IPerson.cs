using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetConf2018.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetConf2018.Interfaces
{
    public interface IPerson
    {
        Task<string> UploadPicture(IFormFile file);
        Task Save(string name, string email, string pictureUrl);
        Task<List<Person>> List();
    }
}