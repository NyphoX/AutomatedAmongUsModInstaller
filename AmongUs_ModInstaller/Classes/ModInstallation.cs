namespace AmongUs_ModInstaller
{
    class ModInstallation
    {
        public ModInfo modInfo;
        public string absolutePath;
        public string installedTag;

        public ModInstallation(ModInfo modInfo, string absolutePath, string installedTag)
        {
            this.modInfo = modInfo;
            this.absolutePath = absolutePath;
            this.installedTag = installedTag;
        }

        public override string ToString()
        {
            return string.Format("{0} (Installed: {1}, Version: {2})", modInfo.name, installedTag, modInfo.version);
        }
    }
}
