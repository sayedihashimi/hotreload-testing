using TaskTimer.Console;

var timerService = new TimerService();
var displayService = new DisplayService();

var presets = new Dictionary<string, TimerPreset>
{
    ["pomodoro"] = new TimerPreset 
    { 
        Name = "Pomodoro", 
        WorkDuration = TimeSpan.FromMinutes(25), 
        BreakDuration = TimeSpan.FromMinutes(5) 
    },
    ["long"] = new TimerPreset 
    { 
        Name = "Long Session", 
        WorkDuration = TimeSpan.FromMinutes(50), 
        BreakDuration = TimeSpan.FromMinutes(10) 
    }
};

while (true)
{
    displayService.ShowMenu();
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            await RunTimer(presets["pomodoro"].WorkDuration, "Pomodoro Session");
            break;

        case "2":
            await RunTimer(presets["long"].WorkDuration, "Long Session");
            break;

        case "3":
            Console.Write("Enter duration in minutes: ");
            if (int.TryParse(Console.ReadLine(), out var minutes) && minutes > 0)
            {
                Console.Write("Enter task name: ");
                var taskName = Console.ReadLine() ?? "Custom Task";
                await RunTimer(TimeSpan.FromMinutes(minutes), taskName);
            }
            else
            {
                Console.WriteLine("Invalid duration. Press any key to continue...");
                Console.ReadKey();
            }
            break;

        case "4":
            displayService.ShowSessionSummary(timerService.GetSessionHistory());
            break;

        case "5":
            Console.WriteLine("Goodbye!");
            return;

        default:
            Console.WriteLine("Invalid option. Press any key to continue...");
            Console.ReadKey();
            break;
    }
}

async Task RunTimer(TimeSpan duration, string taskName)
{
    Console.Clear();
    Console.WriteLine($"=== {taskName} ===");
    Console.WriteLine($"Duration: {duration:mm\\:ss}");
    Console.WriteLine("\nPress 'S' to stop the timer early\n");

    var timerTask = Task.Run(async () =>
    {
        await timerService.StartTimer(duration, taskName);
    });

    var progressTask = Task.Run(async () =>
    {
        var startTime = DateTime.Now;
        while (!timerTask.IsCompleted)
        {
            var elapsed = DateTime.Now - startTime;
            var remaining = duration - elapsed;
            
            if (remaining < TimeSpan.Zero)
                remaining = TimeSpan.Zero;
            
            displayService.ShowProgress(remaining, duration);
            await Task.Delay(1000);
        }
    });

    var inputTask = Task.Run(() =>
    {
        while (!timerTask.IsCompleted)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.S)
                {
                    timerService.StopTimer();
                    Console.WriteLine("\n\nTimer stopped.");
                    break;
                }
            }
            Thread.Sleep(100);
        }
    });

    await timerTask;
    await progressTask;

    Console.WriteLine("\n\nPress any key to continue...");
    Console.ReadKey();
}
