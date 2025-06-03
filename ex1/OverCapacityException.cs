namespace ex1;

public class OverCapacityException : Exception
{
    public OverCapacityException(string message) : base(message) { }
}