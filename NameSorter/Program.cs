// See https://aka.ms/new-console-template for more information
// <summary>
/// Main program orchestrating the name sorting workflow.
/// Demonstrates proper separation of concerns and dependency injection principles.
/// </summary>
public class Program
{
    private const string OutputFileName = "sorted-names-list.txt";

    private readonly IFileService _fileService;
    private readonly INameSortingService _nameSortingService;

    private readonly INameParsingService _nameParsingService;

    public Program(IFileService fileService, INameParsingService nameParsingService, INameSortingService nameSortingService)
    {
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        _nameParsingService = nameParsingService ?? throw new ArgumentNullException(nameof(nameParsingService));
        _nameSortingService = nameSortingService ?? throw new ArgumentNullException(nameof(nameSortingService));
    }

    public static async Task<int> Main(string[] args)
    {
        IFileService fileService = new FileService();
        INameParsingService nameParsingService = new NameParsingService();
        INameSortingService nameSortingService = new NameSortingService();

        var program = new Program(fileService, nameParsingService, nameSortingService);
        return await program.RunAsync(args);
    }


    // <summary>
    /// Main application workflow with comprehensive error handling and user feedback.
    /// </summary>
    public async Task<int> RunAsync(string[] args)
    {
        try
        {
            ValidateArguments(args);
            var inputFilePath = args[0];

            Console.WriteLine($"Reading names from: {inputFilePath}");
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

            Console.WriteLine($"\nSuccessfully sorted {sortedNames.Count()} names");
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            return 1;
        }
    }

    private static void ValidateArguments(string[] args)
    {
        if (args == null || args.Length != 1)
        {
            throw new Exception(
                $"Usage: name-sorter <input-file-path>\n" +
                $"Example: name-sorter ./unsorted-names-list.txt");
        }

        var inputFilePath = args[0];
        if (string.IsNullOrWhiteSpace(inputFilePath))
        {
            throw new Exception("Input file path cannot be empty.");
        }
    }
}
