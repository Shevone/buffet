using System.Text;
using LabWork27_buffet.Models;
using LabWork27_buffet.Models.Persons;

namespace LabWork27_buffet.Serivce;

public class BaseService
{
    private readonly Buffet _buffet;
    private readonly IReader _reader;
    

    public BaseService(IReader reader, Buffet buffet)
    {
        _buffet = buffet;
        _reader = reader;
    }

    public void Run()
    {
        var menuItems = new List<string>()
        {
            "Создать работника", "Создать посетителя", "Создать столик",
            "Посадить посетителей за столик", "Сделать заказ на столик",
            "Очистить столик", "Сменить работника, обслуживающего столик",
            "Посмотреть информацию об объектах"
        };
       

        while (true)
        {
            var index = _reader.SelectMenu(menuItems, "Главное меню");
            switch (index)
            {
                case -1:
                    _reader.Message("Выбран выход");
                    return;
                case 0:
                    // emp creating
                    var empName = _reader.GetStringFromConsole("Введите имя сотрудника");
                    var salary = _reader.GetIntFromConsole("Введите з/п сотрудника") ?? 0;
                    var employee = new Employee(empName, salary);
                    _buffet.AddNewPerson(employee);
                    break;
                case 1 :
                    // visitor creating
                    var visName = _reader.GetStringFromConsole("Введите имя нового посетителя");
                    _buffet.AddNewPerson(new Visitor(visName));
                    break;
                case 2:
                    // Создать столик
                    AddTable();
                    break;
                case 3:
                    // осетители за столик
                    SitPersonsToTable();
                    break;
                case 4:
                    MakeOrder();
                    break;
                case 5:
                    ClearTable();
                    break;
                case 6:
                    ChangeServiceManOfTable();
                    break;
                case 7:
                    GetAllInfoAbout();
                    break;
            }
        }
    }
    
    // Поменять обслуживающего
    private void ChangeServiceManOfTable()
    {
        // выбираем стол из всех не занятых столов
        
        var table = _reader.GetItemFromList(_buffet.FreeTables, "Выберете стоилк для смены обслуживающего");
        if (table == default)
        {
            return;
        }
        // Выбираем
        var employee = _reader.GetItemFromList(_buffet.Employees, "Выберете  для этого стола");
        if (employee == default)
        {
            return;
        }
        table.ChangeServiceMan(employee);
    }
    // Добавить новое блюдо
    // Добавить стол
    private void AddTable()
    {
        // Выбираем сотрудника чтоб приставтить его к этому столу
        var employee = _reader.GetItemFromList(_buffet.Employees, "Выберете сотрудника, который будет обслуживать данный стол");
        if (employee != null)
        {
             _buffet.Tables.Add(new Table(employee));
        }
    }
    private void SitPersonsToTable()
    {
        if(_buffet.FreeVisitors.Count == 0) return;
        var visCount = _reader.GetIntFromConsole("ведите количество посетителей для добавления за стол");
        // Усадить людей за стол
        // Если количество людей введено не корректно, то меняем его
        if(visCount == null) return;
        if (_buffet.Visitors.Count < visCount)
        {
            visCount = _buffet.Visitors.Count;
        }
        if (visCount <= 0) visCount = 1;
        var curVisitors = new List<Visitor>();
        for (int i = 0; i < visCount; i++)
        {
            var visitor = _reader.GetItemFromList(_buffet.FreeVisitors.ToList().Except(curVisitors).ToList(), "Выберете посетителя чтоб добавить к столику");
            if (visitor == default) break; // Если выбор отменяется, то просто сохраним как есть
            curVisitors.Add(visitor);
        }

        var fT = _buffet.FreeTables;
        var table = _reader.GetItemFromList(fT, "Выберете стоилк чтоб усадить туда посетителей");
        table?.SetVisitors(curVisitors);
    }
    // Очищаем стол
    private void ClearTable()
    {
        var table = _reader.GetItemFromList(_buffet.BusyTables, "Выберете стол с которого уходят гости");
        table?.ClearTheTable();
    }
    // Сделать заказ
    private void MakeOrder()
    {
        
       
        // Выбираем столик чтоб принять закз
        var table = _reader.GetItemFromList(_buffet.BusyTables, "Выберете стоилк для того чтобы принять заказ");
        if (table == null)
        {
            return;
        }
        var countProduct = _reader.GetIntFromConsole("Введите число, сколько товаров хотите добавить в заказ");
        if (countProduct > _buffet.Products.Count)
        {
            countProduct = _buffet.Products.Count;
        }
        if (countProduct <= 0) countProduct = 1;
        // Выбираем блюда для заказа
        var prodToOrder = new List<Product>();
        for (int i = 0; i < countProduct; i++)
        {
            var product = _reader.GetItemFromList(_buffet.Products, "Выберете блюда для столика");
            if (product == null) break;
            prodToOrder.Add(product);
        }
        table.MakeOrder(prodToOrder);
    }
    private void GetAllInfoAbout()
    {
        var menuItems = new List<string>()
        {
            "Столики", "Посетители", "Работники", "Блюда"
        };
        var index = _reader.SelectMenu(menuItems, "Выберете категорию для отображения информации об объектах");
        if (index is < -1 or > 3)
        {
            index = -1;
        }

        var sb = new StringBuilder();
        switch (index)
        {
            case -1:
                _reader.Message("Ничего не выбрано");
                break;
            case 0:
                // Tables info
                sb.Append("Информация о столиках\n");
                foreach (var table in _buffet.Tables)
                {
                    sb.Append($"\n{table.Info()}\n");
                }
                _reader.Message(sb.ToString());
                break;
            case 1:
                // VisitorsInfo
                sb.Append("Информация о посетителях\n");
                foreach (var visitor in _buffet.Visitors)
                {
                    sb.Append($"\n{visitor.GetInfo()}\n");
                }
                _reader.Message(sb.ToString());
                break;
            case 2:
                // Employee info
                sb.Append("Информация о сотрудниках\n");
                foreach (var employee in _buffet.Employees)
                {
                    sb.Append($"\n{employee.GetInfo()}\n");
                }
                _reader.Message(sb.ToString());
                break;
            case 3:
                // Prod info
                sb.Append("Информация о блюдах\n");
                foreach (var product in _buffet.Products)
                {
                    sb.Append($"\n{product}\n");
                }
                _reader.Message(sb.ToString());
                break;
        }
    }
}