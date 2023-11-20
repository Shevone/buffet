namespace LabWork27_buffet.Models.Persons;

public class Visitor : Person
{
    public Visitor(string name) : base(name)
    {
        IsGetTable = false;
    }
    public bool IsGetTable { get; set; }
    public override string ToString()
    {
        return $"Посетитель {Name}, Cидит ли за столиком {IsGetTable}";
    }

    public override string Greetings()
    {
        return $"Здравствуйте, у вас есть свободные столики....";
    }
}