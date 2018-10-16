using BionicCapacitorPlugin.Commands;
using BionicPlugin;
using McMaster.Extensions.CommandLineUtils;

namespace BionicCapacitorPlugin {
  public class CapacitorPlatformPlugin : ICommandPlugin {
    public string CommandName { get; } = "capacitor";

    public void Initialize(CommandLineApplication app) {
      
      app.Commands.Add(new CommandLineApplication<InitCommand>());
      app.Commands.Add(new CommandLineApplication<AndroidCommand>());
      app.Commands.Add(new CommandLineApplication<IOSCommand>());
      app.Commands.Add(new CommandLineApplication<BridgeCommand>());

      app.OnExecute(() => app.ShowHelp());
    }

    public int Execute() {
      return 0;
    }

    public int OnExecute(CommandLineApplication app) => Execute();
  }
}