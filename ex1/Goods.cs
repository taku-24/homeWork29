namespace ex1;
public enum GoodsType
{
    Мясо,
    Сухофрукты,
    Зерно,
    Мука,
    Ткани,
    Краска
}
public enum GoodsQuality
{
    Нормальное,
    Слегка_испорчен,
    Половина_испортилась,
    Почти_пропал,
    Испорчен
}
public class Goods
{
    public GoodsType Type { get; }
    public double BaseCost { get; }
    public double Weight { get; }
    public IQualityState QualityState { get; private set; }

    public Goods(GoodsType type, double baseCost, double weight, IQualityState qualityState)
    {
        Type = type;
        BaseCost = baseCost;
        Weight = weight;
        QualityState = qualityState;
    }
    
    public void DegradeQuality()
    {
        QualityState = QualityState.NextQualityState();
    }
    
    public double GetFinalCost()
    {
        return BaseCost * QualityState.Coefficient;
    }
    
}