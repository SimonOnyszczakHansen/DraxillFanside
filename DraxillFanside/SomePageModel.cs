using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
namespace DraxillFanside
{
    public class SomePageModel : PageModel
    {
        public void OnGet()
        {
            var username = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
            {
                Response.Redirect("/Login");
                return;
            }

            var role = UserStore.GetUserRole(username);

            if (role != Roles.Admin)
            {
                Response.Redirect("/Index"); 
            }
        }
    }
}
