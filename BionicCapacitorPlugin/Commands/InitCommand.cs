using System;
using System.IO;
using BionicCapacitorPlugin.Utils;
using McMaster.Extensions.CommandLineUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "init", Description = "Initialize Capacitor project structure")]
  public class InitCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Init();

    private static int Init() {
      Console.WriteLine("☕  Initializing Capacitor...");

      // TODO: Check if required tools (npm/npx) are available
      
      // TODO: Check if already installed
      Helper.RunDotNet("new -i BionicCapacitorTemplate");

      // TODO: Check if platforms/capacitor already exists
      Helper.RunDotNet("new bionic.capacitor -o platforms");

      var cd = Directory.GetCurrentDirectory();
      try {
        Directory.SetCurrentDirectory($"{cd}/platforms/capacitor");
        NpmInstall();
        NpxCapInit();
      }
      catch (Exception e) {
        Console.WriteLine($"☠  Something went wrong during Capacitor install. Please check platforms/capacitor");
        return 1;
      }
      finally {
        Directory.SetCurrentDirectory(cd);
      }

      Console.WriteLine("🚀  Capacitor is ready to go! - try: bionic platform capacitor");
      return 0;
    }

    private static int NpmInstall() => Helper.RunCmd("npm", $"install");

    private static int NpxCapInit() => Helper.RunCmd("npx", $"cap init");
  }
}