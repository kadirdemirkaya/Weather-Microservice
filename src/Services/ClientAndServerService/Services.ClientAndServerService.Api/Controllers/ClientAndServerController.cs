using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.ClientAndServerService.Dtos;
using Services.ClientAndServerService.Features.User.Commands.UserLogin;
using Services.ClientAndServerService.Features.User.Commands.UserLogout;
using Services.ClientAndServerService.Features.User.Commands.UserRegister;
using Services.ClientAndServerService.Features.Weather.Queries.AirPollutionWeather;
using Services.ClientAndServerService.Features.Weather.Queries.CurrentWeather;
using Services.ClientAndServerService.Features.Weather.Queries.DailyWeather;

namespace Services.ClientAndServerService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientAndServerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientAndServerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("ClientAndServer/Server/User/User-Register")]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterModelDto userRegisterModelDto)
        {
            UserRegisterCommandRequest userRegisterCommandRequest = new(userRegisterModelDto);
            UserRegisterCommandResponse userRegisterCommandResponse = await _mediator.Send(userRegisterCommandRequest);
            return Ok(userRegisterCommandResponse.response);
        }

        [HttpPost]
        [Route("ClientAndServer/Server/User/User-Login")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginModelDto userLoginModelDto)
        {
            UserLoginCommandRequest userLoginCommandRequest = new(userLoginModelDto);
            UserLoginCommandResponse userLoginCommandResponse = await _mediator.Send(userLoginCommandRequest);
            return Ok(userLoginCommandResponse.UserLoginResponseModel);
        }

        [HttpPost]
        [Route("ClientAndServer/Server/User/User-Logout")]
        public async Task<IActionResult> UserLogout()
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length).Trim();
                UserLogoutCommandRequest userLogoutCommandRequest = new(token);
                UserLogoutCommandResponse userLogoutCommandResponse = await _mediator.Send(userLogoutCommandRequest);
                return Ok(userLogoutCommandResponse.response);
            }
            return BadRequest(false);
        }


        [HttpGet]
        [Route("ClientAndServer/Server/Weather/Current-Weather")]
        public async Task<IActionResult> CurrentWeather([FromQuery] string city)
        {
            CurrentWeatherQueryRequest currentWeatherQueryRequest = new(city);
            CurrentWeatherQueryResponse currentWeatherQueryResponse = await _mediator.Send(currentWeatherQueryRequest);
            return Ok(currentWeatherQueryResponse.CurrentWeatherModel);
        }

        [HttpGet]
        [Route("ClientAndServer/Server/Weather/Daily-Weather")]
        public async Task<IActionResult> DailyWeather([FromQuery] string city)
        {
            DailyWeatherQueryRequest dailyWeatherQueryRequest = new(city);
            DailyWeatherQueryResponse dailyWeatherQueryResponse = await _mediator.Send(dailyWeatherQueryRequest);
            return Ok(dailyWeatherQueryResponse.DailyWeatherDataModel);
        }

        [HttpGet]
        [Route("ClientAndServer/Server/Weather/Air-Pollution-Weather")]
        public async Task<IActionResult> AirPollutionWeather([FromQuery] string city)
        {
            AirPollutionWeatherQueryRequest airPollutionWeatherQueryRequest = new(city);
            AirPollutionWeatherQueryResponse airPollutionWeatherQueryResponse = await _mediator.Send(airPollutionWeatherQueryRequest);
            return Ok(airPollutionWeatherQueryResponse.AirPollutionModel);
        }
    }
}
