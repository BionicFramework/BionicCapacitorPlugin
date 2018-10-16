using System;
using System.IO;
using BionicCLI;
using BionicCore;
using BionicPlugin;
using McMaster.Extensions.CommandLineUtils;
using static BionicCore.DirectoryUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "init", Description = "Initialize Capacitor Bridge for device API access")]
  public class BridgeInitCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Init();

    private static int Init() {
      Logger.Preparing("Initializing Capacitor Bridge...");

      var cd = Directory.GetCurrentDirectory();
      var capDir = ToOSPath($"{cd}/platforms/capacitor");

      if (!Directory.Exists(capDir)) {
        Logger.Error("Capacitor project must be initialized first.");
        return 1;
      }
      
      try {
        InstallBionicCapacitorBridge();
      }
      catch (Exception) {
        Logger.Error("Something went wrong during Capacitor Bridge initialization. Please check project .csproj file");
        return 1;
      }
      finally {
        Directory.SetCurrentDirectory(cd);
      }

      Logger.Success("Capacitor Bridge is ready to go!");
      return 0;
    }

    private static int InstallBionicCapacitorBridge() => DotNetHelper.RunDotNet("add package BionicBridgeCapacitor");
  }
}