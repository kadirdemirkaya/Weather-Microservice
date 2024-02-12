using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.UserInfoService.Dtos;
using Services.UserInfoService.Features.Commands.UserLogin;
using Services.UserInfoService.Features.Commands.UserLogout;
using Services.UserInfoService.Features.Commands.UserRegister;
using Services.UserInfoService.Features.Queries.RefreshToken;

namespace Services.UserInfoService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public UserInfoController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        [Route("Login/User-Login")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginDto userLoginDto)
        {
            UserLoginCommandRequest userLoginCommandRequest = new(userLoginDto);
            UserLoginCommandResponse? userLoginCommandResponse = await _mediatr.Send(userLoginCommandRequest);
            return Ok(userLoginCommandResponse);
        }

        [HttpPost]
        [Route("Logout/User-Logout")]
        public async Task<IActionResult> UserLogout()
        {
            UserLogoutCommandRequest userLogoutCommandRequest = new("");
            UserLogoutCommandResponse userLogoutCommandResponse = await _mediatr.Send(userLogoutCommandRequest);
            return userLogoutCommandResponse.response is true ? Ok(true) : BadRequest(false);
        }

        [HttpPost]
        [Route("Register/User-Register")]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterDto userRegisterDto)
        {
            UserRegisterCommandRequest userRegisterCommandRequest = new(userRegisterDto);
            UserRegisterCommandResponse? userRegisterCommandResponse = await _mediatr.Send(userRegisterCommandRequest);
            return userRegisterCommandResponse.response is true ? Ok(true) : BadRequest(false);
        }

        [HttpGet]
        [Route("User/Token/Refresh-Token")]
        public async Task<IActionResult> RefreshToken([FromQuery] string token)
        {
            RefreshTokenQueryRequest refreshTokenQuery = new(token);
            RefreshTokenQueryResponse response = await _mediatr.Send(refreshTokenQuery);
            return response.newToken is not null ? Ok(response) : BadRequest(response);
        }
    }
}
