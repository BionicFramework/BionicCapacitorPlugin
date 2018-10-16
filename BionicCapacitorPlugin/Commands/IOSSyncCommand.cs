using System;
using System.IO;
using BionicCLI;
using BionicCore;
using BionicPlugin;
using McMaster.Extensions.CommandLineUtils;
using static BionicCore.DirectoryUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "sync", Description = "Sync iOS Capacitor project")]
  public class IOSSyncCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Sync();

    private static int Sync() {
      Logger.Preparing("Syncing iOS Capacitor...");

      var cd = Directory.GetCurrentDirectory();
      var capDir = ToOSPath($"{cd}/platforms/capacitor");

      if (!Directory.Exists(capDir)) {
        Logger.Error("Capacitor project must be initialized first.");
        return 1;
      }

      try {
        CopyAndRenameFolders(cd);
        Directory.SetCurrentDirectory(capDir);
        ProcessHelper.RunCmd("npx", "cap sync ios");
      }
      catch (Exception) {
        Logger.Error("Unable to sync iOS Capacitor project. Please check platforms/capacitor");
        return 1;
      }
      finally {
        Directory.SetCurrentDirectory(cd);
      }
      
      Logger.Success("Capacitor successfully synced. Try: bionic platform capacitor ios open");

      return 0;
    }
  }
}