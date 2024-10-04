using DirectFerriesTechTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace DirectFerriesTechTest.Controllers
{
    [Route("[controller]/{action}")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
     
        [HttpGet]
        public async Task<IActionResult> GetUserData([FromQuery]string fullName, [FromQuery] DateTime dateOfBirth)
        {

            // I could implement RegEx but for the sake of the test, I implemented basic validation
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return BadRequest(new { message = "error", content = "Please enter a valid name" });
            }
            // Check to see if their date of birth is not greater than today
            if (dateOfBirth > DateTime.Now)
            {
                return BadRequest(new { message = "error", content = "Date of Birth is invalid" }); ;
            }
            UserDetails userDetails = new UserDetails();

            try
            {
                userDetails = new UserDetailsService().GetUniqueUserData(fullName, dateOfBirth);
            }
            catch (Exception)
            {
                // Something went wrong in the UserDetailsService - Debug is needed
                return BadRequest(new { message = "error", content = "Service Unavaliable. Please try again soon." }); ;
            }
           

            return userDetails == null ? NoContent() : Ok(new { message = "success", content = userDetails });
           
         
        }

    }
}