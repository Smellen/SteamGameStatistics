Write-Host 'Starting script for replacing the steam key and steam id environment variables.'
$elapsed = [System.Diagnostics.Stopwatch]::StartNew()

$steamKey = $args[0]
$steamId = $args[1]

Write-Host "Steam key: $steamKey";
Write-Host "Steam id: $steamId";

$launchSettingsFile = Get-ChildItem .\Properties -Recurse -Filter "launchSettings.json"
$filePath = $launchSettingsFile.FullName

(Get-Content $filePath) -replace '<STEAMKEY>', $steamKey -replace '<STEAMID>', $steamId | Set-Content $filePath

$elapsed.stop()
write-host "Script complete. Time taken: $($elapsed.Elapsed.ToString())"