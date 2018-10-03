using System;
using System.IO;
using BionicCapacitorPlugin.Utils;
using McMaster.Extensions.CommandLineUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "sync", Description = "Sync iOS Capacitor project")]
  public class IOSSyncCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Sync();

    private static int Sync() {
      Console.WriteLine("☕  Syncing iOS Capacitor...");

      var cd = Directory.GetCurrentDirectory();
      var capDir = $"{cd}/platforms/capacitor";

      if (!Directory.Exists(capDir)) {
        Console.WriteLine($"☠  Capacitor project must be initialized first.");
        return 1;
      }

      try {
        Helper.CopyAndRenameFolders(cd);
        Directory.SetCurrentDirectory(capDir);
        Helper.RunCmd("npx", "cap sync ios");
      }
      catch (Exception e) {
        Console.WriteLine($"☠  Unable to sync iOS Capacitor project. Please check platforms/capacitor");
        return 1;
      }
      finally
      {
        Directory.SetCurrentDirectory(cd);
      }
      
      Console.WriteLine("🚀  Capacitor successfully synced. Try: bionic platform capacitor ios open");

      return 0;
    }
  }
}