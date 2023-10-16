namespace LabWork27_buffet.Models;

public class Product
{
    public Product(double calories, string dishName)
    {
        Calories = calories;
        DishName = dishName;
    }
    public string DishName { get;}
    private readonly double _calories;
    public double Calories
    {
        get => _calories;
        init => _calories = value > 0 ? value : 0.1;
    }
    public override string ToString()
    {
        return $"Блюдо {DishName}, Количество калорий {Calories}";
    }
}