$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot

function Start-ServiceWindow {
    param(
        [string]$Title,
        [string]$ProjectPath,
        [string]$LaunchProfile
    )

    $command = "Set-Location '$root'; dotnet run --project `"$ProjectPath`" --launch-profile $LaunchProfile"
    Start-Process powershell -ArgumentList "-NoExit", "-Command", $command -WorkingDirectory $root -WindowStyle Normal
}

Write-Host "Opening service consoles..." -ForegroundColor Cyan
Write-Host "Prerequisites:" -ForegroundColor Yellow
Write-Host "  - RabbitMQ running on localhost:5672" -ForegroundColor Yellow
Write-Host "  - PostgreSQL running on localhost:5432" -ForegroundColor Yellow
Write-Host "  - Databases ready: users_db and contacts_db" -ForegroundColor Yellow
Write-Host "  - blockchain_db will be created automatically by ms-blockchain" -ForegroundColor Yellow

Start-ServiceWindow -Title "ms-user" -ProjectPath "src/ms-user/MsUser.csproj" -LaunchProfile "http"
Start-ServiceWindow -Title "ms-contact" -ProjectPath "src/ms-contact/MsContact.csproj" -LaunchProfile "http"
Start-ServiceWindow -Title "ms-audit" -ProjectPath "src/ms-audit/MsAudit.csproj" -LaunchProfile "AuditService"
Start-ServiceWindow -Title "ms-blockchain" -ProjectPath "src/ms-blockchain/MsBlockchain.csproj" -LaunchProfile "BlockchainService"
Start-ServiceWindow -Title "api-gateway" -ProjectPath "src/api-gateway/ApiGateway.csproj" -LaunchProfile "http"

Write-Host "Done. Swagger: http://localhost:7000/swagger" -ForegroundColor Green
Write-Host "RabbitMQ UI: http://localhost:15672" -ForegroundColor Green
