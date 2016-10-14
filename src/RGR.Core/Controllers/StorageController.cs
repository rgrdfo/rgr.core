using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RGR.Core.Controllers.Storage;

namespace RGR.Core.Controllers
{
    public class StorageController : Controller
    {
        private IHostingEnvironment env;

        public StorageController(IHostingEnvironment env)
        {
            this.env = env;
        }
    }
}
