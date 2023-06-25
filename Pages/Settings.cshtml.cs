using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RemoteTV.Models;

namespace RemoteTV.Pages;

public class SettingsModel : PageModel
{
    [BindProperty] public string Password {get; set;}
    [BindProperty] public string MediaDirectory {get; set;}
    [BindProperty] public float BroadcastFrequency { get; set;}
    [BindProperty] public bool TXRFAmp { get; set; } = false;
    [BindProperty] public int TXGain { get; set; } = 10;
    [BindProperty] public bool HideNonPlayableFiles { get; set; } = true;
    [BindProperty] public List<AlarmModel> Alarms { get; set; } = new();

    // Video Mode Settings
    [BindProperty] public string VideoMode { get; set; }= "PAL";
    [BindProperty] public string[] VideoModes { get; set; } = new[] { "PAL", "NTSC", "SECAM", "BW", "MAC" };
    [BindProperty] public Dictionary<string, string> VideoFormats { get; set; } = new Dictionary<string, string>
    {
        { "i", "PAL colour, 25 fps, 625 lines, AM (complex), 6.0 MHz FM audio" },
        { "b", "PAL colour, 25 fps, 625 lines, AM (complex), 5.5 MHz FM audio" },
        { "g", "PAL colour, 25 fps, 625 lines, AM (complex), 5.5 MHz FM audio" },
        { "pal-d", "PAL colour, 25 fps, 625 lines, AM (complex), 6.5 MHz FM audio" },
        { "pal-k", "PAL colour, 25 fps, 625 lines, AM (complex), 6.5 MHz FM audio" },
        { "pal-fm", "PAL colour, 25 fps, 625 lines, FM (complex), 6.5 MHz FM audio" },
        { "pal", "PAL colour, 25 fps, 625 lines, unmodulated (real)" },
        { "pal-m", "PAL colour, 30/1.001 fps, 525 lines, AM (complex), 4.5 MHz FM audio" },
        { "525pal", "PAL colour, 30/1.001 fps, 525 lines, unmodulated (real)" },
        { "m", "NTSC colour, 30/1.001 fps, 525 lines, AM (complex), 4.5 MHz FM audio" },
        { "ntsc-fm", "NTSC colour, 30/1.001 fps, 525 lines, FM (complex), 6.5 MHz FM audio" },
        { "ntsc-bs", "NTSC colour, 30/1.001 fps, 525 lines, FM (complex), BS digital audio" },
        { "ntsc", "NTSC colour, 30/1.001 fps, 525 lines, unmodulated (real)" },
        { "l", "SECAM colour, 25 fps, 625 lines, AM (complex), 6.5 MHz AM audio" },
        { "d", "SECAM colour, 25 fps, 625 lines, AM (complex), 6.5 MHz FM audio" },
        { "k", "SECAM colour, 25 fps, 625 lines, AM (complex), 6.5 MHz FM audio" },
        { "secam-i", "SECAM colour, 25 fps, 625 lines, AM (complex), 6.0 MHz FM audio" },
        { "secam-fm", "SECAM colour, 25 fps, 625 lines, FM (complex), 6.5 MHz FM audio" },
        { "secam", "SECAM colour, 25 fps, 625 lines, unmodulated (real)" },
        { "d2mac-fm", "D2-MAC, 25 fps, 625 lines, FM (complex)" },
        { "d2mac-am", "D2-MAC, 25 fps, 625 lines, AM (complex)" },
        { "d2mac", "D2-MAC, 25 fps, 625 lines, unmodulated (real)" },
        { "dmac-fm", "D-MAC, 25 fps, 625 lines, FM (complex)" },
        { "dmac-am", "D-MAC, 25 fps, 625 lines, AM (complex)" },
        { "dmac", "D-MAC, 25 fps, 625 lines, unmodulated (real)" },
        { "e", "No colour, 25 fps, 819 lines, AM (complex)" },
        { "819", "No colour, 25 fps, 819 lines, unmodulated (real)" },
        { "a", "No colour, 25 fps, 405 lines, AM (complex)" },
        { "405", "No colour, 25 fps, 405 lines, unmodulated (real)" },
        { "240-am", "No colour, 25 fps, 240 lines, AM (complex)" },
        { "240", "No colour, 25 fps, 240 lines, unmodulated (real)" },
        { "30-am", "No colour, 12.5 fps, 30 lines, AM (complex)" },
        { "30", "No colour, 12.5 fps, 30 lines, unmodulated (real)" },
        { "nbtv-am", "No colour, 12.5 fps, 32 lines, AM (complex)" },
        { "nbtv", "No colour, 12.5 fps, 32 lines, unmodulated (real)" },
        { "apollo-fsc-fm", "Field sequential colour, 30/1.001 fps, 525 lines, FM (complex), 1.25 MHz FM audio" },
        { "apollo-fsc", "Field sequential colour, 30/1.001 fps, 525 lines, unmodulated (real)" },
        { "apollo-fm", "No colour, 10 fps, 320 lines, FM (complex), 1.25 MHz FM audio" },
        { "apollo", "No colour, 10 fps, 320 lines, unmodulated (real)" },
        { "m-cbs405", "Field sequential colour, 72 fps, 405 lines, VSB (complex), 4.5MHz FM audio" },
        { "cbs405", "Field sequential colour, 72 fps, 405 lines, unmodulated (real)" }
    };
    [BindProperty] public string SelectedVideoFormat {get; set;}
    [BindProperty] public List<string> DisplayVideoFormats {get
    { // such a fucking hack
        List<string> returndata = new();
        foreach (var item in VideoFormats)
        {
            returndata.Add(item.Key + " : " + item.Value);
        }
        return returndata;
    } 
    private set{}
    }
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
        Password = ProgramSettings.loginPassword;
        MediaDirectory = ProgramSettings.mediaDirectory;
        BroadcastFrequency = ProgramSettings.broadcastFrequency;
        TXRFAmp = ProgramSettings.TXRFAmp;
        TXGain = ProgramSettings.TXGain;
    }
    public void OnPost()
    {
        ProgramSettings.SelectedVideoFormat = SelectedVideoFormat;
        ProgramSettings.loginPassword = Password;
        ProgramSettings.mediaDirectory = MediaDirectory;
        ProgramSettings.broadcastFrequency = BroadcastFrequency;
        ProgramSettings.TXRFAmp = TXRFAmp;
        ProgramSettings.TXGain = TXGain;
    }
}

