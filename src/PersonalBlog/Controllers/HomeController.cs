using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PersonalBlog.Filters;
using PersonalBlog.Interfaces;
using PersonalBlog.Models;

namespace PersonalBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger logger;
        private readonly Func<VersionEnum, IDataService> dataServiceAccessor;
        private readonly VersionEnum version;

        public HomeController(
            ILogger<HomeController> logger,
            Func<VersionEnum, IDataService> dataServiceAccessor,
            IConfiguration configuration)
        {
            this.logger = logger;
            this.dataServiceAccessor = dataServiceAccessor;
            this.version = configuration.GetSection("DataService").GetValue<VersionEnum>("Version");
        }

        public IActionResult Index()
        {
            var dataService = this.dataServiceAccessor(version);

            var posts = dataService.GetAll();

            return this.View(posts);
        }

        [Route("Post")]
        [HttpGet]
        [ServiceFilter(typeof(ProtectorAttribute))]
        public IActionResult CreatePost(Post post)
        {
            return this.View(post);
        }

        [HttpPost]
        [ServiceFilter(typeof(ProtectorAttribute))]
        public IActionResult Post(Post post)
        {
            if (!ModelState.IsValid)
            {
                this.logger.LogWarning("Invalid ModelState", post);

                ModelState.AddModelError("Validation", "Please provide all values");
                return this.View(post);
            }

            var dataService = this.dataServiceAccessor(version);

            dataService.Create(post);

            return this.RedirectToAction("Index");
        }

        public IActionResult GetAll()
        {
            var dataService = this.dataServiceAccessor(version);

            var posts = dataService.GetAll();

            return this.Ok(posts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
