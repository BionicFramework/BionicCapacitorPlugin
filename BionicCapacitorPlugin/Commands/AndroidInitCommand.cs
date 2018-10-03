using System;
using System.IO;
using BionicCapacitorPlugin.Utils;
using McMaster.Extensions.CommandLineUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "init", Description = "Initialize Android project in Capacitor")]
  public class AndroidInitCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Init();

    private static int Init() {
      Console.WriteLine("☕  Initializing Capacitor Android project...");

      var cd = Directory.GetCurrentDirectory();
      var capDir = $"{cd}/platforms/capacitor";

      if (!Directory.Exists(capDir)) {
        Console.WriteLine($"☠  Capacitor project must be initialized first.");
        return 1;
      }
      
      try {
        Directory.SetCurrentDirectory(capDir);
        NpxCapAddAndroid();
      }
      catch (Exception) {
        Console.WriteLine($"☠  Something went wrong during Capacitor Android initialization. Please check platforms/capacitor");
        return 1;
      }
      finally {
        Directory.SetCurrentDirectory(cd);
      }

      Console.WriteLine("🚀  Capacitor Android is ready to go! - try: bionic platform capacitor android open");
      return 0;
    }

    private static int NpxCapAddAndroid() => Helper.RunCmd("npx", $"cap add android");
  }
}