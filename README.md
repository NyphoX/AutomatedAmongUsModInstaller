# AAMI - Automated Among Us Mod Installer

<img align="right" width="250" height="250" src="https://raw.githubusercontent.com/NyphoX/AutomatedAmongUsModInstaller/master/AmongUs_ModInstaller/aami.ico"> AAMI is a standalone tool that is designed to simplify the process of modding Among Us. It is intended to be used with the Steam version of the game.

The goal is to stop players from having to perform  the same tedious copy-paste and delete tasks, whenever a new version of their favorite mod is released or Among Us itself is updated.

Simply select the desired mod from a list, mod your game with one click, and start playing. Apply automatically detected mod-updates with a single click. Switch between different mods in an instant. Or start a round of vanilla Among Us while still keeping your mods installed.

AAMI is currently only developed for Windows.

## How does it work?

I could go on describing how this mod works in all detail. But that would most likely be boring. Have a look at how AAMI works in this short video (3:05). (Clicking on the video opens it on YouTube.)

<p align="center"><a href="https://www.youtube.com/watch?v=51nVjxGa6K0"><img src="https://user-images.githubusercontent.com/17164873/116954130-69303580-ac8f-11eb-8d10-b9e5b3682317.png"></a></p>

## Important: Read before using AAMI

__Before__ using AAMI, make sure your Steam installation of Among Us is "clean". This means that there is no mod installed and only the base Among Us game files are present. If you are unsure if this is the case, do a clean reinstall of Among Us following these steps:
 1. Open Steam and click on _Library > Right-Click on Among Us > Manage > Browse Local Files_.\
 Keep this window open.
 3. Again, click on _Library > Right-Click on Among Us > Manage > Uninstall_.\
 Confirm the uninstall prompt and wait until it is finished.
 5. In the _explorer window from step 1._, check if there are any leftover files.\
 Delete everything that is in this folder.
 7. _Open Steam and install Among Us as usual_.

You can now use AAMI and you will never have to mess with your Among Us folder again.

## Features

 - No setup required. _Automatically detects Steam's installation path for Among Us._
 - Never changes files in your default Among Us installation again.
 - 1-Click-Installation, 1-Click-Deinstallation and 1-Click-Play for all available mods.
 - 1-Click-Update of installed mods. _Mod-specific settings/configurations are saved between mod-updates._
 - 1-Click-Play for Vanilla Among Us (Among Us without any mods).
 - 1-Click-Update for AAMI itself. _All mod installations are saved._ You will be informed about these updates when starting AAMI. Read the section about [Updates](#updates-aami-among-us-mods) to learn more about AAMI's (and ideally also your) update behavior.
 - [Adding support for new mods](#why-is-mod-xyz-not-available) can be done by anyone on GitHub.

## Download / Where to get it?

Download the latest release here: https://github.com/NyphoX/AutomatedAmongUsModInstaller/releases/latest

If you already have AAMI installed, launch it and update if it asks you to.

## Supported Mods

AAMI currently supports the following mods (more to come):
- [Sherrif Mod](https://github.com/Woodi-dev/Among-Us-Sheriff-Mod)
- [The Other Roles](https://github.com/Eisbison/TheOtherRoles)
- [Town of Impostors](https://github.com/AJMix/TownOfImpostors)

## Why is mod "XYZ" not available?

If the mod is on GitHub, create a new [issue](https://github.com/NyphoX/AutomatedAmongUsModInstaller/issues) and link to the missing mod. If the mod just has to be extracted into the Among Us game folder, it can be added with an one-liner. If not, AAMI might need to be changed to support a wider variety of mods. If you know how to, you may also just create a pull request that I will merge afterwards.

## Updates (AAMI, Among Us, Mods)

The only type of update you will never be notified about by AAMI are updates from InnerSloth to Among Us itself. You'll have to check if there was an update through your Steam client. If so, check whether the authors of your desired mods (see links under [Supported Mods](#supported-mods)) have already released a new version. AAMI will also tell you that a mod has received an update when clicking "Play Mod", but you should double-check the GitHub-repository.

Refer to the following decision tree to see what to do on updates:

<p align="center"><img src="https://user-images.githubusercontent.com/17164873/118295596-57932d00-b4dc-11eb-9200-f4bc764c37ef.png"></p>

## Windows protected your PC / Microsoft Defender SmartScreen

Windows may tell you _during the first start_ of AAMI, that it is an unrecognized app/from an unknown publisher. This may happen when you are using a new release. You can continue by clicking on "More Info" and then "Run anyway" in this case, as shown below.

If in doubt, check the files yourself or refer to the VirusTotal.com scans provided for every release.

![Microsoft Defender SmartScreen Window](https://user-images.githubusercontent.com/17164873/116812589-be652d80-ab4f-11eb-8ad6-afb2ad1e9576.jpg) ![Microsoft Defender SmartScreen Window showing more information](https://user-images.githubusercontent.com/17164873/116812590-befdc400-ab4f-11eb-83ff-ce9fd8cb9e57.jpg)

