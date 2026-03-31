using System;

namespace TaskTimer.Console;

public class TimerService : ITimerService
{
    private readonly IDisplayService _displayService;
    private readonly List<TimerSession> _sessions = new();
    private bool _isStopped;

    public TimerService(IDisplayService displayService)
    {
        _displayService = displayService;
    }

    public async Task StartTimer(TimeSpan duration, string taskName)
    {
        _isStopped = false;
        var session = new TimerSession
        {
            StartTime = DateTime.Now,
            Duration = duration,
            TaskName = taskName,
            Completed = false
        };

        System.Console.WriteLine($"\nStarting: {taskName} ({duration.TotalMinutes} minutes)");
        System.Console.WriteLine("Press any key to stop...\n");

        var startTime = DateTime.Now;
        var endTime = startTime.Add(duration);

        while (DateTime.Now < endTime && !_isStopped)
        {
            var remaining = endTime - DateTime.Now;
            _displayService.ShowProgress(remaining, duration);
            
            if (System.Console.KeyAvailable)
            {
                System.Console.ReadKey(true);
                _isStopped = true;
                System.Console.WriteLine("\n\nTimer stopped by user.");
                break;
            }

            await Task.Delay(1000);
        }

        if (!_isStopped)
        {
            session.Completed = true;
            System.Console.WriteLine("\n\nTimer completed! ");
            System.Console.Beep();
        }

        _sessions.Add(session);
        System.Console.WriteLine("\nPress any key to continue...");
        System.Console.ReadKey(true);
    }

    public void StopTimer()
    {
        _isStopped = true;
    }

    public IEnumerable<TimerSession> GetSessionHistory()
    {
        return _sessions;
    }
}
