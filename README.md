# AAMI - Automated Among Us Mod Installer

![AAMI Icon](https://raw.githubusercontent.com/NyphoX/AutomatedAmongUsModInstaller/master/AmongUs_ModInstaller/aami.ico)

AAMI is a standalone tool that is designed to simplify the process of modding Among Us.\
It is intended to be used with the Steam version of the game.

The goal is to stop players from having to perform  the same tedious copy-paste tasks, whenever a new version of their favorite mod is released or Among Us itself is updated.

Simply select the desired mod from a list, mod your game with a single click, and start playing. Apply mod-updates, which are automatically detected for you, with a single click. Switch between different mods in an instant. Or just start a quick round of vanilla Among Us while still keeping your mods installed.

AAMI is currently only developed for Windows.

## IMPORTANT

__Before__ using AAMI, make sure your Steam installation of Among Us is "clean". This means that there is no mod installed and only the base Among Us game files are present. If you are unsure if this is the case, do a clean reinstall of Among Us following these steps:
 1. Open Steam and click on Library > Right-Click on Among Us > Manage > Browse Local Files. Keep this window open.
 2. Again, click on Library > Right-Click on Among Us > Manage > Uninstall. Confirm the uninstall prompt and wait until it is finished.
 3. In the explorer window from step 1., check if there are any leftover files. Delete everything that is in this folder.
 4. Open Steam and install Among Us as usual. You can now use AAMI and you will never have to mess with your Among Us folder again.

## Features

 - Automatically detect Steam's installation path for Among Us. No setup required.
 - Never changes files in your default Among Us installation again.
 - 1-Click-Installation, 1-Click-Deinstallation and 1-Click-Play for all available mods.
 - 1-Click-Update of installed mods. (Upon clicking "Play Mod", you're informed if there is an update available for the chosen mod and can choose to keep the old version or to update.)
 - 1-Click-Play for Vanilla Among Us (Among Us without any mods).
 - Many more mods can be added by basically anyone on GitHub to the "available mods" list, without the need of updating your AAMI client.
 - If mods have been added that do require you to update your AMII client, you will be informed after starting the client. You're immediately redirected to the releases-page of this GitHub-repository, where you can download the new client, if you choose so.

## Download / Where to get it?

Download the latest release here: https://github.com/NyphoX/AutomatedAmongUsModInstaller/releases/latest

If you are updated from an older version of AAMI, it is recommended to first uninstall all mods using your old AAMI client. It will not break anything if you don't do this, but you will waste a few hundred MB if you don't do this multiple times.

## Supported Mods

AAMI currently supports the following mods (more to come):
- [Sherrif Mod](https://github.com/Woodi-dev/Among-Us-Sheriff-Mod)
- [The Other Roles](https://github.com/Eisbison/TheOtherRoles)
- [Town of Impostors](https://github.com/AJMix/TownOfImpostors)

## Why is mod "XYZ" not available?

If the mod is on GitHub, create a new [issue](https://github.com/NyphoX/AutomatedAmongUsModInstaller/issues) and link to the missing mod. If the mod just has to be extracted into the Among Us game folder, it can be added with an one-liner. If not, AAMI might need to be changed to support a wider variety of mods. If you know how to, you may also just create a pull request that I will merge afterwards.

## Concerning updates to "Among Us" itself:

If Among Us is updated, some mods usually break and stop working until the mod in question is updated by the author. If you know that there was an update to Among Us, your __already installed mods__ will continue using the old version of the game and will - possibly - still be working. In such a case, it is recommended to wait until the mod author has updated the mod (see links under [Supported Mods](#supported-mods)) and then perform a clean uninstall and subsequent install using AAMI. The new install will then use your new version of Among Us. You will always get notified about a new version, when you click "Play Mod", so in this case you'll also know it is time to re-install the mod based on the new version.

## How does it work?

A picture says more than a thousand words. Have a look at how AAMI works:

TODO: embed screenshots

## Windows protected your PC / Microsoft Defender SmartScreen

Windows may tell you _during the first start_ of AAMI, that it is unrecognized app/from an unknown publisher. This may happen when you are using a new release. You can continue by clicking on "More Info" and then "Run anyway" in this case, as shown below.

If in doubt, check the files yourself or refer to the VirusTotal.com scans provided for every release.

![Microsoft Defender SmartScreen Window](https://user-images.githubusercontent.com/17164873/116812589-be652d80-ab4f-11eb-8ad6-afb2ad1e9576.jpg) ![Microsoft Defender SmartScreen Window showing more information](https://user-images.githubusercontent.com/17164873/116812590-befdc400-ab4f-11eb-83ff-ce9fd8cb9e57.jpg)

