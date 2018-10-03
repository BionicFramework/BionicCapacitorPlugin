using System;
using System.IO;
using BionicCapacitorPlugin.Utils;
using McMaster.Extensions.CommandLineUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "open", Description = "Open iOS Capacitor project")]
  public class IOSOpenCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Open();

    private static int Open() {
      Console.WriteLine("☕  Opening iOS Capacitor project...");

      var cd = Directory.GetCurrentDirectory();
      var capDir = $"{cd}/platforms/capacitor";

      if (!Directory.Exists(capDir)) {
        Console.WriteLine($"☠  Capacitor project must be initialized first.");
        return 1;
      }
      
      try {
        Directory.SetCurrentDirectory(capDir);
        Helper.RunCmd("npx", "cap open ios");
      }
      catch (Exception e) {
        Console.WriteLine($"☠  Unable to open iOS Capacitor project. Please check platforms/capacitor");
        return 1;
      }
      finally
      {
        Directory.SetCurrentDirectory(cd);
      }

      return 0;
    }
  }
}