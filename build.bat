@echo off
cls
powershell -ExecutionPolicy Unrestricted -NoProfile -Command "& { Import-Module '.\build\tools\psake\psake.psm1'; $psake.use_exit_on_error = $true; Invoke-psake '.\build\build.ps1' -framework 4.0 %*; Remove-Module psake }"