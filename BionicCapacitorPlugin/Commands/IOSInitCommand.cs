using System;
using System.IO;
using BionicCLI;
using BionicCore;
using BionicPlugin;
using McMaster.Extensions.CommandLineUtils;
using static BionicCore.DirectoryUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "init", Description = "Initialize iOS project in Capacitor")]
  public class IOSInitCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Init();

    private static int Init() {
      Logger.Preparing("Initializing Capacitor iOS project...");

      var cd = Directory.GetCurrentDirectory();
      var capDir = ToOSPath($"{cd}/platforms/capacitor");

      if (!Directory.Exists(capDir)) {
        Logger.Error("Capacitor project must be initialized first.");
        return 1;
      }
      
      try {
        Directory.SetCurrentDirectory(capDir);
        NpxCapAddIOS();
      }
      catch (Exception) {
        Logger.Error("Something went wrong during Capacitor iOS initialization. Please check platforms/capacitor");
        return 1;
      }
      finally {
        Directory.SetCurrentDirectory(cd);
      }

      Logger.Success("Capacitor iOS is ready to go! - try: bionic platform capacitor ios open");
      return 0;
    }

    private static int NpxCapAddIOS() => ProcessHelper.RunCmd("npx", $"cap add ios");
  }
}