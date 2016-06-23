README

#Silent Dream
By Adina Shanholtz. Story and art by Dominic D'Andrea. 


This is the github repo for the Unity game Silent Dream (working title project nightfear). Repo should be able to be downloaded and opened in Unity to play. 

### Windows 10 Features

How to set up the Windows 10 UWP app features:

Download plugins from [this github.](https://github.com/microsoft/UnityPlugins)

1. Change the variables as directed in the github instructions above. Make sure they're referencing your version of Unity and nuget. 

2. Make sure you're using Visual Studio 2015.

3. Install Core plugin to use for live tiles/notifications when you run build.ps1. Follow github instructions above. 

4. Package will be made in the downloaded Unity Plugins for Windows 10 github folder "UnityPackages".

5. Import that package into unity. Assets -> Import Package -> Custom Package. 


NOTE: When building your Unity game for the Windows Store, make sure you do not already have a Windows Store build in that folder. They do not completely overwrite each other and therefore will not run. 


Art and Music is licensed under Creative Commons. 

Art [Link] (http://yamikatt.deviantart.com/gallery/58845553/Silent-Dream-art-assets)

Music [Link] (https://soundcloud.com/adina-shanholtz/sets/silent-dream-ost)
