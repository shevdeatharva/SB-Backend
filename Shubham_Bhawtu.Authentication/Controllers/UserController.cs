using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shubham_Bhawtu.Authentication.Filters;
using Shubham_Bhawtu.Authentication.Models;
using Shubham_Bhawtu.Authentication.Services;
using Shubham_Bhawtu.Authentication.Services.Interface;

namespace Shubham_Bhawtu.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userServices;
        private readonly IJwtAuthentcationManager _jwtAuthentcationManager;
        private readonly ILogger<UserController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(IJwtAuthentcationManager jwtAuthentcationManager, ILogger<UserController> logger,
            IUserService userServices, IHttpContextAccessor httpContextAccessor)
        {
            _jwtAuthentcationManager = jwtAuthentcationManager;
            _logger = logger;
            _userServices = userServices;
            _httpContextAccessor = httpContextAccessor;

        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] LoginInfoModel LoginInfo)
        {
            try
            {
                _logger.LogInformation($"AuthController: Authenticating User Cred");

                var user = await _userServices.GetUserDetails(LoginInfo);
                var token = _jwtAuthentcationManager.Authenticate(LoginInfo, false);
                IDictionary<string, object> resultData = new Dictionary<string, object>
            {
                {"AuthenticationToken", token },
                {"UserName", user.Username }
            };

                object returnObj = new
                {
                    Status = true,
                    Code = 200,
                    Message = "Login Sucessful",
                    Data = resultData
                };
                _logger.LogInformation($"AuthController: Login Sucessfully");
                return Ok(returnObj);

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    Code = 400,
                    Message = ex.Message
                });
            }
        }
        [HttpGet("RefreshToken")]
        public async Task<ActionResult> RefreshToken(string Token, bool SamalFlag)
        {
            var data = _jwtAuthentcationManager.RefreshTokenExpirationTime(Token, SamalFlag);
            return Ok(data);
        }
    }
}
