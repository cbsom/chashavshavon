"C:\Program Files\7-Zip\7z.exe " a "Chashavshavon_Installer_Full.zip" "%~1%~2\*"
"C:\Program Files (x86)\ZipGenius 6\zg.exe" -sfx "Chashavshavon_Installer_Full.zip" A1 B1 O0 E0 L0 C"Setup.exe" D"%temp%" U"ChashInstall" H2 I"scroll.ico" S0 T"Extracting Chashavshavon installation files..."
DEL "Chashavshavon_Installer_Full.zip"
"C:\Program Files\7-Zip\7z.exe " a "D:\Repositories\Compute\WebSite\Products\Chashavshavon\Chashavshavon_Installer_Full.zip" "Chashavshavon_Installer_Full.exe"
DEL "Chashavshavon_Installer_Full.exe"
"SetVersionApp.exe " "D:\Repositories\Chashavshavon\Chashavshavon\bin\%~2\Chashavshavon.exe" "D:\Repositories\Compute\WebSite\Products\Chashavshavon"