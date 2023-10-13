namespace AvaloniaApplication3.Model;

public class Table
{
    public Table(int number,int numberOfSeats, int landingZoneId)
    {
        Number = number;
        NumberOfSeats = numberOfSeats;
        LandingZone_id = landingZoneId;
    }
    public int Number { get; set; }
    public int NumberOfSeats { get; set; }
    public int LandingZone_id { get; set; }
}