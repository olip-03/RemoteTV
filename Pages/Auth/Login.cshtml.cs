using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RemoteTV.Pages.Authentication;
[AllowAnonymous]
public class LoginModel : PageModel
{
    private readonly ILogger<LoginModel> _logger;
    public LoginModel(ILogger<LoginModel> logger)
    {
        _logger = logger;
    }
    public async Task<IActionResult> OnPostAsync(string password)
    {
        // Verify the credentials
        // Update to validate against server plz and thank you
        if (password == ProgramSettings.loginPassword)
        {
            // Create the security context
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, "Anon")
            };
            var identity = new ClaimsIdentity(claims, "AuthCookie");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("AuthCookie", principal);
            return RedirectToPage("/Index");
        }

        return Page();
    }
}

