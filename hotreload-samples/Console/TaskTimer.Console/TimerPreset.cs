namespace TaskTimer.Console;

public class TimerPreset
{
    public string Name { get; set; } = string.Empty;
    public TimeSpan WorkDuration { get; set; }
    public TimeSpan BreakDuration { get; set; }
}
