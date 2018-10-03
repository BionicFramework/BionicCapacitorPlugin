using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;

// TODO: Move class to BionicCore lib
namespace BionicCapacitorPlugin.Utils {
  public class Helper {
    public static int RunCmd(string cmd, string args) {
      var process = Process.Start(
        new ProcessStartInfo(cmd, args) {
          CreateNoWindow = true,
          UseShellExecute = false,
          RedirectStandardOutput = false
        }
      );
      process?.WaitForExit();

      return process?.ExitCode ?? 1;
    }

    public static int RunDotNet(string args) {
      var process = Process.Start(DotNetExe.FullPathOrDefault(), args);
      process?.WaitForExit();

      return process?.ExitCode ?? 1;
    }

    public static int CopyAndRenameFolders(string cd) {
      var wwwroot = $"{cd}/wwwroot";
      var www = $"{cd}/platforms/capacitor/www";
      var release = $"{cd}/bin/Release/netstandard2.0/dist";
      var debug = $"{cd}/bin/Debug/netstandard2.0/dist";

      Directory.Delete(www, true);  
      Directory.CreateDirectory(www);
      
      if (!CopyDir(wwwroot, www)) {
        Console.WriteLine("☠  Please make sure your are in a Blazor Client or Standalone directory");
        return 1;
      }

      var result = CopyDir(release, www) || CopyDir(debug, www);
      if (!result) {
        Console.WriteLine(
          "☠  Unable to find compiled project or capacitor has not yet been initialized.\nPlease build your project first. e.g.: dotnet build");
        return 1;
      }
      RenameDirectories(www);

      return 0;
    }

    private static bool CopyDir(string sourceDirName, string destDirName, bool copySubDirs = true) {
      var dir = new DirectoryInfo(sourceDirName);

      if (!dir.Exists) return false;

      var dirs = dir.GetDirectories();
      if (!Directory.Exists(destDirName)) Directory.CreateDirectory(destDirName);

      var files = dir.GetFiles();
      foreach (var file in files) {
        var destPath = Path.Combine(destDirName, file.Name);
        file.CopyTo(destPath, true);
      }

      if (!copySubDirs) return true;

      return !(
        from subdir in dirs
        let destPath = Path.Combine(destDirName, subdir.Name)
        where !CopyDir(subdir.FullName, destPath, copySubDirs)
        select subdir).Any();
    }

    private static void RenameDirectories(string www) {
      Directory.Move($"{www}/_framework", $"{www}/framework");
      Directory.Move($"{www}/framework/_bin", $"{www}/framework/bin");

      var contentDir = $"{www}/_content";
      if (Directory.Exists(contentDir)) {
        Directory.Move(contentDir, $"{www}/content");
      }

      UpdateFile($"{www}/framework/blazor.server.js");
      UpdateFile($"{www}/framework/blazor.webassembly.js");
      UpdateFile($"{www}/framework/blazor.boot.json");
      UpdateFile($"{www}/index.html");
    }

    private static void UpdateFile(string path) {
      var text = File.ReadAllText(path);
      text = text.Replace("_framework", "framework");
      text = text.Replace("_bin", "bin");
      text = text.Replace("_content", "content");
      File.WriteAllText(path, text);
    }
  }
}