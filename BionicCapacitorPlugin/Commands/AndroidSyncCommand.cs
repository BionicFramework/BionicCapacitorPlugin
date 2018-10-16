using System;
using System.IO;
using BionicCLI;
using BionicCore;
using BionicPlugin;
using McMaster.Extensions.CommandLineUtils;
using static BionicCore.DirectoryUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "sync", Description = "Sync Android Capacitor project")]
  public class AndroidSyncCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Sync();

    private static int Sync() {
      Logger.Preparing("Syncing Android Capacitor...");

      var cd = Directory.GetCurrentDirectory();
      var capDir = ToOSPath($"{cd}/platforms/capacitor");

      if (!Directory.Exists(capDir)) {
        Logger.Error("Capacitor project must be initialized first.");
        return 1;
      }

      try {
        CopyAndRenameFolders(cd);
        Directory.SetCurrentDirectory(capDir);
        ProcessHelper.RunCmd("npx", "cap sync android");
      }
      catch (Exception) {
        Logger.Error("Unable to sync Android Capacitor project. Please check platforms/capacitor");
        return 1;
      }
      finally {
        Directory.SetCurrentDirectory(cd);
      }
      
      Logger.Success("Capacitor successfully synced. Try: bionic platform capacitor android open");

      return 0;
    }
  }
}