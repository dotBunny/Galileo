using System;
using System.Collections.Generic;using System.Reflection;
using System.Text;

namespace Galileo.Core
{
    public class HunterProfile
    {
        public string PackageVersion { get; private set; }
        public string CoreLibraryVersion { get; private set; }
        public string ClientLibraryVersion { get; private set; }
        public string ClientVersion { get; private set; }
        public string Localization { get; private set; }

        public HunterProfile()
        {
            SetPackageVersion("Undefined");
            CoreLibraryVersion = "0.0.0.0";
            ClientLibraryVersion = "0.0.0.0";
            ClientVersion = "0.0.0.0";
        }

        public HunterProfile(string packageVersion)
        {
            SetPackageVersion(packageVersion);
            CoreLibraryVersion = "0.0.0.0";
            ClientLibraryVersion = "0.0.0.0";
            ClientVersion = "0.0.0.0";
            Localization = "en-CA";
        }

        public HunterProfile(string packageVersion, Version coreLibrary, Version clientLibrary, Version client, string localization)
        {
            SetLocalization(localization);
            SetPackageVersion(packageVersion);
            SetCoreLibraryVersion(coreLibrary);
            SetClientLibraryVersion(clientLibrary);
            SetClientVersion(client);
        }        public static HunterProfile Create(string appVersion, Version settings, string locale)
        {
            return new HunterProfile(

              appVersion,

              Assembly.GetAssembly(typeof(Core.Hunter)).GetName().Version,

              settings,

              Assembly.GetEntryAssembly().GetName().Version,

              locale);            
        }


        public void SetCoreLibraryVersion(Version version)
        {
            CoreLibraryVersion = version.ToString();
        }

        public void SetClientLibraryVersion(Version version)
        {
            ClientLibraryVersion = version.ToString();
        }

        public void SetClientVersion(Version version)
        {
            ClientVersion = version.ToString();
        }

        public void SetPackageVersion(string version)
        {
            PackageVersion = version;
        }
        public void SetLocalization(string localization)
        {
            Localization = localization;
            // TODO: Load stuff?
        }

        public override string ToString()
        {
            return "Galileo " + PackageVersion + " (" + ClientVersion + "-" + ClientLibraryVersion + "-" + CoreLibraryVersion + ")"; 
        }
    }
}
