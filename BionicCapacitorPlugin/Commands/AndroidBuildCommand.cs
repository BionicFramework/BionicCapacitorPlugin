using System;
using System.IO;
using BionicCapacitorPlugin.Utils;
using McMaster.Extensions.CommandLineUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "build", Description = "Build Android Capacitor project")]
  public class AndroidBuildCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Build();

    private static int Build() {
      Console.WriteLine("☕  Building Android Capacitor...");

      var cd = Directory.GetCurrentDirectory();
      var capDir = $"{cd}/platforms/capacitor";

      if (!Directory.Exists(capDir)) {
        Console.WriteLine($"☠  Capacitor project must be initialized first.");
        return 1;
      }

      try {
        Helper.CopyAndRenameFolders(cd);
        Directory.SetCurrentDirectory(capDir);
        Helper.RunCmd("npx", "cap copy android");
      }
      catch (Exception e) {
        Console.WriteLine($"☠  Unable to build Android Capacitor project. Please check platforms/capacitor");
        return 1;
      }
      finally
      {
        Directory.SetCurrentDirectory(cd);
      }
      
      Console.WriteLine("🚀  Capacitor successfully built. Try: bionic platform capacitor android open");

      return 0;
    }
  }
}