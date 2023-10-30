using System.Text;
using LabWork27_buffet.Models.Persons;

namespace LabWork27_buffet.Models;

public class Table
{
    private int Id;
    private List<Product> _order;
    private List<Visitor> _visitors;
    private Employee ServicePerson { get; set; }
    private static int _freeId = 0; // Следующий айди
    private static int SetNextId // Свойство для установки след айди
    {
        get
        {
            if (_freeId == 0)
            {
                _freeId = 1;
            }
            var i = _freeId;
            _freeId = i + 1; // увеличиваем следующий айди на едииницу
            return i;
        }
        set => _freeId = value >= 1 ? value : 1;
    }
    public bool IsBusy { get; set; }

    public Table(Employee servicePerson)
    {
        ServicePerson = servicePerson;
        servicePerson.ServeOneMoreTable();
        _order = new List<Product>();
        _visitors = new List<Visitor>();
        Id = SetNextId;
        IsBusy = false;
    }

    public void ChangeServiceMan(Employee newServiceMan)
    {
        newServiceMan.ServeOneMoreTable();
        ServicePerson = newServiceMan;
    }
    public void ClearTheTable()
    {
        IsBusy = false;
        _order.Clear();
        foreach (var visitor in _visitors)
        {
            visitor.IsGetTable = false;
        }
        _visitors.Clear();
    }
    public bool SetVisitors(List<Visitor> newVisitors)
    {
        if (IsBusy) return false;
        _visitors = newVisitors;
        foreach (var visitor in newVisitors)
        {
            visitor.IsGetTable = true;
        }
        IsBusy = true;
        return true;
    }
    public bool MakeOrder(List<Product> prodToOrder)
    {
        if (!IsBusy) return false;
        _order = prodToOrder;
        return true;
    }

    public override string ToString()
    {
        return $"-{Id} - обслуживает {ServicePerson}";
    }

    public  string Info()
    {
        var sb = new StringBuilder($"Столик {Id}\nОбслуживающий : {ServicePerson}\nТекущий заказ\n");
        foreach (var product in _order)
        {
            sb.Append($"- {product}\n");
        }
        sb.Append($"Текущие Посетители \n");
        foreach (var visitor in _visitors)
        {
            sb.Append($"- {visitor}\n");
        }
        return sb.ToString();
    }
}