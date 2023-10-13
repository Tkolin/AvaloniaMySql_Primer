using System;

namespace AvaloniaApplication3.Model;

public class Dish
{
    public Dish(){}
    public Dish(int id, string name, float calories, float weihgt)
    {
        Id = id;
        Name = name;
        Calories = calories;
        Weihgt = weihgt;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public float Calories { get; set; }
    public float Weihgt { get; set; }
}