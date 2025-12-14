using Microsoft.AspNetCore.Mvc;

namespace ErrorHandlingProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorHandlingController : ControllerBase
    {
        [HttpGet("HelloWorld")]
        public ActionResult Index()
        {
            return Ok("helloWorld");
        }


        [HttpGet("Division")]   
        public IActionResult GetDivisionResult(int val1, int val2)
        {
            try
            {
                var result = val1 / val2;
                return Ok("Here's the result" + result);
            }
            catch (DivideByZeroException)
            {
                return BadRequest("Division by zero is not allowed.");
            }
        }
    }
}
