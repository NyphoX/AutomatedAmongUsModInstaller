using System;

namespace AmongUs_ModInstaller
{
    class ModInfo
    {
        public string name;
        public string version;
        public string permanentAPIURL;
        public int assetId;
        public string AAMIversion;
        public override string ToString()
        {
            return string.Format("{0} (Version: {1})", name, version);
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
                return false;
            else
            {
                ModInfo mi = (ModInfo)obj;
                return permanentAPIURL.Equals(mi.permanentAPIURL);
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(permanentAPIURL);
        }
    }
}
