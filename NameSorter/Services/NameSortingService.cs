public class NameSortingService : INameSortingService
{
    public IEnumerable<Person> SortAsc(IEnumerable<Person> people)
        => people.OrderBy(p => p);
}
