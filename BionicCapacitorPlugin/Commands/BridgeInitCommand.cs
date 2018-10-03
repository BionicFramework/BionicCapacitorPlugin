using System;
using System.IO;
using BionicCapacitorPlugin.Utils;
using McMaster.Extensions.CommandLineUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "init", Description = "Initialize Capacitor Bridge for device API access")]
  public class BridgeInitCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Init();

    private static int Init() {
      Console.WriteLine("☕  Initializing Capacitor Bridge...");

      var cd = Directory.GetCurrentDirectory();
      var capDir = $"{cd}/platforms/capacitor";

      if (!Directory.Exists(capDir)) {
        Console.WriteLine($"☠  Capacitor project must be initialized first.");
        return 1;
      }
      
      try {
        InstallBionicCapacitorBridge();
      }
      catch (Exception) {
        Console.WriteLine($"☠  Something went wrong during Capacitor Bridge initialization. Please check project .csproj file");
        return 1;
      }
      finally {
        Directory.SetCurrentDirectory(cd);
      }

      Console.WriteLine("🚀  Capacitor Bridge is ready to go!");
      return 0;
    }

    private static int InstallBionicCapacitorBridge() => Helper.RunDotNet("add package BionicBridgeCapacitor");
  }
}