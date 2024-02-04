using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DraxillFanside.Pages
{
    // Manages the logout process for the application.
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
        }

        // Handles the logout action through a POST request.
        public IActionResult OnPost()
        {
            // Clears the user's session, effectively logging them out.
            HttpContext.Session.Clear();

            // After logging out, redirect the user to the homepage.
            // This ensures they are no longer able to access user-specific pages without logging in again.
            return RedirectToPage("/Index");
        }
    }
}
