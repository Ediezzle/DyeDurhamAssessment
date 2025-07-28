public interface INameParsingService
{
    IEnumerable<Person> Parse(IEnumerable<string> lines);
}
