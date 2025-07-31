public interface IFileService
{
    /// <summary>
    /// Reads all lines from a file asynchronously.
    /// </summary>
    /// <param name="filePath">Path to the file to read</param>
    /// <returns>Collection of lines from the file</returns>
    Task<IEnumerable<string>> ReadAllLinesAsync(string filePath);

    /// <summary>
    /// Writes all lines to a file asynchronously.
    /// </summary>
    /// <param name="filePath">Path to the file to write</param>
    /// <param name="lines">Lines to write to the file</param>
    Task WriteAllLinesAsync(string filePath, IEnumerable<string> lines);
}