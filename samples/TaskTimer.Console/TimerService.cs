namespace TaskTimer.Console;

public class TimerService : ITimerService
{
    private readonly List<TimerSession> _sessions = new();
    private CancellationTokenSource? _cancellationTokenSource;
    private bool _isStopped = false;

    public async Task StartTimer(TimeSpan duration, string taskName)
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _isStopped = false;
        
        var session = new TimerSession
        {
            StartTime = DateTime.Now,
            Duration = duration,
            TaskName = taskName,
            Completed = false
        };

        try
        {
            await Task.Delay(duration, _cancellationTokenSource.Token);
            
            if (!_isStopped)
            {
                session.Completed = true;
                System.Console.Beep();
                System.Console.WriteLine("\n\n✓ Timer completed!");
            }
        }
        catch (TaskCanceledException)
        {
            session.Completed = false;
        }
        finally
        {
            _sessions.Add(session);
        }
    }

    public void StopTimer()
    {
        _isStopped = true;
        _cancellationTokenSource?.Cancel();
    }

    public IEnumerable<TimerSession> GetSessionHistory()
    {
        return _sessions.AsReadOnly();
    }
}
