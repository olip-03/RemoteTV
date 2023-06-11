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
            settings.TXRFAmp = ProgramSettings.TXRFAmp;
            settings.TXGain = ProgramSettings.TXGain;
        }
    }
    public void OnPost()
    {
        if(settings != null)
        {
            ProgramSettings.loginPassword = settings.password;
            ProgramSettings.mediaDirectory = settings.mediaDirectory;
            ProgramSettings.broadcastFrequency = settings.broadcastFrequency;
            ProgramSettings.TXRFAmp = settings.TXRFAmp;
            ProgramSettings.TXGain = settings.TXGain;
        }
    }
}
public class SettingsObject 
{
    public string password {get; set;}
    public string mediaDirectory {get; set;}
    public float broadcastFrequency { get; set;}
    public bool TXRFAmp { get; set; } = false;
    public int TXGain { get; set; } = 10;
}

