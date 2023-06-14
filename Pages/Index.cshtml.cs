using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RemoteTV.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    public void OnGet()
    {
        
    }
    public void OnPost()
    {
        ProgramSettings.StopMedia();
    }
    [HttpPost]
    public ActionResult StopMedia() 
    {
        ProgramSettings.StopMedia();
        return Page();
    }
}
