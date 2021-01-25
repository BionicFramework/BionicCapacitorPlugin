using BionicPlugin;
using McMaster.Extensions.CommandLineUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "ios", Description = "Capacitor iOS commands")]
  [Subcommand(typeof(IOSBuildCommand))]
  [Subcommand(typeof(IOSInitCommand))]
  [Subcommand(typeof(IOSOpenCommand))]
  [Subcommand(typeof(IOSSyncCommand))]
  public class IOSCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Init(app);

    private static int Init(CommandLineApplication app) {
      app.ShowHelp();
      return 0;
    }
  }
}