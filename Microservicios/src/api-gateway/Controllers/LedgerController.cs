using ApiGateway.Dtos;
using ApiGateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[ApiController]
[Route("api/ledger")]
public sealed class LedgerController(ILedgerGatewayService ledgerGatewayService) : ControllerBase
{
    [HttpPost("accounts")]
    public async Task<ActionResult<LedgerAccountResponse>> OpenAccount(
        OpenAccountRequest request,
        CancellationToken cancellationToken)
    {
        var account = await ledgerGatewayService.OpenAccountAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetAccountById), new { id = account.Id }, account);
    }

    [HttpGet("accounts/{id:guid}")]
    public async Task<ActionResult<LedgerAccountResponse>> GetAccountById(Guid id, CancellationToken cancellationToken)
    {
        var account = await ledgerGatewayService.GetAccountByIdAsync(id, cancellationToken);
        return account is null ? NotFound() : Ok(account);
    }

    [HttpPost("accounts/{id:guid}/deposits")]
    public async Task<ActionResult<LedgerAccountResponse>> Deposit(
        Guid id,
        DepositFundsRequest request,
        CancellationToken cancellationToken)
    {
        var account = await ledgerGatewayService.DepositAsync(id, request, cancellationToken);
        return account is null ? NotFound() : Ok(account);
    }

    [HttpPost("transfers")]
    public async Task<ActionResult<TransferResponse>> Transfer(
        TransferFundsRequest request,
        CancellationToken cancellationToken)
    {
        var result = await ledgerGatewayService.TransferAsync(request, cancellationToken);
        return result.Transfer is null
            ? BadRequest(new { message = result.Error })
            : Ok(result.Transfer);
    }
}
