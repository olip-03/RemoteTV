using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace RemoteTV.Pages.Browse
{
    public class Index : PageModel
    {
        [BindProperty] public string directory {get; set;}
        [BindProperty] public string lastDirectory {get; set;}
        private readonly ILogger<Index> _logger;
        public Index(ILogger<Index> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> OnGet(string dir)
        {
            lastDirectory = directory;
            directory = dir;
            
            if(directory == null)
            {
                directory = ProgramSettings.mediaDirectory;
            }

            if(ProgramSettings.isFile(directory))
            {
                await ProgramSettings.PlayMediaAsync(dir);
                return RedirectToAction("Index");
            }

            return Page();
        }
    }
}