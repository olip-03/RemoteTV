using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace RemoteTV.Pages
{
    public class InsertFileImage : PageModel
    {
        private readonly ILogger<InsertFileImage> _logger;

        public InsertFileImage(ILogger<InsertFileImage> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}