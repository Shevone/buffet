namespace LabWork27_buffet.Models.Persons;

public abstract class Person
{
    private PasportData _pasportData;
    protected Person(string name)
    {
        _pasportData = new PasportData(name);
    }

    public string Name => _pasportData.Name;

    public abstract string Greetings();
}