namespace ex1;

public class CompletelySpoiledState : IQualityState
{
    public double Coefficient => 0.1;
    public GoodsQuality Quality => GoodsQuality.Испорчен;
    public IQualityState NextQualityState() => this;
}