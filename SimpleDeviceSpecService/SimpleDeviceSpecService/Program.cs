// See https://aka.ms/new-console-template for more information

// Console.WriteLine("Hello, World!");

using SimpleDeviceSpecService;
using Topshelf;

var exitCode = HostFactory.Run(configurator =>
{
    configurator.Service<DeviceSpecifications>(serviceConfigurator =>
    {
        serviceConfigurator.ConstructUsing(deviceSpecs => new DeviceSpecifications());
        serviceConfigurator.WhenStarted(deviceSpecs => deviceSpecs.Start());
        serviceConfigurator.WhenStopped(deviceSpecs => deviceSpecs.Stop());
    });
    configurator.RunAsLocalSystem();
    configurator.SetServiceName("SimpleDeviceSpecService");
    configurator.SetDisplayName("Simple Device Specifications Service");
    configurator.SetDescription("This is a simple device specifications service");
});
int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
Environment.ExitCode = exitCodeValue;