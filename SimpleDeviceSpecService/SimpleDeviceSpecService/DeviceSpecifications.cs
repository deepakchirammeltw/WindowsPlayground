using System;
using System.Timers;
using System.Management;
using Timer = System.Timers.Timer;

namespace SimpleDeviceSpecService;

public class DeviceSpecifications
{
    private readonly Timer _timer;

    public DeviceSpecifications()
    {
        _timer = new Timer(10*1000) { AutoReset = true };
        _timer.Elapsed += TimerElapsed;
    }

    private void TimerElapsed(object? sender, ElapsedEventArgs e)
    {
        Console.Out.WriteLine("Time:" + DateTime.Now);

        var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
        foreach (ManagementObject queryObj in searcher.Get())
        {
            Console.WriteLine("Processor Name: " + queryObj["Name"]);
            Console.WriteLine("Manufacturer: " + queryObj["Manufacturer"]);
            Console.WriteLine("Max Clock Speed: " + queryObj["MaxClockSpeed"]);
        }

        return;
        var managementObjectSearcher = new ManagementObjectSearcher("SELECT LastBootUpTime FROM Win64_OperatingSystem");
        var managementObjectCollection = managementObjectSearcher.Get();
        Console.Out.WriteLine("Count:" + managementObjectCollection.Count);
        if (managementObjectCollection.Count > 0)
        {
            var specs = new List<string>();
            foreach (var managementObject in managementObjectCollection)
            {
                var bootTime = ManagementDateTimeConverter.ToDateTime(managementObject["LastBootUpTime"].ToString());
                specs.Add("LastBootUpTime:" + bootTime);
                Console.Out.WriteLine("LastBootUpTime:" + bootTime);
            }
            File.AppendAllLines(@"C:\Temp\DeviceSpec.txt", specs.ToArray());
        }
    }

    public void Start()
    {
        _timer.Start();
    }

    public void Stop()
    {
        _timer.Stop();
    }
}