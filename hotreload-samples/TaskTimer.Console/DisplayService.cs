using System;

namespace TaskTimer.Console;

public class DisplayService : IDisplayService
{
    public void ShowProgress(TimeSpan remaining, TimeSpan total)
    {
        var percentage = 1 - (remaining.TotalSeconds / total.TotalSeconds);
        var barWidth = 40;
        var filled = (int)(barWidth * percentage);
        var empty = barWidth - filled;

        System.Console.SetCursorPosition(0, System.Console.CursorTop);
        System.Console.Write($"[{new string('█', filled)}{new string('░', empty)}] ");
        System.Console.Write($"{remaining:mm\\:ss} remaining ");
    }

    public void ShowMenu()
    {
        System.Console.Clear();
        System.Console.WriteLine("===================");
        System.Console.WriteLine("=== TaskTimer ===");
        System.Console.WriteLine("===================");
        System.Console.WriteLine();
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
        System.Console.WriteLine("======================");
        System.Console.WriteLine("=== Session History ===");
        System.Console.WriteLine("======================");
        System.Console.WriteLine();

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
                System.Console.WriteLine($"  Duration: {session.Duration.TotalMinutes} min - {status}");
                System.Console.WriteLine();
            }

            var completed = sessions.Count(s => s.Completed);
            var total = sessions.Count();
            System.Console.WriteLine($"Total Sessions: {total} | Completed: {completed}");
        }

        System.Console.WriteLine();
        System.Console.WriteLine("Press any key to return to menu...");
        System.Console.ReadKey(true);
    }
}
