/// <summary>
/// Concrete implementation of file operations with proper error handling.
/// </summary>
public class FileService : IFileService
{
    public async Task<IEnumerable<string>> ReadAllLinesAsync(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                throw new Exception($"File not found: {filePath}");

            return await File.ReadAllLinesAsync(filePath);
        }
        catch (IOException ex)
        {
            throw new Exception($"Error reading file '{filePath}': {ex.Message}", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new Exception($"Access denied reading file '{filePath}': {ex.Message}", ex);
        }
    }

    public async Task WriteAllLinesAsync(string filePath, IEnumerable<string> lines)
    {
        try
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            await File.WriteAllLinesAsync(filePath, lines);
        }
        catch (IOException ex)
        {
            throw new Exception($"Error writing file '{filePath}': {ex.Message}", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new Exception($"Access denied writing file '{filePath}': {ex.Message}", ex);
        }
    }
}

        