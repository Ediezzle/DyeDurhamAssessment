using Microsoft.Extensions.Logging;
using NameSorter.Exceptions;

namespace NameSorter.Application;

public class NameSortingApp : INameSortingApp
{
    private readonly IFileService _fileService;

    private readonly INameParsingService _nameParsingService;

    private readonly INameSortingService _nameSortingService;

    private readonly ILogger<NameSortingApp> _logger;

    public NameSortingApp(IFileService fileService, INameParsingService nameParsingService, INameSortingService nameSortingService, ILogger<NameSortingApp> logger)
    {
        _fileService = fileService;
        _nameParsingService = nameParsingService;
        _nameSortingService = nameSortingService;
        _logger = logger;
    }

    // <summary>
    /// Main application workflow with comprehensive error handling and user feedback.
    /// </summary>
    public async Task RunAsync(string[] args)
    {
        _logger.LogInformation("Name Sorting App Started\n");

        try
        {
            ValidateArguments(args);
            var inputFilePath = args[0];

            Console.WriteLine($"Reading names from: {inputFilePath}\n");
            var unsortedNames = await _fileService.ReadAllLinesAsync(inputFilePath);

            Console.WriteLine($"Sorting {unsortedNames.Count()} names...");
            foreach (var name in unsortedNames)
            {
                Console.WriteLine(name);
            }

            var lines = await _fileService.ReadAllLinesAsync(args[0]);
            var people = _nameParsingService.Parse(lines);

            var sortedPeople = _nameSortingService.SortAsc(people);

            Console.WriteLine("\nSorted names:");
            foreach (var person in sortedPeople)
            {
                Console.WriteLine(person);
            }

            var sortedNames = sortedPeople.Select(p => p.ToString());

            await _fileService.WriteAllLinesAsync("sorted-names-list.txt", sortedNames);

            _logger.LogInformation("\nName Sorting App Completed");

            Console.WriteLine($"\nSuccessfully sorted {sortedNames.Count()} names");
        }
        catch (NameSorterException ex)
        {
            _logger.LogError($"ErrorEventArgs running program: {ex.StackTrace}");
            Console.WriteLine($"An Unexpected error occured: {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"ErrorEventArgs running program: {ex.StackTrace}");
            Console.WriteLine($"An Unexpected error occured");
        }
    }

    private static void ValidateArguments(string[] args)
    {
        if (args == null || args.Length != 1)
        {
            throw new NameSorterException(
                $"Usage: name-sorter <input-file-path>\n" +
                $"Example: name-sorter ./unsorted-names-list.txt");
        }

        var inputFilePath = args[0];
        if (string.IsNullOrWhiteSpace(inputFilePath))
        {
            throw new NameSorterException("Input file path cannot be empty.");
        }
    }
}
