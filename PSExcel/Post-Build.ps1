param(
    [Parameter(Mandatory=$true)] [string] $ProjectName,
    [Parameter(Mandatory=$true)] [string] $ConfigurationName,
    [Parameter(Mandatory=$true)] [string] $TargetDir
)

if (-not $TargetDir) {
    throw "TargetDir parameter is required."
}

# Set the source path
$sourcePath = (Resolve-Path .).Path

# Ensure the target directory exists within the source path
$targetPath = Join-Path -Path $sourcePath -ChildPath 'PSExcel'
if (-not (Test-Path -Path $targetPath)) {
    New-Item -Path $targetPath -ItemType Directory -Force  
}

# Copy only top-level files to the target directory
Get-ChildItem -Path $sourcePath -File | ForEach-Object {
    Copy-Item -Path $_.FullName -Destination $targetPath -Force  
}

