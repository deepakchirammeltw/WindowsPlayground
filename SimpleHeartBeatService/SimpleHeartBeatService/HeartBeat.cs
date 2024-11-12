using System.Timers;
using Timer = System.Timers.Timer;

namespace SimpleHeartBeatService;

public class HeartBeat
{
    private readonly Timer _timer;

    public HeartBeat()
    {
        _timer = new Timer(1000) { AutoReset = true };
        _timer.Elapsed += TimerElapsed;
    }

    private void TimerElapsed(object? sender, ElapsedEventArgs e)
    {
        string[] lines = new string[] { DateTime.Now.ToString() };
        File.AppendAllLines(@"C:\Temp\HeartBeat.txt", lines);
        // Console.Out.WriteLine("HeartBeat:" + DateTime.Now);
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