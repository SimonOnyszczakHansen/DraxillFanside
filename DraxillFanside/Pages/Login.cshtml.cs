using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace DraxillFanside.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; } //Username entered by the user

        [BindProperty]
        public string Password { get; set; } //Password entered by the user

        public void OnGet()
        {
        }

        public IActionResult OnPost() //handle POST request when the user attempts to log in
        {
            if (!ModelState.IsValid) //Checks if the model state is valid 
            {
                return Page(); //Return the same page to display validation errors
            }

            var user = UserStore.GetUser(Username); //Retrieves the user by username 
            if (user != null && user.Password == Password) //Cheks if the user exists and the passwords match
            {
                HttpContext.Session.SetString("username", Username); //Sets the username to signify that the user logged in
                if (!string.IsNullOrEmpty(user.Role)) //Checks if the user has a role
                {
                    HttpContext.Session.SetString("role", user.Role); //If yes, we store it in the session
                }
                else
                {
                    HttpContext.Session.SetString("role", "User"); //if not, we give the user a default role wich is "User"
                }
                return RedirectToPage("/Index"); //redirects the user to the index page when logged in sucessfully
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password"); //If username or password is incorrect, display error
            return Page();
        }
    }
}
