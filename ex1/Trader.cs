namespace ex1;

public class Trader
{
    private Random _random = new Random();

    public string Name { get; }
    public double Money { get; set; }
    public double Capacity { get; }
    public double Speed { get; set; }
    public List<Goods> GoodsList { get; private set; }

    public Trader(string name, double money, double capacity)
    {
        Name = name;
        Money = money;
        Capacity = capacity;
        Speed = _random.Next(1, 6);
        GoodsList = new List<Goods>();
    }

    public void BuyGoods(Goods goods)
    {
        if (Money < goods.BaseCost)
        {
            throw new NotEnoughMoneyException("Недостаточно денег для покупки товара.");
        }
        if (GetCurrentCargoWeight() + goods.Weight > Capacity)
        {
            throw new OverCapacityException("Недостаточно места в телеге.");
        }
        Money -= goods.BaseCost;
        GoodsList.Add(goods);
    }

    public void SellAllGoods(double sellCoefficient = 1.0)
    {
        double totalProfit = 0;
        foreach (var g in GoodsList)
        {
            double price = g.GetFinalCost() * sellCoefficient;
            totalProfit += price;
        }
        GoodsList.Clear();
        Money += totalProfit;
    }

    public double GetCurrentCargoWeight()
    {
        double totalWeight = 0;
        foreach (var g in GoodsList)
        {
            totalWeight += g.Weight;
        }
        return totalWeight;
    }
    
}
public enum EventType
{
    Обычный_день,
    Дождь,
    Ровная_дорога,
    Сломалось_колесо,
    Река,
    Встретил_местного,
    Разбойники,
    Придорожный_трактир,
    Товар_испортился
}