using LabWork27_buffet.Models;
using LabWork27_buffet.Models.Persons;

namespace LabWork27_buffet.Serivce;

public class BaseService
{
    private readonly List<Product> _products;
    private readonly List<Table?> _tables;
    private readonly List<Employee> _employees;
    private readonly List<Visitor?> _visitors;

    private readonly IReader _reader;
    

    public BaseService(IReader reader)
    {
        _reader = reader;
        _products = new List<Product>();
        _tables = new List<Table?>();
        _employees = new List<Employee>();
        _visitors = new List<Visitor?>();
        Start(5);
    }
    // Создаем со старта 5 столов и одного сотрудника
    private  void Start(int countTables)
    {
        var manager = new Employee("Сарший менеджер", 50000);

        for (var i = 0; i < 5; i++)
        {
            _tables.Add(new Table(manager));
        }
    }
    // Поменять обслуживающего
    public void ChangeServiceManOfTable()
    {
        // выбираем стол из всех не занятых столов
        var table = _reader.GetItemFromList(_tables.Where(x => x?.IsBusy == false).ToList(), "Выберете стоилк для смены обслуживающего");
        if (table == default)
        {
            return;
        }
        // Выбираем
        var employee = _reader.GetItemFromList(_employees, "Выберете  для этого стола");
        if (employee == default)
        {
            return;
        }
        table.ChangeServiceMan(employee);
    }
    // добавление созданного человека для взаимодействия
    public void AddNewPerson(Peron newPerson)
    {
        Console.WriteLine(newPerson.Greetings());
        // Добваляем в спиок по типу
        switch (newPerson)
        {
            case Employee emp:
                _employees.Add(emp);
                return;
            case Visitor visitor:
                _visitors.Add(visitor);
                return;
            default:
                return;
        }
    }
    // Добавить новое блюдо
    public void AddNewProduct(Product newProduct)
    {
        _products.Add(newProduct);
    }
    // Добавить стол
    public void AddTable()
    {
        // Выбираем сотрудника чтоб приставтить его к этому столу
        var employee = _reader.GetItemFromList(_employees, "Выберете сотрудника, который будет обслуживать данный стол");
        if (employee != null)
        {
            _tables.Add(new Table(employee));
        }
    }
    public void SitPersonsToTable(int visCount)
    {
        // Усадить людей за стол
        // Если количество людей введено не корректно, то меняем его
        if (_visitors.Count < visCount)
        {
            visCount = _visitors.Count;
        }
        if (visCount <= 0) visCount = 1;
        var curVisitors = new List<Visitor>();
        for (int i = 0; i < visCount; i++)
        {
            var visitor = _reader.GetItemFromList(_visitors.Where(x=>x?.IsGetTable == false).ToList(), "Выберете посетителя чтоб добавить к столику");
            if (visitor == default) break; // Если выбор отменяется, то просто сохраним как есть
            visitor.IsGetTable = true;
            curVisitors.Add(visitor);
        }
        var table = _reader.GetItemFromList(_tables.Where(x => x?.IsBusy == false).ToList(), "Выберете стоилк чтоб усадить туда посетителей");
        table?.SetVisitors(curVisitors);
    }
    // Очищаем стол
    public void ClearTable()
    {
        var table = _reader.GetItemFromList(_tables.Where(x => x!.IsBusy).ToList(), "Выберете стол с которого уходят гости");
        table?.ClearTheTable();
    }
    // Сделать заказ
    public void MakeOrder(int countProduct)
    {
        if (countProduct > _products.Count)
        {
            countProduct = _products.Count;
        }
        if (countProduct <= 0) countProduct = 1;
        // Выбираем столик чтоб принять закз
        var table = _reader.GetItemFromList(_tables.Where(x => x?.IsBusy == true).ToList(), "Выберете стоилк для того чтобы принять заказ");
        if (table == default)
        {
            return;
        }
        // Выбираем блюда для заказа
        var prodToOrder = new List<Product>();
        for (int i = 0; i < countProduct; i++)
        {
            var product = _reader.GetItemFromList(_products.Except(prodToOrder).ToList(), "Выберете блюда для столика");
            if (product == default) break;
            prodToOrder.Add(product);
        }
        table.MakeOrder(prodToOrder);
    }
    public void GetAllInfoAboutTables()
    {
        foreach (var table in _tables)
        {
            Console.WriteLine(table);
        }
    }
}