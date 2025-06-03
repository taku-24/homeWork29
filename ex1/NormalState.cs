namespace ex1;

public class NormalState : IQualityState
{
    public double Coefficient => 1.2;
    public GoodsQuality Quality => GoodsQuality.Нормальное;
    public IQualityState NextQualityState() => new SlightlySpoiledState();
}