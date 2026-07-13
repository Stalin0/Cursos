using System.Text.Json;
using BlockchainService.Entities;
using BlockchainService.Providers;
using BlockchainService.Repositories;
using Contracts.Contacts;
using Contracts.Ledger;
using Contracts.Users;
using Microsoft.Extensions.Logging;

namespace BlockchainService.Services;

public sealed class BlockchainLedgerService(
    IBlockchainBlockRepository blockchainBlockRepository,
    IBlockHashProvider blockHashProvider,
    ILogger<BlockchainLedgerService> logger) : IBlockchainLedgerService
{
    private const string GenesisHash = "GENESIS";

    public Task<BlockchainBlock> RecordUserCreatedAsync(UserCreatedEvent message, CancellationToken cancellationToken)
    {
        return AppendBlockAsync("UserCreated", message.UserId, message.CreatedAt, message, cancellationToken);
    }

    public Task<BlockchainBlock> RecordUserUpdatedAsync(UserUpdatedEvent message, CancellationToken cancellationToken)
    {
        return AppendBlockAsync("UserUpdated", message.UserId, message.UpdatedAt, message, cancellationToken);
    }

    public Task<BlockchainBlock> RecordContactCreatedAsync(ContactCreatedEvent message, CancellationToken cancellationToken)
    {
        return AppendBlockAsync("ContactCreated", message.ContactId, message.CreatedAt, message, cancellationToken);
    }

    public Task<BlockchainBlock> RecordAccountOpenedAsync(AccountOpenedEvent message, CancellationToken cancellationToken)
    {
        return AppendBlockAsync("AccountOpened", message.AccountId, message.CreatedAt, message, cancellationToken);
    }

    public Task<BlockchainBlock> RecordFundsDepositedAsync(FundsDepositedEvent message, CancellationToken cancellationToken)
    {
        return AppendBlockAsync("FundsDeposited", message.AccountId, message.OccurredAt, message, cancellationToken);
    }

    public Task<BlockchainBlock> RecordFundsTransferredAsync(FundsTransferredEvent message, CancellationToken cancellationToken)
    {
        return AppendBlockAsync("FundsTransferred", message.FromAccountId, message.OccurredAt, message, cancellationToken);
    }

    private async Task<BlockchainBlock> AppendBlockAsync(
        string eventType,
        Guid aggregateId,
        DateTime sourceCreatedAt,
        object payload,
        CancellationToken cancellationToken)
    {
        var latestBlock = await blockchainBlockRepository.GetLatestAsync(cancellationToken);
        var previousBlockHash = latestBlock?.BlockHash ?? GenesisHash;
        var nextBlockNumber = latestBlock?.BlockNumber + 1 ?? 1;

        var payloadJson = JsonSerializer.Serialize(payload);
        var payloadHash = blockHashProvider.ComputeHash(payloadJson);
        var recordedAt = DateTime.UtcNow;
        var blockHash = blockHashProvider.ComputeHash(
            $"{nextBlockNumber}|{previousBlockHash}|{eventType}|{aggregateId}|{payloadHash}|{recordedAt:O}");

        var block = new BlockchainBlock
        {
            Id = Guid.NewGuid(),
            BlockNumber = nextBlockNumber,
            EventType = eventType,
            AggregateId = aggregateId,
            PayloadJson = payloadJson,
            PayloadHash = payloadHash,
            PreviousBlockHash = previousBlockHash,
            BlockHash = blockHash,
            SourceCreatedAt = sourceCreatedAt,
            RecordedAt = recordedAt
        };

        await blockchainBlockRepository.AddAsync(block, cancellationToken);
        await blockchainBlockRepository.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "BLOCKCHAIN Block #{BlockNumber} stored for {EventType}. AggregateId: {AggregateId}, BlockHash: {BlockHash}",
            block.BlockNumber,
            block.EventType,
            block.AggregateId,
            block.BlockHash);

        return block;
    }
}
