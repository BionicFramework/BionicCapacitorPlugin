using McMaster.Extensions.CommandLineUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "ios", Description = "Capacitor iOS commands")]
  [Subcommand("build", typeof(IOSBuildCommand))]
  [Subcommand("init", typeof(IOSInitCommand))]
  [Subcommand("open", typeof(IOSOpenCommand))]
  [Subcommand("sync", typeof(IOSSyncCommand))]
  public class IOSCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Init(app);

    private static int Init(CommandLineApplication app) {
      app.ShowHelp();
      return 0;
    }
  }
}