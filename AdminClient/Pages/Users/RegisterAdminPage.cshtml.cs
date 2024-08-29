using AdminClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace AdminClient.Pages.Users
{
    public class RegisterAdminPageModel : PageModel
    {
        [BindProperty]
        public RegisterModel UserData { get; set; }

        public IActionResult OnPost()
        {
            var client = new RestClient("https://localhost:7061/api/identity/");
            var request = new RestRequest("RegisterAdmin", Method.Post);
            var body = new RegisterModel()
            {
                Username = UserData.Username,
                Email = UserData.Email,
                Password = UserData.Password,
                Age = UserData.Age,
                Role = UserData.Role
            };

            request.AddBody(body);

            var responseResult = client.Execute(request);

            if (responseResult.IsSuccessStatusCode)
                return RedirectToPage("/Index");

            ModelState.AddModelError("API", "Request Failed!");
            return Page();
        }
    }
}
