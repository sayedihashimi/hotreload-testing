namespace BookBuddy.RazorPages.Services;

public class ReadingStats
{
    public int TotalBooks { get; set; }
    public int BooksRead { get; set; }
    public int BooksReading { get; set; }
    public int BooksWantToRead { get; set; }
    public int TotalPagesRead { get; set; }
    public int TotalMinutesRead { get; set; }
    public double AverageRating { get; set; }
    public int CurrentYearBooksFinished { get; set; }
    public int CurrentYearPagesRead { get; set; }
}

public interface IReadingStatsService
{
    Task<ReadingStats> GetReadingStatsAsync();
    Task<ReadingStats> GetYearStatsAsync(int year);
    Task<Dictionary<string, int>> GetGenreDistributionAsync();
    Task<List<(DateTime Date, int Pages)>> GetReadingHistoryAsync(int days = 30);
}
