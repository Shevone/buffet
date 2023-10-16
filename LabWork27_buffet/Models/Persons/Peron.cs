namespace LabWork27_buffet.Models.Persons;

public abstract class Peron
{
    protected Peron(string name)
    {
        Name = name;
    }
    public string Name { get; }

    public abstract string Greetings();
}