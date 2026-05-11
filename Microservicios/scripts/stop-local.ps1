$ErrorActionPreference = "SilentlyContinue"

Write-Host "Stopping local dotnet processes..." -ForegroundColor Yellow
Get-Process dotnet | Stop-Process -Force
