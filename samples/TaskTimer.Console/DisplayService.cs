namespace TaskTimer.Console;

public class DisplayService : IDisplayService
{
    public void ShowProgress(TimeSpan remaining, TimeSpan total)
    {
        var percentage = 1.0 - (remaining.TotalSeconds / total.TotalSeconds);
        var barWidth = 40;
        var filledWidth = (int)(percentage * barWidth);
        
        var bar = new string('█', filledWidth) + new string('░', barWidth - filledWidth);
        
        System.Console.SetCursorPosition(0, System.Console.CursorTop);
        System.Console.Write($"[{bar}] {remaining:mm\\:ss} remaining ");
    }

    public void ShowMenu()
    {
        System.Console.Clear();
        System.Console.WriteLine("=== TaskTimer ===");
        System.Console.WriteLine("1. Start Pomodoro (25 min)");
        System.Console.WriteLine("2. Start Long Session (50 min)");
        System.Console.WriteLine("3. Custom Timer");
        System.Console.WriteLine("4. View Session History");
        System.Console.WriteLine("5. Exit");
        System.Console.WriteLine();
        System.Console.Write("Select option: ");
    }

    public void ShowSessionSummary(IEnumerable<TimerSession> sessions)
    {
        System.Console.Clear();
        System.Console.WriteLine("=== Session History ===\n");
        
        if (!sessions.Any())
        {
            System.Console.WriteLine("No sessions recorded yet.");
        }
        else
        {
            foreach (var session in sessions)
            {
                var status = session.Completed ? "✓ Completed" : "✗ Stopped";
                System.Console.WriteLine($"{session.StartTime:yyyy-MM-dd HH:mm} - {session.TaskName}");
                System.Console.WriteLine($"  Duration: {session.Duration:mm\\:ss} - {status}\n");
            }
            
            var totalCompleted = sessions.Count(s => s.Completed);
            var totalTime = TimeSpan.FromTicks(sessions.Sum(s => s.Duration.Ticks));
            System.Console.WriteLine($"Total Sessions: {sessions.Count()} | Completed: {totalCompleted} | Total Time: {totalTime:hh\\:mm\\:ss}");
        }
        
        System.Console.WriteLine("\nPress any key to continue...");
        System.Console.ReadKey();
    }
}
