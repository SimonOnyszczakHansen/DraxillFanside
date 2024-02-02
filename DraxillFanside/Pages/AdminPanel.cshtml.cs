using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;

namespace DraxillFanside.Pages
{
    public class AdminPanelModel : PageModel
    {
        public List<User> Users { get; set; } //Property to hold the list of users
        public IActionResult OnGet() //Handles GET requests to the admin page
        {
            Users = UserStore.GetAllUsers(); //Retrieves all the users from the UserStore and assign them to the Users property
            var role = HttpContext.Session.GetString("role"); //Retrieve the users role
            if (role != Roles.Admin) //Checks if the user is an admin
            {
                return RedirectToPage("/Index"); //returns user to the index page if not an admin
            }
            return Page(); //If the user is an admin, return to the page to display admin panel
        }
        public IActionResult OnPostDelete(string username) //Handles the post request for deleting a user
        {
            UserStore.DeleteUser(username); //Call the DeleteUser method to remove a user from the website
            return RedirectToPage(); //Refresh the page
        }
    }
}
