using CleanArchitectureTemplate.Application.Common.BaseResponse;
using CleanArchitectureTemplate.Application.Common.PaginationQuery;
using CleanArchitectureTemplate.Application.Features.Users.Requests;
using CleanArchitectureTemplate.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CleanArchitectureTemplate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //[Authorize]
        [HttpGet]
        [SwaggerOperation(
             Summary = "Gets users in the database")]

        public async Task<IActionResult> GetPaginatedUsers([FromQuery] PaginationQuery paginationQuery, CancellationToken cancellationToken = default)
        {
            var result = await _userService.GetPagedUsers(paginationQuery, cancellationToken);
            return Ok(BaseResponse.Ok(result));
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Gets users in the database by id")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _userService.GetUserById(id);
            return Ok(BaseResponse.Ok(result));
        }


        [HttpPost]
        [SwaggerOperation(
           Summary = "Creates a new user")]
        public async Task<IActionResult> Post([FromBody] CreateUserRequestDto request, CancellationToken cancellationToken = default)
        {
            var result = await _userService.CreateUser(request, cancellationToken);
            return CreatedAtRoute(new { id = result.Id }, BaseResponse.Created(result));
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
           Summary = "Updates existing user")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateUserRequestDto request, CancellationToken cancellationToken = default)
        {
            var result = await _userService.UpdateUser(id, request, cancellationToken);
            return Ok(BaseResponse.Updated(result));
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
           Summary = "Deletes user")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _userService.DeleteUserById(id);
            return Ok(BaseResponse.Ok(result));
        }
    }
}
