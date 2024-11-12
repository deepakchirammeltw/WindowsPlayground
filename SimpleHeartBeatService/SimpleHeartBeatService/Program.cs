// See https://aka.ms/new-console-template for more information

// Console.WriteLine("Hello, World!");

using SimpleHeartBeatService;
using Topshelf;

var exitCode = HostFactory.Run(configurator =>
{
    configurator.Service<HeartBeat>(serviceConfigurator =>
    {
        serviceConfigurator.ConstructUsing(heartbeat => new HeartBeat());
        serviceConfigurator.WhenStarted(heartbeat => heartbeat.Start());
        serviceConfigurator.WhenStopped(heartbeat => heartbeat.Stop());
    });
    configurator.RunAsLocalSystem();
    configurator.SetServiceName("SimpleHeartBeatService");
    configurator.SetDisplayName("Simple HeartBeat Service");
    configurator.SetDescription("This is a simple heartbeat service");
});
int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
Environment.ExitCode = exitCodeValue;