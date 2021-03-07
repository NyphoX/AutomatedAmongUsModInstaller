﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AmongUs_ModInstaller.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.8.1.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool IsGamePathSet {
            get {
                return ((bool)(this["IsGamePathSet"]));
            }
            set {
                this["IsGamePathSet"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool IsCPUArchitectureSet {
            get {
                return ((bool)(this["IsCPUArchitectureSet"]));
            }
            set {
                this["IsCPUArchitectureSet"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool IsCPUArchitecture64Bit {
            get {
                return ((bool)(this["IsCPUArchitecture64Bit"]));
            }
            set {
                this["IsCPUArchitecture64Bit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("HKEY_LOCAL_MACHINE\\SOFTWARE\\Valve\\Steam")]
        public string SteamRegistryPath32Bit {
            get {
                return ((string)(this["SteamRegistryPath32Bit"]));
            }
            set {
                this["SteamRegistryPath32Bit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("HKEY_LOCAL_MACHINE\\SOFTWARE\\WOW6432Node\\Valve\\Steam")]
        public string SteamRegistryPath64Bit {
            get {
                return ((string)(this["SteamRegistryPath64Bit"]));
            }
            set {
                this["SteamRegistryPath64Bit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("InstallPath")]
        public string SteamRegistryKeyName {
            get {
                return ((string)(this["SteamRegistryKeyName"]));
            }
            set {
                this["SteamRegistryKeyName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("steamapps\\appmanifest_945360.acf")]
        public string AmongUsAppManifestSublocation {
            get {
                return ((string)(this["AmongUsAppManifestSublocation"]));
            }
            set {
                this["AmongUsAppManifestSublocation"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("steamapps\\libraryfolders.vdf")]
        public string SteamLibraryFoldersFileSublocation {
            get {
                return ((string)(this["SteamLibraryFoldersFileSublocation"]));
            }
            set {
                this["SteamLibraryFoldersFileSublocation"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string GamePath {
            get {
                return ((string)(this["GamePath"]));
            }
            set {
                this["GamePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("steamapps\\common\\Among Us")]
        public string GameSublocation {
            get {
                return ((string)(this["GameSublocation"]));
            }
            set {
                this["GameSublocation"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.github.com/repos/NotHunter101/ExtraRolesAmongUs/releases/latest")]
        public string MOD_ExtraRoles_APIURL {
            get {
                return ((string)(this["MOD_ExtraRoles_APIURL"]));
            }
            set {
                this["MOD_ExtraRoles_APIURL"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.Specialized.StringCollection MOD_ExtraRoles_FileList {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["MOD_ExtraRoles_FileList"]));
            }
            set {
                this["MOD_ExtraRoles_FileList"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("(none)")]
        public string MOD_ExtraRoles_InstalledVersion {
            get {
                return ((string)(this["MOD_ExtraRoles_InstalledVersion"]));
            }
            set {
                this["MOD_ExtraRoles_InstalledVersion"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ExtraRoles")]
        public string MOD_ExtraRoles_Name {
            get {
                return ((string)(this["MOD_ExtraRoles_Name"]));
            }
            set {
                this["MOD_ExtraRoles_Name"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool MOD_ExtraRoles_IsInstalled {
            get {
                return ((bool)(this["MOD_ExtraRoles_IsInstalled"]));
            }
            set {
                this["MOD_ExtraRoles_IsInstalled"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.Specialized.StringCollection MOD_ExtraRoles_FolderList {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["MOD_ExtraRoles_FolderList"]));
            }
            set {
                this["MOD_ExtraRoles_FolderList"] = value;
            }
        }
    }
}
