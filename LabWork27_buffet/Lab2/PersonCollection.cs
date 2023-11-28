using System.Collections;
using System.Text;
using LabWork27_buffet.Models.Persons;

namespace LabWork27_buffet.Lab2;

public class PersonCollection<T> : ICollection<T> where T : Person
{
    private T[] _persons;
    private int _i = 0;
   
    // Свойства ICollection
    public int Count => _persons.Length;
    public bool IsReadOnly { get; }

    public PersonCollection()
    {
        _persons = new T[10];
        IsReadOnly = false;
    }
    // метод с полиморфным вызовом
    public string DisplayInfo()
    {
        StringBuilder sb = new StringBuilder($"ИНформация о коллекции {typeof(T).ToString()[20..]}");
        // Проходимся по элементам нашего объекта
        foreach (T person in this)
        {
            // у каждого типа человек есть абстрактный метод DisplayInfo котороый в зваисимости от типа выводит разную инфу
            sb.Append(person.DisplayInfo() + "\n");
        }

        return sb.ToString();
    }
    // Метод сортировки
    // Реализовывает пузырьковую сортирвку
    // Сравнение элементов происходит функцией переданной в качестве входного параметра
    public void SortPersons(Func<T, T, bool> compareFunc)
    {
        var len = _i;
        for (var i = 1; i < len; i++)
        {
            for (var j = 0; j < len - i; j++)
            {
                // Помещаем 2 параметра в функцию сравнения
                // Если первый больше второго, то меняем их местами
                T? p1 = _persons[j];
                T? p2 = _persons[j + 1];
                if(compareFunc(p1,p2))
                {
                    (_persons[j], _persons[j + 1]) = (_persons[j + 1], _persons[j]);
                }
            }
        }
    }
    private void Resize(int newSize)
    {
        T[] copy = _persons;
        _persons = new T[newSize];
        copy.CopyTo(_persons,0);
    }
    // =============================================
    // Методы интерфеса ICollection
    public void Add(T item)
    {
        if (_i == Count)
        {
            Resize(_i * 2);
        }
        _persons[_i] = item;
        _i++;
    }

    public void Clear()
    {
        // очистили
        _persons = new T[10];
    }

    public bool Contains(T item)
    {
        return _persons.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _persons.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        T[] withRemove = new T[Count];
        int i = 0;// счетчик для нового без удаленного
        int j = 0; // счетчик для старого
        foreach (T person in _persons)
        {
            j++;
            if (person != item)
            {
                withRemove[i] = person;
                i++;
            }
        }

        if (i == j)
        {
            // Елси длинна не изменилась то ничего не удалили
            return false;
        }
        // А если же удаилил то вычитаем и возвращаем ture
        _i = i;
        return true;
    }
    // =============================================
    // Интерфес ICollecttion так же реализовывает интерфес IEnumerable
    // Это методы которые обязует реализовать интерфес IEnumerable
    // Они нужны для итерации по нашему объекту
    public IEnumerator<T> GetEnumerator()
    {
        // Метод возращает объект для итерации
        return (IEnumerator<T>)_persons.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    
    
}