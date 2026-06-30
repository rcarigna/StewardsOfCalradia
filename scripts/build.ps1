[CmdletBinding()]
param(
  [ValidateSet('Debug', 'Release')]
  [string]$Configuration = 'Debug',

  [switch]$SkipManifestValidation
)

$ErrorActionPreference = 'Stop'

$scriptRoot = if ($PSScriptRoot) { $PSScriptRoot } else { Split-Path -Parent $MyInvocation.MyCommand.Path }
$repoRoot = (Resolve-Path (Join-Path $scriptRoot '..')).Path
$projectPath = Join-Path $repoRoot 'src\StewardsOfCalradia\StewardsOfCalradia.csproj'
$bannerlordBinPath = Join-Path $repoRoot '..\..\bin\Win64_Shipping_Client'
$moduleOutputPath = Join-Path $repoRoot 'bin\Win64_Shipping_Client'

if (-not (Test-Path -LiteralPath $projectPath)) {
  throw "Project file not found: $projectPath"
}

if (-not $SkipManifestValidation) {
  & (Join-Path $scriptRoot 'validate-manifest.ps1')
}

$requiredAssemblies = @(
  'TaleWorlds.Core.dll',
  'TaleWorlds.Library.dll',
  'TaleWorlds.MountAndBlade.dll',
  'TaleWorlds.CampaignSystem.dll',
  'TaleWorlds.Localization.dll'
)

foreach ($assembly in $requiredAssemblies) {
  $assemblyPath = Join-Path $bannerlordBinPath $assembly
  if (-not (Test-Path -LiteralPath $assemblyPath)) {
    throw "Required Bannerlord assembly not found: $assemblyPath"
  }
}

$dotnet = Get-Command dotnet -ErrorAction SilentlyContinue
if (-not $dotnet) {
  throw "dotnet was not found on PATH. Install the .NET SDK or build with Visual Studio/MSBuild."
}

dotnet build $projectPath --configuration $Configuration

Write-Host "Build output: $moduleOutputPath"
