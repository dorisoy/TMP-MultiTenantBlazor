@echo off

SET NEWLINE=^& echo.

FIND /C /I "# Added for MultiTenantBlazor testing" %WINDIR%\system32\drivers\etc\hosts
IF %ERRORLEVEL% NEQ 0 (
    ECHO %NEWLINE%>>%WINDIR%\System32\drivers\etc\hosts
    ECHO %NEWLINE%^# Added for MultiTenantBlazor testing>>%WINDIR%\System32\drivers\etc\hosts
    ECHO %NEWLINE%^127.0.0.1 tenant1.localhost tenant2.localhost tenant3.localhost>>%WINDIR%\System32\drivers\etc\hosts
    ECHO %NEWLINE%^# End of section>>%WINDIR%\System32\drivers\etc\hosts
)