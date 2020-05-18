@echo off
echo 更新中...
taskkill /f /im CardService.exe
F:
cd  "F:\华发\二院\SecondHospital\CardTest\bin\Debug"
start "" CardService.exe
