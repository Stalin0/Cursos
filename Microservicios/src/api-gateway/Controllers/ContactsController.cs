using ApiGateway.Dtos;
using ApiGateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[ApiController]
[Route("api/contacts")]
public sealed class ContactsController(IContactGatewayService contactGatewayService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ContactResponse>> Create(
        CreateContactRequest request,
        CancellationToken cancellationToken)
    {
        var contact = await contactGatewayService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetByUserId), new { userId = contact.UserId }, contact);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<IReadOnlyList<ContactResponse>>> GetByUserId(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var contacts = await contactGatewayService.GetByUserIdAsync(userId, cancellationToken);
        return Ok(contacts);
    }
}
