public class PersonTests
{
    [Fact]
    public void Constructor_SplitsGivenNamesAndLastNameCorrectly()
    {
        var person = new Person("Edmore Tatenda Mandikiyana");

        Assert.Equal("Mandikiyana", person.LastName);
        Assert.Equal(new List<string> { "Edmore", "Tatenda" }, person.GivenNames);
    }

    [Fact]
    public void ToString_ReturnsCorrectFullName()
    {
        var person = new Person("Edmore Tatenda Mandikiyana");

        Assert.Equal("Edmore Tatenda Mandikiyana", person.ToString());
    }
}
