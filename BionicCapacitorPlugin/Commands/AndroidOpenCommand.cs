using System;
using System.IO;
using BionicCapacitorPlugin.Utils;
using McMaster.Extensions.CommandLineUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "open", Description = "Open Android Capacitor project")]
  public class AndroidOpenCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Open();

    private static int Open() {
      Console.WriteLine("☕  Opening Android Capacitor project...");

      var cd = Directory.GetCurrentDirectory();
      var capDir = $"{cd}/platforms/capacitor";

      if (!Directory.Exists(capDir)) {
        Console.WriteLine($"☠  Capacitor project must be initialized first.");
        return 1;
      }
      
      try {
        Directory.SetCurrentDirectory(capDir);
        Helper.RunCmd("npx", "cap open android");
      }
      catch (Exception) {
        Console.WriteLine($"☠  Unable to open Android Capacitor project. Please check platforms/capacitor");
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