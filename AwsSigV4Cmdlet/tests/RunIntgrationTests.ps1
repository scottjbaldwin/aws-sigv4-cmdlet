#Requires -Version 7.0
[CmdletBinding()]
param (
    [Parameter()]
    [string]
    $ModulePath=(Join-Path $PSScriptRoot "../bin/debug/netstandard2.0/AwsSigV4.dll"),

    [Parameter()]
    [string]
    $AccessKey=$env:AccessKey,

    [Parameter()]
    [string]
    $SecretKey=$env:SecretKey,

    [Parameter()]
    [string]
    $BaseUri=$env:BaseUri
)
Write-Host "Executing pester tests for module $ModulePath"

if ([string]::IsNullOrEmpty($AccessKey) -or [string]::IsNullOrEmpty($SecretKey)){
    throw "AccessKey and SecretKey must be provided"
}

$env:AccessKey = $AccessKey
$env:SecretKey = $SecretKey
$env:BaseUri = $BaseUri

Import-Module $ModulePath
Invoke-Pester  -Path $PSScriptRoot -Output Detailed @Args