```
 _____                 _____                  
|  __ \               |  __ \                 
| |__) |___  _ __ ___ | |__) |___ _ __   ___  
|  _  // _ \| '_ ` _ \|  _  // _ \ '_ \ / _ \ 
| | \ \ (_) | | | | | | | \ \  __/ |_) | (_) |
|_|  \_\___/|_| |_| |_|_|  \_\___| .__/ \___/ 
                                 | |          
          windows service        |_|          
----------------------------------------------
```

Helpful commands for Windows Service:

```
dotnet publish --output "C:\RomRepoService"

sc.exe create "RomRepo Service" binpath= "C:\RomRepoService\romrepo.win.exe"

sc.exe start "RomRepo Service"

sc.exe stop "RomRepo Service"

sc.exe delete "RomRepo Service"
```

WIP