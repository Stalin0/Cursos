using ApiGateway.Dtos;
using ApiGateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[ApiController]
[Route("api/user-contact")]
public sealed class UserContactController(IUserContactAggregatorService aggregatorService) : ControllerBase
{
    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<UserContactResponse>> GetUserWithContacts(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var response = await aggregatorService.GetUserWithContactsAsync(userId, cancellationToken);
        return response is null ? NotFound() : Ok(response);
    }
}
