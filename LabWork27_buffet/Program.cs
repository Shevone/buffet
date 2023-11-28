using System.Globalization;
using System.Text;
using LabWork27_buffet.Lab2;
using LabWork27_buffet.Models;
using LabWork27_buffet.Models.Persons;
using LabWork27_buffet.Serivce;

namespace LabWork27_buffet;

public static class Program
{
    private static readonly ConsoleReader Reader = new();
    private static readonly Buffet Service = new();

    private static List<string> _mainMenuItems = new List<string>()
    {
        "Выход","Взаимодействие со столами", "Взаимодействие с сотрудниками","Взаимодействие с посетителями", "Добавить блюдо","Просмотр информации"
    };

    private static List<string> _tableMenuItems = new List<string>()
    {
        "Выход", "Добавить столик", "Сменить сотрудника за столом","Сделать заказ", "Очистить стол","Посадить посетителя за столик","Посмотреть информацию о столах"
    };

    private static List<string> _empMenuItems = new List<string>()
    {
        "Выход", "Добавить сотрудника", "Сменить сотрудника за столом", "Просмотр информации о сотрудниках", "Сортировка по паспортным данным", "Сортировка по зп(возрастание)", "Сортировка по зп(убывание)"
    };

    private static List<string> _visMenuItems = new List<string>()
    {
        "Выход", "Новый посетитель", "Посадить посетителя за стол", "Информация о постетителях", "Сортировка по паспортным данным", "Сортировка по по посадке(возрастание)", "Сортировка по посадке(убывание)"
    };

    private static List<string> _infoMenuItems = new List<string>()
    {
        "Выход", "Столы", "Продукты", "Сотрудники", "Посетители","Заказы", "ВСЕ"
    };
    public static void Main(string[] args)
    {
        InitializeData(Service);
        while (true)
        {
            string message;
            int index = Reader.SelectMenu(_mainMenuItems, "Главное меню");
            switch (index)
            {
                case -1:
                    return;
                case 1:
                    // Столы
                    index = Reader.SelectMenu(_tableMenuItems, "Меню взаимодествия со столиками");
                    message = Tables(index);
                    Reader.ReadKey(message);
                    break;
                case 2:
                    // Сотрудники
                    index = Reader.SelectMenu(_empMenuItems, "Меню взаимодействия с сотрудниками");
                    message = EmployeeMenu(index);
                    Reader.ReadKey(message);
                    break;
                case 3:
                    // Постеители
                    index = Reader.SelectMenu(_visMenuItems, "Меню взаимодействия с посетителями");
                    message = VisitorsMenu(index);
                    Reader.ReadKey(message);
                    break;
                case 4:
                // Блюдо
                case 5:
                    // Вывод в консоль инфы
                    index = Reader.SelectMenu(_infoMenuItems, "Меню вывода информации");
                    message = InfoMenu(index);
                    Reader.ReadKey(message);
                    break;
            }
        }
    }

    private static string InfoMenu(int index)
    {
        // "Выход", "Столы", "Продукты", "Сотрудники", "Посетители", "ВСЕ"
        var sb = new StringBuilder("Просмотр информации\n");
        switch (index)
        {
            case -1:
                sb.Append("Выбран выход");
                break;
            case 1:
                sb.Append(Service.GetAllInfoAboutTables());
                break;
            case 2:
                foreach (var product in Service.Products)
                {
                    sb.Append(product + "\n");
                }
                break;
            case 3:
                sb.Append(EmployeInfo(sb));
                break;
            case 4:
                sb.Append(VisitorsInfo(sb));
                break;
            case 5:
                foreach (var order in Service.Orders)
                {
                    sb.Append(order + "\n");
                }
                break;
            case 6:
                sb.Append(EmployeInfo(sb) + "\n");
                sb.Append(VisitorsInfo(sb) + "\n");
                sb.Append(Service.GetAllInfoAboutTables());
                foreach (var product in Service.Products)
                {
                    sb.Append(product + "\n");
                }
                break;
        }

        return sb.ToString();
    }
    private static string VisitorsMenu(int index)
    {
        /*"Выход", "Новый посетитель", "Посадить посетителя за стол", "Информация о постетителях"*/
        var sb = new StringBuilder("Взаимодействие с посетителями\n");
        switch (index)
        {
            case -1:
                sb.Append("Выбран выход");
                break;
            case 1:
                sb.Append("Добавление посетителя\n");
                var visName = Reader.StringFromConsole("Введите ФИО нового посетителя");
                if (visName == "")
                {
                    sb.Append("Имя не было введено");
                    break;
                }
                Visitor newVisitor = new Visitor(visName);
                string gr = Service.AddNewPerson(newVisitor);
                sb.Append($"посетитель добавлен\n{gr}");
                break;
            case 2:
                var mes = PersonOnTable();
                sb.Append(mes);
                break;
            case 3:
                sb.Append(VisitorsInfo(sb));
                break;
            case 4:
                // Сортировка по пд
                Service.SortVis(1);
                sb.Append("Произведена сортировка посетителей по паспортным данным");
                break;
            case 5:
                Service.SortVis(2);
                sb.Append("Произведена сортировка посетителей по посадке (возрастание)");
                // Сортировка по посадке
                break;
            case 6:
                // Сортировка по посадке
                sb.Append("Произведена сортировка посетителей по посадке (убыванеи)");
                Service.SortVis(3);
                break;
            
        }
        return sb.ToString();
    }

    private static string VisitorsInfo(StringBuilder sb)
    {
        sb.Append("Просмотр информации о посетителях\n");
        foreach (var visitor in Service.Visitors)
        {
            sb.Append(visitor + "\n");
        }

        return sb.ToString();
    }
    private static string EmployeeMenu(int index)
    {
        var sb = new StringBuilder("Взаимодействие с сотрудниками\n");
        /* "Выход", "Добавить сотрудника", "Сменить сотрудника за столом", "Просмотр информации о сотрудниках"*/
        switch (index)
        {
            case -1:
                sb.Append("Выбран выход");
                break;
            case 1:
                sb.Append("Добавление сотрудника\n");
                var empName = Reader.StringFromConsole("Введите имя нового сотрудника");
                var empSalary = Reader.PositiveIntFromConsole("Введите зарплату сотрудника");
                if (empName == "" || empSalary == -1)
                {
                    sb.Append("Данные введены не корректно");
                    break;
                }
                Employee newEmp = new Employee(empName,empSalary);
                string gr = Service.AddNewPerson(newEmp);
                sb.Append($"Создан новый сотрудник {newEmp}\n{gr}");
                break;
            case 2:
                sb.Append(ChangeTableEmp());
                break;
            case 3:
                sb.Append(EmployeInfo(sb));
                break;
            case 4:
                Service.SortEmp(1);
                sb.Append("Произведена сортировка сотрудников по пампортным данным");
                break;
            case 5:
                Service.SortEmp(2);
                sb.Append("Произведена сортировка сотрудников по возрастанию зп");
                break;
            case 6:
                Service.SortEmp(3);
                sb.Append("Произведена сортировка сотрудников по убыванию зп ");
                break;
        }
        return sb.ToString();
    }

    private static string EmployeInfo(StringBuilder sb)
    {
        sb.Append("Просмотр информации о сотрудниках\n");
        foreach (var employee in Service.Employees)
        {
            sb.Append(employee + "\n");
        }

        return sb.ToString();
    }
    private static string Tables(int index)
    {
        int empIndex = 0;
        Employee employee;
        Table table1;
        var sb = new StringBuilder("Взаимодействие со столами\n");
        switch (index)
        {
            case -1:
                break;
            case 1:
                sb.Append("Добавление стола\n");
                // добавить столик
                empIndex = Reader.SelectMenu(Service.Employees, "Выберите сотрудника для нового столика");
                if (empIndex == -1)
                {
                    sb.Append("Не был выбран сотрудник");
                    break;

                }
                employee = Service.Employees[empIndex];
                table1 = new Table(employee);
                Service.AddTable(table1);
                sb.Append($"Столик был добавлен новый столик с сотрудником {employee}");
                break;
            case 2:
                // сменить
                sb.Append(ChangeTableEmp());
                break;
            case 3:
                // заказ
                int countProduct = Reader.PositiveIntFromConsole("Введите количество блюд для заказа");
                if (countProduct == -1)
                {
                    sb.Append("Количество продутов введено не верно");
                    break;
                }
                if (countProduct > Service.Products.Count)
                {
                    countProduct = Service.Products.Count;
                }
                if (countProduct <= 0) countProduct = 1;
                // Выбираем столик чтоб принять закз
                var table = Reader.SelectMenu(Service.ReservedTables, "Выберете стоилк для того чтобы принять заказ");
                if (table == -1)
                {
                    sb.Append("Стол не выбран");
                    break;
                }
                // Выбираем блюда для заказа
                Order newOrder = new Order();
                for (int i = 0; i < countProduct; i++)
                {
                    var product = Reader.SelectMenu(Service.Products.Except(newOrder.Products).ToList(), "Выберете блюда для столика");
                    if (product == -1) break;
                    newOrder.AddProdToOrder(Service.Products[product]);
                }
                Service.AddOrder(newOrder);
                Service.Tables[table].MakeOrder(newOrder);
                break;
            case 4:
                // очисить
                int tableIndex = Reader.SelectMenu(Service.Tables, "Выберите стол для смены обслуги");
                if (tableIndex == -1)
                {
                    sb.Append("Не был выбран стол");
                    break;
                }
                table1 = Service.Tables[tableIndex];
                table1.ClearTheTable();
                sb.Append("Стол очищен");
                break;
            case 5:
                // Людей за стол
                sb.Append("Добавление посетителей за стол\n" +
                          $"{PersonOnTable()}");
                break;
            case 6:
                // инфа
                sb.Append(Service.GetAllInfoAboutTables());
                break;
        }
        return sb.ToString();
    }
    

    private static string PersonOnTable()
    {
        if (Service.FreeTables.Count == 0)
        {
            return "Нет свободных столиков";
        }
        int visCount = Reader.PositiveIntFromConsole("Введите число посетителей для стола"); 
        // Усадить людей за стол
        // Если количество людей введено не корректно, то меняем его
        if (Service.Visitors.Count < visCount)
        {
            visCount = Service.Visitors.Count;
        }
        if (visCount <= 0) visCount = 1;
        var curVisitors = new List<Visitor>();
        for (int i = 0; i < visCount; i++)
        {
            var visitorIndex = Reader.SelectMenu(Service.FreePersons, "Выберете посетителя чтоб добавить к столику");
            if (visitorIndex == -1) break; // Если выбор отменяется, то просто сохраним как есть
            Visitor visitor = Service.FreePersons[visitorIndex];
            visitor.IsGetTable = true;
            curVisitors.Add(visitor);
        }
        var table = Reader.SelectMenu(Service.FreeTables, "Выберете стоилк чтоб усадить туда посетителей");
        if (table == -1)
        {
            return "Столик не выбран";
        }
        Service.FreeTables[table].SetVisitors(curVisitors);
        return "Столик обновлен";
    }
    private static string ChangeTableEmp()
    {
        int empIndex;
        Employee employee;
        int tableIndex = Reader.SelectMenu(Service.Tables, "Выберите стол для смены обслуги");
        if (tableIndex == -1)
        {
            return ("Не был выбран стол");
            
        }
        Table table = Service.Tables[tableIndex];
        empIndex = Reader.SelectMenu(Service.Employees, "Выберите сотрудника для нового столика");
        if (empIndex == -1)
        {
            return  ("Не был выбран сотрудник");
        }
        employee = Service.Employees[empIndex];
        table.ChangeServiceMan(employee);
        return $"Сотрудник за столом {table} сменён на \n{employee}";
    }

    private static void InitializeData(Buffet service)
    {
        // Создадим Работников
        var empVas = new Employee("Вася", 18000);
        var empVanya = new Employee("Иван", 18000);
        var empEgor = new Employee("Стажер Егор", 15000);
        var empPet = new Employee("Петр", 210000);
        service.AddNewPerson(empVas);
        service.AddNewPerson(empVanya);
        service.AddNewPerson(empEgor);
        service.AddNewPerson(empPet);
        //Создадим Посетителей
        var vis1 = new Visitor("Игор Николаевич");
        var vis2 = new Visitor("Георгий Петрович");
        var vis3 = new Visitor("Николай Дмитриевич");
        var vis4 = new Visitor("Данил Юрьевич");
        var vis5 = new Visitor("Василия Николаевна");
        var vis6 = new Visitor("Виктория Олеговна");
        service.AddNewPerson(vis1);
        service.AddNewPerson(vis2);
        service.AddNewPerson(vis3);
        service.AddNewPerson(vis4);
        service.AddNewPerson(vis5);
        service.AddNewPerson(vis6);
        // Создадим блюда
        var prod1 = new Product(321.2, "Селедка под шубой");
        var prod2 = new Product(32.2, "Картошка");
        var prod3 = new Product(400.2, "Шашлык");
        var prod4 = new Product(1.2, "Салат Цезарь");
        var prod5 = new Product(10.2, "Десерт  сладкий");
        service.AddNewProduct(prod1);
        service.AddNewProduct(prod2);
        service.AddNewProduct(prod3);
        service.AddNewProduct(prod4);
        service.AddNewProduct(prod5);
    }


    public static void CovarianceShow()
    {
        // Мы создаем как бы под типом для посетителей, но при под обобщенным типом
        IPersons<Person> company = new Visitors();
        Person personFromCompany = new Visitor("Вася");
        Console.WriteLine(personFromCompany.Greetings());

        IPersons<Employee> emPersons = new Employees();
        emPersons.NewPerson("Артур");
        Employee? emp = emPersons.GetPersonByNam("Артур");
        emp.Salary = 20000;
        IPersons<Person> emPersons2 = emPersons;
        Person? person = emPersons2.GetPersonByNam("Артур");
        Console.WriteLine(person.DisplayInfo());
        // То есть мы можем присвоить более общему типу IPersons<Person>
        // объект более конкретного типа Employees или IPersons<Employee>
    }
}


public interface IPersons<out T>
{
    T NewPerson(string name);
    T? GetPersonByNam(string name);
}

public class Visitors : IPersons<Visitor>
{
    // Класс - список посетителей
    private List<Visitor> _visitors = new List<Visitor>();
    // Метод который создает нового посетителя и возвращает его
    public Visitor NewPerson(string name)
    {
        Visitor visitor = new Visitor(name);
        _visitors.Add(visitor);
        return visitor;
    }

    public Visitor? GetPersonByNam(string name)
    {
        // ? - null-able
        return _visitors.Find(x => x.Name == name);
    }
}

public class Employees : IPersons<Employee>
{
    private List<Employee> _employees = new List<Employee>();

    public Employee NewPerson(string name)
    {
        // Создаем сотрудника с базовой зп потом можно поднять если че
        Employee employee = new Employee(name, 15000);
        _employees.Add(employee);
        return employee;
    }

    public Employee? GetPersonByNam(string name)
    {
        return _employees.Find(x => x.Name == name);
    }
}