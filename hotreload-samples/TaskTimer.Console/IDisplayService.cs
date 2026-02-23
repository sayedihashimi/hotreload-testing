namespace TaskTimer.Console;

public interface IDisplayService
{
    void ShowProgress(TimeSpan remaining, TimeSpan total);
    void ShowMenu();
    void ShowSessionSummary(IEnumerable<TimerSession> sessions);
}
