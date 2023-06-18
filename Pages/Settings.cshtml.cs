using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RemoteTV.Models;

namespace RemoteTV.Pages;

public class SettingsModel : PageModel
{
    [BindProperty] public string password {get; set;}
    [BindProperty] public string mediaDirectory {get; set;}
    [BindProperty] public float broadcastFrequency { get; set;}
    [BindProperty] public bool TXRFAmp { get; set; } = false;
    [BindProperty] public int TXGain { get; set; } = 10;
    [BindProperty] public bool hideNonPlayableFiles { get; set; } = true;
    [BindProperty] public List<AlarmModel> alarms { get; set; } = new();

    // Video Mode Settings
    [BindProperty] public string videoMode { get; set; }= "PAL";
    [BindProperty] public string[] videoModes { get; set; } = new[] { "PAL", "NTSC", "SECAM", "BW", "MAC" };
    [BindProperty] public int sampleRateMHz { get; set; } = 16;
    [BindProperty] public int pixelRateMHz { get; set; } = 16;
    [BindProperty] public int FMdeviation { get; set; } = 16;
    [BindProperty] public string OverrideMacChannelID { get; set; } = "";
    [BindProperty] public bool fmVideoPreEmphesisFilter { get; set; } = false;
    [BindProperty] public bool audioEnbled { get; set; } = true;
    [BindProperty] public bool nicamSterio { get; set; } = true;
    [BindProperty] public bool disableColour { get; set; } = false;
    [BindProperty] public bool invertVideoPolarity { get; set; } = false;
    // Frequency And TX Settings
    [BindProperty] public string outputDevice { get; set; } = "hackrf";
    [BindProperty] public string[] outputDevices { get; set; } = new[] { "hackrf", "soapySDR", "FL2000", "File"};
    [BindProperty] public string outputDeviceNumber { get; set; } = "";
    [BindProperty] public double frequency { get; set; } = 46.25;
    [BindProperty] public int txGainDB { get; set; } = 0;
    [BindProperty] public bool txRfAmp { get; set; } = false;
    public void OnGet()
    {
        password = ProgramSettings.loginPassword;
        mediaDirectory = ProgramSettings.mediaDirectory;
        broadcastFrequency = ProgramSettings.broadcastFrequency;
        TXRFAmp = ProgramSettings.TXRFAmp;
        TXGain = ProgramSettings.TXGain;
    }
    public void OnPost()
    {
        ProgramSettings.loginPassword = password;
        ProgramSettings.mediaDirectory = mediaDirectory;
        ProgramSettings.broadcastFrequency = broadcastFrequency;
        ProgramSettings.TXRFAmp = TXRFAmp;
        ProgramSettings.TXGain = TXGain;
    }
}

