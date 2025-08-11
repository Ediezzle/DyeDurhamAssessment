

public class Person : IComparable<Person>
{
    public List<string> GivenNames { get; }
    public string LastName { get; }

    public Person(string fullName)
    {
        var parts = fullName.Split(' ');
        LastName = parts.Last();
        GivenNames = parts.Take(parts.Length - 1).ToList();
    }

    public int CompareTo(Person? other)
    {
        if (other == null)
            return 1;

        int lastNameCompare = LastName.CompareTo(other.LastName);
        return lastNameCompare != 0
            ? lastNameCompare
            : string.Join(" ", GivenNames).CompareTo(string.Join(" ", other.GivenNames));
    }

    public override string ToString() => string.Join(" ", GivenNames.Concat(new[] { LastName }));
}
