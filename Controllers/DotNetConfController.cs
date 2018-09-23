using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetConf2018.Entities;
using DotNetConf2018.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetConf2018.Controllers
{
    [Route("api/[controller]")]
    public class DotNetConfController : Controller
    {
        private readonly IPerson _service;

        public DotNetConfController(IPerson service)
        {
            _service = service;
        }

        // GET api/DotNetConf
        [HttpGet]
        public async Task<List<Person>> Get()
        {
            return await _service.List();
        }

        // POST api/DotNetConf
        [HttpPost]
        public async Task<IActionResult> Post(string name, string email, IFormFile file)
        {
            var url = await _service.UploadPicture(file);
            await _service.Save(name, email, url);
            return Redirect("../list.html");
        }
    }
}
