```
 _____                 _____                  
|  __ \               |  __ \                 
| |__) |___  _ __ ___ | |__) |___ _ __   ___  
|  _  // _ \| '_ ` _ \|  _  // _ \ '_ \ / _ \ 
| | \ \ (_) | | | | | | | \ \  __/ |_) | (_) |
|_|  \_\___/|_| |_| |_|_|  \_\___| .__/ \___/ 
                                 | |          
          client tools           |_|          
----------------------------------------------
```

### romrepo.lib
---
Common library for database and business logic.

### romrepo.win
---
Windows Service exe that watches the file system, hosts .NET API endpoints, and hosts the Angular app. This may eventually be adapted to be a cross platform desktop app, but that is yet to be determined.

### romrepo.angular
---
Primary UI for this project. The project output is hosted in the romrepo.win project's ```wwwroot``` folder.