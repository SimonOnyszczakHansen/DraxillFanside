using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DraxillFanside.Pages
{
    // Manages the administrative panel functionality, including user role management and user deletion.
    public class AdminPanelModel : PageModel
    {
        // Holds the list of users for display in the admin panel.
        public List<User> Users { get; set; }

        // Retrieves all users and verifies admin role on page load.
        public IActionResult OnGet()
        {
            Users = UserStore.GetAllUsers(); // Populate the Users property with all users from the store.
            var role = HttpContext.Session.GetString("role");

            // Redirect non-admin users to the homepage to enforce admin-only access.
            if (role != Roles.Admin)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        // Handles user deletion requests.
        public IActionResult OnPostDelete(string username)
        {
            UserStore.DeleteUser(username); // Remove the specified user from the store.
            return RedirectToPage(); // Refresh the page to reflect the deletion.
        }

        // Handles requests to update a user's role.
        public IActionResult OnPostUpdateRole(string username, string newRole)
        {
            // Ensure only admins can change roles.
            if (HttpContext.Session.GetString("role") != Roles.Admin)
            {
                return Unauthorized();
            }

            UserStore.UpdateUserRole(username, newRole); // Update the role for the specified user.
            return RedirectToPage(); // Refresh the page to reflect the role change.
        }
    }
}
