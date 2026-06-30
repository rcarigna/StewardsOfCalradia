[CmdletBinding()]
param(
  [string]$ManifestPath
)

$ErrorActionPreference = 'Stop'

$scriptRoot = if ($PSScriptRoot) { $PSScriptRoot } else { Split-Path -Parent $MyInvocation.MyCommand.Path }

if (-not $ManifestPath) {
  $ManifestPath = Join-Path $scriptRoot '..\SubModule.xml'
}

$resolvedManifestPath = Resolve-Path -LiteralPath $ManifestPath
[xml]$manifest = Get-Content -LiteralPath $resolvedManifestPath -Raw

function Assert-ManifestValue {
  param(
    [string]$Name,
    [string]$Actual,
    [string]$Expected
  )

  if ($Actual -ne $Expected) {
    throw "$Name expected '$Expected' but found '$Actual'."
  }
}

Assert-ManifestValue -Name 'Module Id' -Actual $manifest.Module.Id.value -Expected 'StewardsOfCalradia'
Assert-ManifestValue -Name 'SubModule DLLName' -Actual $manifest.Module.SubModules.SubModule.DLLName.value -Expected 'StewardsOfCalradia.dll'
Assert-ManifestValue -Name 'SubModule class' -Actual $manifest.Module.SubModules.SubModule.SubModuleClassType.value -Expected 'StewardsOfCalradia.SubModule'

$requiredModules = @('Native', 'SandBoxCore', 'Sandbox')
$declaredModules = @($manifest.Module.DependedModules.DependedModule | ForEach-Object { $_.Id })

foreach ($requiredModule in $requiredModules) {
  if ($declaredModules -notcontains $requiredModule) {
    throw "Missing required Bannerlord dependency '$requiredModule'."
  }
}

Write-Host "Manifest validation passed: $($resolvedManifestPath.Path)"
