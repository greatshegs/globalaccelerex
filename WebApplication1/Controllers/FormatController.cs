using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormatController : ControllerBase
    {
        private readonly ILogger<FormatController> _logger;
        private readonly IFormatService _formatService;

        public FormatController(ILogger<FormatController> logger, IFormatService formatService)
        {
            _logger = logger;
            _formatService = formatService;
        }

        [HttpPost]
        public ActionResult<FormatViewModel> Index([FromBody] Root root)
        {
            FormatViewModel result;
            try
            {
                result = _formatService.Convert(root);
                if (result.IsSuccessful)
                {
                    return Ok(result);
                }
                return ValidationProblem("Data not consistent");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                result = new FormatViewModel { ErrorMessage = "Could not complete the request" };
            }
            return new ObjectResult(result) { StatusCode = 500 };
        }
    }
}
