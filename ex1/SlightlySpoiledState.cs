namespace ex1;

public class SlightlySpoiledState : IQualityState
{
    public double Coefficient => 0.95;
    public GoodsQuality Quality => GoodsQuality.Слегка_испорчен;
    public IQualityState NextQualityState() => new HalfSpoiledState();
}