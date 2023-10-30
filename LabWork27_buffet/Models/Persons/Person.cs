namespace LabWork27_buffet.Models.Persons;

public abstract class Person
{
    protected Person(string name)
    {
        Name = name;
    }
    public string Name { get; }

    public abstract string Greetings();
    public abstract string GetInfo();
    
    public override string ToString()
    {
        return $"{Name}";
    }
}