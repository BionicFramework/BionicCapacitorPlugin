using System;
using System.IO;
using BionicCapacitorPlugin.Utils;
using McMaster.Extensions.CommandLineUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "init", Description = "Initialize iOS project in Capacitor")]
  public class IOSInitCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Init();

    private static int Init() {
      Console.WriteLine("☕  Initializing Capacitor iOS project...");

      var cd = Directory.GetCurrentDirectory();
      var capDir = $"{cd}/platforms/capacitor";

      if (!Directory.Exists(capDir)) {
        Console.WriteLine($"☠  Capacitor project must be initialized first.");
        return 1;
      }
      
      try {
        Directory.SetCurrentDirectory(capDir);
        NpxCapAddIOS();
      }
      catch (Exception e) {
        Console.WriteLine($"☠  Something went wrong during Capacitor iOS initialization. Please check platforms/capacitor");
        return 1;
      }
      finally {
        Directory.SetCurrentDirectory(cd);
      }

      Console.WriteLine("🚀  Capacitor iOS is ready to go! - try: bionic platform capacitor ios open");
      return 0;
    }

    private static int NpxCapAddIOS() => Helper.RunCmd("npx", $"cap add ios");
  }
}