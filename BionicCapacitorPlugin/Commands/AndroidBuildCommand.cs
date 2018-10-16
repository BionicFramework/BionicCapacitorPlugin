using System;
using System.IO;
using BionicCLI;
using BionicCore;
using BionicPlugin;
using McMaster.Extensions.CommandLineUtils;
using static BionicCore.DirectoryUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "build", Description = "Build Android Capacitor project")]
  public class AndroidBuildCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Build();

    private static int Build() {
      Logger.Preparing("Building Android Capacitor...");

      var cd = Directory.GetCurrentDirectory();
      var capDir = ToOSPath($"{cd}/platforms/capacitor");

      if (!Directory.Exists(capDir)) {
        Logger.Error("Capacitor project must be initialized first.");
        return 1;
      }

      try {
        CopyAndRenameFolders(cd);
        Directory.SetCurrentDirectory(capDir);
        ProcessHelper.RunCmd("npx", "cap copy android");
      }
      catch (Exception) {
        Logger.Error("Unable to build Android Capacitor project. Please check platforms/capacitor");
        return 1;
      }
      finally {
        Directory.SetCurrentDirectory(cd);
      }
      
      Logger.Success("Capacitor successfully built. Try: bionic platform capacitor android open");

      return 0;
    }
  }
}