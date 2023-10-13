using System;

namespace AvaloniaApplication3.Model;

public class Order
{
    public Order(){}
    public Order(int id, int dishe_id, int table_id, int count, DateTime dateCreate)
    {
        Id = id;
        Dishe_id = dishe_id;
        Table_id = table_id;
        Count = count;
        DateCreate = dateCreate;
    }
    public int Id { get; set; }
    public int Dishe_id { get; set; }
    public int Table_id { get; set; }
    public int Count { get; set; }
    public DateTime DateCreate { get; set; }
    
    public Dish Dish { get; set; }
    public Table Table { get; set; }
}