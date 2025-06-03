namespace ex1;

public class AlmostGoneState : IQualityState
{
    public double Coefficient => 0.25;
    public GoodsQuality Quality => GoodsQuality.Почти_пропал;
    public IQualityState NextQualityState() => new CompletelySpoiledState();
}