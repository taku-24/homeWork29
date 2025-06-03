namespace ex1;

public class HalfSpoiledState : IQualityState
{
    public double Coefficient => 0.55;
    public GoodsQuality Quality => GoodsQuality.Половина_испортилась;
    public IQualityState NextQualityState() => new AlmostGoneState();
}