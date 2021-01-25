using BionicPlugin;
using McMaster.Extensions.CommandLineUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "android", Description = "Capacitor Android commands")]
  [Subcommand(typeof(AndroidBuildCommand))]
  [Subcommand(typeof(AndroidInitCommand))]
  [Subcommand(typeof(AndroidOpenCommand))]
  [Subcommand(typeof(AndroidSyncCommand))]
  public class AndroidCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Init(app);

    private static int Init(CommandLineApplication app) {
      app.ShowHelp();
      return 0;
    }
  }
}