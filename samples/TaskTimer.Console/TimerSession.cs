namespace TaskTimer.Console;

public class TimerSession
{
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public string TaskName { get; set; } = string.Empty;
    public bool Completed { get; set; }
}
