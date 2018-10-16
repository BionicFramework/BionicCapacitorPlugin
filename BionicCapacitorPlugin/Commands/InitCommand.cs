using System;
using System.IO;
using BionicCLI;
using BionicCore;
using BionicPlugin;
using McMaster.Extensions.CommandLineUtils;
using static BionicCore.DirectoryUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "init", Description = "Initialize Capacitor project structure")]
  public class InitCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Init();

    [Option("-a|--app-name", Description = "App Name")]
    private static string _appName { get; set; }

    [Option("-p|--package", Description = "Package (Java like, e.g.: com.bionic.framework.myapp)")]
    private static string _package { get; set; }

    private static int Init() {

      while (_appName == null) _appName = Prompt.GetString("App Name: ", promptColor: ConsoleColor.DarkGreen);

      while (_package == null) _package = Prompt.GetString("Package (Java like, e.g.: com.bionic.framework.myapp): ", promptColor: ConsoleColor.DarkGreen);

      Logger.Preparing("Initializing Capacitor...");

      // TODO: Check if required tools (npm/npx) are available
      
      // TODO: Check if already installed
      DotNetHelper.RunDotNet("new -i BionicCapacitorTemplate");

      // TODO: Check if platforms/capacitor already exists
      DotNetHelper.RunDotNet("new bionic.capacitor -o platforms");

      var cd = Directory.GetCurrentDirectory();
      try {
        Directory.SetCurrentDirectory(ToOSPath($"{cd}/platforms/capacitor"));
        NpmInstall();
        NpxCapInit();
      }
      catch (Exception) {
        Logger.Error("Something went wrong during Capacitor install. Please check platforms/capacitor");
        return 1;
      }
      finally {
        Directory.SetCurrentDirectory(cd);
      }

      Logger.Success("Capacitor is ready to go! - try: bionic platform capacitor");
      return 0;
    }

    private static int NpmInstall() => ProcessHelper.RunCmd("npm", $"install");

    private static int NpxCapInit() => ProcessHelper.RunCmd("npx", $"cap init {_appName} {_package}");
  }
}