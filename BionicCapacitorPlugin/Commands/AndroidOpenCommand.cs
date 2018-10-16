using System;
using System.IO;
using BionicCLI;
using BionicCore;
using BionicPlugin;
using McMaster.Extensions.CommandLineUtils;
using static BionicCore.DirectoryUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "open", Description = "Open Android Capacitor project")]
  public class AndroidOpenCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Open();

    private static int Open() {
      Logger.Preparing("Opening Android Capacitor project...");

      var cd = Directory.GetCurrentDirectory();
      var capDir = ToOSPath($"{cd}/platforms/capacitor");

      if (!Directory.Exists(capDir)) {
        Logger.Error("Capacitor project must be initialized first.");
        return 1;
      }
      
      try {
        Directory.SetCurrentDirectory(capDir);
        ProcessHelper.RunCmdInBackground("npx", "cap open android");
      }
      catch (Exception) {
        Logger.Error("Unable to open Android Capacitor project. Please check platforms/capacitor");
        return 1;
      }
      finally
      {
        Directory.SetCurrentDirectory(cd);
      }

      Logger.Success("Android Studio Launched");

      return 0;
    }
  }
}