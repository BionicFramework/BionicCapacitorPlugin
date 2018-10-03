using McMaster.Extensions.CommandLineUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "android", Description = "Capacitor Android commands")]
  [Subcommand("build", typeof(AndroidBuildCommand))]
  [Subcommand("init", typeof(AndroidInitCommand))]
  [Subcommand("open", typeof(AndroidOpenCommand))]
  [Subcommand("sync", typeof(AndroidSyncCommand))]
  public class AndroidCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Init(app);

    private static int Init(CommandLineApplication app) {
      app.ShowHelp();
      return 0;
    }
  }
}