<#
This script will run on debug.
    It will load in a PowerShell command shell and import the module developed in the project. To end debug, exit this shell.
#>

# Write a reminder on how to end debugging.
$message = "| Exit this shell to end the debug session! |"
$line = "-" * $message.Length
$color = "Cyan"
Write-Host -ForegroundColor $color $line
Write-Host -ForegroundColor $color $message
Write-Host -ForegroundColor $color $line
Write-Host

# Load the module if not already loaded.
$moduleName = 'PSExcel'
$targetDir =  $PSScriptRoot

if (-not (Get-Module -Name $moduleName)) {
    $env:PSModulePath = (Resolve-Path $targetDir).Path + ";" + $env:PSModulePath
    Import-Module $moduleName -ArgumentList @($targetDir) -Verbose
} else {
    Write-Host "Module '$moduleName' is already loaded."
}

 $dir = "D:\temp\Excel"
 $file = "workbook.xlsx"
 
$workbook = New-Workbook -Tabs "Sheet1", "Sheet2", "Sheet3"
$workbook | Export-Workbook  -Directory $dir -FileName $file


$file2 = "workbook2.xlsx"
$opened = Open-Workbook -Directory $dir -FileName $file
$opened | Export-Workbook  -Directory $dir -FileName $file2

$opened | Get-TabName
$opened | Rename-Tab -oldNAme Sheet2 -newName renamedSheet | Get-TabName

