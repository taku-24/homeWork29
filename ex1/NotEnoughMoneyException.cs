namespace ex1;

public class NotEnoughMoneyException : Exception
{
    public NotEnoughMoneyException(string message) : base(message) { }
}