# AAMI - Automated Among Us Mod Installer

<img align="left" width="275" height="275" src="https://raw.githubusercontent.com/NyphoX/AutomatedAmongUsModInstaller/master/AmongUs_ModInstaller/aami.ico"> AAMI is a standalone tool that is designed to simplify the process of modding Among Us.\
It is intended to be used with the Steam version of the game.

The goal is to stop players from having to perform  the same tedious copy-paste and delete tasks, whenever a new version of their favorite mod is released or Among Us itself is updated.

Simply select the desired mod from a list, mod your game with one click, and start playing. Apply automatically detected mod-updates with a single click. Switch between different mods in an instant. Or start a round of vanilla Among Us while still keeping your mods installed.

AAMI is currently only developed for Windows.

## IMPORTANT

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
 - [Adding support for new mods](#why-is-mod-xyz-not-available) can be done by anyone on GitHub, without the need of updating the AAMI clients.
 - For some added mods and changes to the AAMI client, a new version of AAMI is required. You will be informed about this after starting the client and have the option to automatically open the GitHub-website to download the new client.

## Download / Where to get it?

Download the latest release here: https://github.com/NyphoX/AutomatedAmongUsModInstaller/releases/latest

If you are updating from an older version of AAMI, your old mods will be automatically uninstalled. This saves you time and disk space and prevents things from breaking, when new AAMI functions/mods are added. Afterwards, you will have to reconfigure your mods ingame.

## Supported Mods

AAMI currently supports the following mods (more to come):
- [Sherrif Mod](https://github.com/Woodi-dev/Among-Us-Sheriff-Mod)
- [The Other Roles](https://github.com/Eisbison/TheOtherRoles)
- [Town of Impostors](https://github.com/AJMix/TownOfImpostors)

## Why is mod "XYZ" not available?

If the mod is on GitHub, create a new [issue](https://github.com/NyphoX/AutomatedAmongUsModInstaller/issues) and link to the missing mod. If the mod just has to be extracted into the Among Us game folder, it can be added with an one-liner. If not, AAMI might need to be changed to support a wider variety of mods. If you know how to, you may also just create a pull request that I will merge afterwards.

## Updates (AAMI, Among Us, Mods)

The only type of update you will never be notified about is updates from Innersloth to Among Us itself. You'll have to check if there was an update through your Steam client. If so, check whether the authors of your desired mods (see links under [Supported Mods](#supported-mods)) have already released a new version (AAMI tells you that a mod has received an update, but you should double-check the GitHub-repository).

Refer to the following decision tree, if there are updates:

<img align="center" src="https://user-images.githubusercontent.com/17164873/116836142-b9db5c00-abc5-11eb-9990-c22032ff380e.png">

## How does it work?

A picture says more than a thousand words. Have a look at how AAMI works:

TODO: embed screenshots

## Windows protected your PC / Microsoft Defender SmartScreen

Windows may tell you _during the first start_ of AAMI, that it is unrecognized app/from an unknown publisher. This may happen when you are using a new release. You can continue by clicking on "More Info" and then "Run anyway" in this case, as shown below.

If in doubt, check the files yourself or refer to the VirusTotal.com scans provided for every release.

![Microsoft Defender SmartScreen Window](https://user-images.githubusercontent.com/17164873/116812589-be652d80-ab4f-11eb-8ad6-afb2ad1e9576.jpg) ![Microsoft Defender SmartScreen Window showing more information](https://user-images.githubusercontent.com/17164873/116812590-befdc400-ab4f-11eb-83ff-ce9fd8cb9e57.jpg)

