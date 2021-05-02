namespace AmongUs_ModInstaller
{
    class ModInstallation
    {
        public ModInfo modInfo;
        public string absolutePath;

        public ModInstallation(ModInfo modInfo, string absolutePath)
        {
            this.modInfo = modInfo;
            this.absolutePath = absolutePath;
        }

        public override string ToString()
        {
            return modInfo.ToString();
        }
    }
}
