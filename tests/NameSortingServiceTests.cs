public class NameSorterTests
{
    [Fact]
    public void CompareTo_SortsByLastNameThenGivenNames()
    {
        var person1 = new Person("Edmore Mandikiyana");
        var person2 = new Person("Joyce Lucas Adrian");

        Assert.True(person2.CompareTo(person1) < 0);
        Assert.True(person1.CompareTo(person2) > 0);
    }

    [Fact]
    public void CompareTo_WithSameLastName_SortsByGivenNames()
    {
        var person1 = new Person("Edmore Mandikiyana");
        var person2 = new Person("Tatenda Mandikiyana");

        Assert.True(person1.CompareTo(person2) < 0);
    }

    [Fact]
    public void CompareTo_ReturnsPositive_WhenOtherIsNull()
    {
        var person = new Person("Aubrey Mandikiyana");

        Assert.Equal(1, person.CompareTo(null));
    }

    [Fact]
    public void Sort_SortsListCorrectly()
    {
        var names = new List<string>
        {
            "Janet Parsons",
            "Vaughn Lewis",
            "Adonis Julius Archer",
            "Shelby Nathan Yoder"
        };

        var people = names.Select(name => new Person(name)).ToList();
        people.Sort();

        var expectedOrder = new List<string>
        {
            "Adonis Julius Archer",
            "Vaughn Lewis",
            "Janet Parsons",
            "Shelby Nathan Yoder"
        };

        var sortedNames = people.Select(p => p.ToString()).ToList();
        Assert.Equal(expectedOrder, sortedNames);
    }
}
