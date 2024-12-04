using Microsoft.AspNetCore.Mvc;
using SwiftSend.app.Abstracts.Services;

namespace SwiftSend.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolProjectController : ControllerBase
    {
        private readonly IExternalServices _externalServices;

        public SchoolProjectController(IExternalServices externalServicesRepository)
        {
            _externalServices = externalServicesRepository;
        }

        [HttpPost(nameof(CreateStudent))]
        public async Task<IActionResult> CreateStudent(string uri,
            studentDto request)
        {
            var result = await _externalServices.Post(uri, request, "application/json");
            if (result.IsSuccessStatusCode)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }

    //Just for testing
    public class studentDto
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? DepartmentID { get; set; }
    }
}
