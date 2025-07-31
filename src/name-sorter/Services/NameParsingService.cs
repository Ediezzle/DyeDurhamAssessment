public class NameParsingService : INameParsingService
{
    public IEnumerable<Person> Parse(IEnumerable<string> lines)
        => lines.Select(line => new Person(line));
}
