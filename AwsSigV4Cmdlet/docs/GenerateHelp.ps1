<#
.SYNOPSIS

Generates the module help file

.DESCRIPTION

Generates the maml based help file based on the PlatyPS markdown files stored in the current folder

.PARAMETER OutputPath

The output path of the resultant maml file
#>
[CmdletBinding()]
param (
    [Parameter()]
    [String]
    $OutputPath
)

Import-Module PlatyPS -Force

New-ExternalHelp $PSScriptRoot -OutputPath $OutputPath -Force
