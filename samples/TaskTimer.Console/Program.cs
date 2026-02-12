using TaskTimer.Console;

var displayService = new DisplayService();
var timerService = new TimerService(displayService);

while (true)
{
    displayService.ShowMenu();
    var input = Console.ReadLine();

    switch (input)
    {
        case "1":
            await timerService.StartTimer(TimeSpan.FromMinutes(25), "Pomodoro Session");
            break;
        case "2":
            await timerService.StartTimer(TimeSpan.FromMinutes(50), "Long Session");
            break;
        case "3":
            Console.Write("Enter duration in minutes: ");
            if (int.TryParse(Console.ReadLine(), out int minutes))
            {
                Console.Write("Enter task name: ");
                var taskName = Console.ReadLine() ?? "Custom Task";
                await timerService.StartTimer(TimeSpan.FromMinutes(minutes), taskName);
            }
            break;
        case "4":
            displayService.ShowSessionSummary(timerService.GetSessionHistory());
            break;
        case "5":
            Console.WriteLine("Goodbye!");
            return;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}
