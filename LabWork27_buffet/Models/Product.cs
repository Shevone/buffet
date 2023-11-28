namespace LabWork27_buffet.Models;

public class Product
{
    public Product(double calories, string dishName)
    {
        Calories = calories;
        DishName = dishName;
    }

    private string DishName { get;}
    private double _calories;

    private double Calories
    {
        get => _calories;
        init => _calories = value > 0 ? value : 0.1;
    }
    public override string ToString()
    {
        return $"Блюдо {DishName}, Количество калорий {Calories}";
    }
}