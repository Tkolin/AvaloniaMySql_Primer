namespace AvaloniaApplication3.Model;

public class Review
{
    public Review(){}
    public Review(int id, int order_id,int clietn_id, string comments)
    {
        Id = id;
        Order_id = order_id;
        Clietn_id = clietn_id;
        Comments = comments;
    }
    public int Id { get; set; }
    public int Order_id { get; set; }
    public int Clietn_id { get; set; }
    public string Comments { get; set; }
}