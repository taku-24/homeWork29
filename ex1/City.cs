namespace ex1;

public class City
{
    public string Name { get; }
    public double Distance { get; set; }

    public City(string name, double distance)
    {
        Name = name;
        Distance = distance;
    }
}
