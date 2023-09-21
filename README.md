# Bekape
<img width="1000" alt="portfolio_view" src="https://raw.githubusercontent.com/DanielSvoboda/Bekape/main/print1.png">

Bekape is a Windows Forms application that allows users to backup their files to an external drive. It uses the MD5 hash function to verify the integrity of backup files.

•DOWNLOAD: https://github.com/DanielSvoboda/Bekape/raw/main/Bekape.exe

To use Bekape, you first need to place the Bekape.exe program on a mobile device, such as a pendrive or external hard drive, it must be on the same drive as the backup! After opening the program, add the folders you want to back up to the folder list. You can do this by clicking the “Add Folder” button and selecting the folders you want to back up.

After adding the folders you want to back up, the backup process will start automatically, if while using the program you added or modified any information, use the "Synchronize" button. Bekape will compare the MD5 hashes of files in the source folders and backup folders. If the hashes are different, Bekape will copy the file from the source folder to the backup folder.

The next time you open the program, it will automatically start synchronizing after opening, the only action required is if you want to add/remove new folders or user.

Oh yes, each user can have their own backups, separate from other users' backups. This allows each user to back up their personal files without affecting other users' backups.

Bekape also has a console that you can use to view what new files and modified files are. To view the console, click the "Log" button.
<br><br><img width="900" alt="portfolio_view" src="https://raw.githubusercontent.com/DanielSvoboda/Bekape/main/print2.png">


 Dependency 
---------  
  <br>
  •Microsoft.WindowsAPICodePack-Core <br>
  https://www.nuget.org/packages/WindowsAPICodePack-Core/1.1.2
  <br><br>
  •Costura.Fody <br>
  https://www.nuget.org/packages/Costura.Fody
