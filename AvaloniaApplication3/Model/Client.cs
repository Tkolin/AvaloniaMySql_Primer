using System;

namespace AvaloniaApplication3.Model;

public class Client
{
    public Client(){}
    public Client(int id, string firstName, string lastName, DateTime dateBirth)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateBirth = dateBirth;
    }
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateBirth { get; set; }
}