using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DraxillFanside.Pages
{
    public class ProfileModel : PageModel
    {
        public User CurrentUser { get; set; } //Property to hold the current users details
        public string UserRole { get; set; } //Property to hold the role of the current user
        public void OnGet() //OnGet method that is called when the page is accesed through a get request
        {
            var username = HttpContext.Session.GetString("username"); //Retrieve the username
            if (string.IsNullOrEmpty(username)) //Checks if the string is null or empty, wich indicates if someone is logged in
            {
                RedirectToPage("/Login"); //If not logged in, redirect the user to the login page
            }
            else
            {
                CurrentUser = UserStore.GetUser(username); //If logged in, display the users username
                UserRole = CurrentUser.Role; //Display the users role
            }
        }
    }
}
