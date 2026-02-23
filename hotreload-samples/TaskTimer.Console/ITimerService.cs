namespace TaskTimer.Console;

public interface ITimerService
{
    Task StartTimer(TimeSpan duration, string taskName);
    void StopTimer();
    IEnumerable<TimerSession> GetSessionHistory();
}
