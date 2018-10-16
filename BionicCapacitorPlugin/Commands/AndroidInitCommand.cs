using System;
using System.IO;
using BionicCLI;
using BionicCore;
using BionicPlugin;
using McMaster.Extensions.CommandLineUtils;
using static BionicCore.DirectoryUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "init", Description = "Initialize Android project in Capacitor")]
  public class AndroidInitCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Init();

    private static int Init() {
      Logger.Preparing("Initializing Capacitor Android project...");

      var cd = Directory.GetCurrentDirectory();
      var capDir = ToOSPath($"{cd}/platforms/capacitor");

      if (!Directory.Exists(capDir)) {
        Logger.Error("Capacitor project must be initialized first.");
        return 1;
      }
      
      try {
        Directory.SetCurrentDirectory(capDir);
        NpxCapAddAndroid();
      }
      catch (Exception) {
        Logger.Error("Something went wrong during Capacitor Android initialization. Please check platforms/capacitor");
        return 1;
      }
      finally {
        Directory.SetCurrentDirectory(cd);
      }

      Logger.Success("Capacitor Android is ready to go! - try: bionic platform capacitor android open");
      return 0;
    }

    private static int NpxCapAddAndroid() => ProcessHelper.RunCmd("npx", $"cap add android");
  }
}