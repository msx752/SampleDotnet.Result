using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleDotnet.Result.Tests.MockApiServer.Models;

namespace SampleDotnet.Result.Tests.MockApiServer.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("RequestOkResponseModel")]
        public IActionResult RequestOkResponseModel()
        {
            List<TestDatumDto> reponseObj = new() {
                new TestDatumDto() { Value1 = "Value1String1", Value2 = 123 }
                , new TestDatumDto() { Value1 = "Value1String2", Value2 = 456 }
                ,
            };

            return new OkResponse(reponseObj);
        }
    }
}