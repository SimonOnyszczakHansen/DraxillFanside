using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace DraxillFanside
{
    // Checks if the user is an admin before showing the page.
    public class SomePageModel : PageModel
    {
        public void OnGet()
        {
            // Redirect to login if not logged in.
            var username = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
            {
                Response.Redirect("/Login");
                return;
            }

            // Redirect to home if not an admin.
            var role = UserStore.GetUserRole(username);
            if (role != Roles.Admin)
            {
                Response.Redirect("/Index");
            }
        }
    }
}
