$loadEnvPath = Join-Path $PSScriptRoot 'loadEnv.ps1'
if (-Not (Test-Path -Path $loadEnvPath)) {
    $loadEnvPath = Join-Path $PSScriptRoot '..\loadEnv.ps1'
}
. ($loadEnvPath)
$TestRecordingFile = Join-Path $PSScriptRoot 'Remove-AzContainerGroupSpotPriority.Recording.json'
$currentPath = $PSScriptRoot
while(-not $mockingPath) {
    $mockingPath = Get-ChildItem -Path $currentPath -Recurse -Include 'HttpPipelineMocking.ps1' -File
    $currentPath = Split-Path -Path $currentPath -Parent
}
. ($mockingPath | Select-Object -First 1).FullName

Describe 'Remove-AzContainerGroupSpotPriority' {
    It 'Delete' {
        Remove-AzContainerGroup -Name "$($env.spotContainerGroupName)-remove1" -ResourceGroupName $env.resourceGroupName
    }

    It 'DeleteViaIdentity' {
        $remove = Get-AzContainerGroup -ResourceGroupName $env.resourceGroupName -Name "$($env.spotContainerGroupName)-remove2"
        Remove-AzContainerGroup -InputObject $remove
    }
}
