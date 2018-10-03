using McMaster.Extensions.CommandLineUtils;

namespace BionicCapacitorPlugin.Commands {
  [Command(Name = "bridge", Description = "Capacitor Bridge for device's API access")]
  [Subcommand("init", typeof(BridgeInitCommand))]
  public class BridgeCommand : CommandBase {
    protected override int OnExecute(CommandLineApplication app) => Init(app);

    private static int Init(CommandLineApplication app) {
      app.ShowHelp();
      return 0;
    }
  }
}