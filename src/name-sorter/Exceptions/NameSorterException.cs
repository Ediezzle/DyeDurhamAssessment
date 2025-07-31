namespace NameSorter.Exceptions
{
    /// <summary>
    /// Domain-specific exception for name sorting operations.
    /// Provides clear error context for application-specific failures.
    /// </summary>
    public class NameSorterException : Exception
    {
        public NameSorterException(string message) : base(message) { }
        
        public NameSorterException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}