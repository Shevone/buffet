namespace LabWork27_buffet.Models;

// Добалвеям реализацию интерфейса IComparable - нужна для того чтобы можно было сравнивать 
// Объекты данного класса
public class PassportData : IComparable
{
    public string Name { get;  }
    public PassportData(string name)
    {
        Name = name;
    }

    public int CompareTo(object? obj)
    {
        // Сравнение паспортных данных происходи по имени
        if (obj is PassportData passport)
        {
            // Мы смотрим чтоб поступивший объект являлся объектом типа паспортные данные
            return String.Compare(Name, passport.Name, StringComparison.Ordinal);
            
        }
        else
        {
            // Елси не является таким типом то выбрасываем ошибку
            throw new ArgumentException("Некорректное значение параметра");
        }
    }
}