using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RemoteTV.Pages;

public class SettingsModel : PageModel
{
    [BindProperty] public SettingsObject settings { get; set;} 
    private readonly ILogger<SettingsModel> _logger;

    public SettingsModel(ILogger<SettingsModel> logger)
    {
        _logger = logger;
    }
    public void OnGet()
    {
        if(settings != null)
        {
            settings.password = ProgramSettings.loginPassword;
            settings.mediaDirectory = ProgramSettings.mediaDirectory;
            settings.broadcastFrequency = ProgramSettings.broadcastFrequency;
        }
    }
    public void OnPost()
    {
        if(settings != null)
        {
            ProgramSettings.loginPassword = settings.password;
            ProgramSettings.mediaDirectory = settings.mediaDirectory;
            ProgramSettings.broadcastFrequency = settings.broadcastFrequency;
        }
    }
}
public class SettingsObject 
{
    public string password {get; set;}
    public string mediaDirectory {get; set;}
    public float broadcastFrequency { get; set;}
}

